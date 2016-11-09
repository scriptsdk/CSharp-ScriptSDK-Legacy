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
    /// GumpTextEdit control exposes a TextEdit, the player can read or modify.<br/>
    /// This control is a pseudo equivalent to a windows form control but with uo suitable properties only.<br/>
    /// There are no constructors or generators exposed due the design, that the gump should parse that control.
    /// Whenever the owner gump performs an action through radiobutton, button or checkbox (which cause an update of gump)<br/>
    /// the gump will update text controls internal before sending those actions.
    /// </summary>
    public sealed class GumpTextEdit : IGumpControl
    {
        internal GumpTextEdit(Gump owner, TextEntry c)
        {

            Owner = owner;
            Serial = owner.Serial;
            Location = new Point2D(c.X, c.Y);
            Size = new Size(c.Height, c.Width);
            Color = c.Color;
            TextID = c.DefaultTextId;
            //Text = Owner.RawText.Count > TextID ? Owner.RawText[TextID] : string.Empty;
            //Drabadan edit, due to Text setter sends useless api call to stealth, so using private value in ctor;
            _text = Owner.RawText.Count > TextID ? Owner.RawText[TextID] : string.Empty;
            Limit = -1;
            Page = c.Page;
            ElementID = c.ElemNum;
        }

        internal GumpTextEdit(Gump owner, TextEntryLimited c)
        {
            Owner = owner;
            Serial = owner.Serial;
            Location = new Point2D(c.X, c.Y);
            Size = new Size(c.Height, c.Width);
            Color = c.Color;
            TextID = c.DefaultTextId;
            //Text = Owner.RawText.Count > TextID ? Owner.RawText[TextID] : string.Empty;
            //Drabadan edit, due to Text setter sends useless api call to stealth, so using private value in ctor;
            _text = Owner.RawText.Count > TextID ? Owner.RawText[TextID] : string.Empty;
            Limit = c.Limit;
            Page = c.Page;
            ElementID = c.ElemNum;
        }

        private Gump Owner { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private Serial Serial { get; set; }
        /// <summary>
        /// Stores ElementID which expose the unique queue number of element.
        /// </summary>
        public int ElementID { get; private set; }
        /// <summary>
        /// Stores PageID which expose on which layer of gump the element is assigned.
        /// </summary>
        public int Page { get; private set; }
        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        public Point2D Location { get; private set; }

        /// <summary>
        /// Stores size of control.
        /// </summary>
        public Size Size { get; private set; }

        /// <summary>
        /// Stores text color of text.
        /// </summary>
        public int Color { get; private set; }

        /// <summary>
        /// Stores TextID of control, which is only exposed for debug research. The control will handle actions standalone.
        /// </summary>
        public int TextID { get; private set; }

        private string _text { get; set; }

        /// <summary>
        /// Allows to read and write text content. Will be automated updated via gump.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                if (!Limit.Equals(-1))
                    value = value.Substring(0, Limit);
                var index = Gump.GetGumpIndex(Owner.GumpType);
                if (Events.InvokeOnGumpReply(Owner,
                    new GumpReplyEventArgs(this,
                        (index >= 0) && Stealth.Client.NumGumpTextEntry((ushort)index, TextID, value))))
                    _text = value;
            }
        }

        /// <summary>
        /// Stores the character limit of text. Returns -1 if length is infinite.<br/>
        /// Be aware, that the gump has to cut down text if size greater limit.
        /// </summary>
        public int Limit { get; private set; }
    }
}