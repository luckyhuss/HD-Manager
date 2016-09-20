using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HDManager.Stub
{
    /// <summary>
    /// Representation of a movie
    /// </summary>
    public class XmlMovieElement
    {
        public static char LINK_FILENAME_SEPARATOR = ';';

        public static string CheckDuplicateFilenames(string filenames)
        {
            var splittedFilenames = filenames.Split(LINK_FILENAME_SEPARATOR);

            var unique = splittedFilenames.Distinct().ToList();

            return String.Join(LINK_FILENAME_SEPARATOR.ToString(), unique);
        }

        /// <summary>
        /// Cleaned title of characters not allowed in filenames
        /// </summary>
        /// <returns>Cleaned title</returns>
        public string TitleCleaned()
        {
            var cleanedTitle = Title;
            // safen title for filename
            Array.ForEach(Path.GetInvalidFileNameChars(),
                c => cleanedTitle = Title.Replace(c.ToString(), String.Empty));
            return cleanedTitle;
        }

        // class members
        public string Title { set; get; }
        public string ImdbLink { set; get; }
        public string ImdbRating { set; get; }
        public string ImdbContentRating { set; get; }
        public string ImdbPoster { set; get; }
        public string PostLink { set; get; }
        public string Password { set; get; }
        public MovieCategory Category { set; get; }
        public string DateCreated { set; get; }
        public string DateModified { set; get; }
        public DownloadStatus Status { set; get; }
        public List<LinkDetails> Link { set; get; }
        public string Filenames { set; get; }

        // constructor overloading
        public XmlMovieElement()
        {
            Link = new List<LinkDetails>();
        }

        public class LinkDetails
        {
            public string DownloadServer { set; get; }
            public string DownloadLink { set; get; }
            public DownloadStatus DownloadLinkStatus { set; get; }
        }

        public enum DownloadStatus
        {
            New = 0,
            Downloading = 1,
            Completed = 2,
            KO = 3,
            OK = 4 // used by LinkDetails.DownloadLinkStatus
        };

        public enum MovieCategory
        {
            HDDTS = 0,
            mHD = 1,
            Archives = 2
        };
    }
}
