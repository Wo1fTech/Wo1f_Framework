using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Console = global::System.Console;
namespace Wo1f_Framework
{
    public class WConsole
    {
        public static void Pause()
        {            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        public static void Exit()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
            System.Windows.Forms.Application.Exit();
        }
        
        public static void Sys()
        {
            RunWait();
        }
    }
}
