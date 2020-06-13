using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#pragma warning disable 1591

namespace StealthAPI
{
    internal class StealthClient : IDisposable
    {
        private const int BUFFER_SIZE = 512;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly string _host;
        private readonly int _port;
        private readonly Queue<Packet> _replyes;
        private TcpClient _client;
        private BinaryReader _reader;
        private BinaryWriter _writer;

        internal StealthClient(string host, int port)
        {
            _host = host;
            _port = port;
            _replyes = new Queue<Packet>();
        }

        public void Dispose()
        {
            _cts.Cancel();
            if (_client != null)
                _client.Close();
            if (_reader != null)
                _reader.Dispose();
            if (_writer != null)
                _writer.Dispose();
        }

        internal event EventHandler<ServerEventArgs> ServerEventRecieve;
        internal event EventHandler StartStopRecieve;
        internal event EventHandler TerminateRecieve;

        internal void Connect()
        {
            Stealth.AddTraceMessage(string.Format("Connect Stealth client. Host: {0}, Port: {1}", _host, _port),
                "Stealth.Network");
            _client = new TcpClient(_host, _port);
            _reader = new BinaryReader(_client.GetStream());
            _writer = new BinaryWriter(_client.GetStream());

            Stealth.AddTraceMessage("Start reciever", "Stealth.Network");
            var factory = new TaskFactory(_cts.Token, TaskCreationOptions.LongRunning,
                TaskContinuationOptions.LongRunning, TaskScheduler.Default);
            factory.StartNew(Receiver, _cts.Token);
        }

        private void Reconnect()
        {
            _replyes.Clear();
            _client.Close();
            _client = new TcpClient(_host, _port);
            _reader = new BinaryReader(_client.GetStream());
            _writer = new BinaryWriter(_client.GetStream());
        }

        private void Receiver(object state)
        {
            var token = (CancellationToken)state;

            while (!token.IsCancellationRequested)
            {
                if (_reader != null && _reader.BaseStream.CanRead && ((NetworkStream)_reader.BaseStream).DataAvailable)
                {
                    while (((NetworkStream)_reader.BaseStream).DataAvailable)
                    {
                        var packetLen = BitConverter.ToUInt32(_reader.ReadBytes(4), 0);

                        var packet = new Packet();
                        packet.Method = (PacketType)_reader.ReadUInt16();
                        packet.DataLength = _reader.ReadInt32();
                        packet.Data = _reader.ReadBytes((int)packet.DataLength);

                        if (Stealth.EnableTracing)
                            Stealth.AddTraceMessage(string.Format("Read packet. Type: {0}, Param: {1}",
                                packet.Method,
                                string.Join(",", packet.Data.Select(b => b.ToString("X2")))),
                                "Stealth.Network");

                        ProcessPacket(packet);
                    }
                }
                else
                    Thread.Sleep(10);
            }

            token.ThrowIfCancellationRequested();
        }

        private void ProcessPacket(Packet packet)
        {
            switch (packet.Method)
            {
                case PacketType.SCZero:
                    break;
                case PacketType.SCReturnValue:
                    _replyes.Enqueue(packet);
                    break;

                case PacketType.SCScriptDLLTerminate:
                    new Task(OnTerminateRecieve).Start();
                    break;

                case PacketType.SCPauseResumeScript:
                    new Task(OnStartStopRevieve).Start();
                    break;

                case PacketType.SCExecEventProc:
                    var eventType = (EventTypes)packet.Data[0];
                    var paramCount = packet.Data[1];

                    ArrayList parameters = new ArrayList();
                    using (var stream = new MemoryStream(packet.Data, 2, packet.DataLength - 2))
                    using (var reader = new BinaryReader(stream))
                    {
                        while (stream.Position < stream.Length - 1)
                        {
                            var type = reader.ReadByte();
                            switch ((DataType)type)
                            {
                                case DataType.parUnicodeString:
                                    var size = (int)reader.ReadUInt32();
                                    parameters.Add(Encoding.Unicode.GetString(reader.ReadBytes(size * 2)));
                                    break;
                                case DataType.parInteger:
                                    parameters.Add(reader.ReadInt32());
                                    break;
                                case DataType.parCardinal:
                                    parameters.Add(reader.ReadUInt16());
                                    break;
                                case DataType.parBoolean:
                                    parameters.Add(reader.ReadBoolean());
                                    break;
                                case DataType.parWord:
                                    parameters.Add(reader.ReadInt16());
                                    break;
                                case DataType.parByte:
                                    parameters.Add(reader.ReadByte());
                                    break;
                            }
                        }
                    }


                    ExecEventProcData data = new ExecEventProcData(eventType, parameters);
                    new Task(() => OnServerEventRecieve(data)).Start();
                    break;
                default:
                    throw new Exception("Recieve unknown packet. ID: " + (ushort)packet.Method);
            }
        }

        private void OnTerminateRecieve()
        {
            var handler = TerminateRecieve;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void OnStartStopRevieve()
        {
            var handler = StartStopRecieve;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void OnServerEventRecieve(ExecEventProcData data)
        {
            var handler = ServerEventRecieve;
            if (handler != null)
                handler(this, new ServerEventArgs(data));
        }

        private void SendPacket(Packet packet)
        {
            while (_writer == null || !_writer.BaseStream.CanWrite)
                Thread.Sleep(10);

            if (Stealth.EnableTracing)
                Stealth.AddTraceMessage(string.Format("Send packet. Type: {0}, Param: {1}",
                packet.Method,
                string.Join(",", packet.Data.Select(b => b.ToString("X2")))),
                "Stealth.Network");
            var pb = packet.GetBytes();
            var lb = BitConverter.GetBytes(pb.Length).Reverse().ToArray();
            _writer.Write(lb);
            _writer.Write(pb);
            _writer.Flush();
        }

        internal void SendPacket(PacketType packetType, params object[] parameters)
        {
            lock (this)
            {
                var packet = new Packet { Method = packetType };
                foreach (var p in parameters)
                {
                    packet.AddParameter(p);
                }
                packet.DataLength = (short)packet.Data.Length;
                SendPacket(packet);
            }
        }

        internal T SendPacket<T>(PacketType packetType, params object[] parameters)
        {
            lock (this)
            {
                var packet = new Packet { Method = packetType };
                foreach (var p in parameters)
                    packet.AddParameter(p);

                packet.DataLength = (short)packet.Data.Length;
                SendPacket(packet);
                return WaitReply<T>(packetType);
            }
        }

        private T WaitReply<T>(PacketType type)
        {
            Packet packet;
            ushort replyMethod;
            do
            {
                while (_replyes.Count == 0)
                    Thread.Sleep(10);
                packet = _replyes.Dequeue();
                replyMethod = BitConverter.ToUInt16(packet.Data, 0);
            } while (replyMethod != (ushort)type);
            if (typeof(T).IsValueType && Type.GetTypeCode(typeof(T)) != TypeCode.DateTime)
                return packet.Data.Skip(2).ToArray().MarshalToObject<T>();
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.String:
                    {
                        var len = BitConverter.ToUInt32(packet.Data, 2);
                        return (T)(object)Encoding.Unicode.GetString(packet.Data.Skip(6).ToArray());
                    }
                case TypeCode.DateTime:
                    return (T)(object)BitConverter.ToDouble(packet.Data, 2).ToDateTime();
                default:
                    if (typeof(T).IsArray)
                    {
                        var elementType = typeof(T).GetElementType();
                        var barray = packet.Data.Skip(2).ToArray();
                        if (barray == null || barray.Length == 0)
                            return default(T);
                        var uarray = (T)Activator.CreateInstance(typeof(T), barray.Length / Marshal.SizeOf(elementType));
                        Buffer.BlockCopy(barray, 0, uarray as Array, 0, barray.Length);
                        return uarray;
                    }
                    if (typeof(T).IsGenericType && typeof(T).GetInterfaces().Any(i => i == typeof(IList)))
                    {
                        var barray = packet.Data.Skip(2).ToArray();
                        var result = Activator.CreateInstance<T>();
                        var elementType = typeof(T).GetGenericArguments()[0];

                        if (barray == null || barray.Length == 0)
                            return (T)Activator.CreateInstance(typeof(T));
                        if (elementType.IsPrimitive)
                        {
                            var uarray = Array.CreateInstance(elementType, barray.Length / Marshal.SizeOf(elementType));
                            Buffer.BlockCopy(barray, 0, uarray, 0, barray.Length);

                            foreach (var item in uarray)
                            {
                                (result as IList).Add(item);
                            }
                        }
                        else if (Type.GetTypeCode(elementType) == TypeCode.String)
                        {
                            var len = BitConverter.ToUInt32(barray, 0);
                            var str = Encoding.Unicode.GetString(barray.Skip(4).ToArray());
                            str.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                                .ToList()
                                .ForEach(s => (result as IList).Add(s));
                        }
                        else
                        {
                            uint itemCount;
                            switch (type)
                            {
                                case PacketType.SCReadStaticsXY:
                                    itemCount = (uint)barray.Length / 9;
                                    break;
                                case PacketType.SCGetBuffBarInfo:
                                    itemCount = barray[0];
                                    barray = barray.Skip(1).ToArray();
                                    break;
                                case PacketType.SCGetPathArray:
                                case PacketType.SCGetPathArray3D:
                                    itemCount = barray[0];
                                    break;
                                default:
                                    itemCount = BitConverter.ToUInt32(barray, 0);
                                    barray = barray.Skip(4).ToArray();
                                    break;
                            }

                            if (elementType.GetInterfaces().Any(intf => intf == typeof(IDeserialized)))
                            {
                                using (var str = new MemoryStream(barray))
                                using (var br = new BinaryReader(str))
                                {
                                    while (str.Position < str.Length)
                                    {
                                        var el = br.MarshalToObject(elementType);
                                        (result as IList).Add(el);
                                    }
                                }
                            }
                            else
                            {
                                var itemSize = Marshal.SizeOf(elementType);
                                if (itemCount > 0)
                                {
                                    var uarray =
                                        barray.SplitN(itemSize)
                                            .Select(b => b.ToArray().MarshalToObject(elementType))
                                            .ToList();
                                    uarray.ForEach(el => (result as IList).Add(el));
                                }
                            }
                        }
                        return result;
                    }
                    throw new InvalidOperationException(string.Format("Type '{0}' not supported.", typeof(T)));
            }
        }
    }
}