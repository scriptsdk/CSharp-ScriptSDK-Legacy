using System.IO;
#pragma warning disable 1591


namespace StealthAPI
{
    internal interface IDeserialized
    {
        void Deserialize(BinaryReader data);
    }
}
