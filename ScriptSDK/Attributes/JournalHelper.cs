using System;
using System.Collections.Generic;
using System.Diagnostics;
using ScriptSDK.Data;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Journalentry class expose handle, actions and properties about single journal messages.
    /// </summary>
    public sealed class JournalEntry
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ApiCall"></param>
        public JournalEntry(bool ApiCall = true)
        {
            Count = ApiCall ? Stealth.Client.GetLineCount() : -1;
            ID = ApiCall ? Stealth.Client.GetLineID() : 0;
            Index = ApiCall ? Stealth.Client.GetLineIndex() : -1;
            MsgType = ApiCall ? Stealth.Client.GetLineMsgType() : int.MaxValue;
            Name = ApiCall ? Stealth.Client.GetLineName() : "";
            Color = ApiCall ? Stealth.Client.GetLineTextColor() : int.MaxValue;
            Font = ApiCall ? Stealth.Client.GetLineTextFont() : int.MaxValue;
            Timestamp = ApiCall ? Stealth.Client.GetLineTime() : DateTime.Now;
            Type = ApiCall ? Stealth.Client.GetLineType() : int.MaxValue;
            Valid = ApiCall;
        }

        /// <summary>
        /// Stores amount of equal lines.
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Stores ID of line.
        /// </summary>
        public uint ID { get; private set; }
        /// <summary>
        /// Stores index of line.
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// Stores message type.
        /// </summary>
        public int MsgType { get; private set; }
        /// <summary>
        /// Stores line type.
        /// </summary>
        public int Type { get; private set; }
        /// <summary>
        /// Stores content of line.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Stores color of text.
        /// </summary>
        public int Color { get; private set; }
        /// <summary>
        /// Stores font ID of line.
        /// </summary>
        public int Font { get; private set; }
        /// <summary>
        /// Stores timestamp of line
        /// </summary>
        public DateTime Timestamp { get; private set; }
        /// <summary>
        /// Stores if entry is valid.
        /// </summary>
        public bool Valid { get; private set; }
    }

    /// <summary>
    /// Class expose handles, actions and properties about journal messager system.
    /// </summary>
    public class JournalHelper
    {
        /// <summary>
        /// returns reference to player.
        /// </summary>
        public PlayerMobile P { get; private set; }

        private JournalHelper(PlayerMobile p)
        {
            P = p;
            _entries = new List<JournalEntry>();
            _dologging = false;
        }

        private static JournalHelper _instance { get; set; }
        private bool _dologging { get; set; }
        private List<JournalEntry> _entries { get; set; }
        /// <summary>
        /// Enable or disable additional logging here.
        /// </summary>
        public bool Logging { get; set; }

        /// <summary>
        /// Returns logged journal entries.
        /// </summary>
        public List<JournalEntry> Logs
        {
            get { return _entries; }
        }

        /// <summary>
        /// Returns the senior index of journal.
        /// </summary>
        public int SeniorIndex
        {
            get { return Stealth.Client.HighJournal(); }
        }

        /// <summary>
        /// Returns the junior index of journal.
        /// </summary>
        public int JuniorIndex
        {
            get { return Stealth.Client.LowJournal(); }
        }

        /// <summary>
        /// Returns last incoming message.
        /// </summary>
        public string LastMessage
        {
            get { return Stealth.Client.LastJournalMessage(); }
        }

        /// <summary>
        /// Returns instance of journal system.
        /// </summary>
        /// <returns></returns>
        public static JournalHelper GetJournal()
        {
            return _instance ?? (_instance = new JournalHelper(PlayerMobile.GetPlayer()));
        }

        /// <summary>
        /// Adds user to ignore list.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="type"></param>
        public void Ignore(string user, JournalType type)
        {
            switch (type)
            {
                case JournalType.Chat:
                    Stealth.Client.AddChatUserIgnore(user);
                    break;
                case JournalType.Journal:
                    Stealth.Client.AddJournalIgnore(user);
                    break;
            }
        }

        /// <summary>
        /// Clears ignore list.
        /// </summary>
        /// <param name="type"></param>
        public void ClearIgnore(JournalType type)
        {
            switch (type)
            {
                case JournalType.Chat:
                    Stealth.Client.ClearChatUserIgnore();
                    break;
                case JournalType.Journal:
                    Stealth.Client.ClearJournalIgnore();
                    break;
            }
        }

        /// <summary>
        /// Function sends message to specific message system.
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="type"></param>
        public void AddMessage(string Msg, JournalType type)
        {
            switch (type)
            {
                case JournalType.Debug:
                    Debug.WriteLine(Msg);
                    break;
                case JournalType.Journal:
                    Stealth.Client.AddToJournal(Msg);
                    break;
                case JournalType.System:
                    Stealth.Client.AddToSystemJournal(Msg);
                    break;
            }
        }

        /// <summary>
        /// Delete passed journal type.
        /// </summary>
        /// <param name="type"></param>
        public void ClearJournal(JournalType type)
        {
            switch (type)
            {
                case JournalType.Debug:
                    Debug.Flush();
                    break;
                case JournalType.Journal:
                    Stealth.Client.ClearJournal();
                    break;
                case JournalType.System:
                    Stealth.Client.ClearSystemJournal();
                    break;
            }
        }

        /// <summary>
        /// Returns text passed through index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetJournalLine(uint index)
        {
            return Stealth.Client.Journal(index);
        }

        /// <summary>
        /// Manipulates journal line by index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="Msg"></param>
        public void SetJournalLine(uint index, string Msg)
        {
            Stealth.Client.SetJournalLine(index, Msg);
        }

        /// <summary>
        /// Function tries to check if a certain journal message appeared within ppassed journal system and returns line as output.
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="content"></param>
        /// <param name="maxdelayinms"></param>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool WaitForJournalLine(DateTime StartTime, string content, int maxdelayinms, JournalType type,
            out JournalEntry result)
        {
            var state = WaitForJournalLine(StartTime, content, maxdelayinms, type);
            result = new JournalEntry(state);
            if (_dologging)
                _entries.Add(result);
            return state;
        }

        /// <summary>
        ///  Function tries to check if a certain journal message appeared within ppassed journal system and returns line ID as output.
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="content"></param>
        /// <param name="maxdelayinms"></param>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool WaitForJournalLine(DateTime StartTime, string content, int maxdelayinms, JournalType type,
            out int result)
        {
            var state = WaitForJournalLine(StartTime, content, maxdelayinms, type);
            result = state ? Stealth.Client.GetFoundedParamID() : -1;
            return state;
        }
        /// <summary>
        ///  Function tries to check if a certain journal message appeared within ppassed journal system.
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="content"></param>
        /// <param name="maxdelayinms"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool WaitForJournalLine(DateTime StartTime, string content, int maxdelayinms, JournalType type)
        {
            switch (type)
            {
                case JournalType.Journal:
                    return Stealth.Client.WaitJournalLine(StartTime, content, maxdelayinms);
                case JournalType.System:
                    return Stealth.Client.WaitJournalLineSystem(StartTime, content, maxdelayinms);
            }
            return false;
        }
        /// <summary>
        /// Function returns if journal contains content. "Result" returns message.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool InJournal(string content, out JournalEntry result)
        {
            var state = InJournal(content);
            result = new JournalEntry(state);
            if (_dologging)
                _entries.Add(result);
            return state;
        }
        /// <summary>
        /// Function returns if journal contains content. "Result" returns line ID.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool InJournal(string content, out int result)
        {
            var state = InJournal(content);
            result = state ? Stealth.Client.GetFoundedParamID() : -1;
            return state;
        }
        /// <summary>
        /// Function returns if journal contains content.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool InJournal(string content)
        {
            return Stealth.Client.InJournal(content) > -1;
        }
        /// <summary>
        /// Function returns if content was in journal between both timestamps.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool InJournal(string content, DateTime start, DateTime end)
        {
            return Stealth.Client.InJournalBetweenTimes(content, start, end) > -1;
        }
        /// <summary>
        /// Function returns if content was in journal between both timestamps. "Result" returns journal message.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool InJournal(string content, DateTime start, DateTime end, out JournalEntry result)
        {
            var state = InJournal(content, start, end);
            result = new JournalEntry(state);
            if (_dologging)
                _entries.Add(result);
            return state;
        }

        /// <summary>
        /// Function returns if content was in journal between both timestamps. "Result" returns line ID.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool InJournal(string content, DateTime start, DateTime end, out int result)
        {
            var state = InJournal(content, start, end);
            result = state ? Stealth.Client.GetFoundedParamID() : -1;
            return state;
        }
    }
}