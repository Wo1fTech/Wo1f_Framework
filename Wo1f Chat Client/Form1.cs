using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wo1f_Framework;
using Wo1f_Framework.Networking;
using Wo1f_Framework.Cryptography;

namespace Wo1f_Chat_Client
{
    public partial class Form1 : Form
    {
        Client C = new Client();
        Command Cmd = new Command();

        public Form1()
        {
            InitializeComponent();
            C.OnRecv += new Client.PacketReceivedEventHandler(C_OnRecv);
            C.OnDisconnect += new Client.OnDisconnectEventHandler(C_OnDisconnect);
            C.OnConnectSuccess += new Client.OnConnectSuccessEventHandler(C_OnConnectSuccess);

            Wo1f_Framework.Wo1f_Framework.Init();
            CS.SysLog("Form1 Initializing");
            Vars.Add("Hostport", 9001);
            CS.ConsoleLog += new CS.ConsoleLogEventHandler(CS_ConsoleLog);
            CS.EnableLogging = true;
            C.Automate = true;
            Cmd.Register("Msg", new CommandDelegate(OnMsg));
            Cmd.Register("Name", new CommandDelegate(OnName));

            C.Encryption = true;
            CS.SysLog("Form1 Initialized");
        }

        void C_OnConnectSuccess(object sender)
        {
            TryChat("[Network] Successfully connected!");
        }
        private void OnName(object sender, object packet)
        {
            Packet P = (Packet)packet;
            C.Name = P.Arguments[0];
        }
        private void OnMsg(object sender, object packet)
        {
            Packet P = (Packet)packet;
            TryChat(string.Format("[{0}] {1}" , P.Arguments[0], P.Arguments[1]));
        }

        void C_OnDisconnect(object sender)
        {
            TryChat("[Network] Client has disconnected from the server.");

            TryChat("[Network] Attempting to reconnect.");
            C.Connect();
        }

        void C_OnRecv(object sender, Packet P)
        {
            Cmd.Process(sender, P.Command, P);
        }

        void CS_ConsoleLog(string Text)
        {
            TryChat(Text);
            
        }

        delegate void ChatD(string txt);
        private void TryChat(string Text)
        {
            TextBox TXT = TB1;

            if (TXT.InvokeRequired)
            {
                TXT.Invoke(new ChatD(TryChat), Text); 
            }else
                TXT.Text = Text + CS.NL +TXT.Text;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

        }

        int txtindex = 0;
        private void TxtSend_KeyDown(object sender, KeyEventArgs e)
        {
            //string Txt = TxtSend.Text;
            bool settext = false;
            if (e.KeyCode == Keys.Enter)
            {
                BtnSend_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtindex++;
                settext = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtindex--;
                settext = true;
            }
            if (settext)
            {
                if (txtindex == -1)
                    txtindex = SentMem.Count - 1;
                if (txtindex == SentMem.Count)
                    txtindex = 0;
                TxtSend.Text = SentMem[txtindex];
            }
        }

        string LastSent = "";
        List<string> SentMem = new List<string>();

        private void BtnSend_Click(object sender, EventArgs e)
        {
            LastSent = TxtSend.Text;
            TxtSend.Text = "";
            SentMem.Add(LastSent);
            
            

            if (LastSent.StartsWith("/"))
            {
                int endindex = LastSent.IndexOf(" ");
                if (endindex == -1)
                    endindex = LastSent.Length;

                string cmd = LastSent.Substring(1,endindex-1);
                cmd = cmd.ToLower();
                string args = LastSent.Replace("/" + cmd, "").Trim();
                if (cmd == "connect")
                {
                    CS.SysLog("Attempting to connect to '" + args + "'.");
                    LblStatus.Text = "Status: Connecting";
                    if (C.Connect(args, Vars.GetI("Hostport"), true))
                    {
                        LblStatus.Text = "Status: Connected";
                        C.Send(new Packet("Name"));
                    }
                }
                else
                    C.Send(new Packet(cmd, args));
                //Cmd.Process(cmd, args);
            }
            else
            {
                LastSent = "/Msg " + LastSent;

                int endindex = LastSent.IndexOf(" ");
                if (endindex == -1)
                    endindex = LastSent.Length;

                string cmd = LastSent.Substring(1, endindex - 1);
                string args = LastSent.Replace("/" + cmd, "").Trim();


                if (C.Connected)
                {
                    C.Send(new Packet(cmd, args));
                    
                    //TryChat(string.Format("[{0}] {1}", username, args));
                }
                else
                {
                    TryChat("You are not connected to any servers.");
                }
            }
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //to make sure all threads close
        }

        public string username = "Not Connected";
        bool first = true;

        private void TmrRefresh_Tick(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;

               // BtnSend_Click(sender, e);
            }
            Wo1fSocketSettingStats St = C.Socket.Stats;
            LblPps.Text = "rps: " + St.rpps + " - sps: " + St.pps;
            LblPing.Text = "Ping: " + St.Ping;
            LblDebug.Text = "Debug: WFT:" + CS.ThreadCount() + " T: " + "??";
            if (C != null)
            {
                tabControl1.TabPages[0].Text = C.Name;
            }
        }
    }
}
