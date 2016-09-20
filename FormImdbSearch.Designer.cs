namespace HDManager
{
    partial class FormImdbSearch
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
            this.radButtonSearch = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBoxTitle = new Telerik.WinControls.UI.RadTextBox();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            this.radListViewMovieSearch = new Telerik.WinControls.UI.RadListView();
            this.contextMenuStripSubtitles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.subtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListViewMovieSearch)).BeginInit();
            this.contextMenuStripSubtitles.SuspendLayout();
            this.SuspendLayout();
            // 
            // radButtonSearch
            // 
            this.radButtonSearch.Location = new System.Drawing.Point(213, 40);
            this.radButtonSearch.Name = "radButtonSearch";
            this.radButtonSearch.Size = new System.Drawing.Size(110, 24);
            this.radButtonSearch.TabIndex = 0;
            this.radButtonSearch.Text = "&Search";
            this.radButtonSearch.Click += new System.EventHandler(this.radButtonSearch_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(12, 12);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(36, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Title : ";
            // 
            // radTextBoxTitle
            // 
            this.radTextBoxTitle.Location = new System.Drawing.Point(54, 12);
            this.radTextBoxTitle.Name = "radTextBoxTitle";
            this.radTextBoxTitle.Size = new System.Drawing.Size(385, 20);
            this.radTextBoxTitle.TabIndex = 0;
            // 
            // radButtonCancel
            // 
            this.radButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.radButtonCancel.Location = new System.Drawing.Point(329, 40);
            this.radButtonCancel.Name = "radButtonCancel";
            this.radButtonCancel.Size = new System.Drawing.Size(110, 24);
            this.radButtonCancel.TabIndex = 1;
            this.radButtonCancel.Text = "&Cancel";
            this.radButtonCancel.Click += new System.EventHandler(this.radButtonCancel_Click);
            // 
            // radListViewMovieSearch
            // 
            this.radListViewMovieSearch.AllowColumnReorder = false;
            this.radListViewMovieSearch.AllowColumnResize = false;
            this.radListViewMovieSearch.ContextMenuStrip = this.contextMenuStripSubtitles;
            this.radListViewMovieSearch.EnableColumnSort = true;
            this.radListViewMovieSearch.EnableKineticScrolling = true;
            this.radListViewMovieSearch.ItemSpacing = -1;
            this.radListViewMovieSearch.Location = new System.Drawing.Point(12, 74);
            this.radListViewMovieSearch.Name = "radListViewMovieSearch";
            this.radListViewMovieSearch.Size = new System.Drawing.Size(455, 229);
            this.radListViewMovieSearch.TabIndex = 2;
            this.radListViewMovieSearch.Text = "radListView1";
            this.radListViewMovieSearch.ViewType = Telerik.WinControls.UI.ListViewType.DetailsView;
            this.radListViewMovieSearch.DoubleClick += new System.EventHandler(this.radListViewMovieSearch_DoubleClick);
            // 
            // contextMenuStripSubtitles
            // 
            this.contextMenuStripSubtitles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subtitlesToolStripMenuItem});
            this.contextMenuStripSubtitles.Name = "contextMenuStripSubtitles";
            this.contextMenuStripSubtitles.Size = new System.Drawing.Size(120, 26);
            // 
            // subtitlesToolStripMenuItem
            // 
            this.subtitlesToolStripMenuItem.Name = "subtitlesToolStripMenuItem";
            this.subtitlesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.subtitlesToolStripMenuItem.Text = "Subtitles";
            this.subtitlesToolStripMenuItem.Click += new System.EventHandler(this.subtitlesToolStripMenuItem_Click);
            // 
            // FormImdbSearch
            // 
            this.AcceptButton = this.radButtonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.radButtonCancel;
            this.ClientSize = new System.Drawing.Size(477, 312);
            this.Controls.Add(this.radListViewMovieSearch);
            this.Controls.Add(this.radButtonCancel);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radTextBoxTitle);
            this.Controls.Add(this.radButtonSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormImdbSearch";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Get IMDB details for movie";
            this.Load += new System.EventHandler(this.FormImdbSearch_Load);
            this.Shown += new System.EventHandler(this.FormImdbSearch_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListViewMovieSearch)).EndInit();
            this.contextMenuStripSubtitles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButtonSearch;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        public Telerik.WinControls.UI.RadTextBox radTextBoxTitle;
        private Telerik.WinControls.UI.RadListView radListViewMovieSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSubtitles;
        private System.Windows.Forms.ToolStripMenuItem subtitlesToolStripMenuItem;
    }
}