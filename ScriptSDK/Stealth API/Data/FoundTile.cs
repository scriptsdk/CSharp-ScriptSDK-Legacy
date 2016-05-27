using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FoundTile
    {
        public ushort Tile;
        public short X;
        public short Y;
        public byte Z;
    }
}
