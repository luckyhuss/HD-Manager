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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HDManager
{
    public partial class FormImdbSearch : Form
    {
        /// <summary>
        /// Gets the IMDB details for the searched movie
        /// </summary>
        public XmlMovieElement ImdbDetails { get; private set; }

        public FormImdbSearch()
        {
            InitializeComponent();
        }

        private void radButtonSearch_Click(object sender, EventArgs e)
        {
            radTextBoxTitle.Enabled = false;
            // using Named capture groups to extract info from search text
            Regex regTitle = new Regex(@"(?<Title>.+)\((?<Year>\d{4})\)");
            var year = String.Empty;
            var titleWithoutYear = String.Empty;

            if (regTitle.IsMatch(radTextBoxTitle.Text))
            {
                var match = regTitle.Match(radTextBoxTitle.Text);
                titleWithoutYear = match.Groups["Title"].Value;
                year = match.Groups["Year"].Value;
            }
            else
            {
                titleWithoutYear = radTextBoxTitle.Text;
            }

            // title OK
            if (!String.IsNullOrEmpty(titleWithoutYear))
            {
                List<OmdbSearchResult> omdbSearchResults = new OmdbAPI().SearchMovie(titleWithoutYear.Trim(), year);

                if (omdbSearchResults == null)
                {
                    radListViewMovieSearch.DataSource = null;
                    radListViewMovieSearch.Items.Add("There are no records");
                    radTextBoxTitle.Enabled = true;
                    return;
                }

                // set data to control
                radListViewMovieSearch.DataSource = omdbSearchResults;
                radListViewMovieSearch.Columns[0].Width = 250;
                radListViewMovieSearch.Columns[1].Width = 60;
                radListViewMovieSearch.Columns[2].Width = 60;
                radListViewMovieSearch.Columns[3].Width = 60;
            }
            //ImdbDetails = UtilitySelenium.GetInstance.GetImdbByMovieTitleOrUrl(radTextBoxTitle.Text, null);
            radTextBoxTitle.Enabled = true;
        }

        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void radListViewMovieSearch_DoubleClick(object sender, EventArgs e)
        {
            var selectedItem = radListViewMovieSearch.SelectedItem;
            try
            {
                ImdbDetails = new OmdbAPI().GetMovieDetailsByImdbId(selectedItem["imdbID"].ToString());
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }catch(Exception){
                DialogResult = System.Windows.Forms.DialogResult.Abort;
            }

            this.Close();
        }

        private void FormImdbSearch_Load(object sender, EventArgs e)
        {
            
        }

        private void FormImdbSearch_Shown(object sender, EventArgs e)
        {
            radButtonSearch.PerformClick();
        }

        private void subtitlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = radListViewMovieSearch.SelectedItem;
            
            FormOpenSubtitles formOpenSubtitles = new FormOpenSubtitles();
            // IMDB link can not be null at this stage
            var imdbID = selectedItem["imdbID"].ToString();
            Regex regImdb = new Regex(@"tt(?<ImdbIDWithoutTT>\w+)");

            if (regImdb.IsMatch(imdbID))
            {
                formOpenSubtitles.ImdbID = regImdb.Match(imdbID).Groups["ImdbIDWithoutTT"].Value;
                formOpenSubtitles.MovieTitle = String.Format("{0} ({1})", selectedItem["Title"], selectedItem["Year"]);
                formOpenSubtitles.ShowDialog(this);
            }
        }
    }
}
