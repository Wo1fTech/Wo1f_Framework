using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Wo1f_Chat_Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch(Exception E)
            {
                MessageBox.Show("Unhandled Exception\nMsg: " + E.Message + "\nTrace: " + E.StackTrace + "\nSource: " + E.Source );
            }
        }
    }
}
