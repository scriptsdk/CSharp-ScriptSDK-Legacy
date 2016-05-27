using System;
using System.Collections.Generic;
using System.Linq;
using StealthAPI;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// ICQClient expose handles, actions and properties of being an ICQ clone and can be used to read or write messages <br/>
    /// while scripting.
    /// </summary>
    public sealed class ICQClient
    {
        private ICQClient()
        {
            EnableLogging = false;
            _userlist = new Dictionary<uint, string>();
            SndMessages = new List<ICQMessage>();
            RcvMessages = new List<ICQMessage>();
        }


        private static ICQClient _instance { get; set; }


        private byte _status { get; set; }

        private byte _xstatus { get; set; }

        private Dictionary<uint, string> _userlist { get; set; }

        /// <summary>
        /// Returns ICQ status.
        /// </summary>
        public byte Status
        {
            get { return _status; }
            set
            {
                Stealth.Client.ICQ_SetStatus(value);
                _status = value;
            }
        }

        /// <summary>
        /// Returns ICQ X-tra status.
        /// </summary>
        public byte XStatus
        {
            get { return _xstatus; }
            set
            {
                Stealth.Client.ICQ_SetStatus(value);
                _xstatus = value;
            }
        }

        /// <summary>
        /// Gets or sets if logging of incoming and outgoing messages is wanted.
        /// </summary>
        public bool EnableLogging { get; set; }
        /// <summary>
        /// Stores outgoing messages.
        /// </summary>
        public List<ICQMessage> SndMessages { get; private set; }
        /// <summary>
        /// Stores incoming messages.
        /// </summary>
        public List<ICQMessage> RcvMessages { get; private set; }

        /// <summary>
        /// Returns if client is connected to server.
        /// </summary>
        public bool Connected
        {
            get { return Stealth.Client.ICQ_GetConnectedStatus(); }
        }

        /// <summary>
        /// Event which occurs when client disconnects from server.
        /// </summary>
        public EventHandler<EventArgs> Event_Disconnect
        {
            set { Stealth.Client.ICQDisconnect += value; }
        }

        /// <summary>
        /// Event which occirs when client connects to server.
        /// </summary>
        public EventHandler<EventArgs> Event_Connect
        {
            set { Stealth.Client.ICQDisconnect += value; }
        }

        /// <summary>
        /// ICQ event which occurs on incoming messages.
        /// </summary>
        public EventHandler<ICQIncomingTextEventArgs> Event_IncomingMessage
        {
            set { Stealth.Client.ICQIncomingText += value; }
        }

        /// <summary>
        /// ICQ event when any error occur.
        /// </summary>
        public EventHandler<ICQErrorEventArgs> Event_Error
        {
            set { Stealth.Client.ICQError += value; }
        }

        /// <summary>
        /// Returns reference to ICQ client.
        /// </summary>
        /// <returns></returns>
        public static ICQClient GetClient()
        {
            return _instance ?? (_instance = new ICQClient());
        }

        /// <summary>
        /// Tries to connect stealth to icq server.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Connect(uint ID, string password)
        {
            if (Connected)
                return false;
            Stealth.Client.ICQ_Connect(ID, password);
            Stealth.Client.Wait(2000);
            return Connected;
        }

        /// <summary>
        /// Adds a user UIN to user list. Note : This only registers user on SDK cache, if you stop application this will be lost.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool RegisterUser(uint ID, string Name)
        {
            if (_userlist == null)
                _userlist = new Dictionary<uint, string>();
            if (_userlist.ContainsKey(ID))
                return false;
            _userlist.Add(ID, Name);
            return true;
        }

        /// <summary>
        /// Performs a disconnection from icq server.
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            if (!Connected)
                return false;
            Stealth.Client.ICQ_Disconnect();
            Stealth.Client.Wait(2000);
            return !Connected;
        }

        /// <summary>
        /// Sends Message to determined user.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SendMessage(uint ID, string msg)
        {
            if (!Connected)
                return false;
            Stealth.Client.ICQ_SendText(ID, msg);
            if (!EnableLogging) return true;
            if (SndMessages == null)
                SndMessages = new List<ICQMessage>();
            SndMessages.Add(new ICQMessage(ID, msg));
            return true;
        }

        /// <summary>
        /// Sends Message to determined user in userlist of ICQ client.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SendMessage(string user, string msg)
        {
            return _userlist != null &&
                   (from v in _userlist.Keys where _userlist[v].Equals(user) select SendMessage(v, msg)).FirstOrDefault();
        }
    }
}