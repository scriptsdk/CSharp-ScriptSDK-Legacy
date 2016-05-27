using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct CheckBox
    {
        public int X;
        public int Y;
        public int ReleasedId;
        public int PressedId;
        public int Status;
        public int ReturnValue;
        public int Page;
        public int ElemNum;
    }
}
