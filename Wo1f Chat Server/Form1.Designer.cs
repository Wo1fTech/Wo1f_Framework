namespace Wo1f_Chat_Server
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
            this.TxtChat = new System.Windows.Forms.TextBox();
            this.BtnListen = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.TmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.LblSocket = new System.Windows.Forms.Label();
            this.LblSent = new System.Windows.Forms.Label();
            this.LblRecv = new System.Windows.Forms.Label();
            this.LblErr = new System.Windows.Forms.Label();
            this.BtnClient = new System.Windows.Forms.Button();
            this.BtnSend = new System.Windows.Forms.Button();
            this.TxtSend = new System.Windows.Forms.TextBox();
            this.LblSPPS = new System.Windows.Forms.Label();
            this.LblRPPS = new System.Windows.Forms.Label();
            this.LblDebug = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtChat
            // 
            this.TxtChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtChat.Location = new System.Drawing.Point(12, 93);
            this.TxtChat.Multiline = true;
            this.TxtChat.Name = "TxtChat";
            this.TxtChat.Size = new System.Drawing.Size(510, 214);
            this.TxtChat.TabIndex = 0;
            // 
            // BtnListen
            // 
            this.BtnListen.Location = new System.Drawing.Point(12, 12);
            this.BtnListen.Name = "BtnListen";
            this.BtnListen.Size = new System.Drawing.Size(75, 23);
            this.BtnListen.TabIndex = 1;
            this.BtnListen.Text = "Listen";
            this.BtnListen.UseVisualStyleBackColor = true;
            this.BtnListen.Click += new System.EventHandler(this.BtnListen_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(109, 15);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            65353,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            118,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            9001,
            0,
            0,
            0});
            // 
            // TmrRefresh
            // 
            this.TmrRefresh.Enabled = true;
            this.TmrRefresh.Tick += new System.EventHandler(this.TmrRefresh_Tick);
            // 
            // LblSocket
            // 
            this.LblSocket.AutoSize = true;
            this.LblSocket.Location = new System.Drawing.Point(9, 38);
            this.LblSocket.Name = "LblSocket";
            this.LblSocket.Size = new System.Drawing.Size(93, 13);
            this.LblSocket.TabIndex = 3;
            this.LblSocket.Text = "Socket is: Waiting";
            // 
            // LblSent
            // 
            this.LblSent.AutoSize = true;
            this.LblSent.Location = new System.Drawing.Point(9, 51);
            this.LblSent.Name = "LblSent";
            this.LblSent.Size = new System.Drawing.Size(41, 13);
            this.LblSent.TabIndex = 4;
            this.LblSent.Text = "Sent: 0";
            // 
            // LblRecv
            // 
            this.LblRecv.AutoSize = true;
            this.LblRecv.Location = new System.Drawing.Point(9, 64);
            this.LblRecv.Name = "LblRecv";
            this.LblRecv.Size = new System.Drawing.Size(45, 13);
            this.LblRecv.TabIndex = 5;
            this.LblRecv.Text = "Recv: 0";
            // 
            // LblErr
            // 
            this.LblErr.AutoSize = true;
            this.LblErr.Location = new System.Drawing.Point(9, 77);
            this.LblErr.Name = "LblErr";
            this.LblErr.Size = new System.Drawing.Size(32, 13);
            this.LblErr.TabIndex = 6;
            this.LblErr.Text = "Err: 0";
            // 
            // BtnClient
            // 
            this.BtnClient.Location = new System.Drawing.Point(197, 15);
            this.BtnClient.Name = "BtnClient";
            this.BtnClient.Size = new System.Drawing.Size(75, 23);
            this.BtnClient.TabIndex = 7;
            this.BtnClient.Text = "Start Client";
            this.BtnClient.UseVisualStyleBackColor = true;
            this.BtnClient.Click += new System.EventHandler(this.BtnClient_Click);
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(447, 310);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(75, 23);
            this.BtnSend.TabIndex = 8;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // TxtSend
            // 
            this.TxtSend.Location = new System.Drawing.Point(12, 313);
            this.TxtSend.Name = "TxtSend";
            this.TxtSend.Size = new System.Drawing.Size(429, 20);
            this.TxtSend.TabIndex = 9;
            this.TxtSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSend_KeyDown);
            // 
            // LblSPPS
            // 
            this.LblSPPS.AutoSize = true;
            this.LblSPPS.Location = new System.Drawing.Point(139, 51);
            this.LblSPPS.Name = "LblSPPS";
            this.LblSPPS.Size = new System.Drawing.Size(47, 13);
            this.LblSPPS.TabIndex = 10;
            this.LblSPPS.Text = "SPPS: 0";
            // 
            // LblRPPS
            // 
            this.LblRPPS.AutoSize = true;
            this.LblRPPS.Location = new System.Drawing.Point(139, 64);
            this.LblRPPS.Name = "LblRPPS";
            this.LblRPPS.Size = new System.Drawing.Size(48, 13);
            this.LblRPPS.TabIndex = 11;
            this.LblRPPS.Text = "RPPS: 0";
            // 
            // LblDebug
            // 
            this.LblDebug.AutoSize = true;
            this.LblDebug.Location = new System.Drawing.Point(139, 77);
            this.LblDebug.Name = "LblDebug";
            this.LblDebug.Size = new System.Drawing.Size(36, 13);
            this.LblDebug.TabIndex = 12;
            this.LblDebug.Text = "DBG: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 345);
            this.Controls.Add(this.LblDebug);
            this.Controls.Add(this.LblRPPS);
            this.Controls.Add(this.LblSPPS);
            this.Controls.Add(this.TxtSend);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.BtnClient);
            this.Controls.Add(this.LblErr);
            this.Controls.Add(this.LblRecv);
            this.Controls.Add(this.LblSent);
            this.Controls.Add(this.LblSocket);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.BtnListen);
            this.Controls.Add(this.TxtChat);
            this.Name = "Form1";
            this.Text = "Wo1f Chat Server";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtChat;
        private System.Windows.Forms.Button BtnListen;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Timer TmrRefresh;
        private System.Windows.Forms.Label LblSocket;
        private System.Windows.Forms.Label LblSent;
        private System.Windows.Forms.Label LblRecv;
        private System.Windows.Forms.Label LblErr;
        private System.Windows.Forms.Button BtnClient;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.TextBox TxtSend;
        private System.Windows.Forms.Label LblSPPS;
        private System.Windows.Forms.Label LblRPPS;
        private System.Windows.Forms.Label LblDebug;
    }
}

