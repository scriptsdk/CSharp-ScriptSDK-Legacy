using System;
using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    internal static class Win32
    {
        internal struct CopyDataStruct
        {
            public uint dwData;
            public int cbData;
            public IntPtr lpData;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Point
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct NativeMessage
        {
            public IntPtr handle;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Point p;
            public override string ToString()
            {
                return handle + ", " + msg + ", " + wParam + ", " + lParam + ", " + time + ", " + p;
            }
        }

        internal const int WM_COPYDATA = 0x004A;

        internal const uint PM_NOREMOVE = 0x0000;
        internal const uint PM_REMOVE = 0x0001;
        internal const uint PM_NOYIELD = 0x0002;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool PeekMessage(out NativeMessage lpMsg, int window, uint msgFilterMin, uint msgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetMessage(out NativeMessage lpMsg, int window, uint msgFilterMin, uint msgFilterMax);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetCurrentThreadId();
    }
}
