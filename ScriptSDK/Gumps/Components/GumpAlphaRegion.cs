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
    public sealed class GumpAlphaRegion : IGumpComponent
    {
        internal GumpAlphaRegion(CheckerTrans ar)
        {
            Location = new Point2D(ar.X,ar.Y);
            Size = new Size(ar.Width,ar.Height);
            Page = ar.Page;
            ElementID = ar.ElemNum;
        }

        /// <summary>
        /// Stores size of control.
        /// </summary>
        public Size Size { get; private set; }

        /// <summary>
        /// Describes, on which page the component is layered.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        public Point2D Location { get; private set; }

        /// <summary>
        /// Describes the ElementID wich equals the Queue-Nr. from gump packet in wich order the gump has been generated.
        /// </summary>
        public int ElementID { get; private set; }
    }
}