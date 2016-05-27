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
    /// GumpImage component exposes a PictureBox, the player can read.<br/>
    /// This component is a pseudo equivalent to a windows form component but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// </summary>
    public sealed class GumpImage
    {
        internal GumpImage(GumpPic gp)
        {
            Location = new Point2D(gp.X, gp.Y);
            GraphicID = gp.Id;
            Color = gp.Hue;
            Page = gp.Page;
            ElementID = gp.ElemNum;
        }

        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        public Point2D Location { get; private set; }

        /// <summary>
        /// Stores graphic index ID of gump graphic.
        /// </summary>

        public int GraphicID { get; private set; }
        /// <summary>
        /// Stores the color of component (0 = no color!).
        /// </summary>

        public int Color { get; private set; }
        /// <summary>
        /// Stores how many pages this gump.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Stores the unique elementID of component.
        /// </summary>
        public int ElementID { get; private set; }
    }
}