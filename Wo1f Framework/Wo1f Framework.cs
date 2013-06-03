using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wo1f_Framework
{
    public class Wo1f_Framework
    {
        public static void Init()
        {
            
            CS.SysLog("Wo1f Framework Initializing");
            Stopwatch.Start("WFINIT");
            CS.EnableLogging = true;
            CS.SysLog("Logging enabled");
            CS.SysLog("Wo1f Framework Initialized in " + Stopwatch.Stop("WFINIT").TotalMilliseconds + "ms");
        }
    }
}
