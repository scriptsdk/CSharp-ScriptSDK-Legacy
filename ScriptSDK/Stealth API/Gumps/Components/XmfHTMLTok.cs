using System.IO;
using System.Runtime.InteropServices;
using System.Text;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct XmfHTMLTok : IDeserialized
    {

        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int Background;
        public int Scrollbar;
        public int Color;
        public uint ClilocId;
        public string Arguments;
        public int Page;
        public int ElemNum;



        public void Deserialize(BinaryReader br)
        {
            X = br.ReadInt32();
            Y = br.ReadInt32();
            Width = br.ReadInt32();
            Height = br.ReadInt32();
            Background = br.ReadInt32();
            Scrollbar = br.ReadInt32();
            Color = br.ReadInt32();
            ClilocId = br.ReadUInt32();

            var paramLength = br.ReadUInt32();
            Arguments = Encoding.Unicode.GetString(br.ReadBytes((int)paramLength * sizeof(char)), 0, (int)paramLength * sizeof(char));

            Page = br.ReadInt32();
            ElemNum = br.ReadInt32();
        }
    }
}
