using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
//not written by wo1f, just edited.

namespace Wo1f_Framework.Win32
{

    static public class winapi
    {
        public static IntPtr statusbar;

        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        #region WinApi Constants, Enums and structs

        public enum WindowShowStyle : uint
        {
            Hide = 0,
            ShowNormal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            Maximize = 3,
            ShowNormalNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActivate = 7,
            ShowNoActivate = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimized = 11
        }
        public const uint SWP_DRAWFRAME = 0x20;
        public const uint SWP_FRAMECHANGED = 0x20;

        public const uint SWP_ASYNCWINDOWPOS = 0x4000;
        public const uint SWP_DEFERERASE = 0x2000;
        public const uint SWP_HIDEWINDOW = 0x80;
        public const uint SWP_NOACTIVATE = 0x10;
        public const uint SWP_NOCOPYBITS = 0x100;
        public const uint SWP_NOMOVE = 0x2;
        public const uint SWP_NOOWNERZORDER = 0x200;
        public const uint SWPNOREDRAW_ = 0x8;
        public const uint SWP_NOREPOSITION = 0x200;
        public const uint SWP_NOSIZE = 0x1;
        public const uint SWP_NOSENDCHANGING = 0x400;
        public const uint SWP_NOZORDER = 0x4;
        public const uint SWP_SHOWWINDOW = 0x40;


        public const int HWND_BOTTOM = 1;
        public const int HWND_NOTOPMOST = (-2);
        public const int HWND_TOP = 0;
        public const int HWND_TOPMOST = (-1);

        public const int WM_GETICON = 0x7F;
        public const int WM_QUERYDRAGICON = 0x37;

        public const int ICON_SMALL = 0;
        public const int GCL_HICONSM = (-34);

        public const int SMTO_ABORTIFHUNG = 0x2;

        public const int GWL_STYLE = -16;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_SYSMENU = 0x80000;
        public const uint WS_BORDER = 0x800000;

        public const int WM_HOTKEY = 0x0312;

        public const int WM_CLOSE = 0x10;
        public const int WM_DESTROY = 0x0002;


        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_SMALLICON = 0x1;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public struct POINTAPI
        {
            public int x;
            public int y;
        }

        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINTAPI ptMinPosition;
            public POINTAPI ptMaxPosition;
            public RECT rcNormalPosition;
        }

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [Flags]
        public enum KeyModifiers : uint
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        #endregion

        #region WinApi functions

        #region kernel32
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle,
           uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);
        #endregion


        #region User32
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr Hwnd, int X, int Y, int Width, int Height, uint Flags);

        [DllImport("User32.dll")]
        public static extern int GetClassLong(IntPtr hWnd, int index);

        [DllImport("User32.dll")]
        public static extern int SendMessageTimeout(IntPtr hWnd, int uMsg, int wParam, int lParam, int fuFlags, int uTimeout, out int lpdwResult);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hWnd,
           [In] ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);


        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth,
           int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsProc ewp, int lParam);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        [return: MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool DestroyIcon([InAttribute()] System.IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr Handle);

        #endregion


        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("Psapi.dll", SetLastError = true)]
        [PreserveSig]
        public static extern uint GetModuleFileNameEx([In]IntPtr hProcess, [In] IntPtr hModule, [Out] StringBuilder lpFilename,
            [In][MarshalAs(UnmanagedType.U4)]int nSize);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        #endregion


        internal static System.Drawing.Rectangle GetWindowRect(IntPtr hWnd)
        {//by wo1f
            RECT R = new RECT();
            GetWindowRect(hWnd,ref  R);
            System.Drawing.Rectangle R2 = new System.Drawing.Rectangle(R.Left, R.Top, R.Right-R.Left, R.Bottom - R.Top);
            return R2;
        }
    }
}