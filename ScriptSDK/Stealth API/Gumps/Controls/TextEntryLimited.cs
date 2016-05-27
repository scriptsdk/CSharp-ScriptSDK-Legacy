using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TextEntryLimited
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int Color;
        public int ReturnValue;
        public int DefaultTextId;
        public int Limit;
        public int Page;
        public int ElemNum;
    }
}
