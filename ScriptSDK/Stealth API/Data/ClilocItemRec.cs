using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
#pragma warning disable 1591
namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct ClilocItemRec: IDeserialized
    {
        public uint ClilocID { get; set; }
        public List<String> Params { get; set; }

        public void Deserialize(BinaryReader data)
        {
            ClilocID = data.ReadUInt32();
            
            Params = new List<string>();

            var strCount = data.ReadUInt32();

            for (var i = 0; i < strCount; i++)
            {
                var len = data.ReadUInt32();
                var strb = data.ReadBytes((int)len*2);
                Params.Add(Encoding.Unicode.GetString(strb));
            }
        }
    }
}
