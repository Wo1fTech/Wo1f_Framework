using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Wo1f_Framework.Common
{
    public class Timer
    {
        Thread TickT;
        private bool Running = false;
        private int interval;

        public int Interval 
        {
            get 
            { 
                return interval; 
            }

            set
            {
                interval = value;
                if(value == 0)
                    Running = false;
            }
        }

        public Timer()
        {
            Interval = 1000;
            TickT = new Thread(new ThreadStart(TickThread));
            TickT.Start();
        }
        public delegate void OnTickEventHandler(object sender);
        public event OnTickEventHandler OnTick;

        public void Start()
        {
            Running = true;
        }
        public void Stop()
        {
            Running = false;
        }
        private void TickThread()
        {
            while (true)
            {
                while (Running)
                {
                    Thread.Sleep(Interval);
                    if (OnTick != null)
                        OnTick(this);
                }
                Thread.Sleep(50);
            }
        }
    }
}
