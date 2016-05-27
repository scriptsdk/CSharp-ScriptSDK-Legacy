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
    /// GumpRadioButton control exposes a radiobutton, the player can push when he want perform certain actions<br/>
    /// This control is a pseudo equivalent to a windows form control but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// </summary>
    public sealed class GumpRadioButton : IGumpControl
    {
        internal GumpRadioButton(Gump owner, RadioButton rb)
        {
            Serial = owner.Serial;
            Location = new Point2D(rb.X, rb.Y);
            Owner = owner;
            Graphic = new Graphic(rb.PressedId,rb.ReleasedId);
            PacketValue = rb.ReturnValue;
            Page = rb.Page;
            ElementID = rb.ElemNum;
            Checked = rb.Status > 0;
        }

        /// <summary>
        /// Click function performs an update onto gump and expose further actions which are controlled by server.<br/>
        /// Function validates if the radiobutton is still valid (serial of generator is still intact) and then performs a dynamic<br/>
        /// click action regardless of layered index.
        /// </summary>
        /// <returns></returns>
        public bool Click(bool state)
        {
            var index = Gump.GetGumpIndex(Owner.GumpType);
            return Events.InvokeOnGumpReply(Owner, new GumpReplyEventArgs(this, (index >= 0) && Stealth.Client.NumGumpRadiobutton((ushort)index, PacketValue, state ? 1 : 0)));
        }

        /// <summary>
        /// Stores the Graphic ID of pressed and released control.
        /// </summary>
        public Graphic Graphic { get; private set; }

        /// <summary>
        /// Stores if radiobutton is checked or not.
        /// </summary>
        public bool Checked { get; private set; }

        /// <summary>
        /// Stores PageID which expose on which layer of gump the element is assigned.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        public Point2D Location { get; private set; }

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