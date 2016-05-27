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
    /// GumpBackGround component exposes a Panel, the player can read.<br/>
    /// This component is a pseudo equivalent to a windows form component but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// </summary>
    public sealed class GumpBackGround : IGumpComponent
    {
        internal GumpBackGround(ResizePic bg)
        {
            Location = new Point2D(bg.X, bg.Y);
            Size = new Size(bg.Height, bg.Width);
            GumpID = bg.GumpId;
            Page = bg.Page;
            ElementID = bg.ElemNum;
        }

        internal GumpBackGround(GumpPicTiled bg)
        {
            Location = new Point2D(bg.X, bg.Y);
            Size = new Size(bg.Height, bg.Width);
            GumpID = bg.GumpId;
            Page = bg.Page;
            ElementID = bg.ElemNum;
        }

        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        public Point2D Location { get; private set; }

        /// <summary>
        /// Stores size of control.
        /// </summary>
        public Size Size { get; private set; }

        /// <summary>
        /// Stores the logical gump graphic wich the parser behind used to build the background on client side.
        /// </summary>
        public int GumpID { get; private set; }

        /// <summary>
        /// Stores page layer of component.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Stores unique elementID.
        /// </summary>
        public int ElementID { get; private set; }
    }
}