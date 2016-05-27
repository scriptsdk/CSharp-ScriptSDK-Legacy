using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LandTileData
    {

        public uint Flags;
        public uint Flags2;
        public ushort TextureID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Name;
    }
}
