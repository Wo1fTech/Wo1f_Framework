namespace Wo1fFileSearch
{
    partial class Settings
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
            this.NumMaxSize = new System.Windows.Forms.NumericUpDown();
            this.ChkDirs = new System.Windows.Forms.CheckBox();
            this.ChkContents = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkAsync = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ChkCaps = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxSize)).BeginInit();
            this.SuspendLayout();
            // 
            // NumMaxSize
            // 
            this.NumMaxSize.Location = new System.Drawing.Point(12, 80);
            this.NumMaxSize.Name = "NumMaxSize";
            this.NumMaxSize.Size = new System.Drawing.Size(62, 20);
            this.NumMaxSize.TabIndex = 6;
            this.NumMaxSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // ChkDirs
            // 
            this.ChkDirs.AutoSize = true;
            this.ChkDirs.Location = new System.Drawing.Point(80, 12);
            this.ChkDirs.Name = "ChkDirs";
            this.ChkDirs.Size = new System.Drawing.Size(128, 17);
            this.ChkDirs.TabIndex = 7;
            this.ChkDirs.Text = "Only show Directories";
            this.ChkDirs.UseVisualStyleBackColor = true;
            this.ChkDirs.CheckedChanged += new System.EventHandler(this.Settings_ChkChange);
            // 
            // ChkContents
            // 
            this.ChkContents.AutoSize = true;
            this.ChkContents.Location = new System.Drawing.Point(80, 58);
            this.ChkContents.Name = "ChkContents";
            this.ChkContents.Size = new System.Drawing.Size(104, 17);
            this.ChkContents.TabIndex = 8;
            this.ChkContents.Text = "Search contents";
            this.ChkContents.UseVisualStyleBackColor = true;
            this.ChkContents.CheckedChanged += new System.EventHandler(this.Settings_ChkChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Size limit for searching";
            // 
            // ChkAsync
            // 
            this.ChkAsync.AutoSize = true;
            this.ChkAsync.Checked = true;
            this.ChkAsync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkAsync.Location = new System.Drawing.Point(80, 35);
            this.ChkAsync.Name = "ChkAsync";
            this.ChkAsync.Size = new System.Drawing.Size(87, 17);
            this.ChkAsync.TabIndex = 11;
            this.ChkAsync.Text = "Asyncronous";
            this.ChkAsync.UseVisualStyleBackColor = true;
            this.ChkAsync.CheckedChanged += new System.EventHandler(this.ChkAsync_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(64, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Location = new System.Drawing.Point(145, 197);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ChkCaps
            // 
            this.ChkCaps.AutoSize = true;
            this.ChkCaps.Location = new System.Drawing.Point(78, 108);
            this.ChkCaps.Name = "ChkCaps";
            this.ChkCaps.Size = new System.Drawing.Size(100, 17);
            this.ChkCaps.TabIndex = 14;
            this.ChkCaps.Text = "Caps Sensetive";
            this.ChkCaps.UseVisualStyleBackColor = true;
            this.ChkCaps.CheckedChanged += new System.EventHandler(this.Settings_ChkChange);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 232);
            this.Controls.Add(this.ChkCaps);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ChkAsync);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChkContents);
            this.Controls.Add(this.ChkDirs);
            this.Controls.Add(this.NumMaxSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown NumMaxSize;
        private System.Windows.Forms.CheckBox ChkDirs;
        private System.Windows.Forms.CheckBox ChkContents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkAsync;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox ChkCaps;
    }
}