using System.Collections;
#pragma warning disable 1591

namespace StealthAPI
{
    internal class ExecEventProcData
    {
        internal ExecEventProcData()
        {
        }

        public ExecEventProcData(EventTypes eventType, ArrayList param)
        {
            EventType = eventType;
            Parameters = param;
        }

        public EventTypes EventType { get; private set; }
        public ArrayList Parameters { get; private set; }
    }
}