using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TargetInfo
    {
        public uint ID;
        public ushort Tile;
        public ushort X;
        public ushort Y;
        public sbyte Z;
    }
}
