using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
//not written by wo1f, just edited.

namespace Wo1f_Framework.Win32
{

    public class window
    {
        #region private fields
        private winapi.WINDOWPLACEMENT placement;
        private IntPtr handle;
        private string title;
        private int style;
        private int previousstyle;
        private IntPtr parent;
        private IntPtr previousparent;

        private static List<window> openwnd;
        #endregion

        #region Properties

        public IntPtr Handle
        {
            get { return handle; }
        }

        public string Title
        {
            get { return title; }
        }

        public int Style
        {
            get { return style; }
        }

        public IntPtr Parent
        {
            get { return parent; }
        }
        public Point Position
        {
            get
            {
                return new Point(placement.rcNormalPosition.Left, placement.rcNormalPosition.Top);
            }
            set
            {
                winapi.SetWindowPos(handle, value.X, value.Y, 0, 0, winapi.SWP_NOSIZE);
            }
        }
        public Size Size
        {
            get { return new Size(placement.rcNormalPosition.Right - placement.rcNormalPosition.Left, placement.rcNormalPosition.Bottom - placement.rcNormalPosition.Top); }
        }
        public int PreviousStyle
        {
            get { return previousstyle; }
        }

        public Icon ExecutableIcon
        {
            get { return GetExecutableIcon(); }
        }

        public Icon WindowIcon
        {
            get { return GetWindowIcon(); }
        }

        #endregion

        public window(IntPtr handle)
        {
            this.handle = handle;

            title = gettext();
            parent = getparent();
            style = getstyle();
            GetSizeandLocation();
        }

        
        public Rectangle GetRectangle()
        {//by wo1f
            return winapi.GetWindowRect(Handle);

        }
        public void SetParent(IntPtr ParentHandle)
        {
            previousparent = winapi.SetParent(handle, ParentHandle);
            parent = ParentHandle;
        }

        public void RestoreParent()
        {
            parent = previousparent;
            previousparent = winapi.SetParent(handle, previousparent);
        }

        public void Move(Point Location, Size size, bool repaint)
        {
            winapi.MoveWindow(handle, Location.X, Location.Y, size.Width, size.Height, repaint);
        }

        public void SetStyle(int index, IntPtr value)
        {
            previousstyle = winapi.SetWindowLong(handle, index, value);
            style = value.ToInt32();
        }

        public bool Close()
        {
            bool result = winapi.PostMessage(handle, winapi.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            if (!result)
            {
                result = winapi.PostMessage(handle, winapi.WM_DESTROY, IntPtr.Zero, IntPtr.Zero);
            }

            return result;
        }

        public void RestoreLocation()
        {
            winapi.SetWindowPlacement(handle, ref placement);
        }

        public string GetExecutablePath()
        {
            uint dwProcessId;
            winapi.GetWindowThreadProcessId(handle, out dwProcessId);
            IntPtr hProcess = winapi.OpenProcess(winapi.ProcessAccessFlags.VMRead | winapi.ProcessAccessFlags.QueryInformation, false, dwProcessId);
            StringBuilder path = new StringBuilder(1024);
            winapi.GetModuleFileNameEx(hProcess, IntPtr.Zero, path, 1024);
            winapi.CloseHandle(hProcess);
            return path.ToString();
        }

        public static List<window> GetOpenWindows()
        {
            openwnd = new List<window>();

            winapi.EnumWindowsProc callback = new winapi.EnumWindowsProc(EnumWindows);
            winapi.EnumWindows(callback, 0);

            List<window> result = new List<window>(openwnd);
            openwnd.Clear();

            result.RemoveAt(result.Count - 1);
            return result;
        }

        public static IntPtr FindWindow(string classname, string windowtitle)
        {
            return winapi.FindWindow(classname, windowtitle);
        }

        private static bool EnumWindows(IntPtr hWnd, int lParam)
        {
            if (!winapi.IsWindowVisible(hWnd) || hWnd == winapi.statusbar)
                return true;

            openwnd.Add(new window(hWnd));

            return true;
        }

        private string gettext()
        {
            StringBuilder title = new StringBuilder(256);
            winapi.GetWindowText(handle, title, 256);
            return title.ToString();
        }

        private IntPtr getparent()
        {
            return winapi.GetParent(handle);
        }

        private int getstyle()
        {
            return winapi.GetWindowLong(handle, winapi.GWL_STYLE);
        }

        private void GetSizeandLocation()
        {
            placement.length = Marshal.SizeOf(placement);
            winapi.GetWindowPlacement(handle, ref placement);
        }

        private Icon GetExecutableIcon()
        {
            System.Drawing.Icon icon = null;
            string path = GetExecutablePath();
            if (System.IO.File.Exists(path))
            {
                winapi.SHFILEINFO info = new winapi.SHFILEINFO();
                winapi.SHGetFileInfo(path, 0, ref info, (uint)Marshal.SizeOf(info), winapi.SHGFI_ICON | winapi.SHGFI_SMALLICON);

                System.Drawing.Icon temp = System.Drawing.Icon.FromHandle(info.hIcon);
                icon = (System.Drawing.Icon)temp.Clone();
                winapi.DestroyIcon(temp.Handle);
            }

            return icon;
        }

        private Icon GetWindowIcon()
        {
            int result;

            winapi.SendMessageTimeout(handle, winapi.WM_GETICON, winapi.ICON_SMALL, 0,
              winapi.SMTO_ABORTIFHUNG, 1000, out result);

            IntPtr IconHandle = new IntPtr(result);

            if (IconHandle == IntPtr.Zero)
            {
                result = winapi.GetClassLong(handle, winapi.GCL_HICONSM);
                IconHandle = new IntPtr(result);
            }

            if (IconHandle == IntPtr.Zero)
            {
                winapi.SendMessageTimeout(handle, winapi.WM_QUERYDRAGICON, 0, 0,
                    winapi.SMTO_ABORTIFHUNG, 1000, out result);
                IconHandle = new IntPtr(result);
            }

            if (IconHandle == IntPtr.Zero)
            {
                return null;
            }

            System.Drawing.Icon temp = System.Drawing.Icon.FromHandle(IconHandle);
            System.Drawing.Icon icon = (System.Drawing.Icon)temp.Clone();

            winapi.DestroyIcon(IconHandle);

            return icon;
        }
    }
}