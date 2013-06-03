using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

//Written entirely by Wo1f
namespace Wo1f_Framework.Networking
{
    public class Client
    {
        #region Globals and Init
        public int Id { get; private set; }
        public int Ping{get{return Socket.Stats.Ping;}}
        public int Latency = 0;
        public ulong HwId = 0;
        public string Name = "Unregistered";
        public int CmdTime = 0;
        Wo1fSocket WS;
        public bool Automate = true;
        private Command Cmd = new Command();
        private string host = "";
        private int port = 0;

        public ulong uid { get { return WS.uid; } }

        public Client(Wo1fSocket Socket)
        {
            this.WS = Socket;
            Init();
           
        }
        public Client(System.Net.Sockets.TcpClient tcpClient)
        {
            // TODO: Complete member initialization
            this.WS = new Wo1fSocket(tcpClient);
            Init();
        }

        public Client()
        {
            WS = new Wo1fSocket();
            Init();
        }
        private void Init()
        {
            WS.OnRecv += new Wo1fSocket.PacketReceivedEventHandler(WS_ReceivedEvent);
            WS.OnConnectFail += new Wo1fSocket.OnConnectFailEventHandler(WS_OnConnectFail);
            WS.OnDisconnect += new Wo1fSocket.OnDisconnectEventHandler(WS_OnDisconnect);
            if (Automate)
            {
                Cmd.Register("Ping", new CommandDelegate(OnPing));
                Cmd.Register("Pong", new CommandDelegate(OnPong));

                Thread T = new Thread(PingThread);
                T.IsBackground = true;
                T.Start();
                CS.AddThread(T);
            }
        }
        #endregion

        #region Properties
        public Wo1fSocket Socket
        {
            get { return WS; }
        }
        public string IP
        {
            get
            {
                return WS.IP;
            }
        }
        #endregion

       

        bool pingrecv = true;
        DateTime LastPing = DateTime.Now;
        private void PingThread()
        {
            Vars.Set("WF_PingDelay", 15000);
            while (true)
            {
                if (Automate)
                {
                    if (Connected)
                    {
                        if (!pingrecv) //didn't receive last ping (15 seconds) disconnect
                        {
                            if(this.OnDisconnect != null)
                                this.OnDisconnect(this);
                        }
                        pingrecv = false;
                        LastPing = DateTime.Now;
                        this.Send(new Packet("Ping"));
                    }
                }
                else
                    Thread.Sleep(2000);
                
                Thread.Sleep(Vars.GetI("WF_PingDelay"));
            }
        }

        private void OnPing(object sender, object args)
        {
            this.Send(new Packet("Pong"));
        }

        private void OnPong(object sender, object args)
        {
            int ms = (int)DateTime.Now.Subtract(LastPing).TotalMilliseconds;
            if(ms == 0)
                ms = 1;
            Socket.Stats.Ping = ms;
            //CS.SysLog("Pong(" + Socket.Stats.Ping + ");");
        }


        public bool Connect(string Host, int Port, bool Encrypt)
        {
            WS = new Wo1fSocket();
            WS.OnRecv += new Wo1fSocket.PacketReceivedEventHandler(WS_ReceivedEvent);
            WS.OnConnectFail += new Wo1fSocket.OnConnectFailEventHandler(WS_OnConnectFail);
            WS.OnDisconnect += new Wo1fSocket.OnDisconnectEventHandler(WS_OnDisconnect);
            WS.OnConnectSuccess += new Wo1fSocket.OnConnectSuccessEventHandler(WS_OnConnectSuccess);

            bool ret = WS.Connect(Host, Port);
            WS.Encryption = Encrypt;
            CS.SysLog("Connect(" + Host + ", " + Port + "); returned " + ret.ToString() + ".");
            return ret;
        }

        
        public bool Connect()
        {
            if (this.host == "")
                return false;
            return Connect(this.host, this.port);
        }
        public bool Connect(string Host, int Port)
        {
            return Connect(Host, Port, true); //default to encryption
        }

        #region Events

        void WS_OnConnectSuccess(object sender)
        {
            if (OnConnectSuccess != null)
                OnConnectSuccess(sender);
        }

        void WS_OnDisconnect(object sender)
        {
            if (OnDisconnect != null)
            {
                OnDisconnect(this);
            }
        }

        void Disconnect()
        {
            if (OnDisconnect != null)
            {
                CS.SysLog("OnDisconnect(Name=\"" + this.Name + "\");");
                OnDisconnect(this);
            }
            else
                CS.SysLog("Client \"" + this.Name + "\" wanted to disconnect but the event isn't set.");
        }
        
        public delegate void PacketReceivedEventHandler(object sender, Packet P);
        public delegate void OnDisconnectEventHandler(object sender);
        public delegate void OnConnectFailEventHandler(object sender);
        public delegate void PacketSentEventHandler(object sender, Packet P);
        public delegate void OnConnectSuccessEventHandler(object sender);

        public event OnConnectFailEventHandler OnConnectFail;
        public event OnDisconnectEventHandler OnDisconnect;
        public event PacketReceivedEventHandler OnRecv;
        public event PacketSentEventHandler OnSend;
        public event OnConnectSuccessEventHandler OnConnectSuccess;

        //protected virtual void OnPacketSend(object sender, Packet P)
        //{
        //    if (OnSend != null)
        //        OnSend(sender, P);
        //}
        //protected virtual void OnPacketRecv(object sender, Packet P)
        //{
        //    if (OnRecv != null)
        //        OnRecv(sender, P);
        //}
        
        void WS_OnDisconnect()
        {
            //reconnect

        }

        void WS_OnConnectFail(object sender)
        {
            if (OnConnectFail != null)
                OnConnectFail(sender);
            //reconnect
        }

        void WS_ReceivedEvent(object sender, Packet P)
        {
            if (OnRecv == null)
                return;
           // CS.SysLog("Received Packet " + P.Command + " on thread " + Thread.CurrentThread.ManagedThreadId);
            if (Automate) //if automation is enabled and an internal packet is detected
            {//it will not raise the event on the external program
                if (Cmd.Process(sender, P.Command, P.Arguments))
                    return;
            }

            OnRecv(sender, P);
        }
        #endregion

        public void Send(Packet Packet)
        {
            
            try
            {
                WS.Send(Packet);
            }
            catch(Exception E)
            {
                CS.SysLog("Exception during Sending: " + E.Message);
            }
        }

        public bool Connected 
        {
            get
            {
                if(WS == null)
                    return false;
                return WS.Connected;

            }
        }

        public bool Encryption { get { return WS.Encryption; } set { WS.Encryption = value; } }
    }
}
