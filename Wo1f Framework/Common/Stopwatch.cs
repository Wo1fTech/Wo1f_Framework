using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wo1f_Framework
{
    public class Stopwatch
    {
        static Dictionary<string, DateTime> Watches;

        public Stopwatch()
        {

        }
           
        static Stopwatch()
        {
            Watches = new Dictionary<string, DateTime>();
        }

        public static bool Start(string Watch)
        {
            bool ret = false;
            if (Watches.ContainsKey(Watch))
                Watches.Remove(Watch);

            Watches.Add(Watch, DateTime.Now);
            return ret;
        }

        public static TimeSpan Stop(string Watch)
        {
            if (Watches.ContainsKey(Watch))
            {
                return DateTime.Now.Subtract(Watches[Watch]);
            }
            return TimeSpan.MinValue;
        }

        public static int StopMS(string Watch)
        {
            return (int)Stop(Watch).TotalMilliseconds;
        }
        public static int StopS(string Watch)
        {
            return (int)Stop(Watch).TotalSeconds;
        }
        public static int StopM(string Watch)
        {
            return (int)Stop(Watch).TotalMinutes;
        }
    }
}
