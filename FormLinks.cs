using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Web;
using System.Diagnostics;
using HDManager.Stub;
using System.Configuration;
using OpenQA.Selenium;

namespace HDManager
{
    public partial class FormLinks : Form
    {
        private XmlMovieElement _selectedXme = null;
        private ListViewItem _selectedLvi = null;
        private XmlMovieElement.LinkDetails _linkDetail = null;
        
        // constructor overloading
        public FormLinks(string link, XmlMovieElement selectedXme, ListViewItem selectedLvi, XmlMovieElement.LinkDetails linkDetail)
        {
            InitializeComponent();

            _selectedXme = selectedXme;
            _selectedLvi = selectedLvi;
            _linkDetail = linkDetail;

            // set checked all
            radCheckBoxSelectAll.Checked = true;

            // break links and add to checked list box
            foreach (var currentLink in link.Split(XmlMovieElement.LINK_FILENAME_SEPARATOR))
            {
                Uri currentUri = null;
                try
                {
                    currentUri = new Uri(currentLink);
                }
                catch (UriFormatException) { }

                // not an url, get the next one
                if (currentUri == null) continue;

                var listBoxChecked = true;

                // add the last Segment to list box and its full URI
                if (currentLink.Contains(".rev"))
                {
                    listBoxChecked = false;
                }
                checkedListBoxLink.Items.Add(
                    new Link(currentUri.Segments[currentUri.Segments.Length - 1], currentLink),
                    listBoxChecked);
            }

            UpdateCheckedCount();
        }

        private void radButtonDownload_Click(object sender, EventArgs e)
        {
            var sbLink = new StringBuilder();
            foreach (Link l in checkedListBoxLink.CheckedItems)
            {
                sbLink.Append(l.Linkname).Append("\n");
            }

            if (sbLink.ToString().Trim().Length <= 0) return;

            sbLink = sbLink.Remove(sbLink.Length - 1, 1);

            UtilitySelenium.GetInstance.OpenChromeDriver();

            // navigate to the movie page entered
            UtilitySelenium.GetInstance.NavigateChromeDriver(ConfigurationManager.AppSettings["DownloadUrl"]);

            IWebElement linksElement;

            try
            {
                // download now link
                linksElement = UtilitySelenium.GetInstance.ChromeDriver.FindElement(By.Name("links"));
            }
            catch (NoSuchElementException)
            {
                // failed to get the links textarea, stay on login page
                return;
            }

            linksElement.SendKeys(sbLink.ToString());

            if (radCheckBoxHDDLocal.Checked)
            {
                UtilitySelenium.GetInstance.ChromeDriver.FindElement(By.Name("local")).Click();
            }

            UtilitySelenium.GetInstance.ChromeDriver.FindElement(By.Name("download")).Click();
            
            while (true)
            {
                try
                {
                    UtilitySelenium.GetInstance.ChromeDriver.FindElement(By.Id("progress"));
                }
                catch (NoSuchElementException)
                {
                    // failed to get the progress loader, continue execution
                    break;
                }
                Thread.Sleep(3000);
            }

            // check the new links
            var newLinks = UtilitySelenium.GetInstance.ChromeDriver.FindElement(By.Name("links")).Text;

            if (!newLinks.Contains("HDD download initiated for file"))
            {
                var sbLinks = new StringBuilder();

                if (!String.IsNullOrEmpty(newLinks))
                {
                    foreach (var newlink in newLinks.Split('\n'))
                    {
                        var uriLink = new Uri(newlink);
                        sbLinks.Append(uriLink.Segments[uriLink.Segments.Length - 1]).Append(XmlMovieElement.LINK_FILENAME_SEPARATOR.ToString());
                    }

                    // update the selectedXme
                    if (String.IsNullOrEmpty(_selectedXme.Filenames))
                    {
                        _selectedXme.Filenames = sbLinks.ToString(0, sbLinks.Length - 1);
                    }
                    else
                    {
                        _selectedXme.Filenames = String.Concat(_selectedXme.Filenames, XmlMovieElement.LINK_FILENAME_SEPARATOR.ToString(), sbLinks.ToString(0, sbLinks.Length - 1));
                    }

                    // remove duplicates filenames
                    _selectedXme.Filenames = XmlMovieElement.CheckDuplicateFilenames(_selectedXme.Filenames);
                    
                    // change download status
                    _selectedXme.Status = XmlMovieElement.DownloadStatus.Downloading;
                    _linkDetail.DownloadLinkStatus = XmlMovieElement.DownloadStatus.OK;

                    // send to massdownloader (via Clipboard)
                    Clipboard.SetText(newLinks);

                    // 28/11/2014 Integrating uGet as default Download Manager
                    // uget.exe --quiet https://www.google.com/test.exe http://ideaof.me/test.exe ftp://server.com/test.exe                    
                    //var uGet = new Process
                    //{
                    //    StartInfo =
                    //    {
                    //        FileName = ConfigurationManager.AppSettings["Path.uGet"],
                    //        Arguments = String.Format(" --quiet {0}", newLinks.Replace("\r\n", " ")),
                    //        WindowStyle = ProcessWindowStyle.Normal
                    //    }
                    //};

                    //uGet.Start();
                }
                else
                {
                    // error in download links ( 404 or other errors)
                    //TODO replace with balloon message
                    //MessageBox.Show("No links generated.", "Error downloading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    // change download status
                    _selectedXme.Status = XmlMovieElement.DownloadStatus.New;
                    _linkDetail.DownloadLinkStatus = XmlMovieElement.DownloadStatus.KO;
                }
            }
            else
            {
                // MessageBox.Show(@"HDD download", @"You need to check manually.");
                _selectedXme.Status = XmlMovieElement.DownloadStatus.Downloading;
                _linkDetail.DownloadLinkStatus = XmlMovieElement.DownloadStatus.OK;
            }

            // update image color
            _selectedLvi.ImageIndex = (int)_selectedXme.Status;
            // update all nodes in XML
            _selectedXme.DateModified = DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss");

            var parent = this.Owner as FormMain;
            if (parent != null)
            {
                parent.WriteDownload();
            }

            UtilitySelenium.GetInstance.CloseChromeDriver(true);
                        
            // close this window
            Close();
        }

        private void checkedListBoxLink_MouseMove(object sender, MouseEventArgs e)
        {
            var itemIndex = checkedListBoxLink.IndexFromPoint(new Point(e.X, e.Y));

            if (itemIndex < 0) return;

            if (checkedListBoxLink.Items[itemIndex] != null)
            {
                checkedListBoxLink.SetItemChecked(itemIndex, !radCheckBoxSelectAll.Checked);
            }

            if (checkedListBoxLink.CheckedIndices.Count == 0)
            {
                // none checked
                radCheckBoxSelectAll.Checked = false;
            }
            else if (checkedListBoxLink.CheckedIndices.Count == checkedListBoxLink.Items.Count)
            {
                // all checked
                radCheckBoxSelectAll.Checked = true;
            }

            UpdateCheckedCount();
        }

        private void UpdateCheckedCount()
        {
            radLabelSelectedCount.Text =
                String.Format(radLabelSelectedCount.Tag.ToString(), checkedListBoxLink.CheckedItems.Count);
        }

        private void radCheckBoxSelectAll_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            for (var i = 0; i < checkedListBoxLink.Items.Count; i++)
            {
                checkedListBoxLink.SetItemChecked(i, radCheckBoxSelectAll.Checked);
            }

            UpdateCheckedCount();
        }
    }
}
