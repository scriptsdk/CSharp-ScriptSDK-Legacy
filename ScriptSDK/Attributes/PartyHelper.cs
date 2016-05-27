using System;
using System.Collections.Generic;
using System.Linq;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// PartyHelper exposes handles, actions and properties where player can interact through party system.
    /// </summary>
    public class PartyHelper
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="owner"></param>
        protected PartyHelper(PlayerMobile owner)
        {
            _owner = owner;
            _allowlooting = false;
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private  PlayerMobile _owner { get; set; }

        private static PartyHelper _instance { get; set; }

        /// <summary>
        /// Returns a list of mobiles in player party.
        /// </summary>
        public virtual List<Mobile> Members
        {
            get
            {
                var l = Stealth.Client.PartyMembersList();
                return l.Select(e => new Serial(e)).ToList().Select(e => new Mobile(e)).ToList();
            }
        }

        private bool _allowlooting { get; set; }

        /// <summary>
        /// Enable or disable the lootrights on party menu.
        /// </summary>
        public virtual bool AllowLooting
        {
            get { return _allowlooting; }
            set { Stealth.Client.PartyCanLootMe(_allowlooting = value); }
        }

        /// <summary>
        /// Returns if party is alive.
        /// </summary>
        public virtual bool Enabled
        {
            get { return Members.Count > 0; }
        }

        /// <summary>
        /// Event which fires when player gets invitation to a new party.
        /// </summary>
        public virtual EventHandler<PartyInviteEventArgs> Event_OnInvitation
        {
            set { Stealth.Client.PartyInvite += value; }
        }

        /// <summary>
        /// Returns reference of party.
        /// </summary>
        /// <returns></returns>
        public static PartyHelper GetParty()
        {
            return _instance ?? (_instance = new PartyHelper(PlayerMobile.GetPlayer()));
        }

        /// <summary>
        /// Accepts a valid party invitation.
        /// </summary>
        /// <returns></returns>
        public virtual bool Accept()
        {
            var b = Enabled;
            if (!b)
                Stealth.Client.PartyAcceptInvite();
            return !b;
        }
        /// <summary>
        /// Declines a valid party invitation.
        /// </summary>
        /// <returns></returns>
        public virtual bool Decline()
        {
            var b = Enabled;
            if (!b)
                Stealth.Client.PartyDeclineInvite();
            return !b;
        }

        /// <summary>
        /// Leaves party if possible.
        /// </summary>
        /// <returns></returns>
        public virtual bool Leave()
        {
            var b = Enabled;
            if (b)
                Stealth.Client.PartyLeave();
            return b;
        }

        /// <summary>
        /// Invites mobile user to party.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool Invite(Mobile user)
        {
            var m = Members;

            if (m.Any(mobile => mobile.Serial.Equals(user.Serial)))
                return false;
            Stealth.Client.InviteToParty(user.Serial.Value);
            return true;
        }

        /// <summary>
        /// Removes mobile user from party.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool Remove(Mobile user)
        {
            var m = Members;

            if (!m.Any(mobile => mobile.Serial.Equals(user.Serial))) return false;
            Stealth.Client.RemoveFromParty(user.Serial.Value);
            return true;
        }

        /// <summary>
        /// Allows to send messages to party.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public virtual bool Say(string Text)
        {
            if (!Enabled) return false;
            Stealth.Client.PartySay(Text);
            return true;
        }

        /// <summary>
        /// Allows to send a private message to passed member in party.
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool Say(string Text, Mobile user)
        {
            if (!Enabled) return false;
            var members = Members;
            if (!members.Any(mobile => mobile.Serial.Equals(user.Serial))) return false;
            Stealth.Client.PartyMessageTo(user.Serial.Value, Text);
            return true;
        }
    }
}