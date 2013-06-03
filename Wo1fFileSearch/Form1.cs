using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wo1f_Framework.Windows;
using System.IO;

using System.Threading;


namespace Wo1fFileSearch
{
    public partial class Form1 : Form
    {
        bool Cancel = false;
        SearchSettings Options = new SearchSettings();
        Settings S;
        Thread T;
        List<FileInfo> Success = new List<FileInfo>();
        long maxlength = 5 * 1024 * 1024;
        bool searching = false;
        int filecount = 0;
        int filecur = 0;
        string searchstatus = "Waiting";

        public Form1()
        {
            InitializeComponent();
            LV.LargeImageList = Wo1fWindows.GetIconList();
            LV.SmallImageList = Wo1fWindows.GetIconList();
            TxtUrl.Text = Application.StartupPath;

        }
        #region Input events
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            S = new Settings(Options);
            
            S.Show();
            S.BringToFront();
        }
        
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (BtnSearch.Text == "Search")
            {
                if (T != null)
                {
                    T.Abort();
                    T = null;
                }
                Success.Clear();
                LV.Items.Clear();
                T = new Thread(FileSearchThread);
                T.IsBackground = true;
                T.Start();
                BtnSearch.Text = "Cancel";
            }
            else
            {
                Cancel = true;
                BtnSearch.Text = "Search";
                T.Abort();
                searching = false;
                string suffix = "";
                if (Success.Count != 0) //canceled mid-list updating
                {
                    suffix = string.Format(" Listed {0}/{1}({2}%) Items", LV.Items.Count, Success.Count, LV.Items.Count * 100/Success.Count);
                }
                searchstatus = "Search Canceled" + suffix;
            }

        }

        #endregion

        #region Searching methods

        private void FileSearchThread()
        {
            Cancel = false;
            string search = TxtSearch.Text;
            string location = TxtUrl.Text;
            searching = true;
            List<FileInfo> Files = new List<FileInfo>();
            searchstatus = "Collecting files... This may take a while";
            if (Options.OnlyDirs)
            {
                /*List<DirectoryInfo> DIs = new List<DirectoryInfo>();
                DIs.AddRange(Wo1f_Framework.FileSystem.GetDirectories(location, true));
                foreach (DirectoryInfo DI in DIs)
                {
                    if (DI.Name.Contains(search))
                    {
                        SuccessDir.Add(DI);
                    }
                }
                return;*/
            }
            else
                Files.AddRange(Wo1f_Framework.FileSystem.GetFiles(location, true));
            searchstatus = "Searching through files";
            Success.Clear();
            filecount = Files.Count;
            int x = 0;
            foreach(FileInfo FI in Files)
            {
                filecur = x;
                if (FI.Name.Contains(search))
                {
                    Success.Add(FI);
                    continue;
                }
                if (FI.Length < maxlength)
                {
                    try
                    {
                        Thread.Sleep(2);
                        string Txt = File.ReadAllText(FI.FullName);
                        if (Txt.Contains(search))
                        {
                            Success.Add(FI);
                            continue;
                        }
                    }
                    catch
                    {
                        
                    }
                }
                x++;

            }
            searching = false;
            searchstatus = "Done! Found " + Success.Count.ToString("###,###,###,###,###") + " matches!";
        }

        private void TmrRefresh_Tick(object sender, EventArgs e)
        {
            TmrRefresh.Stop();

            LblStatus.Text = "Status: " + searchstatus;
            if (Success.Count != LV.Items.Count && !Cancel)
                UpdateList();
            TmrRefresh.Start();
        }

        private void UpdateList()
        {
            if (Success.Count == 0)
                LV.Items.Clear();
            if (Success.Count > LV.Items.Count)
            {
                if (Cancel)
                {
                    return;
                }
                int count = Success.Count - LV.Items.Count;

                PB.Maximum = Success.Count;
                //PB.Value = 0;
                for (int x = 0; x < count; x++)
                {

                    int y = Success.Count - count;
                    FileInfo FI = Success[x + y];
                    ListViewItem LVI = new ListViewItem(FI.Name);
                    LVI.ImageIndex = Wo1f_Framework.Windows.Wo1fWindows.GetIconKey(FI.Extension);
                    LVI.Tag = FI;
                    LVI.SubItems.Add(FI.Extension);
                    //LVI.SubItems.Add(Wo1f_Framework.CS.GetDateTime(FI.LastAccessTime));
                    LVI.SubItems.Add(Wo1f_Framework.CS.SizeOfLength(FI.Length));
                    LVI.SubItems.Add(FI.FullName);

                    LV.Items.Add(LVI);
                    if (x >= 100)
                    {
                        break;
                    }
                }

                PB.Value = LV.Items.Count;
                if (Success.Count == LV.Items.Count)//done
                {
                    BtnSearch.Text = "Search";
                    searchstatus = "Completed search.  Displaying " + LV.Items.Count + " items that meet search criteria";
                }
                else
                    searchstatus = string.Format("Displaying items {0} out of {1} ({2}%)", PB.Value, Success.Count, PB.Value * 100 / Success.Count);
                LblStatus.Text = "Status: " + searchstatus;
                Application.DoEvents();
            }
        }
        #endregion


        private void LV_DoubleClick(object sender, EventArgs e)
        {
            if (LV.SelectedItems != null)
            {
                ListViewItem LVI = LV.SelectedItems[0];
                FileInfo FI = LVI.Tag as FileInfo;
                System.Diagnostics.Process.Start(FI.Directory.FullName);
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
