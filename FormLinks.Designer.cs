namespace HDManager
{
    partial class FormLinks
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
            this.checkedListBoxLink = new System.Windows.Forms.CheckedListBox();
            this.radCheckBoxHDDLocal = new Telerik.WinControls.UI.RadCheckBox();
            this.radCheckBoxSelectAll = new Telerik.WinControls.UI.RadCheckBox();
            this.radButtonDownload = new Telerik.WinControls.UI.RadButton();
            this.radLabelSelectedCount = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxHDDLocal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxSelectAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelSelectedCount)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBoxLink
            // 
            this.checkedListBoxLink.FormattingEnabled = true;
            this.checkedListBoxLink.HorizontalScrollbar = true;
            this.checkedListBoxLink.Location = new System.Drawing.Point(44, 12);
            this.checkedListBoxLink.Name = "checkedListBoxLink";
            this.checkedListBoxLink.Size = new System.Drawing.Size(447, 229);
            this.checkedListBoxLink.TabIndex = 0;
            this.checkedListBoxLink.MouseMove += new System.Windows.Forms.MouseEventHandler(this.checkedListBoxLink_MouseMove);
            // 
            // radCheckBoxHDDLocal
            // 
            this.radCheckBoxHDDLocal.AutoSize = false;
            this.radCheckBoxHDDLocal.Location = new System.Drawing.Point(262, 247);
            this.radCheckBoxHDDLocal.Name = "radCheckBoxHDDLocal";
            this.radCheckBoxHDDLocal.Size = new System.Drawing.Size(112, 24);
            this.radCheckBoxHDDLocal.TabIndex = 3;
            this.radCheckBoxHDDLocal.Text = "Download to HDD";
            // 
            // radCheckBoxSelectAll
            // 
            this.radCheckBoxSelectAll.AutoSize = false;
            this.radCheckBoxSelectAll.Location = new System.Drawing.Point(190, 247);
            this.radCheckBoxSelectAll.Name = "radCheckBoxSelectAll";
            this.radCheckBoxSelectAll.Size = new System.Drawing.Size(66, 24);
            this.radCheckBoxSelectAll.TabIndex = 4;
            this.radCheckBoxSelectAll.Text = "Select All";
            this.radCheckBoxSelectAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.radCheckBoxSelectAll.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radCheckBoxSelectAll_ToggleStateChanged);
            // 
            // radButtonDownload
            // 
            this.radButtonDownload.Location = new System.Drawing.Point(381, 247);
            this.radButtonDownload.Name = "radButtonDownload";
            this.radButtonDownload.Size = new System.Drawing.Size(110, 24);
            this.radButtonDownload.TabIndex = 5;
            this.radButtonDownload.Text = "Download";
            this.radButtonDownload.Click += new System.EventHandler(this.radButtonDownload_Click);
            // 
            // radLabelSelectedCount
            // 
            this.radLabelSelectedCount.Location = new System.Drawing.Point(44, 247);
            this.radLabelSelectedCount.Name = "radLabelSelectedCount";
            this.radLabelSelectedCount.Size = new System.Drawing.Size(79, 18);
            this.radLabelSelectedCount.TabIndex = 6;
            this.radLabelSelectedCount.Tag = "{0} selected";
            this.radLabelSelectedCount.Text = "selected count";
            // 
            // FormLinks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(537, 277);
            this.Controls.Add(this.radLabelSelectedCount);
            this.Controls.Add(this.radButtonDownload);
            this.Controls.Add(this.radCheckBoxSelectAll);
            this.Controls.Add(this.radCheckBoxHDDLocal);
            this.Controls.Add(this.checkedListBoxLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLinks";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select links to download";
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxHDDLocal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxSelectAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelSelectedCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxLink;
        private Telerik.WinControls.UI.RadCheckBox radCheckBoxHDDLocal;
        private Telerik.WinControls.UI.RadCheckBox radCheckBoxSelectAll;
        private Telerik.WinControls.UI.RadButton radButtonDownload;
        private Telerik.WinControls.UI.RadLabel radLabelSelectedCount;
    }
}