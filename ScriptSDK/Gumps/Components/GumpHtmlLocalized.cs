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
    public sealed class GumpHtmlLocalized : IGumpComponent
    {
        internal GumpHtmlLocalized(XmfHTMLGump xg)
        {
            Location = new Point2D(xg.X, xg.Y);
            Size = new Size(xg.Height, xg.Width);
            Args = string.Empty;
            Background = xg.Background > 0;
            Scrolling = xg.Scrollbar > 0;
            Page = xg.Page;
            ElementID = xg.ElemNum;
            Color = 0;
            Args = string.Empty;
        }

        internal GumpHtmlLocalized(XmfHTMLGumpColor xg)
        {
            Location = new Point2D(xg.X, xg.Y);
            Size = new Size(xg.Height, xg.Width);
            Args = string.Empty;
            Background = xg.Background > 0;
            ClilocID = xg.Cliloc_id;
            Scrolling = xg.Scrollbar > 0;
            Page = xg.Page;
            ElementID = xg.ElemNum;
            Color = xg.Hue;
        }

        internal GumpHtmlLocalized(XmfHTMLTok xg)
        {
            Location = new Point2D(xg.X, xg.Y);
            Size = new Size(xg.Height, xg.Width);
            Args = xg.Arguments;
            ClilocID = xg.ClilocId;
            Background = xg.Background > 0;
            Scrolling = xg.Scrollbar > 0;
            Page = xg.Page;
            ElementID = xg.ElemNum;
            Color = xg.Color;
            Args = xg.Arguments;
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
        /// Stores localizeable cliloc data table ID.
        /// </summary>
        public uint ClilocID { get; private set; }

        /// <summary>
        /// Stores if background is visible.
        /// </summary>
        public bool Background { get; private set; }

        /// <summary>
        /// Stores if scroll bars are visible.
        /// </summary>
        public bool Scrolling { get; private set; }

        /// <summary>
        /// Describes, on which page the component is layered.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Stores the unique elementID of component.
        /// </summary>
        public int ElementID { get; private set; }

        /// <summary>
        /// Stores text color.
        /// </summary>
        public int Color { get; private set; }

        /// <summary>
        /// Stores arguments.
        /// </summary>
        public string Args { get; private set; }
    }
}