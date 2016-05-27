using System;
using ScriptSDK.Data;

namespace ScriptSDK.ContextMenus
{
    /// <summary>
    /// Interface for ContextMenuEntry.
    /// </summary>
    public interface ICMEntry
    {
        /// <summary>
        /// Stores the flag of entry.
        /// </summary>
        CMEFlags Flags { get; }
        /// <summary>
        /// Stores the localized TextID
        /// </summary>
        uint ClilocID { get; }
        /// <summary>
        /// Stores the Tag of context entry.
        /// </summary>
        ushort Tag { get; }
        /// <summary>
        /// Stores the text color of entry.
        /// </summary>
        ushort Color { get; }
        /// <summary>
        /// Stores the native text of entry.
        /// </summary>
        string Text { get; }
    }

    /// <summary>
    /// Class expose a design for context menu entries within the context menu system.
    /// </summary>
    public class ContextMenuEntry : ICMEntry
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="owner"></param>
        public ContextMenuEntry(string properties, ContextMenu owner)
        {
            var list = properties.Split('|');
            var parseable = list.Length.Equals(5);

            Text = parseable ? list[2] : "INVALID ENTRY";
            Flags = parseable ? (CMEFlags) Convert.ToUInt16(list[3]) : CMEFlags.Disabled;
            Color = parseable ? Convert.ToUInt16(list[4]) : (ushort) 0;
            Tag = parseable ? Convert.ToUInt16(list[0]) : (ushort) 0;
            ClilocID = parseable ? Convert.ToUInt32(list[1]) : 0;

            _owner = owner;
        }

        /// <summary>
        /// Stores internal the referencing context menu.
        /// </summary>
        protected ContextMenu _owner { get; set; }
        /// <summary>
        /// Stores the flag of entry.
        /// </summary>
        public CMEFlags Flags { get; private set; }
        /// <summary>
        /// Stores the localized TextID
        /// </summary>
        public uint ClilocID { get; private set; }
        /// <summary>
        /// Stores the Tag of context entry.
        /// </summary>
        public ushort Tag { get; private set; }
        /// <summary>
        /// Stores the text color of entry.
        /// </summary>
        public ushort Color { get; private set; }
        /// <summary>
        /// Stores the native text of entry.
        /// </summary>
        public string Text { get; private set; }
    }
}