namespace Wo1f_Chat_Client
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TB1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TxtConsole = new System.Windows.Forms.TextBox();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.ListUsers = new System.Windows.Forms.ListBox();
            this.TxtSend = new System.Windows.Forms.TextBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblPing = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblPps = new System.Windows.Forms.ToolStripStatusLabel();
            this.TmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.LblDebug = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(418, 311);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TB1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(410, 285);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Not logged in";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TB1
            // 
            this.TB1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TB1.Location = new System.Drawing.Point(0, 0);
            this.TB1.Multiline = true;
            this.TB1.Name = "TB1";
            this.TB1.Size = new System.Drawing.Size(407, 285);
            this.TB1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TxtConsole);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(410, 285);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Console";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TxtConsole
            // 
            this.TxtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtConsole.Location = new System.Drawing.Point(3, 3);
            this.TxtConsole.Multiline = true;
            this.TxtConsole.Name = "TxtConsole";
            this.TxtConsole.Size = new System.Drawing.Size(404, 279);
            this.TxtConsole.TabIndex = 0;
            // 
            // SplitContainer
            // 
            this.SplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainer.Location = new System.Drawing.Point(0, 3);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.tabControl1);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.ListUsers);
            this.SplitContainer.Size = new System.Drawing.Size(602, 311);
            this.SplitContainer.SplitterDistance = 418;
            this.SplitContainer.TabIndex = 2;
            // 
            // ListUsers
            // 
            this.ListUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListUsers.FormattingEnabled = true;
            this.ListUsers.Location = new System.Drawing.Point(3, 3);
            this.ListUsers.Name = "ListUsers";
            this.ListUsers.Size = new System.Drawing.Size(174, 303);
            this.ListUsers.TabIndex = 0;
            // 
            // TxtSend
            // 
            this.TxtSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSend.AutoCompleteCustomSource.AddRange(new string[] {
            "/help",
            "/w",
            "/whisper",
            "/r",
            "/respond",
            "/reconnect",
            "/connect",
            "/poke"});
            this.TxtSend.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TxtSend.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TxtSend.Location = new System.Drawing.Point(4, 320);
            this.TxtSend.Name = "TxtSend";
            this.TxtSend.Size = new System.Drawing.Size(514, 20);
            this.TxtSend.TabIndex = 3;
            this.TxtSend.Text = "/connect 127.0.0.1";
            this.TxtSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSend_KeyDown);
            // 
            // BtnSend
            // 
            this.BtnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSend.Location = new System.Drawing.Point(524, 319);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(75, 21);
            this.BtnSend.TabIndex = 4;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblStatus,
            this.LblPing,
            this.LblPps,
            this.LblDebug});
            this.statusStrip1.Location = new System.Drawing.Point(0, 343);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(602, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LblStatus
            // 
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(99, 17);
            this.LblStatus.Text = "Status: Initializing";
            // 
            // LblPing
            // 
            this.LblPing.Name = "LblPing";
            this.LblPing.Size = new System.Drawing.Size(35, 17);
            this.LblPing.Text = "ms: 0";
            // 
            // LblPps
            // 
            this.LblPps.Name = "LblPps";
            this.LblPps.Size = new System.Drawing.Size(75, 17);
            this.LblPps.Text = "rps: 0 - sps: 0";
            // 
            // TmrRefresh
            // 
            this.TmrRefresh.Enabled = true;
            this.TmrRefresh.Tick += new System.EventHandler(this.TmrRefresh_Tick);
            // 
            // LblDebug
            // 
            this.LblDebug.Name = "LblDebug";
            this.LblDebug.Size = new System.Drawing.Size(118, 17);
            this.LblDebug.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 365);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.TxtSend);
            this.Controls.Add(this.SplitContainer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.ListBox ListUsers;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox TB1;
        private System.Windows.Forms.TextBox TxtSend;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LblStatus;
        private System.Windows.Forms.ToolStripStatusLabel LblPing;
        private System.Windows.Forms.ToolStripStatusLabel LblPps;
        private System.Windows.Forms.Timer TmrRefresh;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox TxtConsole;
        private System.Windows.Forms.ToolStripStatusLabel LblDebug;
    }
}

