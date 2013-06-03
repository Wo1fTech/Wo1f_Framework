using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Wo1f_Framework
{
    public static class CS
    {
        public static bool EnableLogging;
        public static string NL = null;

        public static Windows.PacketLogger PLogger;

        public  delegate void ConsoleLogEventHandler(string Text);
        public static event ConsoleLogEventHandler ConsoleLog;
        private static ulong uid = 0;
        public static ulong GetUid()
        {
            return ++uid;
        }
        static CS()
        {
            SysLog("Wo1f Framework CS Initializing");
            EnableLogging = false;
            Stopwatch.Start("CS");
            NL = Environment.NewLine;
            FS = new FileSystem(Environment.CurrentDirectory);
            FS.logfile = "WF.log";
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.ThreadExit += new EventHandler(Application_ThreadExit);
            //AppDomain.CurrentDomain.FirstChanceException += new EventHandler<System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs>(CurrentDomain_FirstChanceException);
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
           
            SysLog("Wo1f Framework CS Initialized in " + Stopwatch.StopMS("CS") + "ms");
            //EnableLogging = true;

        }
        public static string SizeOfLength(long p)
        {
            double ret = (double)p;
            int inc = 0;
            string[] Suffix = new string[] { "Bytes", "KB", "MB", "GB", "TB" };
            while (ret > 1024)
            {
                ret = ret / 1024;
                inc++;
            }
            string Ret = string.Format("{0} {1}", ret.ToString("###"), Suffix[inc]);
            return Ret;
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            
        }

        static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Exception E = e.Exception;
            string msg = E.Message;
            SysLog(string.Format("Exception detected: \nMsg: {0}\nStack Trace: {1}", E.Message, E.StackTrace));

        }

        private static void OnConsoleLog(string text)
        {
            if (ConsoleLog != null)
                ConsoleLog(text);
        }

        private static void backlog(string text)
        {
            Backlog += CS.NL + Timestamp() + text;
        }

        static void Application_ThreadExit(object sender, EventArgs e)
        {
            SysLog("Thread Exiting");
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            SysLog("Application exit process begin.");
            SysLog("Disposing.");
            CS.Dispose();
            SysLog("Application exiting.  Cya later!\n");
            AppClosed = true; //signal threads to close
            //Application.Exit();
        }

        private static void Dispose()
        {
            Stopwatch.Start("Disposing");
            SysLog("Dispose is killing " + Threads.Count + " Threads");

            for (int x = 0; x < Threads.Count; x++)
            {
                if (Threads[x] != null)
                {
                    Threads[x].Abort();
                }
            }

            SysLog("Done disposing of Wo1f Framework objects. " + Stopwatch.StopMS("Disposing") + "ms");
            
        }

        public static FileSystem FS;
        static string Backlog = "";

        public static void SysLog(string Text)
        {
            if (EnableLogging)
            {
                if (Backlog.Length != 0)
                {
                    OnConsoleLog(Backlog.Trim());
                    FS.Log(Backlog.Trim(), false);
                    Backlog = "";
                }
                //Text = Text.Replace("\r", "");
                //Text = Text.Replace("\n", CS.NL);

                OnConsoleLog(Timestamp() + Text);
                FS.Log(Text, true);
            }
            else
            {
                backlog(Text);
            }

        }

        internal static string Timestamp()
        {
            return Timestamp(0);
        }

        internal static string Timestamp(int level)
        {
            string ret = "";
            DateTime DT = DateTime.Now;
            TimeSpan TS = DT.TimeOfDay;
            string fulldate = DT.ToString();
            string date = fulldate.Split(' ')[0];
            string time = fulldate.Substring(date.Length+1);

            switch(level)
            {
                case 0:
                    ret = string.Format("[{0}] ",fulldate);
                    break;
                case 1:
                    ret = string.Format("[{0} {1}] ", DT.DayOfWeek, fulldate);
                    break;
                default:

                    break;
            }
            return ret;
        }

        static List<Thread> Threads = new List<Thread>();
        public static bool AppClosed = false;

        public static int ThreadCount()
        {
            int ret = 0;
            foreach (Thread T in Threads)
            {
                if (T.IsAlive)
                    ret++;
            }
            return ret;
        }
        internal static void AddThread(System.Threading.Thread T)
        {
            Threads.Add(T);
           
            CS.SysLog("[WF] Thread added.");
        }
    }
}
