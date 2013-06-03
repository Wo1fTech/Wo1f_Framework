using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wo1fGet
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "";
            string local = "";
            string arg = "url https://s3.amazonaws.com/MinecraftDownload/launcher/Minecraft_Server.exe local C:\\minecraft_server.exe";
            
            args = arg.Split(' ');
            for (int x = 0; x < args.Length; x++)
            {
                string a = args[x];
                if (a == "url")
                {
                    x++;
                    url = args[x];
                }
                if (a == "local")
                {
                    x++;
                    local = args[x];
                }
            }
            Console.WriteLine("URL = " + url + "\nlocal= " + local);
            if (url == "" || local == "")
            {
                Console.WriteLine("ERROR: One of the values is null.");
                Console.WriteLine("Closing Wo1fGet");
                return;
            }
            Console.WriteLine("Not coded this far yet.");
        }
    }
}
