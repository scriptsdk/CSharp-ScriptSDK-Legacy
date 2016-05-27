using System;
using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    /// <summary>
    /// About data
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AboutData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ushort[] StealthVersion;
        public ushort Build;
        public double BuildDate;
        public ushort GITRevNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] GITRevision;

        public DateTime BuildDateTime { get { return BuildDate.ToDateTime(); } }
    }
}
