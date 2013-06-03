using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Written entirely by Wo1f
namespace Wo1f_Framework.Networking
{
    public delegate void CommandDelegate(object sender, object Args);

    public class Command
    {
        Dictionary<string, CommandDelegate> Cmds;
        static Dictionary<string, CommandDelegate> CMDs;

        
        public Command()
        {
            Cmds = new Dictionary<string, CommandDelegate>();
        }
        static Command()
        {
            CMDs = new Dictionary<string, CommandDelegate>();
        }

        //public delegate void CommandNotFoundDelegate(object sender, string cmd, object args);
        //public event CommandNotFoundEvent();

        public static bool ProcessCommand(string Cmd, object Args)
        {
            if (CMDs.ContainsKey(Cmd))
                CMDs[Cmd].DynamicInvoke(Args);
            else
            {
                
                return false;
            }
            
            return true;
        }
        public static bool RegisterCommand(string Cmd, CommandDelegate Func)
        {
            if (!CMDs.ContainsKey(Cmd))
                CMDs.Add(Cmd, Func);
            else
                return false;
            return true;

        }

        public  bool Process(object sender, string Cmd, object Args)
        {
            if (Cmds.ContainsKey(Cmd))
                Cmds[Cmd].DynamicInvoke(sender, Args);
            else
                return false;
            return true;
        }
        public bool Register(string Cmd, CommandDelegate Func)
        {
            if (!Cmds.ContainsKey(Cmd))
                Cmds.Add(Cmd, Func);
            else
                return false;
            return true;
                
        }
    }
}
