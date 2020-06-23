using System;
namespace ScriptSDK.Gumps
{
    /// <summary>
    /// OSI based GumpIDs to let you know what gump you are specifically wanting.
    /// </summary>
    /// <example> Gump.GetGump((uint)OSIGumpIDs.Runebook)</example>
    public enum OSIGumpIDs : uint
    {
        /// <summary>
        /// Returns Runebook GumpID
        /// </summary>
        Runebook = 0x0059,
        /// <summary>
        /// Returns Moongate GumpID
        /// </summary>
        Moongate = 0x0258,
    }
}
