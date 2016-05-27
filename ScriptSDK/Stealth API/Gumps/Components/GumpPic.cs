using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct GumpPic
    {
        public int X;
        public int Y;
        public int Id;
        public int Hue;
        public int Page;
        public int ElemNum;
    }
}
