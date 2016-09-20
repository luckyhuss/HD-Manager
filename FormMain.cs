using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Web;
using System.Diagnostics;
using System.Configuration;

using HDManager.Stub;
using OpenQA.Selenium;
using Telerik.WinControls.UI;
using System.Collections.Specialized;
using CookComputing.XmlRpc;

namespace HDManager
{
    public partial class FormMain : Form
    {
        private static readonly string Dir = ConfigurationManager.AppSettings["XMLBaseUrl"];
        private const string Filename = "links.xml";
        private readonly string _path = Path.Combine(Dir, Filename);
        private List<XmlMovieElement> xmlNodes = new List<XmlMovieElement>();
        private string _selectedForum = null;

        // column indices
        private const int LISTVIEWLINK_COLUMN_INDEX_TITLE = 0;
        private const int LISTVIEWLINK_COLUMN_INDEX_DATECREATED = 1;
        private const int LISTVIEWLINK_COLUMN_INDEX_PASSWORD = 2;
        private const int LISTVIEWLINK_COLUMN_INDEX_STATUS = 3;
        private const int LISTVIEWLINK_COLUMN_INDEX_MIRRORS = 4;
        private const int LISTVIEWLINK_COLUMN_INDEX_IMDBRATING = 5;
        private const int LISTVIEWLINK_COLUMN_INDEX_IMDBCONTENTRATING = 6;
        private const int LISTVIEWLINK_COLUMN_INDEX_IMDBLINK = 7;
        private const int LISTVIEWLINK_COLUMN_INDEX_IMDBPOSTER = 8;

        private readonly string[] _supportedForums = new[]
            {
                "x264-bb.com", "shaanig.com", "m-hddl.com", "300mbunited.com", "warez-bb.org"
            };
        
        public FormMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // resize windows controls
            var widthDiff = Width - listViewLink.Width - (listViewLink.Location.X * 4);
            var heightDiff = Height - listViewLink.Height - (listViewLink.Location.Y + 50);

            listViewLink.Width = listViewLink.Width + widthDiff;
            listViewLink.Height = listViewLink.Height + heightDiff;


            if (File.Exists(_path))
            {
                // read download XML
                ReadDownload();
            }

            PopulateControls();

            XMLRPCOpenSubtitles.LogIn logIn = new XMLRPCOpenSubtitles.LogIn();
            XMLRPCOpenSubtitles.IOpenSubtitles proxy = XmlRpcProxyGen.Create<XMLRPCOpenSubtitles.IOpenSubtitles>();

            try
            {
                logIn = proxy.LogIn(XMLRPCOpenSubtitles.USERNAME, XMLRPCOpenSubtitles.PASSWORD,
                    XMLRPCOpenSubtitles.LANGUAGE, XMLRPCOpenSubtitles.USERAGENT);

                XMLRPCOpenSubtitles.logIn = logIn;
                
                UpdateMessage("OpenSubtitles connected", logIn.status);
            }
            catch (Exception ex)
            {
                UpdateMessage("OpenSubtitles error", ex.Message);
            }
        }

        private void UpdateMessage(string message, string status = "")
        {
            if (!String.IsNullOrEmpty(status))
            {
                status = String.Concat(" - ", status);
            }

            if (!String.IsNullOrEmpty(radTextBoxMessage.Text))
            {
                radTextBoxMessage.Text = String.Concat(radTextBoxMessage.Text, Environment.NewLine);
            }

            radTextBoxMessage.Text = String.Concat(
                radTextBoxMessage.Text, DateTime.Now.ToShortTimeString(), " - ", message, status);
        }

        private void PopulateControls()
        {
            // populate combo category from listview groups
            foreach (ListViewGroup lvg in listViewLink.Groups)
            {
                radDropDownListCategory.Items.Add(lvg.Header);
            }

            // populate combo Password from xml ordered passwords (distinct)
            foreach (var password in xmlNodes.Select
                (movie => movie.Password).OrderBy(password => password).Distinct())
            {
                radDropDownListPassword.Items.Add(password);
            }

            // select first element
            radDropDownListCategory.SelectedIndex = 0;

            // get clipboard content
            if (Clipboard.ContainsText() && Clipboard.GetText().Contains("http"))
            {
                radTextBoxParentLink.Text = Clipboard.GetText();
            }

            // populate list view with movies
            PopulateListViewLink();

            // set default status
            toolStripComboBoxStatus.SelectedIndex = 0;

            // activate the textbox
            ActiveControl = radTextBoxParentLink;

            // reset label text
            radLabelCount.Text = String.Empty;

            // set image list for context menu strip
            contextMenuStripLink.ImageList = imageListMovieIcons;
        }

        /// <summary>
        /// Repopulate the list from the list of XML Nodes (xmlNodes)
        /// </summary>
        public void PopulateListViewLink()
        {
            // clear list view first
            listViewLink.Items.Clear();

            // populate list view with movies
            foreach (XmlMovieElement xme in xmlNodes.OrderByDescending(movie => movie.Status))
            {
                // add item to ListViewLink
                AddItemToListViewLink(xme);
            }

            // auto resize content
            AutoResizeListViewLinkColumns();
        }

        private void AddItemToListViewLink(XmlMovieElement xme)
        {
            var lvi = new ListViewItem(
                new string[] { 
                    xme.Title, 
                    xme.DateCreated, 
                    xme.Password, 
                    xme.Status.ToString(), 
                    xme.Link.Count.ToString(), 
                    xme.ImdbRating, 
                    xme.ImdbContentRating }
                    )
                    {
                        ImageIndex = (int)xme.Status,
                        Group = listViewLink.Groups[(int)xme.Category],
                        Tag = xme
                    };

            // Completed or KO
            if (xme.Status == XmlMovieElement.DownloadStatus.Completed ||
                xme.Status == XmlMovieElement.DownloadStatus.KO)
            {
                // send to Archives
                lvi.Group = listViewLink.Groups[(int)XmlMovieElement.MovieCategory.Archives];
            }

            // add IMDB column
            ListViewItem.ListViewSubItem lvsiIMDB = new ListViewItem.ListViewSubItem();
            if (String.IsNullOrEmpty(xme.ImdbLink))
            {
                lvsiIMDB.Text = "Get IMDB";
                lvsiIMDB.Tag = null;
            }
            else
            {
                lvsiIMDB.Text = "Open IMDB";
                lvsiIMDB.Tag = xme.ImdbLink;
            }
            lvi.SubItems.Add(lvsiIMDB);

            // add Poster column
            lvsiIMDB = new ListViewItem.ListViewSubItem();
            if (String.IsNullOrEmpty(xme.ImdbPoster))
            {
                lvsiIMDB.Text = "X";
                lvsiIMDB.Tag = null;
            }
            else
            {
                lvsiIMDB.Text = "Copy image";
                lvsiIMDB.Tag = Path.Combine(ConfigurationManager.AppSettings["PosterBaseUrl"], xme.ImdbPoster);
            }
            lvi.SubItems.Add(lvsiIMDB);

            listViewLink.Items.Add(lvi);
        }

        private void AutoResizeListViewLinkColumns()
        {
            listViewLink.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            // column MIRRORS
            listViewLink.Columns[LISTVIEWLINK_COLUMN_INDEX_MIRRORS].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            // column IMDB RATING
            listViewLink.Columns[LISTVIEWLINK_COLUMN_INDEX_IMDBRATING].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            // column IMDB CONTENT RATING
            listViewLink.Columns[LISTVIEWLINK_COLUMN_INDEX_IMDBCONTENTRATING].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listViewLink_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LaunchAction("Title");
        }

        /// <summary>
        /// For context menu actions
        /// </summary>
        /// <param name="data"></param>
        /// <param name="link"></param>
        /// <param name="linkDetail"></param>
        public void LaunchAction(string data, string link = null, 
            XmlMovieElement.LinkDetails linkDetail = null, XmlMovieElement unrarSelectedXme = null)
        {
            if (listViewLink.SelectedItems.Count != 1 && 
                !data.Equals("Unrar", StringComparison.InvariantCultureIgnoreCase)) return;

            XmlMovieElement selectedXme = null;
            if (listViewLink.SelectedItems.Count > 0)
            {
                selectedXme = (XmlMovieElement)listViewLink.SelectedItems[0].Tag;
            }
            else
            {
                selectedXme = unrarSelectedXme;
            }

            switch (data)
            {
                case "Link":
                    if (String.IsNullOrEmpty(link)) return;

                    if (link.Contains(".rev"))
                    {
                        ShowToolTipMessage("REV Alert", "Links contain REV files", ToolTipIcon.Warning);
                    }
                    Clipboard.SetText(link, TextDataFormat.Text);
                    break;
                case "PostLink":
                    // open url to get new links
                    Process.Start(ConfigurationManager.AppSettings["Browser"], selectedXme.PostLink);
                    break;
                case "Password":
                    if (String.IsNullOrEmpty(selectedXme.Password)) return;
                    Clipboard.SetText(selectedXme.Password, TextDataFormat.Text);
                    break;
                case "Date":
                    ShowToolTipMessage("Dates", String.Concat("Date created : ", selectedXme.DateCreated,
                        Environment.NewLine, "Date modified : ", selectedXme.DateModified), ToolTipIcon.Info);
                    break;
                case "Download":
                    if (String.IsNullOrEmpty(link)) return;

                    var myform = new FormLinks(link, selectedXme, listViewLink.SelectedItems[0], linkDetail);
                    myform.ShowDialog(this);
                    break;
                case "Unrar":
                    //selectedXme
                    if (selectedXme == null || String.IsNullOrEmpty(selectedXme.Filenames))
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();

                        // filter string
                        openFileDialog.Filter = "Winrar (*.rar)|*.rar|Winzip (*.zip)|*.zip";
                        openFileDialog.InitialDirectory = ConfigurationManager.AppSettings["Path.Download"];
                        openFileDialog.Multiselect = false;

                        if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            var fileNameOfRar = Path.GetFileName(openFileDialog.FileName);

                            // search for the movie
                            List<XmlMovieElement> searchResult = SearchMovie(fileNameOfRar, String.Empty);

                            if (searchResult.Count == 1)
                            {
                                // found only one movie, process it
                                selectedXme = searchResult[0];
                            }
                            else
                            {
                                // not found
                                return;
                            }
                        }
                        else
                        {
                            // cancelled
                            return;
                        }
                    }

                    var fileNames = selectedXme.Filenames.Split(XmlMovieElement.LINK_FILENAME_SEPARATOR);
                    var rarNameFirstPart = Path.Combine(ConfigurationManager.AppSettings["Path.Download"], fileNames[0]);
                    var rarNameLastPart = Path.Combine(ConfigurationManager.AppSettings["Path.Download"],
                        fileNames[fileNames.Length - 1]);

                    // commandline "C:\Program Files\WinRAR\WinRAR.exe" e "D:\Quicken Deluxe 2013.part1.rar" "D:\" -panwar

                    if (!File.Exists(rarNameFirstPart) || !File.Exists(rarNameLastPart))
                    {
                        ShowToolTipMessage("Unrar", "Download has not yet completed for this movie", ToolTipIcon.Warning);
                        break;
                    }

                    var title = selectedXme.Title;
                    // safen title for filename // clean title
                    Array.ForEach(Path.GetInvalidFileNameChars(),
                        c => title = title.Replace(c.ToString(), String.Empty));

                    var extractFolder = Path.Combine(ConfigurationManager.AppSettings["Path.Download"],
                        ConfigurationManager.AppSettings["Name.Transfer"], title);

                    // create extract folder
                    Directory.CreateDirectory(extractFolder);

                    // copy movie cover to folder
                    if (!String.IsNullOrEmpty(selectedXme.ImdbPoster))
                    {
                        var filename = Path.GetFileName(selectedXme.ImdbPoster);
                        var posterSavePath = Path.Combine(ConfigurationManager.AppSettings["PosterBaseUrl"],
                            filename);

                        File.Copy(posterSavePath, Path.Combine(extractFolder, filename), true);
                    }

                    var unrar = new Process
                        {
                            StartInfo = 
                                {
                                    FileName = ConfigurationManager.AppSettings["Path.Winrar"],
                                    Arguments = String.Format(" e \"{0}\" \"{1}\" {2} -y {3}",
                                        rarNameFirstPart, extractFolder, 
                                        String.IsNullOrEmpty(selectedXme.Password) ? String.Empty : String.Format("-p{0}", selectedXme.Password),
                                        @"-n*.mkv -n*.avi -n*.vob -n*.m2ts -n*.mp3 -n*.mp4 -n*.flv -n*.wmv -n*.dat -n*.mov -n*.rm"),
                                    WindowStyle = ProcessWindowStyle.Normal
                                }
                        };

                    unrar.Start();
                    //unrar.WaitForExit(); // hang the window while extracting
                    break;
                case "Title":
                    if (String.IsNullOrEmpty(selectedXme.Title)) return;
                    Clipboard.SetText(selectedXme.Title, TextDataFormat.Text);
                    break;
                case "Subtitles":
                    FormOpenSubtitles formOpenSubtitles = new FormOpenSubtitles();
                    // IMDB link can not be null at this stage
                    var imdbLink = selectedXme.ImdbLink;
                    Regex regImdb = new Regex(@"www.imdb.com/title/tt(?<ImdbIDWithoutTT>\w+)");

                    if (regImdb.IsMatch(imdbLink))
                    {
                        formOpenSubtitles.ImdbID = regImdb.Match(imdbLink).Groups["ImdbIDWithoutTT"].Value;
                        formOpenSubtitles.MovieTitle = selectedXme.Title;
                        formOpenSubtitles.ShowDialog(this);
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
            toolTipMain.ToolTipTitle = title;
            toolTipMain.ToolTipIcon = toolTipIcon;
            toolTipMain.Show(message, radTextBoxMessage);
        }

        private void ReadDownload()
        {
            // Create an instance of XmlTextReader
            XmlTextReader textReader = null;

            try
            {
                // call Read method to read the file
                textReader = new XmlTextReader(_path);
                textReader.Read();

                var xme = new XmlMovieElement();

                var previousNode = String.Empty;
                var linkDetail = new XmlMovieElement.LinkDetails();

                // If the node has value
                while (textReader.Read())
                {
                    // Move to Movie element
                    textReader.MoveToElement();

                    if (textReader.LocalName.Equals("Movie", StringComparison.InvariantCultureIgnoreCase)
                        && textReader.NodeType == XmlNodeType.Element)
                    {
                        // create root element
                        xme = new XmlMovieElement();
                    }

                    if (textReader.NodeType == XmlNodeType.CDATA)
                    {
                        // sub Nodes
                        switch (previousNode)
                        {
                            case "Title":
                                xme.Title = textReader.Value;
                                break;
                            case "Category":
                                xme.Category = (XmlMovieElement.MovieCategory)Enum.Parse(
                                    typeof(XmlMovieElement.MovieCategory), textReader.Value);
                                break;
                            case "Password":
                                xme.Password = textReader.Value;
                                break;
                            case "DateCreated":
                                xme.DateCreated = textReader.Value;
                                break;
                            case "DateModified":
                                xme.DateModified = textReader.Value;
                                break;
                            case "DownloadServer":
                                // order is important
                                linkDetail = new XmlMovieElement.LinkDetails();
                                linkDetail.DownloadServer = textReader.Value;
                                break;
                            case "DownloadLink":
                                // order is important
                                linkDetail.DownloadLink = textReader.Value;
                                break;
                            case "DownloadLinkStatus":
                                // order is important
                                linkDetail.DownloadLinkStatus = (XmlMovieElement.DownloadStatus)Enum.Parse(
                                    typeof(XmlMovieElement.DownloadStatus), textReader.Value);
                                xme.Link.Add(linkDetail);
                                break;
                            case "PostLink":
                                xme.PostLink = textReader.Value;
                                break;
                            case "Status":
                                xme.Status = (XmlMovieElement.DownloadStatus)Enum.Parse(
                                    typeof(XmlMovieElement.DownloadStatus), textReader.Value);
                                break;
                            case "ImdbRating":
                                xme.ImdbRating = textReader.Value;
                                break;
                            case "Filenames":
                                xme.Filenames = textReader.Value;
                                break;
                            case "ImdbLink":
                                xme.ImdbLink = textReader.Value;
                                break;
                            case "ImdbContentRating":
                                xme.ImdbContentRating = textReader.Value;
                                break;
                            case "ImdbPoster":
                                xme.ImdbPoster = textReader.Value;
                                break;
                        }
                    }

                    if (textReader.LocalName.Equals("Movie", StringComparison.InvariantCultureIgnoreCase)
                        && textReader.NodeType == XmlNodeType.EndElement)
                    {
                        // add movie element
                        xmlNodes.Add(xme);
                    }

                    // save the previous node name
                    previousNode = textReader.LocalName;
                }
            }
            catch (Exception e)
            {
                ShowToolTipMessage("Read error", e.Message, ToolTipIcon.Error);
            }
            finally
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
            }
        }

        private bool SaveNewElement()
        {
            if (radTextBoxLink.Text.Trim().Length == 0 ||
                radTextBoxDescription.Text.Trim().Length == 0 ||
                radTextBoxPostLink.Text.Trim().Length == 0)
            {
                ShowToolTipMessage("Save error", "Please fill in all details", ToolTipIcon.Error);                
                return false;
            }

            var xme = new XmlMovieElement
                {
                    Title = radTextBoxDescription.Text.Trim(),
                    Category = (XmlMovieElement.MovieCategory)radDropDownListCategory.SelectedIndex,
                    Password = radDropDownListPassword.Text.Trim(),
                    DateCreated = DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss")
                };

            var link = radTextBoxLink.Text.Trim().Replace("url: ", string.Empty);
            // search for carriage return + new line and replace
            link = Regex.Replace(link, "(\r\n){1,}", "\n");
            link = Regex.Replace(link, "(\n){1,}", XmlMovieElement.LINK_FILENAME_SEPARATOR.ToString());

            // check if Tag is not null
            if (radTextBoxLink.Tag != null)
            {
                xme.Link = radTextBoxLink.Tag as List<XmlMovieElement.LinkDetails>;
            }
            else
            {
                var firstDownloadLink = new Uri(radTextBoxLink.Text.Substring(0, 40));
                var domain = firstDownloadLink.Host;
                
                var linkDetail = new XmlMovieElement.LinkDetails();
                linkDetail.DownloadServer = domain;
                linkDetail.DownloadLink = link;
                linkDetail.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;
                xme.Link.Add(linkDetail);
            }

            xme.PostLink = radTextBoxPostLink.Text.Trim().Replace(ConfigurationManager.AppSettings["SpamUrl"], string.Empty);
            xme.Status = 0;
            
            // get all IMDB details for the movie
            XmlMovieElement xmeImdb = (XmlMovieElement)radTextBoxImdbRating.Tag;
            if (xmeImdb != null)
            {
                xme.ImdbRating = xmeImdb.ImdbRating;
                xme.ImdbContentRating = xmeImdb.ImdbContentRating;
                xme.ImdbLink = xmeImdb.ImdbLink;
                xme.ImdbPoster = xmeImdb.ImdbPoster;
            }

            // add new movie element
            xmlNodes.Add(xme);
            
            // add item to ListViewLink
            AddItemToListViewLink(xme);

            // auto resize content
            AutoResizeListViewLinkColumns();

            // update password combo box
            if (!radDropDownListPassword.Items.Contains(xme.Password))
            {
                radDropDownListPassword.Items.Add(xme.Password);
            }

            // reset controls
            radTextBoxDescription.Text = radTextBoxLink.Text =
                radDropDownListPassword.Text = radTextBoxPostLink.Text =
                radTextBoxImdbRating.Text = String.Empty;
            // reset IMDB details
            radTextBoxImdbRating.Tag = null;

            return true;
        }

        public bool WriteDownload()
        {
            XmlTextWriter textWriter = null;
            try
            {
                // check dir first
                if (!Directory.Exists(Dir))
                {
                    Directory.CreateDirectory(Dir);
                }

                // Create a new file
                textWriter = new XmlTextWriter(_path, Encoding.UTF8);

                // Opens the document
                textWriter.WriteStartDocument();

                // Write comments
                textWriter.WriteComment("File containing all links to download");

                textWriter.WriteStartElement("m", "Database", "urn:movie");

                foreach (XmlMovieElement xme in xmlNodes)
                {
                    // Write Movie element
                    textWriter.WriteStartElement("Movie", "urn:movie");

                    // Write Title element
                    textWriter.WriteStartElement("Title", "urn:movie");
                    textWriter.WriteCData(xme.Title);
                    textWriter.WriteEndElement();
                    
                    // Write ImdbLink element
                    textWriter.WriteStartElement("ImdbLink", "urn:movie");
                    textWriter.WriteCData(xme.ImdbLink);
                    textWriter.WriteEndElement();

                    // Write ImdbRating element
                    textWriter.WriteStartElement("ImdbRating", "urn:movie");
                    textWriter.WriteCData(xme.ImdbRating);
                    textWriter.WriteEndElement();

                    // Write ImdbContentRating element
                    textWriter.WriteStartElement("ImdbContentRating", "urn:movie");
                    textWriter.WriteCData(xme.ImdbContentRating);
                    textWriter.WriteEndElement();

                    // Write ImdbPoster element
                    textWriter.WriteStartElement("ImdbPoster", "urn:movie");
                    textWriter.WriteCData(xme.ImdbPoster);
                    textWriter.WriteEndElement();
                                        
                    // Write PostLink element
                    textWriter.WriteStartElement("PostLink", "urn:movie");
                    textWriter.WriteCData(xme.PostLink);
                    textWriter.WriteEndElement();

                    // Write Password element
                    textWriter.WriteStartElement("Password", "urn:movie");
                    textWriter.WriteCData(xme.Password);
                    textWriter.WriteEndElement();

                    // Write Category element
                    textWriter.WriteStartElement("Category", "urn:movie");
                    textWriter.WriteCData(xme.Category.ToString());
                    textWriter.WriteEndElement();

                    // Write DateCreated element
                    textWriter.WriteStartElement("DateCreated", "urn:movie");
                    textWriter.WriteCData(xme.DateCreated);
                    textWriter.WriteEndElement();

                    // Write DateModified element
                    textWriter.WriteStartElement("DateModified", "urn:movie");
                    textWriter.WriteCData(xme.DateModified);
                    textWriter.WriteEndElement();

                    // Write Status element
                    textWriter.WriteStartElement("Status", "urn:movie");
                    textWriter.WriteCData(xme.Status.ToString());
                    textWriter.WriteEndElement();

                    // Write Links element
                    textWriter.WriteStartElement("Links", "urn:movie");

                    foreach (var currentLink in xme.Link)
                    {
                        // order is important
                        // Write Link element
                        textWriter.WriteStartElement("l", "Link", "urn:link");

                        // Write DownloadServer element
                        textWriter.WriteStartElement("DownloadServer", "urn:link");
                        textWriter.WriteCData(currentLink.DownloadServer);
                        textWriter.WriteEndElement();

                        // Write DownloadLink element
                        textWriter.WriteStartElement("DownloadLink", "urn:link");
                        textWriter.WriteCData(currentLink.DownloadLink);
                        textWriter.WriteEndElement();

                        // Write DownloadLinkStatus element
                        textWriter.WriteStartElement("DownloadLinkStatus", "urn:link");
                        textWriter.WriteCData(currentLink.DownloadLinkStatus.ToString());
                        textWriter.WriteEndElement();

                        // Write Link end element
                        textWriter.WriteEndElement();
                    }

                    // Write Links end element
                    textWriter.WriteEndElement();
                                        
                    // Write Filenames element
                    textWriter.WriteStartElement("Filenames", "urn:movie");
                    textWriter.WriteCData(xme.Filenames);
                    textWriter.WriteEndElement();
                    
                    // Write Movie end element
                    textWriter.WriteEndElement();
                }

                // End the document
                textWriter.WriteEndDocument();
            }
            catch (Exception e)
            {                
                ShowToolTipMessage("Save error", e.Message, ToolTipIcon.Error);
                return false;
            }
            finally
            {
                // close writer
                if (textWriter != null)
                {
                    textWriter.Close();
                }
            }
            return true;
        }

        private void passwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchAction("Password");
        }

        private void dateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchAction("Date");
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(
                @"Are you sure ?", @"Delete links",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);

            if (dr == DialogResult.Yes)
            {
                if (listViewLink.SelectedItems.Count == 1)
                {
                    var selectedLvi = listViewLink.SelectedItems[0];
                    var selectedXme = (XmlMovieElement)selectedLvi.Tag;
                    listViewLink.Items.Remove(selectedLvi);
                    xmlNodes.Remove(selectedXme);
                }
                else
                    if (listViewLink.CheckedItems.Count > 0)
                    {
                        foreach (ListViewItem selectedLvi in listViewLink.CheckedItems)
                        {
                            var selectedXme = selectedLvi.Tag as XmlMovieElement;
                            listViewLink.Items.Remove(selectedLvi);
                            xmlNodes.Remove(selectedXme);
                        }
                    }
            }

            // write to hdd in xml
            WriteDownload();
        }

        private void contextMenuStripLink_Opened(object sender, EventArgs e)
        {
            // set status
            if (listViewLink.SelectedItems.Count == 1)
            {
                // unhide controls
                toolStripComboBoxStatus.Visible =
                    linkToolStripMenuItem.Visible =
                    dateToolStripMenuItem.Visible =
                    passwordToolStripMenuItem.Visible =
                    downloadToolStripMenuItem.Visible =
                    toolStripSeparator1.Visible =
                    postLinkToolStripMenuItem.Visible =
                    subtitlesToolStripMenuItem.Visible =
                    unrarToolStripMenuItem.Visible = true;

                checkDownloadsToolStripMenuItem.Visible = false;

                var selectedXme = (XmlMovieElement)listViewLink.SelectedItems[0].Tag;
                toolStripComboBoxStatus.SelectedIndex = (int)selectedXme.Status;

                passwordToolStripMenuItem.Visible = !String.IsNullOrEmpty(selectedXme.Password); 

                // TODO set link count
                linkToolStripMenuItem.DropDownItems.Clear();
                downloadToolStripMenuItem.DropDownItems.Clear();

                foreach (var currentLink in selectedXme.Link)
                {
                    var link = currentLink;

                    // link menu strip
                    linkToolStripMenuItem.DropDownItems.Add(
                        String.Format("{0} ({1} link/s)",
                        currentLink.DownloadServer, currentLink.DownloadLink.Split(XmlMovieElement.LINK_FILENAME_SEPARATOR).Length),
                        imageListMovieIcons.Images[(int)currentLink.DownloadLinkStatus],
                        delegate
                        {
                            LaunchAction("Link", link.DownloadLink.Replace(XmlMovieElement.LINK_FILENAME_SEPARATOR.ToString(), Environment.NewLine));
                        }
                    );

                    // download menu strip
                    downloadToolStripMenuItem.DropDownItems.Add(
                        String.Format("{0} ({1} link/s)",
                        currentLink.DownloadServer, currentLink.DownloadLink.Split(XmlMovieElement.LINK_FILENAME_SEPARATOR).Length),
                        imageListMovieIcons.Images[(int)currentLink.DownloadLinkStatus],
                        delegate
                        {
                            LaunchAction("Download", link.DownloadLink, currentLink);
                        }
                    );
                }


                // set date
                dateToolStripMenuItem.Text = String.Format("Added : {0}", selectedXme.DateCreated);

                // check if Imdb link is set (already) for subtitles
                if (String.IsNullOrEmpty(selectedXme.ImdbLink))
                {
                    subtitlesToolStripMenuItem.Visible = false;
                }
            }
            else
            {
                // hide controls
                toolStripComboBoxStatus.Visible =
                    linkToolStripMenuItem.Visible =
                    dateToolStripMenuItem.Visible =
                    passwordToolStripMenuItem.Visible =
                    downloadToolStripMenuItem.Visible =
                    postLinkToolStripMenuItem.Visible =
                    subtitlesToolStripMenuItem.Visible = false;
                    //unrarToolStripMenuItem.Visible = false;

                // show controls
                checkDownloadsToolStripMenuItem.Visible =
                    toolStripSeparator1.Visible = true;

            }
        }

        private void toolStripComboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // modify status
            if (listViewLink.SelectedItems.Count != 1) return;

            var selectedXme = (XmlMovieElement)listViewLink.SelectedItems[0].Tag;

            var itemIndex = xmlNodes.IndexOf(selectedXme);
            if (itemIndex == -1 || (int)xmlNodes[itemIndex].Status == toolStripComboBoxStatus.SelectedIndex) return;

            // found
            xmlNodes[itemIndex].Status = (XmlMovieElement.DownloadStatus)toolStripComboBoxStatus.SelectedIndex;
            xmlNodes[itemIndex].DateModified = DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss");

            // write to hdd in xml because changed
            WriteDownload();

            // hide the context menu
            contextMenuStripLink.Close();

            // update image color
            listViewLink.SelectedItems[0].ImageIndex = (int)selectedXme.Status;

            // update group - Completed or KO
            if (xmlNodes[itemIndex].Status == XmlMovieElement.DownloadStatus.Completed || 
                xmlNodes[itemIndex].Status == XmlMovieElement.DownloadStatus.KO)
            {
                listViewLink.SelectedItems[0].Group = listViewLink.Groups[2];
            }
            else
            {
                listViewLink.SelectedItems[0].Group = listViewLink.Groups[(int)xmlNodes[itemIndex].Category];
            }
        }
        
        private void postLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchAction("PostLink");
        }

        /// <summary>
        /// Search the XML for a movie by link or name
        /// </summary>
        /// <param name="link"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private List<XmlMovieElement> SearchMovie(string link, string name)
        {
            var searchResult = new List<XmlMovieElement>();
            link = link.ToLower();
            name = name.ToLower();

            if (!String.IsNullOrEmpty(link) && !String.IsNullOrEmpty(name))
            {
                // TODO search link
                //searchResult = xmlNodes.Select(movie => movie).Where(movie => movie.Link.ToLower().Contains(link) && movie.Title.ToLower().Contains(name)).ToList();
            }
            else
                if (!String.IsNullOrEmpty(link))
                {
                    // TODO search link
                    searchResult = xmlNodes.Select(movie => movie).Where(movie => movie.Link.Any(links => links.DownloadLink.ToLower().Contains(link))).ToList();
                }
                else
                    if (!String.IsNullOrEmpty(name))
                    {
                        searchResult = xmlNodes.Select(movie => movie).Where(movie => movie.Title.ToLower().Contains(name)).ToList();
                    }
            return searchResult;
        }

        private void radButtonSave_Click(object sender, EventArgs e)
        {
            // write to hdd in xml
            if (SaveNewElement() && WriteDownload())
            {
                ShowToolTipMessage("HD Manager", "Download links saved", ToolTipIcon.Info);                
            }
        }

        private void radButtonDeleteAllLive_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you sure ?", @"Do you want to delete all ?", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
            {
                return;
            }

            //Process.Start(ConfigurationManager.AppSettings["Browser"], ConfigurationManager.AppSettings["DeleteUrl"]);
            UtilitySelenium.GetInstance.OpenChromeDriver();

            UtilitySelenium.GetInstance.NavigateChromeDriver(ConfigurationManager.AppSettings["DeleteUrl"]);

            UtilitySelenium.GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
        }

        private void radTextBoxLink_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(radTextBoxLink.Text.Trim()))
            {
                radLabelCount.Text = String.Format("{0} files",
                    radTextBoxLink.Text.Split('\n').Length.ToString(CultureInfo.InvariantCulture));
            }
        }

        private void radTextBoxLink_MouseUp(object sender, MouseEventArgs e)
        {
            radTextBoxLink.SelectAll();
        }

        private void radTextBoxLink_Enter(object sender, EventArgs e)
        {
            radTextBoxLink_MouseUp(sender, null);
        }

        private void radButtonGetLinks_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(radTextBoxParentLink.Text.Trim())) return;

            UtilitySelenium.GetInstance.OpenChromeDriver();

            // navigate to the movie page entered
            UtilitySelenium.GetInstance.NavigateChromeDriver(radTextBoxParentLink.Text.Trim());

            switch (_selectedForum)
            {
                case "x264-bb.com":
                    radDropDownListCategory.SelectedIndex = 0;
                    UtilitySelenium.GetInstance.X264ReadAllLinks(radTextBoxLink, radTextBoxParentLink, 
                        radTextBoxDescription, radTextBoxPostLink, radTextBoxImdbRating, radDropDownListPassword,
                        radCheckBoxAutoClose);
                    break;
                case "shaanig.com":
                    radDropDownListCategory.SelectedIndex = 1;
                    UtilitySelenium.GetInstance.ShaanigReadAllLinks(radTextBoxLink, radTextBoxParentLink,
                       radTextBoxDescription, radTextBoxPostLink, radTextBoxImdbRating, radDropDownListPassword,
                       radCheckBoxAutoClose);
                    break;
                case "m-hddl.com":
                    radDropDownListCategory.SelectedIndex = 0;
                    UtilitySelenium.GetInstance.MHDDLReadAllLinks(radTextBoxLink, radTextBoxParentLink, 
                        radTextBoxDescription, radTextBoxPostLink, radTextBoxImdbRating, radDropDownListPassword,
                        radCheckBoxAutoClose);
                    break;
                //case "300mbunited.com":
                //    break;
                //case "warez-bb.org":
                //    break;
                default:
                    UtilitySelenium.GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
                    return;
            }

            if (!String.IsNullOrEmpty(radTextBoxDescription.Text))
            {
                // load the second tab
                tabControlMain.SelectTab(tabPageLink);
            }
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabControl = sender as TabControl;
            if (tabControl == null) return;

            switch (tabControl.SelectedIndex)
            {
                case 0:
                    radTextBoxParentLink.Focus();
                    AcceptButton = radButtonGetLinks;
                    break;
                case 1:
                    AcceptButton = radButtonSave;
                    break;
                case 2:
                    AcceptButton = radButtonSearch;
                    break;
            }
        }

        private void radTextBoxParentLink_Enter(object sender, EventArgs e)
        {
            radTextBoxParentLink.SelectAll();
        }

        private void radTextBoxParentLink_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!radTextBoxParentLink.Text.Contains("http"))
                {
                    // insert http://
                    radTextBoxParentLink.Text = String.Concat("http://", radTextBoxParentLink.Text);
                }

                var forumPost = new Uri(radTextBoxParentLink.Text.Trim());

                // reset forum
                _selectedForum = null;

                radLabelSelectedForum.Text =
                    String.Format(radLabelSelectedForum.Tag.ToString(), forumPost.Host);
                
                if (_supportedForums.Count(forum => forumPost.Host.Contains(forum)) > 0)
                {
                    // supported
                    radLabelSelectedForum.Text += @" ( Forum supported).";

                    // get the supported forum
                    _selectedForum = _supportedForums.First(forum => forumPost.Host.Contains(forum));
                }
                else
                {
                    // not supported
                    radLabelSelectedForum.Text += @" ( /!\ Forum not supported).";
                }
            }
            catch (UriFormatException)
            {
                radLabelSelectedForum.Text =
                    String.Format(radLabelSelectedForum.Tag.ToString(), @"   /!\ Error");
            }
        }

        private void unrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchAction("Unrar");
        }

        private void radButtonCloseWebBrowser_Click(object sender, EventArgs e)
        {
            UtilitySelenium.GetInstance.CloseChromeDriver(true);
        }

        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            PopulateListViewLink();
        }

        private void radButtonSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(radTextBoxLinkSearch.Text.Trim()) ||
                !String.IsNullOrEmpty(radTextBoxNameSearch.Text.Trim()))
            {
                List<XmlMovieElement> searchResult = SearchMovie(
                    radTextBoxLinkSearch.Text.Trim(), radTextBoxNameSearch.Text.Trim());

                if (searchResult.Count > 0)
                {
                    // clear all items
                    listViewLink.Items.Clear();

                    foreach (XmlMovieElement xme in searchResult)
                    {
                        // add searched result
                        AddItemToListViewLink(xme);
                    }

                    AutoResizeListViewLinkColumns();
                }
                else
                {
                    ShowToolTipMessage("Search result", "No such download", ToolTipIcon.Warning);
                }
            }
        }

        private void listViewLink_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = listViewLink.HitTest(e.Location);
            int columnIndex = hit.Item.SubItems.IndexOf(hit.SubItem);
            
            // check which column clicked
            switch (columnIndex)
            {
                case LISTVIEWLINK_COLUMN_INDEX_IMDBLINK:
                    // IMDB
                    var imdbUrl = hit.SubItem.Tag;

                    if (imdbUrl != null)
                    {
                        // open IMDB
                        Process.Start(imdbUrl.ToString());
                        return;
                    }

                    FormImdbSearch formImdbSearch = new FormImdbSearch();
                    formImdbSearch.radTextBoxTitle.Text = listViewLink.GetItemAt(e.X, e.Y).Text;

                    if (formImdbSearch.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        // user launched the search function
                        // get the IMDB link, IMDB rating and IMDB content rating
                        if (formImdbSearch.ImdbDetails != null)
                        {
                            // ask for confirmation
                            var message = String.Format(
                                "Do you want to update movie details with these data : \nTitle : {0}\nLink : {1}\nRating : {2}\nContent rating : {3}\nPoster : {4}",
                                formImdbSearch.ImdbDetails.Title,
                                formImdbSearch.ImdbDetails.ImdbLink,
                                formImdbSearch.ImdbDetails.ImdbRating,
                                formImdbSearch.ImdbDetails.ImdbContentRating,
                                Path.GetFileName(formImdbSearch.ImdbDetails.ImdbPoster));

                            if (MessageBox.Show(message,
                                String.Concat("Update for movie : ", formImdbSearch.radTextBoxTitle.Text),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                            {
                                return;
                            }

                            // read all values
                            var selectedXme = (XmlMovieElement)hit.Item.Tag;

                            var itemIndex = xmlNodes.IndexOf(selectedXme);
                            if (itemIndex == -1) return;

                            // found
                            xmlNodes[itemIndex].Title = formImdbSearch.ImdbDetails.Title;
                            xmlNodes[itemIndex].ImdbLink = formImdbSearch.ImdbDetails.ImdbLink;
                            xmlNodes[itemIndex].ImdbRating = formImdbSearch.ImdbDetails.ImdbRating;
                            xmlNodes[itemIndex].ImdbContentRating = formImdbSearch.ImdbDetails.ImdbContentRating;
                            xmlNodes[itemIndex].ImdbPoster = formImdbSearch.ImdbDetails.ImdbPoster;
                            xmlNodes[itemIndex].DateModified = DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss");

                            // write to hdd in xml because changed
                            WriteDownload();

                            // list view item update
                            var lvi = listViewLink.GetItemAt(e.X, e.Y);
                            lvi.Text = String.Format("* {0}", xmlNodes[itemIndex].Title);
                            lvi.SubItems[LISTVIEWLINK_COLUMN_INDEX_IMDBRATING].Text = xmlNodes[itemIndex].ImdbRating;
                            lvi.SubItems[LISTVIEWLINK_COLUMN_INDEX_IMDBCONTENTRATING].Text = xmlNodes[itemIndex].ImdbContentRating;
                            lvi.SubItems[LISTVIEWLINK_COLUMN_INDEX_IMDBLINK].Text = "Open IMDB";
                            lvi.SubItems[LISTVIEWLINK_COLUMN_INDEX_IMDBLINK].Tag = xmlNodes[itemIndex].ImdbLink;
                            if (!String.IsNullOrEmpty(xmlNodes[itemIndex].ImdbPoster))
                            {
                                lvi.SubItems[LISTVIEWLINK_COLUMN_INDEX_IMDBPOSTER].Text = "Copy image";
                                lvi.SubItems[LISTVIEWLINK_COLUMN_INDEX_IMDBPOSTER].Tag = xmlNodes[itemIndex].ImdbPoster;
                            }
                            else
                            {
                                lvi.SubItems[LISTVIEWLINK_COLUMN_INDEX_IMDBPOSTER].Text = "X";
                            }
                        }
                    }
                    break;
                case LISTVIEWLINK_COLUMN_INDEX_IMDBPOSTER:
                    if (hit.SubItem.Tag != null)
                    {
                        // copy image to clipboard
                        StringCollection fileCollection = new StringCollection();
                        fileCollection.Add(hit.SubItem.Tag.ToString());
                        Clipboard.SetFileDropList(fileCollection);
                        ShowToolTipMessage("Copied", "IMDB poster copied to clipboard", ToolTipIcon.Warning);
                        return;
                    }
                    break;
                default:
                    break;
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // close all browsers
            UtilitySelenium.GetInstance.CloseChromeDriver(true);

            // Logout of OpenSubtitles
            XMLRPCOpenSubtitles.IOpenSubtitles proxy = XmlRpcProxyGen.Create<XMLRPCOpenSubtitles.IOpenSubtitles>();
            XMLRPCOpenSubtitles.LogIn logIn = XMLRPCOpenSubtitles.logIn;
            try
            {
                XMLRPCOpenSubtitles.LogOut logOut = proxy.LogOut(logIn.token);
            }
            catch (Exception) { }
        }

        public ListView GetLinkView()
        {
            return listViewLink;
        }

        private void subtitlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchAction("Subtitles");
        }

        private void timerOpenSubtitles_Tick(object sender, EventArgs e)
        {
            XMLRPCOpenSubtitles.IOpenSubtitles proxy = XmlRpcProxyGen.Create<XMLRPCOpenSubtitles.IOpenSubtitles>();
            XMLRPCOpenSubtitles.LogIn logIn = XMLRPCOpenSubtitles.logIn;

            try
            {
                // refresh session object
                XMLRPCOpenSubtitles.NoOperation noOperation = proxy.NoOperation(logIn.token);
                UpdateMessage("OpenSubtitles refreshed", noOperation.status);
            }
            catch (Exception ex)
            {
                UpdateMessage("OpenSubtitles KO", ex.Message);
            }
        }

        private void checkDownloadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDownloadStatus formDownloadStatus = new FormDownloadStatus();
            formDownloadStatus.xmlDownloadingMovies = xmlNodes
                .Where(movie => movie.Status == XmlMovieElement.DownloadStatus.Downloading)
                .Select(movie => movie)
                .OrderBy(movie => movie.Title).ToList();
            formDownloadStatus.ShowDialog(this);
        }

        private void buttonMyDownloader_Click(object sender, EventArgs e)
        {
            Process.Start(@"E:\Google Drive\WinApp\MyDownloader\MyDownloader.App\bin\Debug\MyDownloader.App.exe", "/sw https://test2.com/test.exe https://test2.com/test1.exe https://test2.com/test2.exe");
        }
    }
}
