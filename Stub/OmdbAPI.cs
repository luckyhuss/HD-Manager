using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace HDManager.Stub
{
    /// <summary>
    /// 07/02/2014
    /// Title="The Matrix" 
    /// Year="1999" 
    /// imdbID="tt0133093" 
    /// Type="movie"
    /// </summary>
    public class OmdbSearchResult
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
    }
    
    /// <summary>
    /// 07/02/2014
    /// title="The Matrix" 
    /// year="1999" 
    /// rated="R" 
    /// released="31 Mar 1999" 
    /// runtime="136 min" 
    /// genre="Action, Adventure, Sci-Fi" 
    /// director="Andy Wachowski, Lana Wachowski" 
    /// writer="Andy Wachowski, Lana Wachowski" 
    /// actors="Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss, Hugo Weaving" 
    /// plot="A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers." 
    /// language="English" 
    /// country="USA, Australia" 
    /// awards="Won 4 Oscars. Another 29 wins & 36 nominations." 
    /// poster="http://ia.media-imdb.com/images/M/MV5BMjEzNjg1NTg2NV5BMl5BanBnXkFtZTYwNjY3MzQ5._V1_SX300.jpg" 
    /// metascore="73" 
    /// imdbRating="8.7" 
    /// imdbVotes="808,529" 
    /// imdbID="tt0133093" 
    /// type="movie"
    /// </summary>
    public class OmdbMovieDetails
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }
    }

    public class SearchResult
    {
        public List<OmdbSearchResult> Search { get; set; }
    }

    public class OmdbAPI
    {
        public List<OmdbSearchResult> SearchMovie(string title, string year)
        {
            var url = String.Format("http://www.omdbapi.com/?s={0}", HttpUtility.UrlEncode(title));
            if (!year.Equals("0"))
            {
                // not empty
                url = String.Format("{0}&y={1}", url, year);
            }
            var json = GetHtmlFromUrl(url);

            SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(json);

            return searchResult.Search;
        }

        private OmdbMovieDetails GetMovieDetails(string imdbID)
        {
            var url = String.Format("http://www.omdbapi.com/?i={0}", imdbID);

            var json = GetHtmlFromUrl(url);

            OmdbMovieDetails omdbMovieDetails = JsonConvert.DeserializeObject<OmdbMovieDetails>(json);

            return omdbMovieDetails;
        }

        public XmlMovieElement GetMovieDetailsByImdbId(string imdbID)
        {
            // call the OmdbAPI
            OmdbMovieDetails omdbMovieDetails = GetMovieDetails(imdbID);

            XmlMovieElement ImdbDetails = new XmlMovieElement();
            ImdbDetails.Title = String.Format("{0} ({1})", omdbMovieDetails.Title, omdbMovieDetails.Year);
            ImdbDetails.ImdbContentRating = omdbMovieDetails.Rated;
            ImdbDetails.ImdbLink = String.Format(ConfigurationManager.AppSettings["ImdbContent"], omdbMovieDetails.imdbID);
            ImdbDetails.ImdbRating = omdbMovieDetails.imdbRating;

            if (!omdbMovieDetails.Poster.Contains("N/A"))
            {
                var posterFilename = ImdbDetails.Title;
                // safen filename
                Array.ForEach(Path.GetInvalidFileNameChars(),
                    c => posterFilename = posterFilename.Replace(c.ToString(), String.Empty));

                var posterUrl = omdbMovieDetails.Poster;

                var posterSavePath = String.Format(@"{0}\{1}{2}",
                    ConfigurationManager.AppSettings["PosterBaseUrl"],
                    posterFilename,
                    Path.GetExtension(posterUrl));

                Image posterImage = null;
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(posterUrl);
                HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebReponse.GetResponseStream();
                posterImage = Image.FromStream(stream);

                // save poster image
                posterImage.Save(posterSavePath);
                posterImage.Dispose();

                ImdbDetails.ImdbPoster = Path.GetFileName(posterSavePath);
            }
            return ImdbDetails;
        }

        private string GetHtmlFromUrl(string url)
        {
            var html = String.Empty;

            if (String.IsNullOrEmpty(url)) return html;

            var request = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response = null;
            try
            {
                // get the response, to later read the stream
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (response != null)
            {
                // use a stream reader that understands UTF8
                var reader = new StreamReader(response.GetResponseStream(), encoding: Encoding.UTF8);
                html = reader.ReadToEnd();
                // close the reader
                reader.Close();
                response.Close();
            }

            return html; //return html content
        }
    }
}
