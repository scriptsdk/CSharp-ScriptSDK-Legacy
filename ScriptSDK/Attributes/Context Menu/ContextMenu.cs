using System;
using System.Collections.Generic;
using System.Linq;
using ScriptSDK.Configuration;
using ScriptSDK.Data;
using ScriptSDK.Engines;
using StealthAPI;

namespace ScriptSDK.ContextMenus
{
    /// <summary>
    /// Object exposes handling for context menus. This class is commonly attached to entities.
    /// </summary>
    public class ContextMenu
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="owner"></param>
        public ContextMenu(UOEntity owner)
        {
            Entries = new List<ContextMenuEntry>();
            Owner = owner;
        }

        /// <summary>
        /// Stores the refered owner.
        /// </summary>
        protected UOEntity Owner { get; set; }

        /// <summary>
        /// Stores the parsed entries of context menu attachments.
        /// </summary>
        public List<ContextMenuEntry> Entries { get; private set; }

        /// <summary>
        /// Event Handler for context menu.
        /// </summary>
        public event EventHandler<ContextMenuEventArgs> OnClick;

        /// <summary>
        /// Performs a single click onto context menu.
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="strict"></param>
        /// <returns></returns>
        public bool Click(string Text, bool strict = true)
        {
            var e = Entries;

            if (Text.Trim().Equals(string.Empty))
                return false;
            if (!strict)
                Text = Text.ToLower();
            if (e == null)
                return false;
            if (e.Count < 1)
                return false;

            foreach (var a in e)
            {
                var t = a.Text;
                if (!strict)
                    t = t.ToLower();
                if (t.Equals(Text))
                    return Click(a);
            }
            return false;
        }

        /// <summary>
        /// Performs a single click onto context menu.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool Click(ContextMenuEntry entry)
        {
            if (Owner == null)
                return false;

            var LastUser = ContextOptions.AssignedObject.Value;

            if (!LastUser.Equals(Owner.Serial.Value))
                return OnUse(new ContextMenuEventArgs { Entry = entry, State = false });

            var i = Owner.ContextMenu.Entries.IndexOf(entry);
            return OnUse(new ContextMenuEventArgs { Entry = entry, State = (i > -1), EntryIndex = i });
        }

        /// <summary>
        /// Performs a single click onto context menu.
        /// </summary>
        /// <param name="ClilocID"></param>
        /// <returns></returns>
        public bool Click(uint ClilocID)
        {
            foreach (var e in Entries.Where(e => e.ClilocID.Equals(ClilocID)))
            {
                return Click(e);
            }
            return OnUse(new ContextMenuEventArgs { Entry = new ContextMenuEntry(string.Empty, this), State = false });
        }

        /// <summary>
        /// Deletes old Context Attachment and generate a new attachment, then parsing the context and store it to the corrosponding object.
        /// </summary>
        /// <returns></returns>
        public bool Parse()
        {
            if (ContextOptions.AssignedObject == null)
                return false;

            Entries.Clear();

            if (Owner == null)
                return false;

            var LastUser = ContextOptions.AssignedObject;

            if (!LastUser.Value.Equals(0) && !LastUser.Equals(Owner.Serial))
                return false;

            Stealth.Client.ClearContextMenu();
            Entries = new List<ContextMenuEntry>();
            if (Owner == null)
                return false;

            Stealth.Client.RequestContextMenu(Owner.Serial.Value);

            if (ContextOptions.ParserDelay > 0)
                Stealth.Client.Wait(ContextOptions.ParserDelay);

            var list = Stealth.Client.GetContextMenu();
            if (!list.Contains("\r\n"))
            {
                const string Code =
                    "ContextMenu Parsing Error!\nFollowing choices could solve the issue:\n* Increase Parser Delay\n* visit https://bitbucket.org/Stealthadmin/stealth-beta-client/issue/11/70411-update";

                ScriptLogger.WriteLine(Code);

                return false;
            }

            list = list.Replace("\r\n", ContextOptions.ParserSymbol.ToString());

            var s = list.Split(ContextOptions.ParserSymbol);

            Entries.Clear();

            foreach (var e in s)
                Entries.Add(new ContextMenuEntry(e, this));

            ContextOptions.AssignedObject = Owner.Serial;

            return true;
        }

        /// <summary>
        /// Function returns the refered owner.
        /// </summary>
        /// <returns></returns>
        public UOEntity GetOwner()
        {
            return new UOEntity(Owner.Serial.Value);
        }

        /// <summary>
        /// Function handles internal the Click + Event.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual bool OnUse(ContextMenuEventArgs e)
        {
            if (e.State)
            {
                var lu = ContextOptions.AssignedObject;

                if (Owner == null || !Owner.Serial.Value.Equals(lu.Value))
                    e.State = false;

                if ((!e.Entry.Flags.Equals(CMEFlags.Disabled)) && (e.State))
                {
                    Stealth.Client.ClearContextMenu();
                    Stealth.Client.SetContextMenuHook(lu.Value, (byte)e.EntryIndex);
                    Stealth.Client.RequestContextMenu(lu.Value);
                    if (ContextOptions.ParserDelay > 0)
                        Stealth.Client.Wait(ContextOptions.ParserDelay);
                    Stealth.Client.ClearContextMenu();
                    Stealth.Client.RequestContextMenu(0);
                    Stealth.Client.SetContextMenuHook(0, 0);
                }
                else
                    e.State = false;
            }
            var handler = OnClick;
            if (handler != null)
            {
                handler(this, e);
            }
            return e.State;
        }
    }
}