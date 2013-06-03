using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

//Written entirely by Wo1f
namespace Wo1f_Framework.Networking
{
    public class Wo1fSocketSetting
    {
        public Wo1fSocketThreadMode ThreadMode = Wo1fSocketThreadMode.Async;
        public Wo1fSocketProtocol Protocol = Wo1fSocketProtocol.TCP;
        public bool DisableAutoTick = false;
        
        public int Port;
        public string IP;
        public bool PacketTracking = false;
        public bool PacketLogging = true;
        public int Netrate = 2;
        public int BufferSize = 4096;
    }

    public class Wo1fSocketSettingStats
    {
        public bool Listening = false;
        public int pps = 0;
        public long packetssent = 0;
        public long packetsrecv = 0;

        private long lpr = 0;
        private long lps = 0;

        public int rpps = 0;

        public int LostPackets = 0;
        public int RecvErrors = 0;
        public int SendErrors = 0;
        //public int Latency = 0;
        public int Ping = 0;
        public DateTime LastPacketRecv = DateTime.Now;
        public DateTime LastPacketSent = DateTime.Now;

        public Wo1fSocketSettingStats()
        {
            Thread T = new Thread(TickThread);
            T.IsBackground = true;
            T.Start();
        }
        private void TickThread()
        {
            while (true)
            {

                //DateTime LastCheck = DateTime.Now;
                Thread.Sleep(1000);
                rpps = (int)(packetsrecv - lpr);
                pps = (int)(packetssent - lps);
                lpr = packetsrecv;
                lps = packetssent;

                //while (DateTime.Now.Subtract(LastCheck).TotalMilliseconds <= 999)
                //{

                //}
            }
        }
    }
    
    public enum Wo1fSocketProtocol {TCP, UDP };
    public enum Wo1fSocketThreadMode
    {
        Async, //everything is sent asyncronously without extra threads
        MultiThreadCount, //Specify number of multiple threads
        MultiThreadedEach, //Multi threaded where each connection is its own thread
        SingleThreaded}; //One thread for all connections
    

    /* Notes
     * MultiThreadCount is probably the most efficient method.  Play with the numbers
     * Use MultiThreadEach and UDP when you want the lowest delay
     * SingleThreaded is for control or something.... 
     * 
    */
}
