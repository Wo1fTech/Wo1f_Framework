using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wo1f_Framework.Networking;

namespace Wo1f_Framework.Windows
{
    public partial class PacketLogger : Form
    {
        public PacketLogger()
        {
            InitializeComponent();
        }
        public void Send(Packet P)
        {
            string args = "";
            foreach(string S in P.Arguments)
            {
                args += S + "|";
            }
            args = args.Remove(args.Length-1);
            OnSend(P.Command + " - " + args);
        }
        delegate void OnD(string Txt);
        private void OnSend(string txt)
        {
            if (ListSend.InvokeRequired)
            {
                ListSend.Invoke(new OnD(OnSend), txt);
            }
            else
                ListSend.Items.Add(txt);
        }
        private void OnRecv(string txt)
        {
            if (ListRecv.InvokeRequired)
            {
                ListRecv.Invoke(new OnD(OnRecv), txt);
            }
            else
                ListRecv.Items.Add(txt);
        }
        public void Recv(Packet P)
        {
            string args = "";
            foreach (string S in P.Arguments)
            {
                args += S + "|";
            }
            args = args.Remove(args.Length - 1);
            OnRecv(P.Command + " - " + args);
        }

        private void PacketLogger_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
