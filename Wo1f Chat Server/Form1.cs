using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wo1f_Framework;
using Wo1f_Framework.Networking;
using System.Net.Sockets;
using System.Data;

namespace Wo1f_Chat_Server
{
    public partial class Form1 : Form
    {
        Wo1fSocketListener Listener;
        public ClientCollection Clients = new ClientCollection();
        Command Cmd = new Command();

        public Form1()
        {
            InitializeComponent();

            CS.ConsoleLog += new CS.ConsoleLogEventHandler(CS_ConsoleLog);  
            CS.EnableLogging = true;

            Listener = new Wo1fSocketListener((int)numericUpDown1.Value);
            Listener.OnAccept += new Wo1fSocketListener.SocketAcceptedEventHandler(Listener_OnAccept);

            //Wo1f_Chat_Client.Form1 F = new Wo1f_Chat_Client.Form1();
            //F.Show();
            
            //Listener.Settings.IsServer = true;
            //Listener.Listen();
            Vars.Add("Servername", "Server");
            Vars.Add("motd", "This is the MOTD.");
            Clients.DB.DBType = Wo1f_Framework.SQL.DatabaseType.MySQL;
        restart:
            int tries = 0;
            if (Clients.DB.Connect("127.0.0.1", "Wo1fChat", "root", "Kiba10"))
            {
                this.TryChat("[System] Connected to SQL Database");
                bool resettables = false;
                if (resettables)
                {
                    Clients.DB.SendCmd("DROP TABLE clients");
                }
                if (!Clients.DB.CheckExists("clients"))
                {
                    this.TryChat("[System] Table not found.  Generating...");
                    //create tables
                    Clients.DB.SendCmd("CREATE TABLE clients (id INT NOT NULL AUTO_INCREMENT, " +
                        "name VARCHAR(32), pass VARCHAR(32), hash VARCHAR(64), info1 VARCHAR(255), info2 VARCHAR(255), primary key(id));");
                    if (Clients.DB.CheckExists("clients"))
                        this.TryChat("[System] Table created.");
                    else
                        this.TryChat("[System] Table not created for some reason.");
                }
                else
                {
                    DataTable DT = Clients.DB.QueryCmd("SELECT COUNT(*) FROM clients");
                    DataRowCollection DR = DT.Rows;
                    object[] ret = DR[0].ItemArray;
                    var i = ret[0];

                    TryChat("Table loaded successfully. Found " + i + " entries.");
                }
                //ready to load info if needed
                
                
            }
            else if(Clients.DB.Connect("127.0.0.1", "", "root", "Kiba10"))
            {
                TryChat("Database does not exist.  Attempting to generate");
                //needs to create DB
                Clients.DB.SendCmd("CREATE DATABASE Wo1fChat");

                if (tries <= 1)
                {
                    tries++;
                    goto restart;
                }
                TryChat("Database generation retry amount exceeded.");
            }
            else
                this.TryChat("[System] Database connection failed.");

            //packets

            Cmd.Register("refresh", new CommandDelegate(OnRefreshRequest));
            Cmd.Register("msg", new CommandDelegate(OnMsg));
            Cmd.Register("reg", new CommandDelegate(OnRegister));
            Cmd.Register("register", new CommandDelegate(OnRegister));
            Cmd.Register("name", new CommandDelegate(OnNameRequest));
            Cmd.Register("login", new CommandDelegate(OnLoginRequest));
        }
        private void OnRefreshRequest(object sender, object packet)
        {
            Packet P = (Packet)packet;
            Wo1fSocket WS = (Wo1fSocket)sender;
            Client C = Clients[WS];

            List<string> Names = new List<string>();
            foreach(Client C2 in Clients.Clients)
                Names.Add(C2.Name);
            C.Send(new Packet("refresh",Names.ToArray()));

        }
        private void OnLoginRequest(object sender, object packet)
        {
            Packet P = (Packet)packet;
            Wo1fSocket WS = (Wo1fSocket)sender;
            Client C = Clients[WS];
            string s = P.Arguments[0];
            string user = s.Split(' ')[0];
            string pass = s.Split(' ')[1];
            Clients.CheckLogin(C, user, pass);
        }
        private void OnNameRequest(object sender, object packet)
        {
            Packet P = (Packet)packet;
            Wo1fSocket WS = (Wo1fSocket)sender;
            Client C = Clients[WS];
            C.Send(new Packet("Name", C.Name));
        }
        private void OnRegister(object sender, object packet)
        {
            Packet P = (Packet)packet;
            Wo1fSocket WS = (Wo1fSocket)sender;
            Client C = Clients[WS];
            string[] split = P.Arguments[0].Split(' ');
            Clients.Register(C, split[0], split[1]);
        }
        private void OnMsg(object sender, object packet)
        {
            Packet P = (Packet)packet;
            Wo1fSocket WS = (Wo1fSocket)sender;
            Client C = Clients[WS];
            string msg = P.Arguments[0];
            if (msg == "") //spam
            {

            }
            else
            {
                Clients.SendToAll(new Packet("Msg", new string[] {C.Name, msg}));
                TryChat(string.Format("[{0}] {1}" , C.Name, msg));
            }
        }
       
        void CS_ConsoleLog(string Text)
        {
            TryChat(Text);
        }

        public delegate void TryChatD(string text);
        void TryChat(string text)
        {
            if (TxtChat.InvokeRequired)
            {
                TxtChat.Invoke(new TryChatD(TryChat),text);
            }
            else
            {
                 TxtChat.Text =  text + CS.NL + TxtChat.Text;
            }
        }

        void Listener_OnAccept(object sender)
        {
            
            Client C = new Client((TcpClient)sender);
            C.OnRecv += new Client.PacketReceivedEventHandler(C_OnRecv);
            Clients.Add(C);
            string n = "Unregistered";
            
            int x = 0;
            string Name = n;
            while (Clients.HasName(Name))
            {
                x++;
                Name = n + x;
            }
            C.Name = Name;           

            TryChat("[Server] Accepted client " + C.Name + "(" + C.IP + ")");
            C.Send(new Packet("msg", new string[] {Vars.GetS("Servername"), "You have connected successfully.\n" + Vars.GetS("motd")}));
            
        }

        void C_OnRecv(object sender, Packet P)
        {
            if (!Cmd.Process(sender, P.Command.ToLower(), P))
            {
                CS.SysLog("Unhandled Cmd: " + P.Command.Trim());
            }

        }

        #region Input Handling
        bool listening = false;
        private void BtnListen_Click(object sender, EventArgs e)
        {
            listening = !listening;
            if (listening)
            {
                BtnListen.Text = "Stop";
                
                Listener.Listen();
                
            }
            else
            {
                BtnListen.Text = "Listen";
                Listener.StopListening();
            }
        }
        #endregion

        private void TmrRefresh_Tick(object sender, EventArgs e)
        {
            if (!listening)
                LblSocket.Text = "Socket is: Waiting";
            else
            {
                if (Listener.Stats.Listening)
                {
                    LblSocket.Text = "Socket is: Listening on port " + Listener.Port;//.Settings.Port;
                    //LblRecv.Text = "Recv: " + 
                }

            }
            LblSent.Text = "Sent: " + Clients.Sent();
            LblRecv.Text = "Recv: " + Clients.Recv();
            LblRPPS.Text = "RPPS: " + Clients.RecvPPS();
            LblSPPS.Text = "SPPS: " + Clients.SentPPS();
            LblDebug.Text = string.Format("WFT: {0} - T: {1}", CS.ThreadCount(), "??");
        }

        private void BtnClient_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Wo1f Chat Client.exe");
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            string s = TxtSend.Text;
            

            int endindex = s.IndexOf(" ");
            if (endindex == -1)
                endindex = s.Length;

            string cmd = s.Substring(0, endindex );
            string args = s.Replace(cmd, "").Trim();
            cmd = cmd.Replace("/", "");
            if (s.StartsWith("/"))
            {
                //Packet P = new Packet(cmd, args);
                
            }
            else
            {
                cmd = "say";
                args = s.Replace(cmd, "").Trim();
            }
            if (cmd == "say")
            {
                Clients.SendToAll(new Packet("Msg", new string[] { "Server", args }));
                TryChat("[Server] " + args);
            }
            else
            {

            }
                
            
            TxtSend.Text = "";
        }

        private void TxtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSend_Click(sender, e);
        }

    }
}
