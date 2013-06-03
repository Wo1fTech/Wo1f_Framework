using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Wo1f_Framework.Common;
using System.Threading;

using Timer = Wo1f_Framework.Common.Timer;
//Written entirely by Wo1f
namespace Wo1f_Framework.Networking
{
    public class Wo1fSocket
    {
        #region Globals
        
        UdpClient Udp;
        TcpClient Tcp;
        Thread TickThread;

        public Wo1fSocketSettingStats Stats = new Wo1fSocketSettingStats();
        public Wo1fSocketSetting Settings = new Wo1fSocketSetting();

        string _IP;
        int _Port;

        public ulong uid { get;  private set; }
        DateTime DCTimer = DateTime.Now;

        
        #endregion

        #region Constructors
        public Wo1fSocket()
        {
            Tcp = new TcpClient();
            Settings.Protocol = Wo1fSocketProtocol.TCP;
            Settings.ThreadMode = Wo1fSocketThreadMode.Async;
            Tcp.Client.NoDelay = true;
            Tcp.Client.ReceiveBufferSize = Settings.BufferSize;
            Tcp.Client.SendBufferSize = Settings.BufferSize;
            
            Init();
            
        }
        public Wo1fSocket(UdpClient Udp)
        {
            //this.Udp = Udp;
           // Settings.Protocol = Wo1fSocketProtocol.UDP;
            //Init();
            DoThreadSettings();
        }
        public Wo1fSocket(TcpClient Tcp)
        {
            this.Tcp = Tcp;
            Settings.Protocol = Wo1fSocketProtocol.TCP;

            DoThreadSettings();
            
            //Init();
        }
        /*public Wo1fSocket(int Port) //server
        {
            Settings.IsServer = true;
            _Port = Port;
            Init();
        }*/

        public Wo1fSocket(string HostName, int Port)
        {
            _IP = HostName;
            _Port = Port;
            Init();
        }

        private void Init()
        {
            CS.SysLog("Wo1fSocket initializing.");
            Settings.Port = _Port;
            Settings.IP = _IP;
            DoThreadSettings();
            uid = CS.GetUid();
        }
        #endregion

        #region Threading

        private void DoThreadSettings()
        {
            CS.SysLog("WS::DoThreadSettings(" + Settings.ThreadMode + ");");
            switch (Settings.ThreadMode)
            {
                case Wo1fSocketThreadMode.Async:
                    CS.SysLog("Enabling Async socket mode");
                    BeginReceive();
                    break;
                case Wo1fSocketThreadMode.SingleThreaded:
                    /*
                    if (TickThread != null)
                        TickThread.Abort();
                    TickTimer.Interval = netrate;
                    TickTimer.OnTick += new Timer.OnTickEventHandler(TickTimer_OnTick);*/
                    break;
                case Wo1fSocketThreadMode.MultiThreadedEach:
                    if (!Connected)
                    {
                        CS.SysLog("Socket not connected - threads not prepared.");
                        return;
                    }
                    CS.SysLog("Socket thread prepared");
                    TickThread = new Thread(new ThreadStart(TickT));
                    TickThread.IsBackground = true;
                    TickThread.Start();//sending
                    CS.AddThread(TickThread);
                    BeginReceive();
                    //CS.AddThread(TickThread);
                    break;
                case Wo1fSocketThreadMode.MultiThreadCount:
                    if (TickThread != null)
                        TickThread.Abort();
                    break;
            }
        }

        byte[] RecvBuffer = new byte[9086];
        private void BeginReceive()
        {
            
            if (Connected)
            {
               // CS.SysLog("BeginRecv();");
                RecvBuffer = new byte[Settings.BufferSize];
                Tcp.Client.BeginReceive(RecvBuffer, 0, Settings.BufferSize, SocketFlags.None, new AsyncCallback(BeginRecvCallback), null);
            }
            else
            {
                CS.SysLog("Can't begin receiving before connecting.");
                if(TickThread != null)
                    TickThread.Abort();
            }
        }

        private void BeginRecvCallback(IAsyncResult Res)
        {
            //CS.SysLog("BeginRecvCallback();");
            if (Res.IsCompleted)
            {
               
               // CS.SysLog("Completed Callback");
                try
                {
                    Tcp.Client.EndReceive(Res);
                    Packet P = new Packet(RecvBuffer);

                    if (OnRecv != null)
                        OnRecv(this, P);
                    Stats.LastPacketRecv = DateTime.Now;
                    Stats.packetsrecv++;
                    if (P.ExtraPacket)
                        Stats.packetsrecv += P.Extras.Count;
                    BeginReceive();
                }
                catch
                {
                    OnDC(this);
                }

            }
        }

        private void TickT()
        {
            while (!CS.AppClosed)
            {
                HandleSends();
                Thread.Sleep(Settings.Netrate);
            }
        }
        #endregion




        #region Properties

        #endregion

        #region Events and Delegates

        
        public delegate void PacketReceivedEventHandler(object sender, Packet P);
        public delegate void OnDisconnectEventHandler(object sender);
        public delegate void OnConnectFailEventHandler(object sender);
        public delegate void OnConnectSuccessEventHandler(object sender);
        public delegate void PacketSentEventHandler(object sender, Packet P);

        public event OnConnectFailEventHandler OnConnectFail;
        public event OnDisconnectEventHandler OnDisconnect;
        public event PacketReceivedEventHandler OnRecv;
        public event PacketSentEventHandler OnSend;
        public event OnConnectSuccessEventHandler OnConnectSuccess;

        protected virtual void OnConnect()
        {
            if (OnConnectSuccess != null)
                OnConnectSuccess(this);
        }

        protected virtual void OnFailConnect()
        {
            if (OnConnectFail != null)
                OnConnectFail(this);
        }

        protected virtual void OnDC(object sender)
        {
            Tcp.Close();
            if (OnDisconnect != null)
                OnDisconnect(sender);
        }

        public void Disconnect()
        {
            OnDC(this);
        }

        protected virtual void OnPacketSend(object sender, Packet P)
        {
            if (OnSend != null)
                OnSend(sender, P);
        }

        #endregion

        #region Methods

        #region Socket related

        private void BeginSendCallback(IAsyncResult Res)
        {
            if(Res.IsCompleted)
            {
                //CS.SysLog("Packet Sent Asyncronously");
                Tcp.Client.EndSend(Res);
                //asyncbusy = false;
                //AsyncSend(null);
            }
        }

        List<Packet> SQueue = new List<Packet>();
        public void Send(Packet P)
        {
            if(Encryption)
                P.Encrypt();

            switch (Settings.ThreadMode)
            {
                case Wo1fSocketThreadMode.Async:
                    AsyncSend(P);
                    break;
                case Wo1fSocketThreadMode.SingleThreaded:
                    Send(P.ToBytes());
                    break;
                case Wo1fSocketThreadMode.MultiThreadedEach: //internal tick
                    SQueue.Add(P);
                    break;
                case Wo1fSocketThreadMode.MultiThreadCount: //wait for external tick
                    SQueue.Add(P);
                    break;
            }

        }

        bool asyncbusy = false;
        private void AsyncSend(Packet P)
        {
            if (asyncbusy)
            {
                SQueue.Add(P);
                return;
            }
            
            if (SQueue.Count > 0)
            {
                int x = 0;
                P.ExtraPacket = true;
                while (x < 10) //x packets at a time or until full(todo: add code to check if full)
                {
                    if (SQueue.Count == 0)
                        break;
                    P.Extras.Add(SQueue[0]);
                    SQueue.RemoveAt(0);
                    x++;
                }
                CS.SysLog("Send queue backed up.  Sending " + P.Extras.Count + " extra packets.");
            }
            asyncbusy = true;
            byte[] info = P.ToBytes();
            try
            {
                Tcp.Client.BeginSend(info, 0, info.Length, SocketFlags.None, new AsyncCallback(BeginSendCallback), this);
            }
            catch(Exception E)
            {
                //OnSendFail
                
                CS.SysLog("Packet sending failed");
            }
            asyncbusy = false;
            this.Stats.packetssent++;
            if (P.ExtraPacket)
                this.Stats.packetssent += P.Extras.Count;
            
        }

        private void Send(byte[] Bytes) //final send
        {
            
            Stats.packetssent++;

            Packet P = new Packet(Bytes);
            try
            {
                if (Settings.Protocol == Wo1fSocketProtocol.UDP)
                {
                    //Udp.Connect(_IP,_Port);
                    Udp.Client.Send(Bytes);

                }
                else if (Settings.Protocol == Wo1fSocketProtocol.TCP)
                {
                    Tcp.Client.Send(Bytes);
                    // CS.SysLog("Sending packet " + P.Command);

                }
                if (CS.PLogger != null)
                    CS.PLogger.Send(P);
                OnPacketSend(this, P);
            }
            catch(Exception E)
            {
                Stats.LostPackets++;
                if (Stats.LostPackets >= 10)
                {
                    OnDC(this);
                }
            }
            

        }

        #endregion
        
        private void HandleSends()
        {
            if (SQueue.Count == 0)
                return;
            if (Tcp == null)
                return;
            if (!Tcp.Client.Connected)
                return;
            if (SQueue.Count <= 3)
            {
                Packet P = SQueue[0];
                SQueue.RemoveAt(0);
                if(P!=null)
                    Send(P.ToBytes());
            }
            else //backlogged packets, so send faster
            {
                CS.SysLog("Backlogged packets.  Sending in bulk");
                int count = 10;
                Packet P = SQueue[0];
                Packet P2 = SQueue[0];
                SQueue.RemoveAt(0);
                if (P == null)
                    return;
                P.ExtraPacket = true;
                for (int x = 1; x < count; x++)
                {
                    if (SQueue.Count == 0)
                        break;
                    P.Extras.Add(SQueue[0]);
                    SQueue.RemoveAt(0);
                }

                Send(P.ToBytes());

                if(P.ExtraPacket)
                    Stats.packetssent += P.Extras.Count;
               
            }
        }

        #endregion

        public bool Connect(string Host, int Port)
        {
            if (Settings.Protocol == Wo1fSocketProtocol.TCP)
            {
                try
                {
                    Tcp = new TcpClient();//Host, Port);
                    Tcp.BeginConnect(Host, Port, new AsyncCallback(BeginConnect), Tcp);
                    _IP = Host;
                    _Port = Port;
                }
                catch
                {
                    return false;
                }

                return true;
            }
            else
            {
                return true;
                //udp
            }
            return false;
        }

        Timer TickTimer = new Timer();

        public bool Encryption = false;
        private void BeginConnect(IAsyncResult Res)
        {
            if (Settings.Protocol == Wo1fSocketProtocol.TCP)
            {
                try
                {
                    Tcp.EndConnect(Res);

                    DoThreadSettings();
                    CS.SysLog("Connected!");
                    OnConnect();
                    switch (Settings.ThreadMode)
                    {
                        case Wo1fSocketThreadMode.MultiThreadedEach:
                            TickTimer.Start();
                            //TickTimer.Interval = 5;
                            break;
                        case Wo1fSocketThreadMode.SingleThreaded:

                            break;
                    }

                }
                catch(Exception E)
                {
                    OnFailConnect();
                    CS.SysLog("Connection failed.  Exception: " +E.Message ); 
                }

            }
            else
            {
                //udp
            }
        }

        public string IP 
        {
            get
            {
                return Tcp.Client.RemoteEndPoint.ToString().Split(':')[0];
            }
        }



        public bool Connected
        {
            get { return Tcp.Connected; }
        }
    }
}
