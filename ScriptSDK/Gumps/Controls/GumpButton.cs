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
    /// GumpButton control exposes a button which the player can click when he wants to perform a certain action.<br/>
    /// This control is a pseudo equivalent to a windows form control but only contains UO suitable properties.<br/>
    /// There are no constructors or generators exposed due the to design of the SDK.<br/>
    /// Use Gump.GetGump(GumpType).Buttons to retrieve a list of GumpButton instances from the specified gump.
    /// </summary>
    public sealed class GumpButton : IGumpControl
    {
        internal GumpButton(Gump owner, ButtonTileArt btn)
        {
            Serial = owner.Serial;
            Location = new Point2D(btn.X, btn.Y);
            ArtLocation = new Point2D(btn.ArtX, btn.ArtY);
            Page = btn.PageId;
            ElementID = btn.ElemNum;
            ArtID = btn.ArtId;
            Graphic = new Graphic(btn.PressedId, btn.ReleasedId);
            PacketValue = btn.ReturnValue;
            QuitID = btn.Quit;
            Color = btn.Hue;
            Owner = owner;
        }

        internal GumpButton(Gump owner, StealthAPI.GumpButton btn)
        {
            Serial = owner.Serial;
            Location = new Point2D(btn.X, btn.Y);
            ArtLocation = new Point2D(-1, -1);
            Page = btn.Page;
            ElementID = btn.ElemNum;
            ArtID = -1;
            Graphic = new Graphic(btn.PressedId, btn.ReleasedId);
            PacketValue = btn.ReturnValue;
            QuitID = btn.Quit;
            Color = -1;
            Owner = owner;
        }

        /// <summary>
        /// Click function performs an update onto gump and expose further actions which are controlled by server.<br/>
        /// Function validates if the button is still valid (serial of generator is still intact) and then performs a dynamic<br/>
        /// click action regardless of layered index.
        /// </summary>
        /// <returns></returns>
        public bool Click()
        {
            var index = Gump.GetGumpIndex(Owner.GumpType);
            return Events.InvokeOnGumpReply(Owner, new GumpReplyEventArgs(this, (index >= 0) && Stealth.Client.NumGumpButton((ushort)index, PacketValue)));
        }

        private Gump Owner { get; set; }

        private Serial Serial { get; set; }

        /// <summary>
        /// Stores how many pages this gump.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        public Point2D Location { get; private set; }

        /// <summary>
        /// Stores location of art, if control inherits from tiled button control, else returns (-1,-1).
        /// </summary>
        public Point2D ArtLocation { get; set; }

        /// <summary>
        /// Stores ID of art, if control inherits from tiled button control, else returns -1.
        /// </summary>
        public int ArtID { get; private set; }

        /// <summary>
        /// Stores ElementID which expose the unique queue number of element.
        /// </summary>
        public int ElementID { get; private set; }

        /// <summary>
        /// Stores packet value of control, which is only exposed for debug research. The control will handle actions standalone.
        /// </summary>
        public int PacketValue { get; private set; }

        /// <summary>
        /// Stores color of art, if control inherits from tiled button control, else returns -1.
        /// </summary>
        public int Color { get; private set; }

        /// <summary>
        /// Purpose is unknown. TODO : Find out.
        /// </summary>
        public int QuitID { get; private set; }

        /// <summary>
        /// Stores the Graphic ID of pressed and released control.
        /// </summary>
        public Graphic Graphic { get; private set; }
    }
}