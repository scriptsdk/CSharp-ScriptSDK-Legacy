using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Tooltip
    {
        public uint Cliloc_ID;
        public string Arguments;
        public int Page;
        public int ElemNum;
    }
}
