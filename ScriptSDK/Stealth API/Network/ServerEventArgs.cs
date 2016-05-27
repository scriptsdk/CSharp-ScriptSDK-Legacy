using System;
#pragma warning disable 1591

namespace StealthAPI
{
    internal class ServerEventArgs : EventArgs
    {
        public ServerEventArgs()
        {
        }

        public ServerEventArgs(ExecEventProcData data)
        {
            Data = data;
        }

        public ExecEventProcData Data { get; set; }
    }
}