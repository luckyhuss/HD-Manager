namespace HDManager
{
    partial class FormDownloadStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDownloadStatus));
            this.radGridViewMovieDownloading = new Telerik.WinControls.UI.RadGridView();
            this.contextMenuStripDownloadStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.deleteRARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipDownloadStatus = new System.Windows.Forms.ToolTip(this.components);
            this.radLabelTooltip = new Telerik.WinControls.UI.RadLabel();
            this.notYetCompletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewMovieDownloading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewMovieDownloading.MasterTemplate)).BeginInit();
            this.contextMenuStripDownloadStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelTooltip)).BeginInit();
            this.SuspendLayout();
            // 
            // radGridViewMovieDownloading
            // 
            this.radGridViewMovieDownloading.ContextMenuStrip = this.contextMenuStripDownloadStatus;
            this.radGridViewMovieDownloading.Location = new System.Drawing.Point(12, 22);
            // 
            // radGridViewMovieDownloading
            // 
            this.radGridViewMovieDownloading.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
            this.radGridViewMovieDownloading.MasterTemplate.AllowAddNewRow = false;
            this.radGridViewMovieDownloading.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.radGridViewMovieDownloading.MasterTemplate.AllowColumnReorder = false;
            this.radGridViewMovieDownloading.MasterTemplate.AllowColumnResize = false;
            this.radGridViewMovieDownloading.MasterTemplate.AllowDeleteRow = false;
            this.radGridViewMovieDownloading.MasterTemplate.AllowDragToGroup = false;
            this.radGridViewMovieDownloading.MasterTemplate.AllowEditRow = false;
            this.radGridViewMovieDownloading.MasterTemplate.AllowRowResize = false;
            this.radGridViewMovieDownloading.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.radGridViewMovieDownloading.MasterTemplate.EnableSorting = false;
            this.radGridViewMovieDownloading.MasterTemplate.ShowFilteringRow = false;
            this.radGridViewMovieDownloading.MasterTemplate.ShowRowHeaderColumn = false;
            this.radGridViewMovieDownloading.Name = "radGridViewMovieDownloading";
            this.radGridViewMovieDownloading.ReadOnly = true;
            this.radGridViewMovieDownloading.Size = new System.Drawing.Size(579, 331);
            this.radGridViewMovieDownloading.TabIndex = 4;
            // 
            // contextMenuStripDownloadStatus
            // 
            this.contextMenuStripDownloadStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unrarToolStripMenuItem,
            this.checkFileToolStripMenuItem,
            this.toolStripSeparator,
            this.deleteRARToolStripMenuItem,
            this.notYetCompletedToolStripMenuItem});
            this.contextMenuStripDownloadStatus.Name = "contextMenuStripDownloadStatus";
            this.contextMenuStripDownloadStatus.Size = new System.Drawing.Size(170, 98);
            this.contextMenuStripDownloadStatus.Opened += new System.EventHandler(this.contextMenuStripDownloadStatus_Opened);
            // 
            // unrarToolStripMenuItem
            // 
            this.unrarToolStripMenuItem.Name = "unrarToolStripMenuItem";
            this.unrarToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.unrarToolStripMenuItem.Text = "&Unrar";
            this.unrarToolStripMenuItem.Click += new System.EventHandler(this.unrarToolStripMenuItem_Click);
            // 
            // checkFileToolStripMenuItem
            // 
            this.checkFileToolStripMenuItem.Name = "checkFileToolStripMenuItem";
            this.checkFileToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.checkFileToolStripMenuItem.Text = "&Check file";
            this.checkFileToolStripMenuItem.Click += new System.EventHandler(this.checkFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(166, 6);
            // 
            // deleteRARToolStripMenuItem
            // 
            this.deleteRARToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.deleteRARToolStripMenuItem.Name = "deleteRARToolStripMenuItem";
            this.deleteRARToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.deleteRARToolStripMenuItem.Text = "&Delete RAR";
            this.deleteRARToolStripMenuItem.Click += new System.EventHandler(this.deleteRARToolStripMenuItem_Click);
            // 
            // toolTipDownloadStatus
            // 
            this.toolTipDownloadStatus.AutomaticDelay = 1000;
            this.toolTipDownloadStatus.IsBalloon = true;
            this.toolTipDownloadStatus.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // radLabelTooltip
            // 
            this.radLabelTooltip.Location = new System.Drawing.Point(12, 12);
            this.radLabelTooltip.Name = "radLabelTooltip";
            this.radLabelTooltip.Size = new System.Drawing.Size(2, 2);
            this.radLabelTooltip.TabIndex = 5;
            // 
            // notYetCompletedToolStripMenuItem
            // 
            this.notYetCompletedToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notYetCompletedToolStripMenuItem.ForeColor = System.Drawing.Color.LightCoral;
            this.notYetCompletedToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("notYetCompletedToolStripMenuItem.Image")));
            this.notYetCompletedToolStripMenuItem.Name = "notYetCompletedToolStripMenuItem";
            this.notYetCompletedToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.notYetCompletedToolStripMenuItem.Text = "Not yet completed";
            this.notYetCompletedToolStripMenuItem.ToolTipText = "Wait for download to complete";
            // 
            // FormDownloadStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(603, 365);
            this.Controls.Add(this.radLabelTooltip);
            this.Controls.Add(this.radGridViewMovieDownloading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDownloadStatus";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Download status";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDownloadStatus_FormClosed);
            this.Load += new System.EventHandler(this.FormDownloadStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewMovieDownloading.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewMovieDownloading)).EndInit();
            this.contextMenuStripDownloadStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radLabelTooltip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView radGridViewMovieDownloading;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDownloadStatus;
        private System.Windows.Forms.ToolStripMenuItem unrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem deleteRARToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTipDownloadStatus;
        private Telerik.WinControls.UI.RadLabel radLabelTooltip;
        private System.Windows.Forms.ToolStripMenuItem notYetCompletedToolStripMenuItem;
    }
}