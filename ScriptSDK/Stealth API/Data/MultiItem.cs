using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MultiItem
    {
        public uint ID { get; set; }
        public ushort X { get; set; }
        public ushort Y { get; set; }
        public sbyte Z { get; set; }

        public ushort XMin { get; set; }
        public ushort XMax { get; set; }
        public ushort YMin { get; set; }
        public ushort YMax { get; set; }

        public ushort Width { get; set; }
        public ushort Height { get; set; }
    }
}
