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
    public partial class FormDownloadStatus : Form
    {
        /// <summary>
        /// Gets the IMDB details for the searched movie
        /// </summary>
        public List<XmlMovieElement> xmlDownloadingMovies = null;
        
        public FormDownloadStatus()
        {
            InitializeComponent();
        }

        private void FormDownloadStatus_Load(object sender, EventArgs e)
        {
            if (xmlDownloadingMovies != null)
            {
                DataTable dt = new DataTable("xmlDownloadingMovies");
                dt.Columns.Add("Title", typeof(string));
                dt.Columns.Add("XmlMovieElement", typeof(XmlMovieElement));

                foreach (var movie in xmlDownloadingMovies)
                {
                    var dr = dt.NewRow();
                    dr["Title"] = movie.Title;
                    dr["XmlMovieElement"] = movie;
                    dt.Rows.Add(dr);
                }

                radGridViewMovieDownloading.DataSource = dt;

                radGridViewMovieDownloading.Columns.Add("Progress", "Progress");
                radGridViewMovieDownloading.Columns.Add(new GridViewCheckBoxColumn("Extracted", "Extracted"));
                //var gvcbc = new GridViewComboBoxColumn("Status", "Status");
                //radGridViewMovieDownloading.Columns.Add(gvcbc);
                radGridViewMovieDownloading.Columns.Add("Status", "Status");

                radGridViewMovieDownloading.Columns["Title"].ReadOnly = true;
                radGridViewMovieDownloading.Columns["Title"].Width = 250;
                radGridViewMovieDownloading.Columns["Progress"].ReadOnly = true;
                radGridViewMovieDownloading.Columns["Progress"].Width = 50;
                radGridViewMovieDownloading.Columns["Extracted"].ReadOnly = true;
                radGridViewMovieDownloading.Columns["Extracted"].Width = 50;
                radGridViewMovieDownloading.Columns["XmlMovieElement"].IsVisible = false;

                // manipulate rows' values
                foreach (var gvr in radGridViewMovieDownloading.Rows)
                {
                    XmlMovieElement xmeCurrentRow =
                            (XmlMovieElement)gvr.Cells["XmlMovieElement"].Value;

                    var fileNames = xmeCurrentRow.Filenames.Split(XmlMovieElement.LINK_FILENAME_SEPARATOR);
                    double percentPerFilename = 100.0 / (double)fileNames.Length;
                    double percentage = 0.0;

                    foreach (var filename in fileNames)
                    {
                        if (File.Exists(Path.Combine(ConfigurationManager.AppSettings["Path.Download"], filename)))
                        {
                            // increment percentage
                            percentage += percentPerFilename;
                        }
                    }

                    gvr.Cells["Progress"].Value = String.Format("{0:N0} %", percentage);
                    
                    var extractFolder = Path.Combine(ConfigurationManager.AppSettings["Path.Download"],
                        ConfigurationManager.AppSettings["Name.Transfer"],  xmeCurrentRow.TitleCleaned());

                    if (Directory.Exists(extractFolder))
                    {
                        long totalSize = 0;
                        // check if file is present
                        foreach (var filename in Directory.EnumerateFiles(extractFolder))
                        {
                            FileInfo info = new FileInfo(filename);
                            totalSize += info.Length;
                        }

                        if (totalSize > (long)300000000)
                        {
                            gvr.Cells["Extracted"].Value = true;
                            gvr.Cells["Status"].Value = String.Format("{0:N2} GB - OK", (double)totalSize / (double)(1024 * 1024 * 1024));
                        }
                    }
                }

                
            }
        }

        private void LaunchAction(string data)
        {
            if (radGridViewMovieDownloading.SelectedRows.Count != 1) return;
            XmlMovieElement selectedXme = (XmlMovieElement)radGridViewMovieDownloading.SelectedRows[0].Cells["XmlMovieElement"].Value;
            var extracted = radGridViewMovieDownloading.SelectedRows[0].Cells["Extracted"].Value == null ? false : true;
            var parent = this.Owner as FormMain;

            switch (data)
            {
                case "Unrar":
                    if (extracted)
                    {
                        var responseExtracted = MessageBox.Show("Already extracted. Want to extract again ?", "RAR Extraction", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (responseExtracted == System.Windows.Forms.DialogResult.No)
                        {
                            break;
                        }
                    }

                    if (parent != null)
                    {
                        parent.LaunchAction("Unrar", null, null, selectedXme);
                    }

                    break;
                case "CheckFile":
                    var found = false;
                    var extractFolder = Path.Combine(ConfigurationManager.AppSettings["Path.Download"],
                        ConfigurationManager.AppSettings["Name.Transfer"], selectedXme.TitleCleaned());
                    if (Directory.Exists(extractFolder))
                    {
                        var filenames = Directory.EnumerateFiles(extractFolder, "*.mkv", SearchOption.AllDirectories).ToList();
                        if (filenames.Count > 0)
                        {
                            found = true;
                            Process.Start(filenames[0]);
                        }
                    }

                    if (!found)
                    {
                        ShowToolTipMessage("Not yet extracted", "File is not present, please extract first", ToolTipIcon.Error);
                    }
                    break;
                case "DeleteRAR":
                    var fileNames = selectedXme.Filenames.Split(XmlMovieElement.LINK_FILENAME_SEPARATOR);
                    List<string> rarToDelete = new List<string>(fileNames.Length);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Are you sure to delete these files : ");
                    foreach (var filename in fileNames)
                    {
                        var fullFilename = Path.Combine(ConfigurationManager.AppSettings["Path.Download"], filename);
                        FileInfo info = new FileInfo(fullFilename);
                        double fileSize = 0.0;
                        try
                        {
                            fileSize = info.Length; // FileNotFoundException
                        }
                        catch (FileNotFoundException) { }
                        finally
                        {
                            sb.AppendFormat("{0:N2} GB - {1}", (double)fileSize / (double)(1024 * 1024 * 1024), fullFilename);
                            sb.AppendLine();
                            rarToDelete.Add(fullFilename);
                        }
                    }

                    var responseDelete = MessageBox.Show(sb.ToString(), "Delete RAR files", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (responseDelete == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (var rar in rarToDelete)
                        {
                            File.Delete(rar);
                        }

                        selectedXme.Status = XmlMovieElement.DownloadStatus.Completed;
                        radGridViewMovieDownloading.Rows.Remove(radGridViewMovieDownloading.SelectedRows[0]);

                        if (parent != null)
                        {
                            parent.WriteDownload();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Show a balloon with a message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="window"></param>
        /// <param name="toolTipIcon"></param>
        private void ShowToolTipMessage(string title, string message, ToolTipIcon toolTipIcon)
        {
            toolTipDownloadStatus.ToolTipTitle = title;
            toolTipDownloadStatus.ToolTipIcon = toolTipIcon;
            toolTipDownloadStatus.Show(message, radLabelTooltip);
        }

        private void unrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchAction("Unrar");
        }

        private void checkFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchAction("CheckFile");
        }

        private void deleteRARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchAction("DeleteRAR");
        }

        private void contextMenuStripDownloadStatus_Opened(object sender, EventArgs e)
        {
            notYetCompletedToolStripMenuItem.Visible = false;

            if (radGridViewMovieDownloading.SelectedRows.Count == 1)
            {
                var extracted = radGridViewMovieDownloading.SelectedRows[0].Cells["Extracted"].Value == null ? false : true;
                var percentage = radGridViewMovieDownloading.SelectedRows[0].Cells["Progress"].Value.ToString().Replace(" %", String.Empty);
                
                checkFileToolStripMenuItem.Visible =
                    deleteRARToolStripMenuItem.Visible =
                    toolStripSeparator.Visible = extracted;

                if (Convert.ToInt32(percentage) < 100)
                {
                    unrarToolStripMenuItem.Visible = false;
                    notYetCompletedToolStripMenuItem.Visible = true;
                }
                else
                {
                    unrarToolStripMenuItem.Visible = true;
                }
            }
        }

        private void FormDownloadStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            var parent = this.Owner as FormMain;
            if (parent != null)
            {
                parent.PopulateListViewLink();
            }
        }
    }
}
