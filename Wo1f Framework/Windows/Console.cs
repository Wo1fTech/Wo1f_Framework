using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wo1f_Framework;

namespace Wo1f_Framework.Windows
{
    public partial class Console : Form
    {
        public Console()
        {
            InitializeComponent();
            Wo1f_Framework.Init();
            CS.ConsoleLog += new CS.ConsoleLogEventHandler(CS_ConsoleLog);

            
        }

        delegate void LogD(string Txt);
        void CS_ConsoleLog(string Text)
        {
            if (TxtConsole.InvokeRequired)
            {
                TxtConsole.Invoke(new LogD(CS_ConsoleLog), Text);
            }
            else
                TxtConsole.Text = Text + "\r\n" + TxtConsole.Text;
        }
    }
}
