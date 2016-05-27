using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StaticItemRealXY
    {

        public ushort Tile;
        public ushort X;
        public ushort Y;
        public byte Z;
        public ushort Color;
    }
}
