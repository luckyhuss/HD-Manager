namespace HDManager
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("HD/DTS Movies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("m-HD Movies", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Archives", System.Windows.Forms.HorizontalAlignment.Left);
            this.imageListMovieIcons = new System.Windows.Forms.ImageList(this.components);
            this.listViewLink = new System.Windows.Forms.ListView();
            this.columnHeaderTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDateAdded = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPassword = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMirrors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderIMDBRating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderIMDBContentRating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderIMDBLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderIMDBPoster = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripLink = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxStatus = new System.Windows.Forms.ToolStripComboBox();
            this.checkDownloadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageGetLinks = new System.Windows.Forms.TabPage();
            this.radTextBoxMessage = new Telerik.WinControls.UI.RadTextBox();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radButtonCloseWebBrowser = new Telerik.WinControls.UI.RadButton();
            this.radCheckBoxAutoClose = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabelSelectedForum = new Telerik.WinControls.UI.RadLabel();
            this.radLabel9 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBoxParentLink = new Telerik.WinControls.UI.RadTextBox();
            this.radButtonGetLinks = new Telerik.WinControls.UI.RadButton();
            this.tabPageLink = new System.Windows.Forms.TabPage();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.radLabel10 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBoxImdbRating = new Telerik.WinControls.UI.RadTextBox();
            this.radDropDownListPassword = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radDropDownListCategory = new Telerik.WinControls.UI.RadDropDownList();
            this.radTextBoxLink = new Telerik.WinControls.UI.RadTextBox();
            this.radButtonSave = new Telerik.WinControls.UI.RadButton();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radButtonDeleteAllLive = new Telerik.WinControls.UI.RadButton();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBoxPostLink = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBoxDescription = new Telerik.WinControls.UI.RadTextBox();
            this.radLabelCount = new Telerik.WinControls.UI.RadLabel();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.radButtonSearch = new Telerik.WinControls.UI.RadButton();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            this.radTextBoxNameSearch = new Telerik.WinControls.UI.RadTextBox();
            this.radTextBoxLinkSearch = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.timerOpenSubtitles = new System.Windows.Forms.Timer(this.components);
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.buttonMyDownloader = new System.Windows.Forms.Button();
            this.contextMenuStripLink.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageGetLinks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCloseWebBrowser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxAutoClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelSelectedForum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxParentLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonGetLinks)).BeginInit();
            this.tabPageLink.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxImdbRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonDeleteAllLive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxPostLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelCount)).BeginInit();
            this.tabPageSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxNameSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxLinkSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            this.SuspendLayout();
            // 
            // imageListMovieIcons
            // 
            this.imageListMovieIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMovieIcons.ImageStream")));
            this.imageListMovieIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMovieIcons.Images.SetKeyName(0, "1386338610_Add.png");
            this.imageListMovieIcons.Images.SetKeyName(1, "1386338503_Stock Index Down.png");
            this.imageListMovieIcons.Images.SetKeyName(2, "1386338389_Check.png");
            this.imageListMovieIcons.Images.SetKeyName(3, "1386338458_Remove.png");
            this.imageListMovieIcons.Images.SetKeyName(4, "1386338389_Check.png");
            // 
            // listViewLink
            // 
            this.listViewLink.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewLink.CheckBoxes = true;
            this.listViewLink.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTitle,
            this.columnHeaderDateAdded,
            this.columnHeaderPassword,
            this.columnHeaderStatus,
            this.columnHeaderMirrors,
            this.columnHeaderIMDBRating,
            this.columnHeaderIMDBContentRating,
            this.columnHeaderIMDBLink,
            this.columnHeaderIMDBPoster});
            this.listViewLink.ContextMenuStrip = this.contextMenuStripLink;
            this.listViewLink.FullRowSelect = true;
            listViewGroup1.Header = "HD/DTS Movies";
            listViewGroup1.Name = "listViewGroupHD";
            listViewGroup2.Header = "m-HD Movies";
            listViewGroup2.Name = "listViewGroupMHD";
            listViewGroup3.Header = "Archives";
            listViewGroup3.Name = "listViewGroupArchives";
            this.listViewLink.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.listViewLink.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLink.HotTracking = true;
            this.listViewLink.HoverSelection = true;
            this.listViewLink.Location = new System.Drawing.Point(12, 191);
            this.listViewLink.MultiSelect = false;
            this.listViewLink.Name = "listViewLink";
            this.listViewLink.Size = new System.Drawing.Size(755, 251);
            this.listViewLink.SmallImageList = this.imageListMovieIcons;
            this.listViewLink.TabIndex = 5;
            this.listViewLink.UseCompatibleStateImageBehavior = false;
            this.listViewLink.View = System.Windows.Forms.View.Details;
            this.listViewLink.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewLink_MouseClick);
            this.listViewLink.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewLink_MouseDoubleClick);
            // 
            // columnHeaderTitle
            // 
            this.columnHeaderTitle.Text = "Title";
            this.columnHeaderTitle.Width = 32;
            // 
            // columnHeaderDateAdded
            // 
            this.columnHeaderDateAdded.Text = "Date added";
            this.columnHeaderDateAdded.Width = 68;
            // 
            // columnHeaderPassword
            // 
            this.columnHeaderPassword.Text = "Password";
            this.columnHeaderPassword.Width = 58;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "Status";
            this.columnHeaderStatus.Width = 42;
            // 
            // columnHeaderMirrors
            // 
            this.columnHeaderMirrors.Text = "Mirrors";
            this.columnHeaderMirrors.Width = 43;
            // 
            // columnHeaderIMDBRating
            // 
            this.columnHeaderIMDBRating.Text = "Rating";
            this.columnHeaderIMDBRating.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeaderIMDBContentRating
            // 
            this.columnHeaderIMDBContentRating.Text = "Content rating";
            this.columnHeaderIMDBContentRating.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeaderIMDBLink
            // 
            this.columnHeaderIMDBLink.Text = "IMDb Link";
            this.columnHeaderIMDBLink.Width = 39;
            // 
            // columnHeaderIMDBPoster
            // 
            this.columnHeaderIMDBPoster.Text = "Poster";
            // 
            // contextMenuStripLink
            // 
            this.contextMenuStripLink.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem,
            this.linkToolStripMenuItem,
            this.passwordToolStripMenuItem,
            this.postLinkToolStripMenuItem,
            this.dateToolStripMenuItem,
            this.toolStripComboBoxStatus,
            this.checkDownloadsToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteToolStripMenuItem,
            this.unrarToolStripMenuItem,
            this.subtitlesToolStripMenuItem});
            this.contextMenuStripLink.Name = "contextMenuStripLink";
            this.contextMenuStripLink.Size = new System.Drawing.Size(182, 235);
            this.contextMenuStripLink.Opened += new System.EventHandler(this.contextMenuStripLink_Opened);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            // 
            // linkToolStripMenuItem
            // 
            this.linkToolStripMenuItem.Name = "linkToolStripMenuItem";
            this.linkToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.linkToolStripMenuItem.Text = "Link";
            // 
            // passwordToolStripMenuItem
            // 
            this.passwordToolStripMenuItem.Name = "passwordToolStripMenuItem";
            this.passwordToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.passwordToolStripMenuItem.Text = "Password";
            this.passwordToolStripMenuItem.Click += new System.EventHandler(this.passwordToolStripMenuItem_Click);
            // 
            // postLinkToolStripMenuItem
            // 
            this.postLinkToolStripMenuItem.Name = "postLinkToolStripMenuItem";
            this.postLinkToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.postLinkToolStripMenuItem.Text = "Open post";
            this.postLinkToolStripMenuItem.Click += new System.EventHandler(this.postLinkToolStripMenuItem_Click);
            // 
            // dateToolStripMenuItem
            // 
            this.dateToolStripMenuItem.Name = "dateToolStripMenuItem";
            this.dateToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.dateToolStripMenuItem.Text = "Date";
            this.dateToolStripMenuItem.Click += new System.EventHandler(this.dateToolStripMenuItem_Click);
            // 
            // toolStripComboBoxStatus
            // 
            this.toolStripComboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxStatus.Items.AddRange(new object[] {
            "New",
            "Downloading",
            "Completed",
            "KO"});
            this.toolStripComboBoxStatus.Name = "toolStripComboBoxStatus";
            this.toolStripComboBoxStatus.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxStatus_SelectedIndexChanged);
            // 
            // checkDownloadsToolStripMenuItem
            // 
            this.checkDownloadsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.checkDownloadsToolStripMenuItem.Name = "checkDownloadsToolStripMenuItem";
            this.checkDownloadsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.checkDownloadsToolStripMenuItem.Text = "Check downloads";
            this.checkDownloadsToolStripMenuItem.Click += new System.EventHandler(this.checkDownloadsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // unrarToolStripMenuItem
            // 
            this.unrarToolStripMenuItem.Name = "unrarToolStripMenuItem";
            this.unrarToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.unrarToolStripMenuItem.Tag = "Unrar ({0} / {1} completed)";
            this.unrarToolStripMenuItem.Text = "Unrar";
            this.unrarToolStripMenuItem.Click += new System.EventHandler(this.unrarToolStripMenuItem_Click);
            // 
            // subtitlesToolStripMenuItem
            // 
            this.subtitlesToolStripMenuItem.Name = "subtitlesToolStripMenuItem";
            this.subtitlesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.subtitlesToolStripMenuItem.Text = "Subtitles";
            this.subtitlesToolStripMenuItem.Click += new System.EventHandler(this.subtitlesToolStripMenuItem_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageGetLinks);
            this.tabControlMain.Controls.Add(this.tabPageLink);
            this.tabControlMain.Controls.Add(this.tabPageSearch);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(759, 173);
            this.tabControlMain.TabIndex = 8;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            // 
            // tabPageGetLinks
            // 
            this.tabPageGetLinks.Controls.Add(this.radTextBoxMessage);
            this.tabPageGetLinks.Controls.Add(this.radGroupBox1);
            this.tabPageGetLinks.Location = new System.Drawing.Point(4, 22);
            this.tabPageGetLinks.Name = "tabPageGetLinks";
            this.tabPageGetLinks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGetLinks.Size = new System.Drawing.Size(751, 147);
            this.tabPageGetLinks.TabIndex = 2;
            this.tabPageGetLinks.Text = "Get Links";
            this.tabPageGetLinks.UseVisualStyleBackColor = true;
            // 
            // radTextBoxMessage
            // 
            this.radTextBoxMessage.AutoSize = false;
            this.radTextBoxMessage.Location = new System.Drawing.Point(501, 16);
            this.radTextBoxMessage.Multiline = true;
            this.radTextBoxMessage.Name = "radTextBoxMessage";
            this.radTextBoxMessage.ReadOnly = true;
            this.radTextBoxMessage.Size = new System.Drawing.Size(244, 125);
            this.radTextBoxMessage.TabIndex = 1;
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.buttonMyDownloader);
            this.radGroupBox1.Controls.Add(this.radButtonCloseWebBrowser);
            this.radGroupBox1.Controls.Add(this.radCheckBoxAutoClose);
            this.radGroupBox1.Controls.Add(this.radLabelSelectedForum);
            this.radGroupBox1.Controls.Add(this.radLabel9);
            this.radGroupBox1.Controls.Add(this.radLabel8);
            this.radGroupBox1.Controls.Add(this.radTextBoxParentLink);
            this.radGroupBox1.Controls.Add(this.radButtonGetLinks);
            this.radGroupBox1.HeaderText = "Load all links";
            this.radGroupBox1.Location = new System.Drawing.Point(6, 6);
            this.radGroupBox1.Name = "radGroupBox1";
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(489, 135);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "Load all links";
            // 
            // radButtonCloseWebBrowser
            // 
            this.radButtonCloseWebBrowser.Location = new System.Drawing.Point(355, 106);
            this.radButtonCloseWebBrowser.Name = "radButtonCloseWebBrowser";
            this.radButtonCloseWebBrowser.Size = new System.Drawing.Size(119, 24);
            this.radButtonCloseWebBrowser.TabIndex = 1;
            this.radButtonCloseWebBrowser.Text = "Close Web Dirver";
            this.radButtonCloseWebBrowser.Click += new System.EventHandler(this.radButtonCloseWebBrowser_Click);
            // 
            // radCheckBoxAutoClose
            // 
            this.radCheckBoxAutoClose.AutoSize = false;
            this.radCheckBoxAutoClose.Location = new System.Drawing.Point(127, 106);
            this.radCheckBoxAutoClose.Name = "radCheckBoxAutoClose";
            this.radCheckBoxAutoClose.Size = new System.Drawing.Size(73, 24);
            this.radCheckBoxAutoClose.TabIndex = 4;
            this.radCheckBoxAutoClose.Text = "Autoclose ";
            this.radCheckBoxAutoClose.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // radLabelSelectedForum
            // 
            this.radLabelSelectedForum.Location = new System.Drawing.Point(5, 81);
            this.radLabelSelectedForum.Name = "radLabelSelectedForum";
            this.radLabelSelectedForum.Size = new System.Drawing.Size(91, 18);
            this.radLabelSelectedForum.TabIndex = 3;
            this.radLabelSelectedForum.Tag = "Selected forum : {0}";
            this.radLabelSelectedForum.Text = "Selected forum : ";
            // 
            // radLabel9
            // 
            this.radLabel9.Location = new System.Drawing.Point(5, 21);
            this.radLabel9.Name = "radLabel9";
            this.radLabel9.Size = new System.Drawing.Size(76, 18);
            this.radLabel9.TabIndex = 3;
            this.radLabel9.Text = "Open forum : ";
            // 
            // radLabel8
            // 
            this.radLabel8.Location = new System.Drawing.Point(5, 47);
            this.radLabel8.Name = "radLabel8";
            this.radLabel8.Size = new System.Drawing.Size(68, 18);
            this.radLabel8.TabIndex = 2;
            this.radLabel8.Text = "Forum link : ";
            // 
            // radTextBoxParentLink
            // 
            this.radTextBoxParentLink.Location = new System.Drawing.Point(79, 47);
            this.radTextBoxParentLink.Name = "radTextBoxParentLink";
            this.radTextBoxParentLink.Size = new System.Drawing.Size(395, 20);
            this.radTextBoxParentLink.TabIndex = 1;
            this.radTextBoxParentLink.TabStop = false;
            this.radTextBoxParentLink.TextChanged += new System.EventHandler(this.radTextBoxParentLink_TextChanged);
            this.radTextBoxParentLink.Enter += new System.EventHandler(this.radTextBoxParentLink_Enter);
            // 
            // radButtonGetLinks
            // 
            this.radButtonGetLinks.Location = new System.Drawing.Point(206, 106);
            this.radButtonGetLinks.Name = "radButtonGetLinks";
            this.radButtonGetLinks.Size = new System.Drawing.Size(143, 24);
            this.radButtonGetLinks.TabIndex = 0;
            this.radButtonGetLinks.Text = "Get links from forum";
            this.radButtonGetLinks.Click += new System.EventHandler(this.radButtonGetLinks_Click);
            // 
            // tabPageLink
            // 
            this.tabPageLink.Controls.Add(this.radGroupBox2);
            this.tabPageLink.Controls.Add(this.radLabelCount);
            this.tabPageLink.Location = new System.Drawing.Point(4, 22);
            this.tabPageLink.Name = "tabPageLink";
            this.tabPageLink.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLink.Size = new System.Drawing.Size(751, 147);
            this.tabPageLink.TabIndex = 0;
            this.tabPageLink.Text = "Movie details";
            this.tabPageLink.UseVisualStyleBackColor = true;
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Controls.Add(this.radLabel10);
            this.radGroupBox2.Controls.Add(this.radLabel1);
            this.radGroupBox2.Controls.Add(this.radTextBoxImdbRating);
            this.radGroupBox2.Controls.Add(this.radDropDownListPassword);
            this.radGroupBox2.Controls.Add(this.radLabel5);
            this.radGroupBox2.Controls.Add(this.radDropDownListCategory);
            this.radGroupBox2.Controls.Add(this.radTextBoxLink);
            this.radGroupBox2.Controls.Add(this.radButtonSave);
            this.radGroupBox2.Controls.Add(this.radLabel4);
            this.radGroupBox2.Controls.Add(this.radButtonDeleteAllLive);
            this.radGroupBox2.Controls.Add(this.radLabel3);
            this.radGroupBox2.Controls.Add(this.radTextBoxPostLink);
            this.radGroupBox2.Controls.Add(this.radLabel2);
            this.radGroupBox2.Controls.Add(this.radTextBoxDescription);
            this.radGroupBox2.HeaderText = "Movie details";
            this.radGroupBox2.Location = new System.Drawing.Point(6, 6);
            this.radGroupBox2.Name = "radGroupBox2";
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox2.Size = new System.Drawing.Size(544, 129);
            this.radGroupBox2.TabIndex = 10;
            this.radGroupBox2.Text = "Movie details";
            // 
            // radLabel10
            // 
            this.radLabel10.Location = new System.Drawing.Point(231, 101);
            this.radLabel10.Name = "radLabel10";
            this.radLabel10.Size = new System.Drawing.Size(42, 18);
            this.radLabel10.TabIndex = 8;
            this.radLabel10.Text = "IMDB : ";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(5, 21);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(40, 18);
            this.radLabel1.TabIndex = 9;
            this.radLabel1.Text = "Links : ";
            // 
            // radTextBoxImdbRating
            // 
            this.radTextBoxImdbRating.Location = new System.Drawing.Point(273, 100);
            this.radTextBoxImdbRating.Name = "radTextBoxImdbRating";
            this.radTextBoxImdbRating.Size = new System.Drawing.Size(34, 20);
            this.radTextBoxImdbRating.TabIndex = 7;
            this.radTextBoxImdbRating.TabStop = false;
            // 
            // radDropDownListPassword
            // 
            this.radDropDownListPassword.Location = new System.Drawing.Point(406, 48);
            this.radDropDownListPassword.Name = "radDropDownListPassword";
            this.radDropDownListPassword.Size = new System.Drawing.Size(129, 20);
            this.radDropDownListPassword.TabIndex = 16;
            // 
            // radLabel5
            // 
            this.radLabel5.Location = new System.Drawing.Point(341, 48);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(62, 18);
            this.radLabel5.TabIndex = 9;
            this.radLabel5.Text = "Password : ";
            // 
            // radDropDownListCategory
            // 
            this.radDropDownListCategory.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.radDropDownListCategory.Location = new System.Drawing.Point(77, 100);
            this.radDropDownListCategory.Name = "radDropDownListCategory";
            this.radDropDownListCategory.Size = new System.Drawing.Size(148, 20);
            this.radDropDownListCategory.TabIndex = 17;
            // 
            // radTextBoxLink
            // 
            this.radTextBoxLink.AcceptsReturn = true;
            this.radTextBoxLink.AutoSize = false;
            this.radTextBoxLink.Location = new System.Drawing.Point(77, 22);
            this.radTextBoxLink.Multiline = true;
            this.radTextBoxLink.Name = "radTextBoxLink";
            this.radTextBoxLink.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.radTextBoxLink.Size = new System.Drawing.Size(458, 20);
            this.radTextBoxLink.TabIndex = 9;
            this.radTextBoxLink.TabStop = false;
            this.radTextBoxLink.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.radTextBoxLink.TextChanged += new System.EventHandler(this.radTextBoxLink_TextChanged);
            this.radTextBoxLink.Enter += new System.EventHandler(this.radTextBoxLink_Enter);
            this.radTextBoxLink.MouseUp += new System.Windows.Forms.MouseEventHandler(this.radTextBoxLink_MouseUp);
            // 
            // radButtonSave
            // 
            this.radButtonSave.Location = new System.Drawing.Point(313, 100);
            this.radButtonSave.Name = "radButtonSave";
            this.radButtonSave.Size = new System.Drawing.Size(106, 20);
            this.radButtonSave.TabIndex = 18;
            this.radButtonSave.Text = "Save";
            this.radButtonSave.Click += new System.EventHandler(this.radButtonSave_Click);
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(5, 101);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(60, 18);
            this.radLabel4.TabIndex = 9;
            this.radLabel4.Text = "Category : ";
            // 
            // radButtonDeleteAllLive
            // 
            this.radButtonDeleteAllLive.Location = new System.Drawing.Point(425, 100);
            this.radButtonDeleteAllLive.Name = "radButtonDeleteAllLive";
            this.radButtonDeleteAllLive.Size = new System.Drawing.Size(110, 20);
            this.radButtonDeleteAllLive.TabIndex = 19;
            this.radButtonDeleteAllLive.Text = "Delete all ( live)";
            this.radButtonDeleteAllLive.Click += new System.EventHandler(this.radButtonDeleteAllLive_Click);
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(5, 75);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(57, 18);
            this.radLabel3.TabIndex = 9;
            this.radLabel3.Text = "Post link : ";
            // 
            // radTextBoxPostLink
            // 
            this.radTextBoxPostLink.Location = new System.Drawing.Point(77, 74);
            this.radTextBoxPostLink.Name = "radTextBoxPostLink";
            this.radTextBoxPostLink.Size = new System.Drawing.Size(458, 20);
            this.radTextBoxPostLink.TabIndex = 20;
            this.radTextBoxPostLink.TabStop = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(5, 49);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(36, 18);
            this.radLabel2.TabIndex = 7;
            this.radLabel2.Text = "Title : ";
            // 
            // radTextBoxDescription
            // 
            this.radTextBoxDescription.Location = new System.Drawing.Point(77, 48);
            this.radTextBoxDescription.Name = "radTextBoxDescription";
            this.radTextBoxDescription.Size = new System.Drawing.Size(258, 20);
            this.radTextBoxDescription.TabIndex = 21;
            this.radTextBoxDescription.TabStop = false;
            // 
            // radLabelCount
            // 
            this.radLabelCount.Location = new System.Drawing.Point(556, 27);
            this.radLabelCount.Name = "radLabelCount";
            this.radLabelCount.Size = new System.Drawing.Size(35, 18);
            this.radLabelCount.TabIndex = 9;
            this.radLabelCount.Text = "count";
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.radButtonSearch);
            this.tabPageSearch.Controls.Add(this.radButtonCancel);
            this.tabPageSearch.Controls.Add(this.radTextBoxNameSearch);
            this.tabPageSearch.Controls.Add(this.radTextBoxLinkSearch);
            this.tabPageSearch.Controls.Add(this.radLabel7);
            this.tabPageSearch.Controls.Add(this.radLabel6);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearch.Size = new System.Drawing.Size(751, 147);
            this.tabPageSearch.TabIndex = 1;
            this.tabPageSearch.Text = "Search";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // radButtonSearch
            // 
            this.radButtonSearch.Location = new System.Drawing.Point(277, 65);
            this.radButtonSearch.Name = "radButtonSearch";
            this.radButtonSearch.Size = new System.Drawing.Size(110, 24);
            this.radButtonSearch.TabIndex = 26;
            this.radButtonSearch.Text = "&Search";
            this.radButtonSearch.Click += new System.EventHandler(this.radButtonSearch_Click);
            // 
            // radButtonCancel
            // 
            this.radButtonCancel.Location = new System.Drawing.Point(393, 65);
            this.radButtonCancel.Name = "radButtonCancel";
            this.radButtonCancel.Size = new System.Drawing.Size(110, 24);
            this.radButtonCancel.TabIndex = 25;
            this.radButtonCancel.Text = "&Cancel";
            this.radButtonCancel.Click += new System.EventHandler(this.radButtonCancel_Click);
            // 
            // radTextBoxNameSearch
            // 
            this.radTextBoxNameSearch.AutoCompleteCustomSource.AddRange(new string[] {
            "anwar",
            "buchoo"});
            this.radTextBoxNameSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.radTextBoxNameSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.radTextBoxNameSearch.Location = new System.Drawing.Point(57, 39);
            this.radTextBoxNameSearch.Name = "radTextBoxNameSearch";
            this.radTextBoxNameSearch.Size = new System.Drawing.Size(446, 20);
            this.radTextBoxNameSearch.TabIndex = 24;
            this.radTextBoxNameSearch.TabStop = false;
            // 
            // radTextBoxLinkSearch
            // 
            this.radTextBoxLinkSearch.Location = new System.Drawing.Point(57, 11);
            this.radTextBoxLinkSearch.Name = "radTextBoxLinkSearch";
            this.radTextBoxLinkSearch.Size = new System.Drawing.Size(446, 20);
            this.radTextBoxLinkSearch.TabIndex = 23;
            this.radTextBoxLinkSearch.TabStop = false;
            // 
            // radLabel7
            // 
            this.radLabel7.Location = new System.Drawing.Point(6, 39);
            this.radLabel7.Name = "radLabel7";
            this.radLabel7.Size = new System.Drawing.Size(45, 18);
            this.radLabel7.TabIndex = 22;
            this.radLabel7.Text = "Name : ";
            // 
            // radLabel6
            // 
            this.radLabel6.Location = new System.Drawing.Point(6, 11);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(35, 18);
            this.radLabel6.TabIndex = 21;
            this.radLabel6.Text = "Link : ";
            // 
            // timerOpenSubtitles
            // 
            this.timerOpenSubtitles.Enabled = true;
            this.timerOpenSubtitles.Interval = 600000;
            this.timerOpenSubtitles.Tick += new System.EventHandler(this.timerOpenSubtitles_Tick);
            // 
            // toolTipMain
            // 
            this.toolTipMain.AutomaticDelay = 1000;
            this.toolTipMain.IsBalloon = true;
            this.toolTipMain.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // buttonMyDownloader
            // 
            this.buttonMyDownloader.Location = new System.Drawing.Point(6, 106);
            this.buttonMyDownloader.Name = "buttonMyDownloader";
            this.buttonMyDownloader.Size = new System.Drawing.Size(75, 23);
            this.buttonMyDownloader.TabIndex = 5;
            this.buttonMyDownloader.Text = "Downloader";
            this.buttonMyDownloader.UseVisualStyleBackColor = true;
            this.buttonMyDownloader.Click += new System.EventHandler(this.buttonMyDownloader_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(873, 454);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.listViewLink);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HD Download Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.contextMenuStripLink.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageGetLinks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCloseWebBrowser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxAutoClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelSelectedForum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxParentLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonGetLinks)).EndInit();
            this.tabPageLink.ResumeLayout(false);
            this.tabPageLink.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxImdbRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonDeleteAllLive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxPostLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelCount)).EndInit();
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxNameSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxLinkSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListMovieIcons;
        private System.Windows.Forms.ListView listViewLink;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLink;
        private System.Windows.Forms.ToolStripMenuItem linkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passwordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxStatus;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postLinkToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageLink;
        private System.Windows.Forms.TabPage tabPageSearch;
        private Telerik.WinControls.UI.RadLabel radLabel7;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadTextBox radTextBoxLinkSearch;
        private Telerik.WinControls.UI.RadTextBox radTextBoxNameSearch;
        private System.Windows.Forms.TabPage tabPageGetLinks;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadLabel radLabel8;
        private Telerik.WinControls.UI.RadTextBox radTextBoxParentLink;
        private Telerik.WinControls.UI.RadButton radButtonGetLinks;
        private Telerik.WinControls.UI.RadLabel radLabel9;
        private Telerik.WinControls.UI.RadLabel radLabelCount;
        private Telerik.WinControls.UI.RadLabel radLabel10;
        private Telerik.WinControls.UI.RadTextBox radTextBoxImdbRating;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadTextBox radTextBoxLink;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox radTextBoxDescription;
        private Telerik.WinControls.UI.RadTextBox radTextBoxPostLink;
        private Telerik.WinControls.UI.RadButton radButtonDeleteAllLive;
        private Telerik.WinControls.UI.RadButton radButtonSave;
        private Telerik.WinControls.UI.RadDropDownList radDropDownListCategory;
        private Telerik.WinControls.UI.RadDropDownList radDropDownListPassword;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadLabel radLabelSelectedForum;
        private System.Windows.Forms.ToolStripMenuItem unrarToolStripMenuItem;
        private Telerik.WinControls.UI.RadCheckBox radCheckBoxAutoClose;
        private Telerik.WinControls.UI.RadButton radButtonCloseWebBrowser;
        private System.Windows.Forms.ColumnHeader columnHeaderTitle;
        private System.Windows.Forms.ColumnHeader columnHeaderDateAdded;
        private System.Windows.Forms.ColumnHeader columnHeaderPassword;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderMirrors;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        private Telerik.WinControls.UI.RadButton radButtonSearch;
        private System.Windows.Forms.ColumnHeader columnHeaderIMDBLink;
        private System.Windows.Forms.ColumnHeader columnHeaderIMDBRating;
        private System.Windows.Forms.ColumnHeader columnHeaderIMDBContentRating;
        private System.Windows.Forms.ColumnHeader columnHeaderIMDBPoster;
        private System.Windows.Forms.ToolStripMenuItem subtitlesToolStripMenuItem;
        private System.Windows.Forms.Timer timerOpenSubtitles;
        private Telerik.WinControls.UI.RadTextBox radTextBoxMessage;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.ToolStripMenuItem checkDownloadsToolStripMenuItem;
        private System.Windows.Forms.Button buttonMyDownloader;

    }
}

