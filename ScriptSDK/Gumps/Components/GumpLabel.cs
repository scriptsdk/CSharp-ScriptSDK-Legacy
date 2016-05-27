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
    /// GumpLabel component exposes a Panel, the player can read.<br/>
    /// This component is a pseudo equivalent to a windows form component but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// </summary>
    public sealed class GumpLabel : IGumpComponent
    {
        internal GumpLabel(Gump owner, GumpText ct)
        {
            Location = new Point2D(ct.X, ct.Y);
            Size = new Size(-1, -1);
            Color = ct.Color;
            Page = ct.Page;
            ElementID = ct.ElemNum;
            Text = owner.RawText.Count > ct.TextId ? owner.RawText[ct.TextId] : string.Empty;
        }

        internal GumpLabel(Gump owner, CroppedText ct)
        {
            Location = new Point2D(ct.X, ct.Y);
            Size = new Size(ct.Height, ct.Width);
            Color = ct.Color;
            Page = ct.Page;
            ElementID = ct.ElemNum;
            Text = owner.RawText.Count > ct.TextId ? owner.RawText[ct.TextId] : string.Empty;
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

        /// <summary>
        /// Stores the Parsed Text.
        /// </summary>
        public string Text { get; private set; }
    }
}