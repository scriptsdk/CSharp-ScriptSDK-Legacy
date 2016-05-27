using System.Collections.Generic;
#pragma warning disable 1591

namespace StealthAPI
{
    public struct ContextMenu
    {
        public uint ID { get; set; }
        public byte EntriesNumber { get; set; }
        public bool NewCliloc { get; set; }
        public List<ContextMenuEntry> Entries { get; set; }
    }
}
