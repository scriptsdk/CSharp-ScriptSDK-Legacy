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
    /// GumpItem component exposes a PictureBox, the player can read.<br/>
    /// This component is a pseudo equivalent to a windows form component but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// </summary>
    public sealed class GumpItem
    {
        internal GumpItem(TilePicture tp)
        {
            Location = new Point2D(tp.X, tp.Y);
            GraphicID = tp.Id;
            Color = tp.Color;
            Page = tp.Page;
            ElementID = tp.ElemNum;
        }

        internal GumpItem(TilePic tp)
        {
            Location = new Point2D(tp.X, tp.Y);
            GraphicID = tp.Id;
            Color = 0;
            Page = tp.Page;
            ElementID = tp.ElemNum;
        }

        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        public Point2D Location { get; private set; }

        /// <summary>
        /// Stores the graphic ID of object from art.uop\mul datatable.
        /// </summary>
        public int GraphicID { get; private set; }

        /// <summary>
        /// Stores text color.
        /// </summary>
        public int Color { get; private set; }

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