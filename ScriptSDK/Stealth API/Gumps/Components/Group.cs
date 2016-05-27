using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Group
    {
        public int GroupNumber;
        public int Page;
        public int ElemNum;
    }
}
