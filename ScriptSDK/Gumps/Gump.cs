﻿// /*
// ███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
// ██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
// ███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
// ╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
// ███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
// ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
// */

using System;
using System.Collections.Generic;
using System.Linq;
using ScriptSDK.Data;
using StealthAPI;

namespace ScriptSDK.Gumps
{
    /// <summary>
    /// The class Gump is an convenient class for handling the complexity of native UO-client gumps. The user <b>must</b> understand that gumps are <br/>
    /// server dictated/controlled user interfaces which allow very less but powerful actions through interface actions.<br/>
    /// Components and controls of a gump <b>must never</b> be manually initiliased, they are generated by the object parser of this class.<br/>
    /// One more side note : Components are read only, controls have custom actions!
    /// </summary>
    public sealed class Gump
    {
        internal Gump(GumpInfo g)
        {
            GumpType = g.GumpID;
            Serial = new Serial(g.Serial);
            Location = new Point2D(g.X, g.Y);
            Pages = g.Pages;
            Movable = !g.NoMove;
            Resizeable = !g.NoResize;
            Disposeable = !g.NoDispose;
            Closeable = !g.NoClose;


            RawText = new List<string>();
            if (g.Text != null)
                foreach (var e in g.Text)
                    RawText.Add(e);

            Groups = new List<GumpGroup>();
            if (g.Groups != null)
                foreach (var e in g.Groups)
                    Groups.Add(new GumpGroup(e));
            if (g.EndGroups != null)
                foreach (var e in g.EndGroups)
                    Groups.Add(new GumpGroup(e));

            BackGrounds = new List<GumpBackGround>();
            if (g.ResizePics != null)
                foreach (var e in g.ResizePics)
                    BackGrounds.Add(new GumpBackGround(e));
            if (g.GumpPicTiled != null)
                foreach (var e in g.GumpPicTiled)
                    BackGrounds.Add(new GumpBackGround(e));

            AlphaRegions = new List<GumpAlphaRegion>();
            if (g.CheckerTrans != null)
                foreach (var e in g.CheckerTrans)
                    AlphaRegions.Add(new GumpAlphaRegion(e));

            Buttons = new List<GumpButton>();
            if (g.ButtonTileArts != null)
                foreach (var e in g.ButtonTileArts)
                    Buttons.Add(new GumpButton(this, e));
            if (g.GumpButtons != null)
                foreach (var e in g.GumpButtons)
                    Buttons.Add(new GumpButton(this, e));

            Checkboxes = new List<GumpCheckBox>();
            if (g.CheckBoxes != null)
                foreach (var e in g.CheckBoxes)
                    Checkboxes.Add(new GumpCheckBox(this, e));

            RadioButtons = new List<GumpRadioButton>();
            if (g.RadioButtons != null)
                foreach (var e in g.RadioButtons)
                    RadioButtons.Add(new GumpRadioButton(this, e));

            HTMLTexts = new List<GumpHtml>();
            if (g.HtmlGump != null)
                foreach (var e in g.HtmlGump)
                    HTMLTexts.Add(new GumpHtml(this, e));

            HTMLLocalizedTexts = new List<GumpHtmlLocalized>();
            if (g.XmfHtmlGump != null)
                foreach (var e in g.XmfHtmlGump)
                    HTMLLocalizedTexts.Add(new GumpHtmlLocalized(e));
            if (g.XmfHTMLGumpColor != null)
                foreach (var e in g.XmfHTMLGumpColor)
                    HTMLLocalizedTexts.Add(new GumpHtmlLocalized(e));
            if (g.XmfHTMLTok != null)
                foreach (var e in g.XmfHTMLTok)
                    HTMLLocalizedTexts.Add(new GumpHtmlLocalized(e));

            TextEdits = new List<GumpTextEdit>();
            if (g.TextEntries != null)
                foreach (var e in g.TextEntries)
                    TextEdits.Add(new GumpTextEdit(this, e));
            if (g.TextEntriesLimited != null)
                foreach (var e in g.TextEntriesLimited)
                    TextEdits.Add(new GumpTextEdit(this, e));

            Images = new List<GumpImage>();
            if (g.TextEntriesLimited != null)
                foreach (var e in g.GumpPics)
                    Images.Add(new GumpImage(e));

            Labels = new List<GumpLabel>();
            if (g.GumpText != null)
                foreach (var e in g.GumpText)
                    Labels.Add(new GumpLabel(this, e));

            if (g.CroppedText != null)
                if (g.CroppedText.Length > 0)
                    foreach (var e in g.CroppedText)
                        Labels.Add(new GumpLabel(this, e));

            Items = new List<GumpItem>();
            if (g.TilePics != null)
                foreach (var e in g.TilePics)
                    Items.Add(new GumpItem(e));
            if (g.TilePicHue != null)
                foreach (var e in g.TilePicHue)
                    Items.Add(new GumpItem(e));

            Tooltips = new List<GumpTooltip>();
            if (g.Tooltips != null)
                foreach (var e in g.Tooltips)
                    Tooltips.Add(new GumpTooltip(e));

            ItemProperties = new List<GumpItemProperty>();
            if (g.ItemProperties != null)
                foreach (var e in g.ItemProperties)
                    ItemProperties.Add(new GumpItemProperty(e));
        }

        /// <summary>
        /// Stores unique type generated by server compiler.
        /// Same gump classes will send same gump type, like crafting : Smith, tailor and more will send same gump type.
        /// </summary>
        public uint GumpType { get; private set; }

        /// <summary>
        /// Stores unique Serial of instanced gump.
        /// </summary>
        public Serial Serial { get; private set; }


        /// <summary>
        /// Stores coords which expose location of element.
        /// </summary>
        public Point2D Location { get; private set; }

        /// <summary>
        /// Stores how many pages this gump 
        /// </summary>
        public int Pages { get; private set; }

        /// <summary>
        /// Stores if the gump is moveable on the graphical client.
        /// </summary>
        public bool Movable { get; private set; }

        /// <summary>
        /// Stores if the gump can be resized.
        /// </summary>
        public bool Resizeable { get; private set; }

        /// <summary>
        /// Stores if the gump is disposeable.
        /// </summary>
        public bool Disposeable { get; private set; }

        /// <summary>
        /// Stores if the gump can be closed via right click(true) or requires packet value as button reply(false).
        /// </summary>
        public bool Closeable { get; private set; }

        /// <summary>
        /// Stores all "Groupbox" components.
        /// </summary>
        public List<GumpGroup> Groups { get; private set; }

        /// <summary>
        /// Stores all "Background" components.
        /// </summary>
        public List<GumpBackGround> BackGrounds { get; private set; }

        /// <summary>
        /// Stores all "Panel" components.
        /// </summary>
        public List<GumpAlphaRegion> AlphaRegions { get; private set; }

        /// <summary>
        /// Stores all "Button" controls.
        /// </summary>
        public List<GumpButton> Buttons { get; private set; }

        /// <summary>
        /// Stores all "Checkbox" controls.
        /// </summary>
        public List<GumpCheckBox> Checkboxes { get; private set; }

        /// <summary>
        /// Stores all "RadioButton" controls.
        /// </summary>
        public List<GumpRadioButton> RadioButtons { get; private set; }

        /// <summary>
        /// Stores all "HTML-Memo" components.
        /// </summary>
        public List<GumpHtml> HTMLTexts { get; private set; }

        /// <summary>
        /// Stores all "Localized HTML-Memo" components.
        /// </summary>
        public List<GumpHtmlLocalized> HTMLLocalizedTexts { get; private set; }

        /// <summary>
        /// Stores all "TextEdit" controls.
        /// </summary>
        public List<GumpTextEdit> TextEdits { get; private set; }

        /// <summary>
        /// Stores all "Gump Picture" components.
        /// </summary>
        public List<GumpImage> Images { get; private set; }

        /// <summary>
        /// Stores all "Label" components.
        /// </summary>
        public List<GumpLabel> Labels { get; private set; }

        /// <summary>
        /// Stores all "Item Picture" components.
        /// </summary>
        public List<GumpItem> Items { get; private set; }

        /// <summary>
        /// Stores all "Tooltip" components.
        /// </summary>
        public List<GumpTooltip> Tooltips { get; private set; }

        /// <summary>
        /// Stores all "Item Property" components.
        /// </summary>
        public List<GumpItemProperty> ItemProperties { get; private set; }

        internal List<string> RawText { get; set; }

        /// <summary>
        /// Returns index of active object by serial or -1 when no longer valid.
        /// </summary>
        public int Index
        {
            get
            {
                return GetGumpIndex(Serial);
            }
        }

        /// <summary>
        /// Function tries to close gump but returns false.
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            var index = Index;
            if (index < 0)
                return true;
            if (Closeable)
                Stealth.Client.CloseSimpleGump((ushort)index);
            else
                return Stealth.Client.NumGumpButton((ushort)index, 0);
            return true;
        }

        /// <summary>
        /// Adds serial to ignore list for further gump actions.
        /// </summary>
        public void IgnoreSerial()
        {
            Stealth.Client.AddGumpIgnoreBySerial(Serial.Value);
        }

        /// <summary>
        /// Adds gump type to ignore list for further gump actions.
        /// </summary>
        public void IgnoreType()
        {
            Stealth.Client.AddGumpIgnoreByID(GumpType);
        }

        #region Static members

        /// <summary>
        /// Returns amount of active gumps regardless of type or serial.
        /// </summary>
        public static uint Count
        {
            get { return Stealth.Client.GetGumpsCount(); }
        }

        /// <summary>
        /// Returns list of all active gump serials regardless of type.
        /// </summary>
        public static List<Serial> Serials
        {
            get
            {
                var c = Count;
                var r = new List<Serial>();
                for (var a = 0; a < c; a++)
                {
                    var t = new Serial(Stealth.Client.GetGumpSerial((ushort)a));
                    if (t.Value > 0)
                        r.Add(t);
                }
                return r;
            }
        }

        /// <summary>
        /// Returns list of all active gump types regardless of serials.
        /// </summary>
        public static List<uint> Types
        {
            get
            {
                var c = Count;
                var r = new List<uint>();
                for (var a = 0; a < c; a++)
                {
                    var t = Stealth.Client.GetGumpID((ushort)a);
                    if (t > 0)
                        r.Add(t);
                }
                return r;
            }
        }

        /// <summary>
        /// Returns list of active gumps regardless of types or serials.
        /// </summary>
        public static List<Gump> ActiveGumps
        {
            get
            {
                uint c = Count;
                List<Gump> info = new List<Gump>();
                for (var a = 0; a < c; a++)
                {
                    //uint serial = Stealth.Client.GetGumpSerial((ushort) a);

                    //Gump t = GetGump(serial);
                    //if ((t != null) && (t.Serial.Value > 0))
                    //    info.Add(t);
                    info.Add(new Gump(Stealth.Client.GetGumpInfo((ushort)a)));
                }
                return info;
            }
        }

        /// <summary>
        /// Scans for active gump with given type and returns a proper object image of it. Or returns null if no result found.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static Gump GetGump(uint Type)
        {
            var index = GetGumpIndex(Type);
            return index > -1 ? new Gump(Stealth.Client.GetGumpInfo((ushort)index)) : null;
        }

        /// <summary>
        /// Scans for active gump with given serial and returns a proper object image of it. Or returns null if no result found.
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public static Gump GetGump(Serial serial)
        {
            var index = GetGumpIndex(serial);
            return index > -1 ? new Gump(Stealth.Client.GetGumpInfo((ushort)index)) : null;
        }

        internal static int GetGumpIndex(Serial serial)
        {
            var count = Count;
            if (count < 1)
                return -1;

            for (ushort i = 0; i < count; i++)
            {
                if (Stealth.Client.GetGumpSerial(i).Equals(serial.Value))
                    return i;
            }
            return -1;
        }

        internal static int GetGumpIndex(uint Type)
        {
            var count = Count;
            if (count < 1)
                return -1;

            for (ushort i = 0; i < count; i++)
            {
                uint id = Stealth.Client.GetGumpID(i);
                var check = id.Equals(Type);

                if (check)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Clears ignorelist for gump types and serial.
        /// </summary>
        public static void ClearIgnoreList()
        {
            Stealth.Client.ClearGumpsIgnore();
        }

        public static bool WaitForGump(string text, double MaxDelay, out Gump gump)
        {
            var start = DateTime.UtcNow;
            var finish = start.AddMilliseconds(MaxDelay);
            bool rstate = false;
            gump = null;
            var cnt = Count;
            do
                for(ushort i = 0; i < cnt;i++)
                {
                    var raw = Stealth.Client.GetGumpTextLines((ushort)(i));
                    if (raw.FirstOrDefault(t => t.Contains(text)) != null)
                    {
                        rstate = true;
                        gump = new Gump( Stealth.Client.GetGumpInfo(i));
                        break;
                    }
                }
            while (!rstate && DateTime.UtcNow < finish);
            return rstate;
        }
        public static bool WaitForGump(string text, double MaxDelay)
        {
            var start = DateTime.UtcNow;
            var finish = start.AddMilliseconds(MaxDelay);
            bool rstate = false;
            do
                foreach(var g in Gump.ActiveGumps)
                {
                    if (g.RawText.FirstOrDefault(t => t.Contains(text)) != null)
                    {
                        rstate = true;
                        break;
                    }
                }                
            while (!rstate && DateTime.UtcNow < finish);
            return rstate;
        }

        /// <summary>
        /// Function describes a process where the user waits for a certain gump type dynamicly but <br/>
        /// for a given timespan. Returns true if this event occured or false if not.
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="MaxDelay"></param>
        /// <returns></returns>
        public static bool WaitForGump(uint Type, double MaxDelay)
        {
            var start = DateTime.UtcNow;
            var finish = start.AddMilliseconds(MaxDelay);
            bool rstate;
            do
                rstate = GetGumpIndex(Type) > -1;
            while (!rstate && DateTime.UtcNow < finish);
            return rstate;
        }

        /// <summary>
        /// Function describes a process where the user waits for a certain gump type dynamicly but <br/>
        /// for a given timespan. Returns true if this event occured or false if not.<br/>
        /// Additional generates a gump object if possible or return null.
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="MaxDelay"></param>
        /// <param name="gump"></param>
        /// <returns></returns>
        public static bool WaitForGump(uint Type, double MaxDelay, out Gump gump)
        {
            var result = WaitForGump(Type, MaxDelay);
            gump = result ? GetGump(Type) : null;
            return result;
        }

        /// <summary>
        /// Function allows to check if a certain gump closes within maxdelay.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="maxdelay"></param>
        /// <returns></returns>
        public static bool WaitForGumpClose(uint type, double maxdelay)
        {
            var start = DateTime.UtcNow;
            var finish = start.AddMilliseconds(maxdelay);
            bool rstate;
            do
            {
                int index = GetGumpIndex(type);
                rstate = index < 0;
            }
            while (!rstate && DateTime.UtcNow < finish);
            return rstate;
        }

        /// <summary>
        /// Function allows to check if a certain gump closes within maxdelay.
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="maxdelay"></param>
        /// <returns></returns>
        public static bool WaitForGumpClose(Serial serial, double maxdelay)
        {
            var start = DateTime.UtcNow;
            var finish = start.AddMilliseconds(maxdelay);
            bool rstate;
            do
                rstate = GetGumpIndex(serial) < 0;
            while (!rstate && DateTime.UtcNow < finish);
            return rstate;
        }
        #endregion

    }
}