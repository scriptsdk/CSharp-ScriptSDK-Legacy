using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct XmfHTMLGump
    {

        public int X;
        public int Y;
        public int Width;
        public int Height;
        public uint Cliloc_id;
        public int Background;
        public int Scrollbar;
        public int Page;
        public int ElemNum;
    }
}
