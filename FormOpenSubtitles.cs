using CookComputing.XmlRpc;
using HDManager.Stub;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace HDManager
{
    public partial class FormOpenSubtitles : Form
    {
        public string ImdbID { get; set; }
        public string MovieTitle { get; set; }

        public FormOpenSubtitles()
        {
            InitializeComponent();
        }

        private void FormOpenSubtitles_Load(object sender, EventArgs e)
        {
            this.Text = String.Format(this.Tag.ToString(), this.MovieTitle);
            //var movieFile = @"F:\_download\movies\transfer\The.Goonies.1985.m720p.AC3.x264~estres.mkv";
            //byte[] moviehash1 = movieHasher.ComputeMovieHash(movieFile);
            //MessageBox.Show(String.Format("The hash of the movie-file is: {0} like 8e245d9679d31e12", movieHasher.ToHexadecimal(moviehash1)));
        }

        private void FormOpenSubtitles_Shown(object sender, EventArgs e)
        {
            radDropDownListLanguage.Items[0].Selected = true; // select "eng"
        }

        private void SearchSubtitles()
        {
            XMLRPCOpenSubtitles.SearchSubtitles searchSubtitles = new XMLRPCOpenSubtitles.SearchSubtitles();
            XMLRPCOpenSubtitles.IOpenSubtitles proxy = XmlRpcProxyGen.Create<XMLRPCOpenSubtitles.IOpenSubtitles>();

            try
            {
                // HD Download Manager v5.7.2
                //var rarr = proxy.ServerInfo();

                XMLRPCOpenSubtitles.LogIn logIn = XMLRPCOpenSubtitles.logIn;

                if (!logIn.status.Contains("OK")) return;

                XMLRPCOpenSubtitles.QueryParameters[] movie = new XMLRPCOpenSubtitles.QueryParameters[1];
                movie[0] = new XMLRPCOpenSubtitles.QueryParameters();
                movie[0].sublanguageid = radDropDownListLanguage.SelectedItem.Text;
                movie[0].moviehash = String.Empty;
                movie[0].moviebytesize = String.Empty;
                // IMDB ID is without the \tt
                movie[0].imdbid = ImdbID;

                // TODO Data subfile = false (bool) when nothing returns from server
                searchSubtitles = proxy.SearchSubtitles(logIn.token, movie);

                // TODO do not logout each time, keep token in session
                //XMLRPCOpenSubtitles.LogOut logOut = proxy.LogOut(logIn.token);
            }
            catch (Exception ex)
            {
                radLabelSubtitlesCount.Text = ex.Message;
                return;
            }

            var subtitlesList = searchSubtitles.data.ToList();
            subtitlesList = subtitlesList.OrderByDescending(s => Convert.ToInt32(s.SubDownloadsCnt)).ToList();

            // reset for column renaming second time made possible
            radGridViewSubtitles.DataSource = null;

            // set datasource
            radGridViewSubtitles.DataSource = subtitlesList;

            // count the number of subtitles available from OpenSubtitles server
            radLabelSubtitlesCount.Text = String.Format(radLabelSubtitlesCount.Tag.ToString(), subtitlesList.Count);

            var listOfColumnToDisplay = "SubDownloadsCnt;MovieReleaseName;SubFormat;SubSumCD;SubAddDate";

            var columnsToHide = radGridViewSubtitles.Columns.Where(t => !listOfColumnToDisplay.Contains(t.HeaderText)).ToList();
            foreach (var column in columnsToHide)
            {
                column.IsVisible = false;
            }

            radGridViewSubtitles.Columns["MovieReleaseName"].Width = 70;
            radGridViewSubtitles.Columns["MovieReleaseName"].HeaderText = "Movie name (uploaded)";

            radGridViewSubtitles.Columns["SubDownloadsCnt"].HeaderText = "# Downloads";
            radGridViewSubtitles.Columns["SubFormat"].Width = 10;
            radGridViewSubtitles.Columns["SubFormat"].HeaderText = "Format";
            radGridViewSubtitles.Columns["SubSumCD"].Width = 10;
            radGridViewSubtitles.Columns["SubSumCD"].HeaderText = "# CD";
            radGridViewSubtitles.Columns["SubAddDate"].HeaderText = "Date";

            GridViewTextBoxColumn gvtxtcol = new GridViewTextBoxColumn("#");
            gvtxtcol.Width = 5;

            radGridViewSubtitles.Columns.Insert(0, gvtxtcol);
        }

        private void radGridViewSubtitles_DoubleClick(object sender, EventArgs e)
        {
            Encoding subtitleEncoding = Encoding.UTF8;
            if (radGridViewSubtitles.SelectedRows.Count == 1)
            {
                XMLRPCOpenSubtitles.IOpenSubtitles proxy = XmlRpcProxyGen.Create<XMLRPCOpenSubtitles.IOpenSubtitles>();
                XMLRPCOpenSubtitles.DownloadSubtitles downloadSubtitles = new XMLRPCOpenSubtitles.DownloadSubtitles();
                XMLRPCOpenSubtitles.LogIn logIn = XMLRPCOpenSubtitles.logIn;

                int[] downloadSubtitle = new int[1];

                var idSubtitleFile = radGridViewSubtitles.SelectedRows[0].Cells["IDSubtitleFile"].Value.ToString();
                downloadSubtitle[0] = Convert.ToInt32(idSubtitleFile);

                downloadSubtitles = proxy.DownloadSubtitles(logIn.token, downloadSubtitle);

                byte[] compressedSubtitleData = Convert.FromBase64String(downloadSubtitles.data[0].data);

                var uncompressedSubtitleData = XMLRPCOpenSubtitles.GZipDecompress(compressedSubtitleData, subtitleEncoding);
                
                var title = MovieTitle;
                // safen title for filename
                Array.ForEach(Path.GetInvalidFileNameChars(),
                    c => title = title.Replace(c.ToString(), String.Empty));

                var extractFolder = Path.Combine(ConfigurationManager.AppSettings["Path.Download"],
                        ConfigurationManager.AppSettings["Name.Transfer"], title);
                
                var subFileName = radGridViewSubtitles.SelectedRows[0].Cells["SubFileName"].Value.ToString();

                // check directory
                if (!Directory.Exists(extractFolder))
                {
                    Directory.CreateDirectory(extractFolder);
                }

                // save subtitle file to disk
                File.WriteAllText(Path.Combine(extractFolder, subFileName), uncompressedSubtitleData, subtitleEncoding);

                toolTipSubtitles.ToolTipTitle = "Successful";
                toolTipSubtitles.ToolTipIcon = ToolTipIcon.Info;
                toolTipSubtitles.Show(String.Format("Subtitle {0} downloaded successfully", subFileName), radGridViewSubtitles);

                // update image of first column
                radGridViewSubtitles.SelectedRows[0].Cells["#"].Value = "OK";
            }
        }

        private void radDropDownListLanguage_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            SearchSubtitles();
        }
    }
}
