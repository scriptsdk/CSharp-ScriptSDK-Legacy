// /*
// ███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
// ██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
// ███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
// ╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
// ███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
// ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
// */

using ScriptSDK.Data;
using StealthAPI;

namespace ScriptSDK.Gumps
{
    /// <summary>
    /// GumpTooltip component exposes a Tooltip, the player can read.<br/>
    /// This component is a pseudo equivalent to a windows form component but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// </summary>
    public sealed class GumpTooltip : IGumpComponent
    {
        internal GumpTooltip(Tooltip t)
        {
            ClilocID = t.Cliloc_ID;
            Page = t.Page;
            ElementID = t.ElemNum;
        }
        
        private Serial Serial { get; set; }
        /// <summary>
        /// Stored ClilocID of ToolTip.
        /// </summary>
        public uint ClilocID { get; private set; }
        /// <summary>
        /// Stored Page.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Stored ElementID.
        /// </summary>
        public int ElementID { get; private set; }
    }
}