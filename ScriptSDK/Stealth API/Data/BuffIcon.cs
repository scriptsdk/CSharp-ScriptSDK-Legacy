using System;
using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class BuffIcon
    {
        public ushort Attribute_ID { get; set; }
        public DateTime TimeStart { get; set; }
        public ushort Seconds { get; set; }
        public uint ClilocID1 { get; set; }
        public uint ClilocID2 { get; set; }
    }
}
