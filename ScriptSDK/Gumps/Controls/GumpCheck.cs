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
    /// GumpCheckBox control exposes a checkbox, the player can push when he want perform certain actions<br/>
    /// This control is a pseudo equivalent to a windows form control but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// </summary>
    public sealed class GumpCheckBox : IGumpControl
    {
        internal GumpCheckBox(Gump owner, CheckBox cb)
        {
            Location = new Point2D(cb.X,cb.Y);
            Graphic = new Graphic(cb.PressedId,cb.ReleasedId);
            PacketValue = cb.ReturnValue;
            Page = cb.Page;
            ElementID = cb.ElemNum;
            Checked = cb.Status > 0;
            Serial = owner.Serial;
            Owner = owner;
        }

        /// <summary>
        /// Click function performs an update onto gump and expose further actions which are controlled by server.<br/>
        /// Function validates if the checkbox is still valid (serial of generator is still intact) and then performs a dynamic<br/>
        /// click action regardless of layered index.
        /// </summary>
        /// <returns></returns>
        public bool Click(bool state)
        {
            var index = Gump.GetGumpIndex(Owner.GumpType);
            return Events.InvokeOnGumpReply(Owner, new GumpReplyEventArgs(this, (index >= 0) && Stealth.Client.NumGumpCheckBox((ushort)index, PacketValue, state ? 1 : 0)));
        }

        /// <summary>
        /// Stores if Checkbox is checked or not.
        /// </summary>
        public bool Checked { get; private set; }

        /// <summary>
        /// Stores how many pages this gump.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        public Point2D Location { get; private set; }
        
        /// <summary>
        /// Stores the Graphic ID of pressed and released control.
        /// </summary>
        public Graphic Graphic { get; private set; }
        
        /// <summary>
        /// Stores packet value of control, which is only exposed for debug research. The control will handle actions standalone.
        /// </summary>
        public int PacketValue { get; private set; }

        /// <summary>
        /// Stores ElementID which expose the unique queue number of element.
        /// </summary>
        public int ElementID { get; private set; }

        private Gump Owner { get; set; }

        private Serial Serial { get; set; }
    }
}