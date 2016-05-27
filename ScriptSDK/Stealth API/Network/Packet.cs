using System;
using System.IO;
using System.Linq;
using System.Text;
#pragma warning disable 1591

namespace StealthAPI
{
    internal class Packet
    {
        internal byte[] Data;
        internal short DataLength;
        internal PacketType Method;

        public Packet()
        {
            Data = new byte[0];
        }

        public override string ToString()
        {
            return string.Join(" ", Data.Select(b => b.ToString("X2")));
        }

        internal void AddParameter(object p)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                switch (Type.GetTypeCode(p.GetType()))
                {
                    case TypeCode.Boolean:
                        bw.Write((bool) p);
                        break;
                    case TypeCode.Byte:
                        bw.Write((byte) p);
                        break;
                    case TypeCode.Char:
                        bw.Write((char) p);
                        break;
                    case TypeCode.Decimal:
                        bw.Write((decimal) p);
                        break;
                    case TypeCode.Double:
                        bw.Write((double) p);
                        break;
                    case TypeCode.Single:
                        bw.Write((float) p);
                        break;
                    case TypeCode.Int16:
                        bw.Write((short) p);
                        break;
                    case TypeCode.String:
                        var bytes = Encoding.Unicode.GetBytes((string) p);
                        bw.Write(((string) p).Length);
                        bw.Write(bytes, 0, bytes.Length);
                        break;
                    case TypeCode.Int32:
                        bw.Write((int) p);
                        break;
                    case TypeCode.Int64:
                        bw.Write((long) p);
                        break;
                    case TypeCode.SByte:
                        bw.Write((sbyte) p);
                        break;
                    case TypeCode.UInt16:
                        bw.Write((ushort) p);
                        break;
                    case TypeCode.UInt32:
                        bw.Write((uint) p);
                        break;
                    case TypeCode.UInt64:
                        bw.Write((ulong) p);
                        break;
                    case TypeCode.DateTime:
                        bw.Write(((DateTime) p).ToDouble());
                        break;
                    default:
                        if (p.GetType() == typeof (byte[]))
                        {
                            bw.Write((byte[]) p);
                        }
                        else if (p.GetType() == typeof (char[]))
                        {
                            bw.Write((char[]) p);
                        }
                        else if (p.GetType() == typeof (ushort[]))
                        {
                            var bytearray = ((ushort[]) p).SelectMany(i => BitConverter.GetBytes(i)).ToArray();
                            bw.Write(bytearray);
                        }
                        else if (p.GetType() == typeof (uint[]))
                        {
                            var bytearray = ((uint[]) p).SelectMany(i => BitConverter.GetBytes(i)).ToArray();
                            bw.Write(bytearray);
                        }
                        else
                        {
                            throw new ArgumentException("List type not supported");
                        }

                        break;
                }
                ms.Seek(0, SeekOrigin.Begin);
                var tmp = ms.ToArray();
                var pos = Data.Length;
                Array.Resize(ref Data, Data.Length + tmp.Length);
                Array.Copy(tmp, 0, Data, pos, tmp.Length);
            }
        }

        internal byte[] GetBytes()
        {
            byte[] buffer;
            using (var tmp = new MemoryStream())
            using (var bw = new BinaryWriter(tmp))
            {
                bw.Write((ushort) Method);
                bw.Write(DataLength);
                bw.Write(Data);
                bw.Flush();
                buffer = tmp.ToArray();
            }
            return buffer;
        }
    }
}