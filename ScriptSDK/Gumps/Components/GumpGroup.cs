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
    /// GumpAlphaRegion component exposes a Panel, the player can read.<br/>
    /// This component is a pseudo equivalent to a windows form component but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// </summary>
    public sealed class GumpGroup : IGumpComponent
    {
        internal GumpGroup(Group g)
        {
            ID = g.GroupNumber;
            Page = g.Page;
            ElementID = g.ElemNum;
        }

        internal GumpGroup(EndGroup g)
        {
            ID = g.GroupNumber;
            Page = g.Page;
            ElementID = g.ElemNum;
        }

        /// <summary>
        /// Stores group ID.
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Describes, on which page the component is layered.
        /// </summary>
        public int Page { get; private set; }
        /// <summary>
        /// Stores the unique elementID of component.
        /// </summary>
        public int ElementID { get; private set; }
    }
}