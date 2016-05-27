using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TilePic
    {
        public int X;
        public int Y;
        public int Id;
        public int Page;
        public int ElemNum;
    }
}
