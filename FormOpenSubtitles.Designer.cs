namespace HDManager
{
    partial class FormOpenSubtitles
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            this.radGridViewSubtitles = new Telerik.WinControls.UI.RadGridView();
            this.radDropDownListLanguage = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabelSubtitlesCount = new Telerik.WinControls.UI.RadLabel();
            this.toolTipSubtitles = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewSubtitles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewSubtitles.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelSubtitlesCount)).BeginInit();
            this.SuspendLayout();
            // 
            // radGridViewSubtitles
            // 
            this.radGridViewSubtitles.AutoSizeRows = true;
            this.radGridViewSubtitles.Location = new System.Drawing.Point(12, 40);
            // 
            // radGridViewSubtitles
            // 
            this.radGridViewSubtitles.MasterTemplate.AllowAddNewRow = false;
            this.radGridViewSubtitles.MasterTemplate.AllowColumnChooser = false;
            this.radGridViewSubtitles.MasterTemplate.AllowColumnReorder = false;
            this.radGridViewSubtitles.MasterTemplate.AllowDeleteRow = false;
            this.radGridViewSubtitles.MasterTemplate.AllowEditRow = false;
            this.radGridViewSubtitles.MasterTemplate.AllowRowResize = false;
            this.radGridViewSubtitles.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.radGridViewSubtitles.MasterTemplate.ShowRowHeaderColumn = false;
            this.radGridViewSubtitles.Name = "radGridViewSubtitles";
            this.radGridViewSubtitles.Size = new System.Drawing.Size(847, 384);
            this.radGridViewSubtitles.TabIndex = 0;
            this.radGridViewSubtitles.Text = "radGridView1";
            this.radGridViewSubtitles.DoubleClick += new System.EventHandler(this.radGridViewSubtitles_DoubleClick);
            // 
            // radDropDownListLanguage
            // 
            this.radDropDownListLanguage.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            radListDataItem3.Text = "eng";
            radListDataItem3.TextWrap = true;
            radListDataItem4.Text = "fre";
            radListDataItem4.TextWrap = true;
            this.radDropDownListLanguage.Items.Add(radListDataItem3);
            this.radDropDownListLanguage.Items.Add(radListDataItem4);
            this.radDropDownListLanguage.Location = new System.Drawing.Point(82, 14);
            this.radDropDownListLanguage.Name = "radDropDownListLanguage";
            this.radDropDownListLanguage.Size = new System.Drawing.Size(48, 20);
            this.radDropDownListLanguage.TabIndex = 1;
            this.radDropDownListLanguage.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownListLanguage_SelectedIndexChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(12, 14);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(64, 18);
            this.radLabel1.TabIndex = 2;
            this.radLabel1.Text = "Language : ";
            // 
            // radLabelSubtitlesCount
            // 
            this.radLabelSubtitlesCount.Location = new System.Drawing.Point(757, 14);
            this.radLabelSubtitlesCount.Name = "radLabelSubtitlesCount";
            this.radLabelSubtitlesCount.Size = new System.Drawing.Size(102, 18);
            this.radLabelSubtitlesCount.TabIndex = 3;
            this.radLabelSubtitlesCount.Tag = "Subtitles count : {0}";
            this.radLabelSubtitlesCount.Text = "Subtitles count : {0}";
            // 
            // toolTipSubtitles
            // 
            this.toolTipSubtitles.AutomaticDelay = 1000;
            this.toolTipSubtitles.IsBalloon = true;
            this.toolTipSubtitles.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // FormOpenSubtitles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(871, 436);
            this.Controls.Add(this.radLabelSubtitlesCount);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radDropDownListLanguage);
            this.Controls.Add(this.radGridViewSubtitles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOpenSubtitles";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "Search subtitles for movie : {0}";
            this.Text = "Search subtitles for movie : {0}";
            this.Load += new System.EventHandler(this.FormOpenSubtitles_Load);
            this.Shown += new System.EventHandler(this.FormOpenSubtitles_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewSubtitles.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewSubtitles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelSubtitlesCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView radGridViewSubtitles;
        private Telerik.WinControls.UI.RadDropDownList radDropDownListLanguage;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabelSubtitlesCount;
        private System.Windows.Forms.ToolTip toolTipSubtitles;



    }
}