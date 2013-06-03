using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Wo1f_Framework.Networking
{
    public class Wo1fSocketListener
    {
        TcpListener TCP;
        public bool Listening
        {
            get
            {
                return Stats.Listening;
            }
        }
        public Wo1fSocketSetting Settings;
        public Wo1fSocketSettingStats Stats { get; private set; }
        public int Port { get; private set; }

        public Wo1fSocketListener(int Port)
        {
            Settings = new Wo1fSocketSetting();
            Stats = new Wo1fSocketSettingStats();
            this.Port = Port;
        }

        private void BeginListen()
        {
            Stats.Listening = true;
            TCP = new TcpListener(IPAddress.Any, Port);

            TCP.Start();
            TCP.BeginAcceptTcpClient(new AsyncCallback(BeginAccept), TCP);
            CS.SysLog(string.Format("Listening on {0}:{1}", IPAddress.Any, Port));

        }

        public void Listen()
        {
            //CS.SysLog("Listen();");

            if (!Stats.Listening)
            {
                //TickTimer.Stop();
                BeginListen();
            }
            else
                CS.SysLog("Socket already listening for connections.");
            //throw new Exception("Socket already listening for connections.");
        }
        public void StopListening()
        {
            CS.SysLog("StopListening();");
            Stats.Listening = false;
            TCP.Stop();
        }

        public delegate void SocketAcceptedEventHandler(object sender);
         public event SocketAcceptedEventHandler OnAccept;

        protected virtual void OnSocketAccepted(object sender)
        {
            CS.SysLog("OnSocketAccepted");
            if (OnAccept != null)
                OnAccept(sender);
        }

        private void BeginAccept(IAsyncResult Res)
        {
            CS.SysLog("BeginAccept();");

            TcpClient S = TCP.EndAcceptTcpClient(Res);
            OnSocketAccepted(S);
            if (Stats.Listening)
                TCP.BeginAcceptSocket(new AsyncCallback(BeginAccept), TCP);
        }
        

    }
}
