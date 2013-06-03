namespace Wo1fFileSearch
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LV = new System.Windows.Forms.ListView();
            this.ColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColExtension = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BtnSearch = new System.Windows.Forms.Button();
            this.TxtUrl = new System.Windows.Forms.TextBox();
            this.BtnSettings = new System.Windows.Forms.Button();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.PB = new System.Windows.Forms.ProgressBar();
            this.TmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.LblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LV
            // 
            this.LV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColName,
            this.ColExtension,
            this.ColSize,
            this.ColLocation});
            this.LV.FullRowSelect = true;
            this.LV.Location = new System.Drawing.Point(12, 70);
            this.LV.Name = "LV";
            this.LV.Size = new System.Drawing.Size(666, 254);
            this.LV.TabIndex = 0;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.View = System.Windows.Forms.View.Details;
            this.LV.DoubleClick += new System.EventHandler(this.LV_DoubleClick);
            // 
            // ColName
            // 
            this.ColName.Text = "Name";
            this.ColName.Width = 130;
            // 
            // ColExtension
            // 
            this.ColExtension.DisplayIndex = 2;
            this.ColExtension.Text = "Extension";
            // 
            // ColSize
            // 
            this.ColSize.DisplayIndex = 1;
            this.ColSize.Text = "Size";
            // 
            // ColLocation
            // 
            this.ColLocation.Text = "Full Location";
            this.ColLocation.Width = 408;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnSearch.Location = new System.Drawing.Point(603, 12);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(75, 23);
            this.BtnSearch.TabIndex = 1;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // TxtUrl
            // 
            this.TxtUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TxtUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.TxtUrl.Location = new System.Drawing.Point(12, 12);
            this.TxtUrl.Name = "TxtUrl";
            this.TxtUrl.Size = new System.Drawing.Size(462, 20);
            this.TxtUrl.TabIndex = 2;
            // 
            // BtnSettings
            // 
            this.BtnSettings.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnSettings.BackgroundImage")));
            this.BtnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSettings.FlatAppearance.BorderSize = 0;
            this.BtnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.BtnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.BtnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnSettings.Location = new System.Drawing.Point(530, -2);
            this.BtnSettings.Margin = new System.Windows.Forms.Padding(0);
            this.BtnSettings.Name = "BtnSettings";
            this.BtnSettings.Size = new System.Drawing.Size(64, 64);
            this.BtnSettings.TabIndex = 5;
            this.BtnSettings.UseVisualStyleBackColor = true;
            this.BtnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // TxtSearch
            // 
            this.TxtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TxtSearch.Location = new System.Drawing.Point(12, 38);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(462, 20);
            this.TxtSearch.TabIndex = 6;
            this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // PB
            // 
            this.PB.Location = new System.Drawing.Point(12, 358);
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(666, 12);
            this.PB.TabIndex = 7;
            // 
            // TmrRefresh
            // 
            this.TmrRefresh.Enabled = true;
            this.TmrRefresh.Interval = 1;
            this.TmrRefresh.Tick += new System.EventHandler(this.TmrRefresh_Tick);
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.LblStatus.Location = new System.Drawing.Point(12, 327);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(79, 13);
            this.LblStatus.TabIndex = 8;
            this.LblStatus.Text = "Status: Waiting";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 373);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.PB);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.BtnSettings);
            this.Controls.Add(this.TxtUrl);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.LV);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LV;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.TextBox TxtUrl;
        private System.Windows.Forms.Button BtnSettings;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.ProgressBar PB;
        private System.Windows.Forms.Timer TmrRefresh;
        private System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.ColumnHeader ColName;
        private System.Windows.Forms.ColumnHeader ColSize;
        private System.Windows.Forms.ColumnHeader ColExtension;
        private System.Windows.Forms.ColumnHeader ColLocation;
    }
}

