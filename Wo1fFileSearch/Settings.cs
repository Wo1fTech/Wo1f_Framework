using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wo1fFileSearch
{
    public partial class Settings : Form
    {
        SearchSettings SS;
        public bool Caps = false;
        public bool Async = true;
        public bool OnlyDirectories = false;
        public bool CheckContents = true;
        public int MaxSize = 5;

        public Settings(SearchSettings SS)
        {
            this.SS = SS;
            InitializeComponent();
            Properties.Settings.Default.Reload();

            ChkAsync.Checked = Properties.Settings.Default.Async;
            NumMaxSize.Value = Properties.Settings.Default.MaxSize;
            ChkDirs.Checked = Properties.Settings.Default.Dirs;
            ChkContents.Checked = Properties.Settings.Default.Contents;
            ChkCaps.Checked = Properties.Settings.Default.CapsSensetive;
            MaxSize = Properties.Settings.Default.MaxSize;
        }

        private void Upd()
        {
            Caps = ChkCaps.Checked;
            Async = ChkAsync.Checked;
            OnlyDirectories = ChkDirs.Checked;
            CheckContents = ChkContents.Checked;
            Caps = ChkCaps.Checked;
            MaxSize = (int)NumMaxSize.Value;
        }

        private void ChkAsync_CheckedChanged(object sender, EventArgs e)
        {
            ChkAsync.Checked = true;
            Upd();
        }

        private void Settings_ChkChange(object sender, EventArgs e)
        {
            Upd();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Upd();
            Properties.Settings.Default.CapsSensetive = Caps;
            Properties.Settings.Default.Async = Async;
            Properties.Settings.Default.Contents = CheckContents;
            Properties.Settings.Default.Dirs = OnlyDirectories;
            Properties.Settings.Default.MaxSize = MaxSize;
            Properties.Settings.Default.Save();

        }
    }
}
