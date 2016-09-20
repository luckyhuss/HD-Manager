using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace HDManager.Stub
{
    public class XMLRPCOpenSubtitles
    {
        public static readonly string USERNAME = String.Empty;
        public static readonly string PASSWORD = String.Empty;
        public static readonly string LANGUAGE = "en";
        public static readonly string USERAGENT = "HD Download Manager v5.7.2";

        // LogIn token
        public static LogIn logIn { get; set; }

        // INPUT TYPES
        
        //      array(  <--- array/list of video files
        //  struct(   <--- information about one video file
        //    (string) [sublanguageid],
        //    (string) [moviehash],
        //    (double) [moviebytesize],
        //    (string) [imdbid]
        //  ), struct( ), ...
        //)
        // 11/02/2014
        public struct QueryParameters
        {
            public string sublanguageid;
            public string moviehash;
            public string moviebytesize;
            public string imdbid;
        }
        
        // RETURN TYPES

        public struct LastUpdateStrings
        {
            public string en;
            public string fr;
        }

        //        struct(
        //  (string)    [idsubtitlefile],
        //  (string)    [data]
        //)
        // 12/02/2014
        public struct SubContents
        {
            // order is important
            public string idsubtitlefile;
            public string data;
        }

        //["QueryParameters"]: {CookComputing.XmlRpc.XmlRpcStruct}
        //["LanguageName"]: "English"
        //["QueryNumber"]: "0"
        //["SubComments"]: "0"
        //["SubSize"]: "78230"
        //["MovieYear"]: "2013"
        //["ZipDownloadLink"]: "http://dl.opensubtitles.org/en/download/subad/src-api/vrf-664575a114/5468458"
        //["SubHD"]: "0"
        //["IDSubtitleFile"]: "1954063810"
        //["MovieFPS"]: "23.976"
        //["UserRank"]: "sub leecher"
        //["IDMovieImdb"]: "1091191"
        //["MovieNameEng"]: ""
        //["ISO639"]: "en"
        //["SubActualCD"]: "1"
        //["MovieByteSize"]: "0"
        //["SubDownloadsCnt"]: "36453"
        //["MovieName"]: "Lone Survivor"
        //["MovieKind"]: "movie"
        //["SubDownloadLink"]: "http://dl.opensubtitles.org/en/download/filead/src-api/vrf-23f19c70e8/1954063810.gz"
        //["IDMovie"]: "160233"
        //["MovieReleaseName"]: "Lone.Survivor.2013.DVDScr.x264.NO1KNOWS"
        //["SubHearingImpaired"]: "0"
        //["SeriesEpisode"]: "0"
        //["SeriesIMDBParent"]: "0"
        //["MatchedBy"]: "imdbid"
        //["MovieTimeMS"]: "0"
        //["SubSumCD"]: "1"
        //["MovieImdbRating"]: "7.7"
        //["SubBad"]: "0"
        //["SubFeatured"]: "0"
        //["SubLanguageID"]: "eng"
        //["SubHash"]: "5f0f54add345da94e2286b2de216841c"
        //["SeriesSeason"]: "0"
        //["UserNickName"]: "mach77"
        //["SubFileName"]: "Lone.Survivor.2013.DVDScr.x264.NO1KNOWS.srt"
        //["SubtitlesLink"]: "http://www.opensubtitles.org/en/subtitles/5468458/lone-survivor-en"
        //["IDSubMovieFile"]: "0"
        //["SubAuthorComment"]: "Works with Lone.Survivor.2013.DVDSCR.Xvid.Ac3-MiLLENiUM"
        //["UserID"]: "1536178"
        //["IDSubtitle"]: "5468458"
        //["SubFormat"]: "srt"
        //["MovieHash"]: "0"
        //["SubRating"]: "0.0"
        //["SubAddDate"]: "2013-12-31 00:32:01"
        // 12/02/2014
        public struct SubFile
        {
            // order is important
            public string MovieReleaseName { get; set; } // set; get; for accessibility in GridView
            public string SubDownloadsCnt { get; set; }
            public string SubFormat { get; set; }
            public string SubSumCD { get; set; }            
            public string SubAddDate { get; set; }
            public string ZipDownloadLink { get; set; }

            // 04/06/2014
            //public QueryParameters QueryParameters;
            public string LanguageName { get; set; }
            // 04/06/2014
            //public string QueryNumber { get; set; }
            public string SubComments { get; set; }
            public string SubSize { get; set; }
            public string MovieYear { get; set; }            
            public string SubHD { get; set; }
            public string IDSubtitleFile { get; set; }
            public string MovieFPS { get; set; }
            public string UserRank { get; set; }
            public string IDMovieImdb { get; set; }
            public string MovieNameEng { get; set; }
            public string SubActualCD { get; set; }
            public string MovieByteSize { get; set; }
            public string MovieName { get; set; }
            public string MovieKind { get; set; }
            public string SubDownloadLink { get; set; }
            public string IDMovie { get; set; }
            public string SubHearingImpaired { get; set; }
            public string SeriesEpisode { get; set; }
            public string SeriesIMDBParent { get; set; }
            public string MatchedBy { get; set; }
            public string MovieTimeMS { get; set; }
            public string MovieImdbRating { get; set; }
            public string SubBad { get; set; }
            public string SubFeatured { get; set; }
            public string SubLanguageID { get; set; }
            public string SubHash { get; set; }
            public string SeriesSeason { get; set; }
            public string UserNickName { get; set; }
            public string SubFileName { get; set; }
            public string SubtitlesLink { get; set; }
            public string IDSubMovieFile { get; set; }
            public string SubAuthorComment { get; set; }
            public string UserID { get; set; }
            public string IDSubtitle { get; set; }
            public string MovieHash { get; set; }
            public string SubRating { get; set; }
            public string ISO639 { get; set; }
        }

        //        struct(
        //  (string) [xmlrpc_version],
        //  (string) [xmlrpc_url],
        //  (string) [application],
        //  (string) [contact],
        //  (string) [website_url],
        //  (string) [users_online_total],
        //  (string) [users_online_program],
        //  (string) [users_loggedin],
        //  (string) [users_max_alltime],
        //  (string) [users_registered],
        //  (string) [subs_downloads],
        //  (string) [subs_subtitle_files],
        //  (string) [movies_total],
        //  (string) [movies_aka],
        //  (string) [total_subtitles_languages],
        //  struct(
        //    (string) [<language ISO639 2-letter code>],
        //    ... more languages go here ...
        //  ) [last_update_strings],
        //  (double) [seconds]
        //)
        // 11/02/2014
        public struct ServerInfo
        {
            public string xmlrpc_version;
            public string xmlrpc_url;
            public string application;
            public string contact;
            public string website_url;
            public int users_online_total;
            public int users_online_program;
            public int users_loggedin;
            public string users_max_alltime;
            public string users_registered;
            public string subs_downloads;
            public string subs_subtitle_files;
            public string movies_total;
            public string movies_aka;
            public string total_subtitles_languages;
            public LastUpdateStrings last_update_strings;
            public double seconds;
        }

        //      struct(
        //  (string) [token],
        //  (string) [status],
        //  (double) [seconds]
        //)
        // 11/02/2014
        public struct LogIn
        {
            public string token;
            public string status;
            public double seconds;
        }

        //      struct(
        //  (string) [status],
        //  (double) [seconds]
        //)
        // 11/02/2014
        public struct LogOut
        {
            public string status;
            public double seconds;
        }

        //      struct(
        //  (string) [status],
        //  (double) [seconds]
        //)
        // 11/02/2014
        public struct NoOperation
        {
            public string status;
            public double seconds;
        }

        //      struct(
        //  array(
        //    struct( subfile ), struct( subfile ), ...
        //  ) [data],
        //  (double) [seconds]
        //)
        // 11/02/2014
        public struct SearchSubtitles
        {
            public SubFile[] data;
            public string status;
            public double seconds;
        }

        //      struct(
        //  array(
        //    struct( subcontents ), struct( subcontents ), ...
        //  ) [data],
        //  (double) [seconds]
        //)
        // 20/02/2014
        public struct DownloadSubtitles
        {
            public SubContents[] data;
            public string status;
            public double seconds;
        }

        /// <summary>
        /// More info http://trac.opensubtitles.org/projects/opensubtitles/wiki/XMLRPC
        /// </summary>
        [XmlRpcUrl("http://api.opensubtitles.org/xml-rpc")] 
        public interface IOpenSubtitles : IXmlRpcProxy
        {
            [XmlRpcMethod("ServerInfo")]
            ServerInfo ServerInfo();

            [XmlRpcMethod("LogIn")]
            LogIn LogIn(string username, string password, string language, string useragent);

            [XmlRpcMethod("LogOut")]
            LogOut LogOut(string token);

            [XmlRpcMethod("NoOperation")]
            NoOperation NoOperation(string token);

            [XmlRpcMethod("SearchSubtitles")]
            SearchSubtitles SearchSubtitles(string token, QueryParameters[] queryparameters);

            [XmlRpcMethod("DownloadSubtitles")]
            DownloadSubtitles DownloadSubtitles(string token, int[] idsubtitlefiles);
        }
        
        /// <summary>
        /// Uncompress the GZip compressed subtitle
        /// </summary>
        /// <param name="compressedBytes"></param>
        /// <returns></returns>
        public static string GZipDecompress(Byte[] compressedBytes, Encoding encoding)
        {
            using (var uncompressed = new MemoryStream())
            using (var compressed = new MemoryStream(compressedBytes))
            using (GZipStream dc = new GZipStream(compressed, CompressionMode.Decompress))
            {
                dc.CopyTo(uncompressed);
                return encoding.GetString(uncompressed.ToArray());
            }
        }
    }
}
