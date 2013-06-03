using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wo1f_Framework.Networking;
using System.Windows.Forms;
using System.Threading;

namespace Wo1f_Chat_Client_Console
{
    class Program
    {
        static Wo1fSocket WS;
        static Command Cmd;
        
        static void Main(string[] args)
        {
            WS = new Wo1fSocket();
            Cmd = new Command();
            //WS.Connect();
            Cmd.Register("connect", new CommandDelegate(TryConnect));
            //Cmd.Register("dc", new CommandDelegate(TryDC));
            Thread T = new Thread(InputThread);
            T.IsBackground = true;
            T.Start();
            Application.Run();
        }

        private static void TryConnect(object sender, object args)
        {
            string[] Args = (string[])args;
            string IP = Args[0];
            WS.OnRecv += new Wo1fSocket.PacketReceivedEventHandler(WS_OnRecv);
            WS.OnDisconnect += new Wo1fSocket.OnDisconnectEventHandler(WS_OnDisconnect);
            WS.OnConnectFail += new Wo1fSocket.OnConnectFailEventHandler(WS_OnConnectFail);
            WS.OnSend += new Wo1fSocket.PacketSentEventHandler(WS_OnSend);
            WS.Connect(IP, 9001);
        }

        static void WS_OnSend(object sender, Packet P)
        {
            throw new NotImplementedException();
        }

        static void WS_OnConnectFail(object sender)
        {
            throw new NotImplementedException();
        }

        static void WS_OnDisconnect()
        {
            throw new NotImplementedException();
        }

        static void WS_OnRecv(object sender, Packet P)
        {
            throw new NotImplementedException();
        }
        private static void InputThread()
        {
            while (true)
            {
                string Ln = Console.ReadLine();

                string[] split = Ln.Split(' ');
                string cmd = split[0];
                string[] Args = new string[split.Length - 1];
                string args = "";
                for (int x = 1; x < split.Length; x++)
                {
                    Args[x - 1] = split[x];
                    args += split[x] + " ";
                    args = args.Trim();
                }
                Cmd.Process(null, cmd, Args);
                return;
                switch (cmd.ToLower())
                {
                    case "connect":

                        break;
                    case "dc":
                        break;
                }

            }
        }
    }
}
