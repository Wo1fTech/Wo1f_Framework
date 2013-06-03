using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Wo1f_Framework.SQL;
using System.Data;

//Written entirely by Wo1f
namespace Wo1f_Framework.Networking
{
    public class ClientCollection
    {
        public bool AutomateDB = true;
        public bool Automate = true;
        public Database DB;
        Thread T;

        public ClientCollection()
        {
            T = new Thread(Worker);
            T.IsBackground = true;
            T.Start();
            if (AutomateDB)
            {
                DB = new Database();
                DB.DBType = DatabaseType.MSSQL;
                
            }
        }
        public bool HasName(string Name)
        {
            return this[Name] != null;
        }
        public bool CheckLogin(Client cl, string Username, string Password)
        {
            DataTable DT = DB.QueryCmd("SELECT * FROM clients WHERE name='" + Username + "';");
            DataRowCollection DR = DT.Rows;
            if (DR.Count == 0)
            {
                cl.Send(new Packet("Msg", new string[] { "Server", "Username not found." }));
            }
            else
            {
                if (((string)DR[0]["pass"]).Equals(Password)) //can't use == on strings
                {
                    CS.SysLog(string.Format("Client {0} has successfully logged in as {1}", cl.Name, Username));
                    cl.Name = Username;
                    cl.Send(new Packet("Msg", new string[] { "Server", "Successfully logged in!" }));
                    cl.Send(new Packet("Name", cl.Name));
                    
                    return true;
                }
                else
                {
                    cl.Send(new Packet("Msg", new string[] { "Server", "Incorrect password!" }));
                    return false;
                }

            }

            //check query


            return true;
        }

        public bool Register(Client cl, string Username, string Password)
        {
            Dictionary<string, object> Extras = new Dictionary<string,object>();
            cl.Name = Username;
            DataTable DT = DB.QueryCmd("SELECT * FROM clients WHERE name='" + Username + "';");
            if (DT.Rows.Count != 0)
            {
                cl.Send(new Packet("Msg", new string[] { "Server", "Username already registered." }));
            }
            else
            {
                Dictionary<string, object> Vals = new Dictionary<string,object>();
                Vals.Add("name", Username);
                Vals.Add("pass", Password);
                DB.Insert("clients", Vals);
                cl.Send(new Packet("Msg", new string[] { "Server", "You have successfully registered " + Username + "!." }));
            }
            return true;
        }

        private void Worker()
        {
            while (true)
            {
                Stopwatch.Start("CCWorker");
                long sent = 0;
                long recv = 0;

                foreach (Client C in Clients)
                {
                    sent += C.Socket.Stats.packetssent;
                    recv += C.Socket.Stats.packetsrecv;
                }


                _recv = recv;
                _sent = sent;

                rpps = recv - lastrecv;
                pps = sent - lastsent;

                lastrecv = recv;
                lastsent = sent;
                
                int ms = (int)Stopwatch.Stop("CCWorker").TotalMilliseconds;
                Thread.Sleep(1000-ms);
            }
        }
        public List<Client> Clients = new List<Client>();
        public Client this[int index]
        {
            get
            {
                return Clients[index];
            }
        }

        public Client this[Wo1fSocket Socket]
        {
            get
            {
                foreach (Client C in Clients)
                {
                    if (C.uid == Socket.uid)
                        return C;
                }
                return null;
            }
        }

        public Client this[string Name]
        {
            get
            {
                foreach (Client C in Clients)
                {
                    if (C.Name == Name)
                        return C;
                }
                return null;
            }
        }
        public void SendToAll(Packet P)
        {
            foreach (Client C in Clients)
            {
                C.Send(P);
            }
        }
        private long lastsent = 0;
        private long lastrecv = 0;
        private long _sent = 0;
        private long _recv = 0;
        private long pps = 0;
        private long rpps = 0;
        public long RecvPPS()
        {
            return rpps;
        }
        public long SentPPS()
        {
            return pps;
        }

        public long Sent()
        {
            return _sent;
        }
        public long Recv()
        {
            return _recv;
        }

        public void Add(Client client)
        {
            Clients.Add(client);
            client.OnDisconnect += new Client.OnDisconnectEventHandler(client_OnDisconnect);
        }

        void client_OnDisconnect(object sender)
        {
            Client C = (Client)sender;
            this.Remove(C);
            CS.SysLog("Removing client " + C.Name + " because of OnDisconnect();");
        }

        public void Remove(Client client)
        {
            Clients.Remove(client);
        }
        public void RemoveAt(int index)
        {
            Clients.RemoveAt(index);
        }

        public void Tick()
        {
            
            
        }
    }
}
