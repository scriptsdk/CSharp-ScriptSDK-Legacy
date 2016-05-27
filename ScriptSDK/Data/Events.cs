// /*
// ███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
// ██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
// ███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
// ╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
// ███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
// ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
// */

using System;
using ScriptSDK.Attributes;
using ScriptSDK.Gumps;
using StealthAPI;
using ContextMenuEntry = ScriptSDK.ContextMenus.ContextMenuEntry;

namespace ScriptSDK.Data
{
    /// <summary>
    /// Events class expose event handlers for customize event handling.<br/>
    /// Be aware that those events can be delegated by Stealth client or native written in .NET.
    /// </summary>
    public static class Events
    {
        #region Gumps
        /// <summary>
        /// Event handler describes the interaction wich will be fired, whenever a gump control has been successful<br/>
        /// triggered. The event arguments expose different informations about the handler which allow to design additional customizing.<br/>
        /// Event is written native in .NET and doesnt call delegates by stealth as base.
        /// </summary>
        public static event EventHandler<GumpReplyEventArgs> OnGumpReply;

        internal static bool InvokeOnGumpReply(Gump sender, GumpReplyEventArgs e)
        {
            if (OnGumpReply != null)
                OnGumpReply(sender, e);
            return e.State;
        }
        #endregion




        /// <summary>
        /// Function resets all assigned event handlers.
        /// </summary>
        public static void Reset()
        {
            OnGumpReply = null;
        }
    }

    /// <summary>
    /// Event arguments for skill handler events.
    /// </summary>
    public class SkillEventArgs : EventArgs
    {
        /// <summary>
        /// Stores the used skill, regardless of result.
        /// </summary>
        public UseableSkill Skill { get; set; }
        /// <summary>
        /// Stores the result of handler.
        /// </summary>
        public bool State { get; set; }
    }

    /// <summary>
    /// Event arguments for virtues handler events
    /// </summary>
    public class VirtuesEventArgs : EventArgs
    {
        /// <summary>
        /// Stores the used virtue, regardless of result.
        /// </summary>
        public Virtue Virtue { get; set; }
        /// <summary>
        /// Stores the result of handler
        /// </summary>
        public bool State { get; set; }
    }

    /// <summary>
    /// Event arguments for script logger events
    /// </summary>
    public class ScriptLoggerArgs : EventArgs
    {
        /// <summary>
        /// Stores Text passed by Scriptlogger
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Returns state if it used Write or WriteLine.
        /// </summary>
        public bool full { get; set; }
    }

    /// <summary>
    /// Event arguments for targethelper events.
    /// </summary>
    public class TargetReplyEventArgs : EventArgs
    {
        /// <summary>
        /// Stores the target type.
        /// </summary>
        public TargetType ReplyType { get; set; }

        /// <summary>
        /// Stores the handler type.
        /// </summary>
        public TargetActionType ActionType { get; set; }

        /// <summary>
        /// Stores the result of handler.
        /// </summary>
        public bool State { get; set; }
    }

    /// <summary>
    /// Event arguments for context menu events
    /// </summary>
    public class ContextMenuEventArgs : EventArgs
    {
        /// <summary>
        /// Stores reference of last called context menu entry.
        /// </summary>
        public ContextMenuEntry Entry { get; set; }

        /// <summary>
        /// Stores the result of handler.
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// Stores the entry index of selected context menu entry
        /// </summary>
        public int EntryIndex { get; set; }

#pragma warning disable 1591
        public ContextMenuEventArgs()
#pragma warning restore 1591
        {
            EntryIndex = -1;
        }
    }

    /// <summary>
    /// Event arguments for gump events
    /// </summary>
    public class GumpReplyEventArgs : EventArgs
    {
        /// <summary>
        /// Stores reference of controll which caused the <b>OnGumpReply</b> event.
        /// </summary>
        public IGumpControl Control { get; protected set; }

        /// <summary>
        /// Stores the result of handler.
        /// </summary>
        public bool State { get; protected set; }

        internal GumpReplyEventArgs(IGumpControl control, bool state)
        {
            Control = control;
            State = state;
        }
    }
}