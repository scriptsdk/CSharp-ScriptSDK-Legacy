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
    /// GumpHtml component exposes a Panel, the player can read.<br/>
    /// This component is a pseudo equivalent to a windows form component but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// </summary>
    public sealed class GumpHtml : IGumpComponent
    {
        internal GumpHtml(Gump owner,HtmlGump hg)
        {
            Location = new Point2D(hg.X,hg.Y);
            Size = new Size(hg.Height,hg.Width);
            Background = hg.Background > 0;
            Scrolling = hg.Scrollbar > 0;
            Page = hg.Page;
            ElementID = hg.ElemNum;
            Text = owner.RawText.Count > hg.TextId ? owner.RawText[hg.TextId] : string.Empty;
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
        /// Stores if background is visible.
        /// </summary>
        public bool Background { get; private set; }

        /// <summary>
        /// Stores if scrollbars are visible.
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
        /// Stores the Parsed Text.
        /// </summary>
        public string Text { get; private set; }
    }
}