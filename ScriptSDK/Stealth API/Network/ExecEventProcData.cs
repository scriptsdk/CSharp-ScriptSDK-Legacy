using System.Collections;
#pragma warning disable 1591

namespace StealthAPI
{
    internal class ExecEventProcData
    {
        internal ExecEventProcData()
        {
        }

        internal ExecEventProcData(byte eventCode, EventTypes eventType, ArrayList param)
        {
            EventCode = eventCode;
            EventType = eventType;
            Parameters = param;
        }

        internal byte EventCode { get; private set; }
        internal EventTypes EventType { get; private set; }
        internal ArrayList Parameters { get; private set; }
    }
}