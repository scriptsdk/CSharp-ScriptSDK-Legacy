using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Media;
using ScriptSDK.Data;

#pragma warning disable 1591

namespace StealthAPI
{
    public class Stealth : IDisposable
    {
        /// <summary>
        /// Connect timeout. Default 1 minute
        /// </summary>
        public static TimeSpan StealthAttachTimeout = new TimeSpan(0, 1, 0);
        public static bool EnableTracing { get; set; }

        public static void AddTraceMessage(object o, string s)
        {
            if (EnableTracing)
                Trace.WriteLine(o, s);
        }
        public static void AddTraceMessage(string s1, string s2)
        {
            if (EnableTracing)
                Trace.WriteLine(s1, s2);
        }
        public static void AddTraceMessage(object o)
        {
            if (EnableTracing)
                Trace.WriteLine(o);
        }
        public static void AddTraceMessage(string s)
        {
            if (EnableTracing)
                Trace.WriteLine(s);
        }

        private readonly Version SUPPORTED_VERSION = new Version(8, 8, 5, 0);

        #region Events
        private event EventHandler<ItemEventArgs> ItemInfoInternal;
        private event EventHandler<ItemEventArgs> ItemDeletedInternal;
        private event EventHandler<SpeechEventArgs> SpeechInternal;
        private event EventHandler<ObjectEventArgs> DrawGamePlayerInternal;
        private event EventHandler<MoveRejectionEventArgs> MoveRejectionInternal;
        private event EventHandler<DrawContainerEventArgs> DrawContainerInternal;
        private event EventHandler<AddItemToContainerEventArgs> AddItemToContainerInternal;
        private event EventHandler<AddMultipleItemsInContainerEventArgs> AddMultipleItemsInContainerInternal;
        private event EventHandler<RejectMoveItemEventArgs> RejectMoveItemInternal;
        private event EventHandler<ObjectEventArgs> UpdateCharInternal;
        private event EventHandler<ObjectEventArgs> DrawObjectInternal;
        private event EventHandler<MenuEventArgs> MenuInternal;
        private event EventHandler<MapMessageEventArgs> MapMessageInternal;
        private event EventHandler<AllowRefuseAttackEventArgs> AllowRefuseAttackInternal;
        private event EventHandler<ClilocSpeechEventArgs> ClilocSpeechInternal;
        private event EventHandler<ClilocSpeechAffixEventArgs> ClilocSpeechAffixInternal;
        private event EventHandler<UnicodeSpeechEventArgs> UnicodeSpeechInternal;
        private event EventHandler<Buff_DebuffSystemEventArgs> Buff_DebuffSystemInternal;
        private event EventHandler<EventArgs> ClientSendResyncInternal;
        private event EventHandler<CharAnimationEventArgs> CharAnimationInternal;
        private event EventHandler<EventArgs> ICQDisconnectInternal;
        private event EventHandler<EventArgs> ICQConnectInternal;
        private event EventHandler<ICQIncomingTextEventArgs> ICQIncomingTextInternal;
        private event EventHandler<ICQErrorEventArgs> ICQErrorInternal;
        private event EventHandler<IncomingGumpEventArgs> IncomingGumpInternal;
        private event EventHandler<EventArgs> Timer1Internal;
        private event EventHandler<EventArgs> Timer2Internal;
        private event EventHandler<WindowsMessageEventArgs> WindowsMessageInternal;
        private event EventHandler<SoundEventArgs> SoundInternal;
        private event EventHandler<DeathEventArgs> DeathInternal;
        private event EventHandler<QuestArrowEventArgs> QuestArrowInternal;
        private event EventHandler<PartyInviteEventArgs> PartyInviteInternal;
        private event EventHandler<MapPinEventArgs> MapPinInternal;
        private event EventHandler<GumpTextEntryEventArgs> GumpTextEntryInternal;
        private event EventHandler<GraphicalEffectEventArgs> GraphicalEffectInternal;
        private event EventHandler<IRCIncomingTextEventArgs> IRCIncomingTextInternal;
        private event EventHandler<SkypeIncomingTextEventArgs> SkypeIncomingTextInternal;

        public event EventHandler<ItemEventArgs> ItemInfo
        {
            add
            {
                var handler = ItemInfoInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.ItemInfo, _eventFunctionCounter++);
                ItemInfoInternal += value;
            }
            remove
            {
                ItemInfoInternal -= value;

                var handler = ItemInfoInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.ItemInfo);
                }
            }
        }
        public event EventHandler<ItemEventArgs> ItemDeleted
        {
            add
            {
                var handler = ItemDeletedInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.ItemDeleted, _eventFunctionCounter++);
                ItemDeletedInternal += value;
            }
            remove
            {
                ItemDeletedInternal -= value;

                var handler = ItemDeletedInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.ItemDeleted);
                }
            }
        }
        public event EventHandler<SpeechEventArgs> Speech
        {
            add
            {
                var handler = SpeechInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.Speech, _eventFunctionCounter++);
                SpeechInternal += value;
            }
            remove
            {
                SpeechInternal -= value;

                var handler = SpeechInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.Speech);
                }
            }
        }
        public event EventHandler<ObjectEventArgs> DrawGamePlayer
        {
            add
            {
                var handler = DrawGamePlayerInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.DrawGamePlayer, _eventFunctionCounter++);
                DrawGamePlayerInternal += value;
            }
            remove
            {
                DrawGamePlayerInternal -= value;

                var handler = DrawGamePlayerInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.DrawGamePlayer);
                }
            }
        }
        public event EventHandler<MoveRejectionEventArgs> MoveRejection
        {
            add
            {
                var handler = MoveRejectionInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.MoveRejection, _eventFunctionCounter++);
                MoveRejectionInternal += value;
            }
            remove
            {
                MoveRejectionInternal -= value;

                var handler = MoveRejectionInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.MoveRejection);
                }
            }
        }
        public event EventHandler<DrawContainerEventArgs> DrawContainer
        {
            add
            {
                var handler = DrawContainerInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.DrawContainer, _eventFunctionCounter++);
                DrawContainerInternal += value;
            }
            remove
            {
                DrawContainerInternal -= value;

                var handler = DrawContainerInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.DrawContainer);
                }
            }
        }
        public event EventHandler<AddItemToContainerEventArgs> AddItemToContainer
        {
            add
            {
                var handler = AddItemToContainerInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.AddItemToContainer, _eventFunctionCounter++);
                AddItemToContainerInternal += value;
            }
            remove
            {
                AddItemToContainerInternal -= value;

                var handler = AddItemToContainerInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.AddItemToContainer);
                }
            }
        }
        public event EventHandler<AddMultipleItemsInContainerEventArgs> AddMultipleItemsInContainer
        {
            add
            {
                var handler = AddMultipleItemsInContainerInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.AddMultipleItemsInCont, _eventFunctionCounter++);
                AddMultipleItemsInContainerInternal += value;
            }
            remove
            {
                AddMultipleItemsInContainerInternal -= value;

                var handler = AddMultipleItemsInContainerInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.AddMultipleItemsInCont);
                }
            }
        }
        public event EventHandler<RejectMoveItemEventArgs> RejectMoveItem
        {
            add
            {
                var handler = RejectMoveItemInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.RejectMoveItem, _eventFunctionCounter++);
                RejectMoveItemInternal += value;
            }
            remove
            {
                RejectMoveItemInternal -= value;

                var handler = RejectMoveItemInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.RejectMoveItem);
                }
            }
        }
        public event EventHandler<ObjectEventArgs> UpdateChar
        {
            add
            {
                var handler = UpdateCharInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.UpdateChar, _eventFunctionCounter++);
                UpdateCharInternal += value;
            }
            remove
            {
                UpdateCharInternal -= value;

                var handler = UpdateCharInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.UpdateChar);
                }
            }
        }
        public event EventHandler<ObjectEventArgs> DrawObject
        {
            add
            {
                var handler = DrawObjectInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.DrawObject, _eventFunctionCounter++);
                DrawObjectInternal += value;
            }
            remove
            {
                DrawObjectInternal -= value;

                var handler = DrawObjectInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.DrawObject);
                }
            }
        }
        public event EventHandler<MenuEventArgs> Menu
        {
            add
            {
                var handler = MenuInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.Menu, _eventFunctionCounter++);
                MenuInternal += value;
            }
            remove
            {
                MenuInternal -= value;

                var handler = MenuInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.Menu);
                }
            }
        }
        public event EventHandler<MapMessageEventArgs> MapMessage
        {
            add
            {
                var handler = MapMessageInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.MapMessage, _eventFunctionCounter++);
                MapMessageInternal += value;
            }
            remove
            {
                MapMessageInternal -= value;

                var handler = MapMessageInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.MapMessage);
                }
            }
        }
        public event EventHandler<AllowRefuseAttackEventArgs> AllowRefuseAttack
        {
            add
            {
                var handler = AllowRefuseAttackInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.Allow_RefuseAttack, _eventFunctionCounter++);
                AllowRefuseAttackInternal += value;
            }
            remove
            {
                AllowRefuseAttackInternal -= value;

                var handler = AllowRefuseAttackInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.Allow_RefuseAttack);
                }
            }
        }
        public event EventHandler<ClilocSpeechEventArgs> ClilocSpeech
        {
            add
            {
                var handler = ClilocSpeechInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.ClilocSpeech, _eventFunctionCounter++);
                ClilocSpeechInternal += value;
            }
            remove
            {
                ClilocSpeechInternal -= value;

                var handler = ClilocSpeechInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.ClilocSpeech);
                }
            }
        }
        [Obsolete("Deprecated, use ClilocSpeech instead")]
        public event EventHandler<ClilocSpeechAffixEventArgs> ClilocSpeechAffix
        {
            add
            {
                var handler = ClilocSpeechAffixInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.ClilocSpeechAffix, _eventFunctionCounter++);
                ClilocSpeechAffixInternal += value;
            }
            remove
            {
                ClilocSpeechAffixInternal -= value;

                var handler = ClilocSpeechAffixInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.ClilocSpeechAffix);
                }
            }
        }
        [Obsolete("Deprecated, use ClilocSpeech instead")]
        public event EventHandler<UnicodeSpeechEventArgs> UnicodeSpeech
        {
            add
            {
                var handler = UnicodeSpeechInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.UnicodeSpeech, _eventFunctionCounter++);
                UnicodeSpeechInternal += value;
            }
            remove
            {
                UnicodeSpeechInternal -= value;

                var handler = UnicodeSpeechInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.UnicodeSpeech);
                }
            }
        }
        public event EventHandler<Buff_DebuffSystemEventArgs> Buff_DebuffSystem
        {
            add
            {
                var handler = Buff_DebuffSystemInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.Buff_DebuffSystem, _eventFunctionCounter++);
                Buff_DebuffSystemInternal += value;
            }
            remove
            {
                Buff_DebuffSystemInternal -= value;

                var handler = Buff_DebuffSystemInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.Buff_DebuffSystem);
                }
            }
        }
        public event EventHandler<EventArgs> ClientSendResync
        {
            add
            {
                var handler = ClientSendResyncInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.ClientSendResync, _eventFunctionCounter++);
                ClientSendResyncInternal += value;
            }
            remove
            {
                ClientSendResyncInternal -= value;

                var handler = ClientSendResyncInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.ClientSendResync);
                }
            }
        }
        public event EventHandler<CharAnimationEventArgs> CharAnimation
        {
            add
            {
                var handler = CharAnimationInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.CharAnimation, _eventFunctionCounter++);
                CharAnimationInternal += value;
            }
            remove
            {
                CharAnimationInternal -= value;

                var handler = CharAnimationInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.CharAnimation);
                }
            }
        }
        public event EventHandler<EventArgs> ICQDisconnect
        {
            add
            {
                var handler = ICQDisconnectInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.ICQDisconnect, _eventFunctionCounter++);
                ICQDisconnectInternal += value;
            }
            remove
            {
                ICQDisconnectInternal -= value;

                var handler = ICQDisconnectInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.ICQDisconnect);
                }
            }
        }
        public event EventHandler<EventArgs> ICQConnect
        {
            add
            {
                var handler = ICQConnectInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.ICQConnect, _eventFunctionCounter++);
                ICQConnectInternal += value;
            }
            remove
            {
                ICQConnectInternal -= value;

                var handler = ICQConnectInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.ICQConnect);
                }
            }
        }
        public event EventHandler<ICQIncomingTextEventArgs> ICQIncomingText
        {
            add
            {
                var handler = ICQIncomingTextInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.ICQIncomingText, _eventFunctionCounter++);
                ICQIncomingTextInternal += value;
            }
            remove
            {
                ICQIncomingTextInternal -= value;

                var handler = ICQIncomingTextInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.ICQIncomingText);
                }
            }
        }
        public event EventHandler<ICQErrorEventArgs> ICQError
        {
            add
            {
                var handler = ICQErrorInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.ICQError, _eventFunctionCounter++);
                ICQErrorInternal += value;
            }
            remove
            {
                ICQErrorInternal -= value;

                var handler = ICQErrorInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.ICQError);
                }
            }
        }
        public event EventHandler<IncomingGumpEventArgs> IncomingGump
        {
            add
            {
                var handler = IncomingGumpInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.IncomingGump, _eventFunctionCounter++);
                IncomingGumpInternal += value;
            }
            remove
            {
                IncomingGumpInternal -= value;

                var handler = IncomingGumpInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.IncomingGump);
                }
            }
        }
        public event EventHandler<EventArgs> Timer1
        {
            add
            {
                var handler = Timer1Internal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.Timer1, _eventFunctionCounter++);
                Timer1Internal += value;
            }
            remove
            {
                Timer1Internal -= value;

                var handler = Timer1Internal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.Timer1);
                }
            }
        }
        public event EventHandler<EventArgs> Timer2
        {
            add
            {
                var handler = Timer2Internal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.Timer2, _eventFunctionCounter++);
                Timer2Internal += value;
            }
            remove
            {
                Timer2Internal -= value;

                var handler = Timer2Internal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.Timer2);
                }
            }
        }
        public event EventHandler<WindowsMessageEventArgs> WindowsMessage
        {
            add
            {
                var handler = WindowsMessageInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.WindowsMessage, _eventFunctionCounter++);
                WindowsMessageInternal += value;
            }
            remove
            {
                WindowsMessageInternal -= value;

                var handler = WindowsMessageInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.WindowsMessage);
                }
            }
        }
        public event EventHandler<SoundEventArgs> Sound
        {
            add
            {
                var handler = SoundInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.Sound, _eventFunctionCounter++);
                SoundInternal += value;
            }
            remove
            {
                SoundInternal -= value;

                var handler = SoundInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.Sound);
                }
            }
        }
        public event EventHandler<DeathEventArgs> Death
        {
            add
            {
                var handler = DeathInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.Death, _eventFunctionCounter++);
                DeathInternal += value;
            }
            remove
            {
                DeathInternal -= value;

                var handler = DeathInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.Death);
                }
            }
        }
        public event EventHandler<QuestArrowEventArgs> QuestArrow
        {
            add
            {
                var handler = QuestArrowInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.QuestArrow, _eventFunctionCounter++);
                QuestArrowInternal += value;
            }
            remove
            {
                QuestArrowInternal -= value;

                var handler = QuestArrowInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.QuestArrow);
                }
            }
        }
        public event EventHandler<PartyInviteEventArgs> PartyInvite
        {
            add
            {
                var handler = PartyInviteInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.PartyInvite, _eventFunctionCounter++);
                PartyInviteInternal += value;
            }
            remove
            {
                PartyInviteInternal -= value;

                var handler = PartyInviteInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.PartyInvite);
                }
            }
        }

        public event EventHandler<MapPinEventArgs> MapPin
        {
            add
            {
                var handler = MapPinInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.MapPin, _eventFunctionCounter++);
                MapPinInternal += value;
            }
            remove
            {
                MapPinInternal -= value;

                var handler = MapPinInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.MapPin);
                }
            }
        }

        public event EventHandler<GumpTextEntryEventArgs> GumpTextEntry
        {
            add
            {
                var handler = GumpTextEntryInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.GumpTextEntry, _eventFunctionCounter++);
                GumpTextEntryInternal += value;
            }
            remove
            {
                GumpTextEntryInternal -= value;

                var handler = GumpTextEntryInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.GumpTextEntry);
                }
            }
        }

        public event EventHandler<GraphicalEffectEventArgs> GraphicalEffect
        {
            add
            {
                var handler = GraphicalEffectInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.GraphicalEffect, _eventFunctionCounter++);
                GraphicalEffectInternal += value;
            }
            remove
            {
                GraphicalEffectInternal -= value;

                var handler = GraphicalEffectInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.GraphicalEffect);
                }
            }
        }

        public event EventHandler<IRCIncomingTextEventArgs> IRCIncomingText
        {
            add
            {
                var handler = IRCIncomingTextInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.IRCIncomingText, _eventFunctionCounter++);
                IRCIncomingTextInternal += value;
            }
            remove
            {
                IRCIncomingTextInternal -= value;

                var handler = IRCIncomingTextInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.IRCIncomingText);
                }
            }
        }

        public event EventHandler<SkypeIncomingTextEventArgs> SkypeIncomingText
        {
            add
            {
                var handler = SkypeIncomingTextInternal;
                if (handler == null)
                    _client.SendPacket(PacketType.SCSetEventProc, EventTypes.SkypeEvent, _eventFunctionCounter++);
                SkypeIncomingTextInternal += value;
            }
            remove
            {
                SkypeIncomingTextInternal -= value;

                var handler = SkypeIncomingTextInternal;
                if (handler == null)
                {
                    _eventFunctionCounter--;
                    _client.SendPacket(PacketType.SCClearEventProc, EventTypes.SkypeEvent);
                }
            }
        }

        public event EventHandler<StartStopEventArgs> StartStop;
        #endregion

        private StealthClient _client;
        //private readonly Dictionary<string, int> _skills = new Dictionary<string, int>();
        private uint _dropDelay;
        private byte _eventFunctionCounter;
        private bool _isStopped;

        private static Stealth _instance;
        public static Stealth Client
        {
            get { return _instance ?? (_instance = new Stealth()); }
        }

        private Stealth()
        {
            Win32.NativeMessage msg;
            uint value = 0x600;
            Win32.PeekMessage(out msg, 0, value, value, Win32.PM_REMOVE);

            AddTraceMessage("Prepare message for Stealth", "Stealth.Main");
            var procstring = Process.GetCurrentProcess().Id.ToString("X8") + Process.GetCurrentProcess().MainModule.FileName.Replace(".vshost", "") + '\0';
            AddTraceMessage(string.Format("Procstring: {0}", procstring), "Stealth.Main");
            var aCopyData = new Win32.CopyDataStruct
            {
                dwData = (uint)Win32.GetCurrentThreadId(),
                cbData = Process.GetCurrentProcess().MainModule.FileName.Replace(".vshost", "").Length + 8 + 1,
                lpData = Marshal.StringToHGlobalAnsi(procstring)
            };

            IntPtr copyDataPtr = aCopyData.MarshalToPtr();

            try
            {
                AddTraceMessage("Find Stealth window", "Stealth.Main");
                IntPtr tWndPtr = Win32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "TStealthForm", null);
                if (tWndPtr != IntPtr.Zero)
                {
                    AddTraceMessage("Stealth window found. Send message.", "Stealth.Main");
                    Win32.SendMessage(tWndPtr, Win32.WM_COPYDATA, IntPtr.Zero, copyDataPtr);
                    AddTraceMessage("Message sended. Wait message from Stealth.", "Stealth.Main");
                    uint peekvalue = 0x600;
                    var sw = Stopwatch.StartNew();
                    while (!Win32.PeekMessage(out msg, 0, peekvalue, peekvalue, Win32.PM_REMOVE))
                    {
                        Thread.Sleep(10);
                        if (sw.Elapsed >= StealthAttachTimeout)
                            throw new InvalidOperationException("Could not attach to Stealth. Exiting.");
                    }
                    AddTraceMessage(string.Format("Message recieved. Port: {0}", (int)(msg.wParam)), "Stealth.Main");
                }
                else
                    throw new InvalidOperationException("Could not attach to Stealth. Exiting.");
            }
            catch (Exception ex)
            {
                AddTraceMessage(ex, "Stealth.Main");
            }
            finally
            {
                Marshal.FreeHGlobal(copyDataPtr);
            }

            int port = (int)(msg.wParam);
            _isStopped = false;

            AddTraceMessage("Create Stealth client", "Stealth.Main");
            _client = new StealthClient("localhost", port);
            _client.ServerEventRecieve += cln_ServerEventRecieve;
            _client.StartStopRecieve += _client_StartStopRecieve;
            _client.TerminateRecieve += _client_TerminateRecieve;
            _client.Connect();
            var about = GetStealthInfo();
            new Version(about.StealthVersion[0], about.StealthVersion[1], about.StealthVersion[2], about.GITRevNumber);
            //if (ver < SUPPORTED_VERSION)
            //    throw new NotSupportedException("Support Stealth version 6.5.3 rev 847 or above");
        }

        ~Stealth()
        {
            Dispose();
        }

        void _client_TerminateRecieve(object sender, EventArgs e)
        {
            Dispose();
        }

        void _client_StartStopRecieve(object sender, EventArgs e)
        {
            _isStopped = !_isStopped;
            OnStartStop(_isStopped);
        }

        private void OnStartStop(bool isStopped)
        {
            var handle = StartStop;
            if (handle != null)
                handle(this, new StartStopEventArgs(isStopped));
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }
            Environment.Exit(0);
        }

        #region Process Events
        private void cln_ServerEventRecieve(object sender, ServerEventArgs e)
        {
            ProcessEvent(e.Data);
        }


        private void ProcessEvent(ExecEventProcData data)
        {
            #region Handle Event Packet

            switch (data.EventType)
            {
                case EventTypes.ItemInfo:
                    OnItemInfo((uint)data.Parameters[0]);
                    break;
                case EventTypes.ItemDeleted:
                    OnItemDeleted((uint)data.Parameters[0]);
                    break;
                case EventTypes.Speech:
                    OnSpeech((string)data.Parameters[0], (string)data.Parameters[1], (uint)data.Parameters[2]);
                    break;
                case EventTypes.DrawGamePlayer:
                    OnDrawGamePlayer((uint)data.Parameters[0]);
                    break;
                case EventTypes.MoveRejection:
                    OnMoveRejection((ushort)data.Parameters[0], (ushort)data.Parameters[1], (byte)data.Parameters[2], (ushort)data.Parameters[3], (ushort)data.Parameters[4]);
                    break;
                case EventTypes.DrawContainer:
                    OnDrawContainer((uint)data.Parameters[0], (ushort)data.Parameters[1]);
                    break;
                case EventTypes.AddItemToContainer:
                    OnAddItemToContainer((uint)data.Parameters[0], (uint)data.Parameters[1]);
                    break;
                case EventTypes.AddMultipleItemsInCont:
                    OnAddMultipleItemsInContainer((uint)data.Parameters[0]);
                    break;
                case EventTypes.RejectMoveItem:
                    OnRejectMoveItem((RejectMoveItemReasons)data.Parameters[0]);
                    break;
                case EventTypes.UpdateChar:
                    OnUpdateChar((uint)data.Parameters[0]);
                    break;
                case EventTypes.DrawObject:
                    OnDrawObject((uint)data.Parameters[0]);
                    break;
                case EventTypes.Menu:
                    OnMenu((uint)data.Parameters[0], (ushort)data.Parameters[1]);
                    break;
                case EventTypes.MapMessage:
                    OnMapMessage((uint)data.Parameters[0], (int)data.Parameters[1], (int)data.Parameters[2]);
                    break;
                case EventTypes.Allow_RefuseAttack:
                    OnAllowRefuseAttack((uint)data.Parameters[0], Convert.ToBoolean(data.Parameters[1]));
                    break;
                case EventTypes.ClilocSpeech:
                    OnClilocSpeech((int)data.Parameters[0], (string)data.Parameters[1], (string)data.Parameters[2]);
                    break;
                case EventTypes.ClilocSpeechAffix:
                    OnClilocSpeechAffix((int)data.Parameters[0], (string)data.Parameters[1], (string)data.Parameters[2], (string)data.Parameters[3]);
                    break;
                case EventTypes.UnicodeSpeech:
                    OnUnicodeSpeech((string)data.Parameters[0], (string)data.Parameters[1], (uint)data.Parameters[2]);
                    break;
                case EventTypes.Buff_DebuffSystem:
                    OnBuff_DebuffSystem((uint)data.Parameters[0], (ushort)data.Parameters[1], (bool)data.Parameters[2]);
                    break;
                case EventTypes.ClientSendResync:
                    OnClientSendResync();
                    break;
                case EventTypes.CharAnimation:
                    OnCharAnimation((uint)data.Parameters[0], (uint)data.Parameters[1]);
                    break;
                case EventTypes.ICQDisconnect:
                    OnICQDisconnect();
                    break;
                case EventTypes.ICQConnect:
                    OnICQConnect();
                    break;
                case EventTypes.ICQIncomingText:
                    OnICQIncomingText((uint)data.Parameters[0], (string)data.Parameters[1]);
                    break;
                case EventTypes.ICQError:
                    OnICQError((string)data.Parameters[0]);
                    break;
                case EventTypes.IncomingGump:
                    OnIncomingGump((uint)data.Parameters[0], (uint)data.Parameters[1], (uint)data.Parameters[2], (uint)data.Parameters[3]);
                    break;
                case EventTypes.Timer1:
                    OnTimer1();
                    break;
                case EventTypes.Timer2:
                    OnTimer2();
                    break;
                case EventTypes.WindowsMessage:
                    OnWindowsMessage((uint)data.Parameters[0]);
                    break;
                case EventTypes.Sound:
                    OnSound((ushort)data.Parameters[0], (ushort)data.Parameters[1], (ushort)data.Parameters[2], (int)data.Parameters[3]);
                    break;
                case EventTypes.Death:
                    OnDeath(Convert.ToBoolean(data.Parameters[0]));
                    break;
                case EventTypes.QuestArrow:
                    OnQuestArrow((ushort)data.Parameters[0], (ushort)data.Parameters[1], Convert.ToBoolean(data.Parameters[2]));
                    break;
                case EventTypes.PartyInvite:
                    OnPartyInvite((uint)data.Parameters[0]);
                    break;
                case EventTypes.MapPin:
                    OnMapPin((uint)data.Parameters[0], (byte)data.Parameters[1], (byte)data.Parameters[2], (ushort)data.Parameters[3], (ushort)data.Parameters[4]);
                    break;
                case EventTypes.GumpTextEntry:
                    OnGumpTextEntry((uint)data.Parameters[0], (string)data.Parameters[1], (byte)data.Parameters[2], (uint)data.Parameters[3], (string)data.Parameters[4]);
                    break;
                case EventTypes.GraphicalEffect:
                    OnGraphicalEffect((uint)data.Parameters[0], (ushort)data.Parameters[1], (ushort)data.Parameters[2], (int)data.Parameters[3],
                        (uint)data.Parameters[4], (ushort)data.Parameters[5], (ushort)data.Parameters[6], (int)data.Parameters[7],
                        (byte)data.Parameters[8], (ushort)data.Parameters[9], (byte)data.Parameters[10]);
                    break;
                case EventTypes.IRCIncomingText:
                    OnIRCIncomingText((string)data.Parameters[0]);
                    break;
                case EventTypes.SkypeEvent:
                    OnSkypeIncomingText((string)data.Parameters[0], (string)data.Parameters[1], (string)data.Parameters[2], (byte)data.Parameters[3]);
                    break;
            }
            #endregion
        }

        private void OnSkypeIncomingText(string senderId, string recieverId, string msg, byte eventCode)
        {
            var handler = SkypeIncomingTextInternal;
            if (handler != null)
                handler(this, new SkypeIncomingTextEventArgs(senderId, recieverId, msg, eventCode));
        }

        private void OnIRCIncomingText(string message)
        {
            var handler = IRCIncomingTextInternal;
            if (handler != null)
                handler(this, new IRCIncomingTextEventArgs(message));
        }

        private void OnGraphicalEffect(uint srcId, ushort srcX, ushort srcY, int srcZ, uint dstId, ushort dstX, ushort dstY, int dstZ, byte type, ushort itemId, byte fixedDir)
        {
            var handler = GraphicalEffectInternal;
            if (handler != null)
                handler(this, new GraphicalEffectEventArgs(srcId, srcX, srcY, srcZ, dstId, dstX, dstY, dstZ, type, itemId, fixedDir));
        }

        private void OnGumpTextEntry(uint gumpTextEntryId, string title, byte inputStyle, uint maxValue, string title2)
        {
            var handler = GumpTextEntryInternal;
            if (handler != null)
                handler(this, new GumpTextEntryEventArgs(gumpTextEntryId, title, inputStyle, maxValue, title2));
        }

        private void OnMapPin(uint id, byte action, byte pinId, ushort x, ushort y)
        {
            var handler = MapPinInternal;
            if (handler != null)
                handler(this, new MapPinEventArgs(id, action, pinId, x, y));
        }

        private void OnPartyInvite(uint inviterId)
        {
            var handler = PartyInviteInternal;
            if (handler != null)
                handler(this, new PartyInviteEventArgs(inviterId));
        }

        private void OnQuestArrow(ushort x, ushort y, bool isActive)
        {
            var handler = QuestArrowInternal;
            if (handler != null)
                handler(this, new QuestArrowEventArgs(x, y, isActive));
        }

        private void OnDeath(bool isDead)
        {
            var handler = DeathInternal;
            if (handler != null)
                handler(this, new DeathEventArgs(isDead));
        }

        private void OnSound(ushort soundId, ushort x, ushort y, int z)
        {
            var handler = SoundInternal;
            if (handler != null)
                handler(this, new SoundEventArgs(soundId, x, y, z));
        }

        private void OnWindowsMessage(uint lParam)
        {
            var handler = WindowsMessageInternal;
            if (handler != null)
                handler(this, new WindowsMessageEventArgs(lParam));
        }

        private void OnTimer2()
        {
            var handler = Timer2Internal;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void OnTimer1()
        {
            var handler = Timer1Internal;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void OnIncomingGump(uint serial, uint gumpId, uint x, uint y)
        {
            var handler = IncomingGumpInternal;
            if (handler != null)
                handler(this, new IncomingGumpEventArgs(serial, gumpId, x, y));
        }

        private void OnICQError(string text)
        {
            var handler = ICQErrorInternal;
            if (handler != null)
                handler(this, new ICQErrorEventArgs(text));
        }

        private void OnICQIncomingText(uint uin, string text)
        {
            var handler = ICQIncomingTextInternal;
            if (handler != null)
                handler(this, new ICQIncomingTextEventArgs(uin, text));
        }

        private void OnICQConnect()
        {
            var handler = ICQConnectInternal;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void OnICQDisconnect()
        {
            var handler = ICQDisconnectInternal;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void OnCharAnimation(uint objectId, uint action)
        {
            var handler = CharAnimationInternal;
            if (handler != null)
                handler(this, new CharAnimationEventArgs(objectId, action));
        }

        private void OnClientSendResync()
        {
            var handler = ClientSendResyncInternal;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void OnBuff_DebuffSystem(uint objectId, ushort attributeId, bool isEnabled)
        {
            var handler = Buff_DebuffSystemInternal;
            if (handler != null)
                handler(this, new Buff_DebuffSystemEventArgs(objectId, attributeId, isEnabled));
        }

        private void OnUnicodeSpeech(string text, string senderName, uint senderId)
        {
            var handler = UnicodeSpeechInternal;
            if (handler != null)
                handler(this, new UnicodeSpeechEventArgs(text, senderName, senderId));
        }

        private void OnClilocSpeechAffix(int senderId, string senderName, string affix, string text)
        {
            var handler = ClilocSpeechAffixInternal;
            if (handler != null)
                handler(this, new ClilocSpeechAffixEventArgs(senderId, senderName, affix, text));
        }

        private void OnClilocSpeech(int senderId, string senderName, string text)
        {
            var handler = ClilocSpeechInternal;
            if (handler != null)
                handler(this, new ClilocSpeechEventArgs(senderId, senderName, text));
        }

        private void OnAllowRefuseAttack(uint targetId, bool isAttackOk)
        {
            var handler = AllowRefuseAttackInternal;
            if (handler != null)
                handler(this, new AllowRefuseAttackEventArgs(targetId, isAttackOk));
        }

        private void OnMapMessage(uint itemId, int centerX, int centerY)
        {
            var handler = MapMessageInternal;
            if (handler != null)
                handler(this, new MapMessageEventArgs(itemId, centerX, centerY));
        }

        private void OnMenu(uint dialogId, ushort menuId)
        {
            var handler = MenuInternal;
            if (handler != null)
                handler(this, new MenuEventArgs(dialogId, menuId));
        }

        private void OnDrawObject(uint objectId)
        {
            var handler = DrawObjectInternal;
            if (handler != null)
                handler(this, new ObjectEventArgs(objectId));
        }

        private void OnUpdateChar(uint objectId)
        {
            var handler = UpdateCharInternal;
            if (handler != null)
                handler(this, new ObjectEventArgs(objectId));
        }

        private void OnRejectMoveItem(RejectMoveItemReasons reason)
        {
            var handler = RejectMoveItemInternal;
            if (handler != null)
                handler(this, new RejectMoveItemEventArgs(reason));
        }

        private void OnAddMultipleItemsInContainer(uint containerId)
        {
            var handler = AddMultipleItemsInContainerInternal;
            if (handler != null)
                handler(this, new AddMultipleItemsInContainerEventArgs(containerId));
        }

        private void OnAddItemToContainer(uint itemId, uint containerId)
        {
            var handler = AddItemToContainerInternal;
            if (handler != null)
                handler(this, new AddItemToContainerEventArgs(itemId, containerId));
        }

        private void OnDrawContainer(uint containerId, ushort modelGump)
        {
            var handler = DrawContainerInternal;
            if (handler != null)
                handler(this, new DrawContainerEventArgs(containerId, modelGump));
        }

        private void OnMoveRejection(ushort xSource, ushort ySource, byte direction, ushort xDest, ushort yDest)
        {
            var handler = MoveRejectionInternal;
            if (handler != null)
                handler(this, new MoveRejectionEventArgs(xSource, ySource, direction, xDest, yDest));
        }

        private void OnDrawGamePlayer(uint objectId)
        {
            var handler = DrawGamePlayerInternal;
            if (handler != null)
                handler(this, new ObjectEventArgs(objectId));
        }

        private void OnSpeech(string text, string senderName, uint senderId)
        {
            var handler = SpeechInternal;
            if (handler != null)
                handler(this, new SpeechEventArgs(text, senderName, senderId));
        }

        private void OnItemDeleted(uint itemId)
        {
            var handler = ItemDeletedInternal;
            if (handler != null)
                handler(this, new ItemEventArgs(itemId));
        }

        private void OnItemInfo(uint itemId)
        {
            var handler = ItemInfoInternal;
            if (handler != null)
                handler(this, new ItemEventArgs(itemId));
        }
        #endregion

        #region AddToSystemJournal
        public void AddToSystemJournal(string text)
        {
            _client.SendPacket(PacketType.SCAddToSystemJournal, text);
        }
        #endregion

        #region Connect - Disconnect
        public void Connect()
        {
            _client.SendPacket(PacketType.SCConnect);
        }
        public void Disconnect()
        {
            _client.SendPacket(PacketType.SCDisconnect);
        }
        #endregion

        #region Stealth Info
        public AboutData GetStealthInfo()
        {
            return _client.SendPacket<AboutData>(PacketType.SCGetStealthInfo);

        }
        #endregion

        #region Pause Script on disconnect
        public void SetPauseScriptOnDisconnectStatus(bool value)
        {
            _client.SendPacket(PacketType.SCSetPauseScriptOnDisconnectStatus, value);
        }
        public bool GetPauseScriptOnDisconnectStatus()
        {
            return _client.SendPacket<bool>(PacketType.SCGetPauseScriptOnDisconnectStatus);
        }
        #endregion

        #region Auto Reconnector
        public void SetARStatus(bool value)
        {
            _client.SendPacket(PacketType.SCSetPauseScriptOnDisconnectStatus, value);
        }
        public bool GetARStatus()
        {
            return _client.SendPacket<bool>(PacketType.SCGetARStatus);
        }
        #endregion

        #region Connected
        public bool GetConnectedStatus()
        {
            return _client.SendPacket<bool>(PacketType.SCGetConnectedStatus);
        }
        #endregion

        #region Char name
        public string GetCharName()
        {
            return _client.SendPacket<string>(PacketType.SCGetCharName);
        }
        #endregion

        #region Profile name
        public int ChangeProfile(string profileName)
        {
            return _client.SendPacket<int>(PacketType.SCChangeProfile, profileName);
        }

        public int ChangeProfile(string profileName, string shardName, string charName)
        {
            return _client.SendPacket<int>(PacketType.SCChangeProfileEx, profileName, shardName, charName);
        }

        public string ProfileName()
        {
            return _client.SendPacket<string>(PacketType.SCGetProfileName);
        }
        #endregion

        #region Self

        public uint GetSelfID()
        {
            return _client.SendPacket<uint>(PacketType.SCGetSelfID);
        }
        public byte GetSelfSex()
        {
            return _client.SendPacket<byte>(PacketType.SCGetSelfSex);
        }
        public string GetCharTitle()
        {
            return _client.SendPacket<string>(PacketType.SCGetCharTitle);
        }
        public uint GetSelfGold()
        {
            return _client.SendPacket<uint>(PacketType.SCGetGetSelfGold);
        }
        public ushort GetSelfPhysicalResist()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetSelfArmor);
        }
        public ushort GetSelfArmor() // Legacy Stealth support
        {
            return GetSelfPhysicalResist();
        }
        public ushort GetSelfWeight()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetSelfWeight);
        }
        public ushort GetSelfMaxWeight()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetSelfMaxWeight);
        }
        public byte GetWorldNum()
        {
            return _client.SendPacket<byte>(PacketType.SCGetWorldNum);
        }
        public byte GetSelfRace()
        {
            return _client.SendPacket<byte>(PacketType.SCGetSelfRace);
        }
        public byte GetSelfPetsMax()
        {
            return _client.SendPacket<byte>(PacketType.SCGetSelfPetsMax);
        }
        public byte GetSelfPetsCurrent()
        {
            return _client.SendPacket<byte>(PacketType.SCGetSelfPetsCurrent);
        }
        public ushort GetSelfFireResist()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetSelfFireResist);
        }
        public ushort GetSelfColdResist()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetSelfColdResist);
        }
        public ushort GetSelfPoisonResist()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetSelfPoisonResist);
        }
        public ushort GetSelfEnergyResist()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetSelfEnergyResist);
        }
        public DateTime GetConnectedTime()
        {
            return _client.SendPacket<DateTime>(PacketType.SCGetConnectedTime);
        }
        public DateTime GetDisconnectedTime()
        {
            return _client.SendPacket<DateTime>(PacketType.SCGetDisconnectedTime);
        }
        public uint GetLastContainer()
        {
            return _client.SendPacket<uint>(PacketType.SCGetLastContainer);
        }
        public uint GetLastTarget()
        {
            return _client.SendPacket<uint>(PacketType.SCGetLastTarget);
        }
        public uint GetLastAttack()
        {
            return _client.SendPacket<uint>(PacketType.SCGetLastAttack);
        }
        public uint GetLastStatus()
        {
            return _client.SendPacket<uint>(PacketType.SCGetLastStatus);
        }
        public uint GetLastObject()
        {
            return _client.SendPacket<uint>(PacketType.SCGetLastObject);
        }
        #endregion

        #region Shard name
        public string GetShardName()
        {
            return _client.SendPacket<string>(PacketType.SCGetShardName);
        }
        #endregion

        #region Profile Shard name
        public string GetProfileShardName()
        {
            return _client.SendPacket<string>(PacketType.SCGetProfileShardName);

        }
        #endregion

        #region Proxy
        public string GetProxyIP()
        {
            return _client.SendPacket<string>(PacketType.SCGetProxyIP);

        }
        public ushort GetProxyPort()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetProxyPort);

        }
        public bool GetUseProxy()
        {
            return _client.SendPacket<bool>(PacketType.SCGetUseProxy);

        }
        #endregion

        #region Backpack id
        public uint GetBackpackID()
        {
            return _client.SendPacket<uint>(PacketType.SCGetBackpackID);
        }
        #endregion

        #region Ground id
        public uint GetGroundID()
        {
            return 0x00;// _client.SendPacket<uint>(PacketType.SCGetGround);
        }
        #endregion

        #region Char Stats
        public int GetSelfStr()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfStr);
        }
        public int GetSelfInt()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfInt);
        }
        public int GetSelfDex()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfDex);
        }
        public int GetSelfLife()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfLife);
        }
        public int GetSelfMana()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfMana);
        }
        public int GetSelfStam()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfStam);
        }
        public int GetSelfMaxLife()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfMaxLife);
        }
        public int GetSelfMaxMana()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfMaxMana);
        }
        public int GetSelfMaxStam()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfMaxStam);
        }
        public int GetSelfLuck()
        {
            return _client.SendPacket<int>(PacketType.SCGetSelfLuck);
        }
        public ExtendedInfo GetExtInfo()
        {
            return _client.SendPacket<ExtendedInfo>(PacketType.SCGetExtInfo);
        }
        #endregion

        #region Hidden
        public bool GetHiddenStatus()
        {
            return _client.SendPacket<bool>(PacketType.SCGetHiddenStatus);
        }
        #endregion

        #region Poisoned
        public bool GetPoisonedStatus()
        {
            return _client.SendPacket<bool>(PacketType.SCGetPoisonedStatus);
        }
        #endregion

        #region Paralyzed
        public bool GetParalyzedStatus()
        {
            return _client.SendPacket<bool>(PacketType.SCGetParalyzedStatus);
        }
        #endregion

        #region Dead
        public bool GetDeadStatus()
        {
            return _client.SendPacket<bool>(PacketType.SCGetDeadStatus);
        }
        #endregion

        #region Attack and WarMode
        public bool GetWarModeStatus()
        {
            return IsWarMode(GetSelfID());
        }
        public uint GetWarTarget()
        {
            return _client.SendPacket<uint>(PacketType.SCGetWarTarget);
        }
        public void SetWarMode(bool value)
        {
            _client.SendPacket(PacketType.SCSetWarMode, value);
        }
        public void Attack(uint attackedID)
        {
            _client.SendPacket(PacketType.SCAttack, attackedID);
        }
        #endregion

        #region Work with paperdoll scrolls
        public void UseSelfPaperdollScroll()
        {
            _client.SendPacket(PacketType.SCUseSelfPaperdollScroll);

        }
        public void UseOtherPaperdollScroll(uint id)
        {
            _client.SendPacket(PacketType.SCUseOtherPaperdollScroll, id);


        }
        #endregion

        #region Target
        public uint GetTargetID()
        {
            return _client.SendPacket<uint>(PacketType.SCGetTargetID);
        }
        public bool GetTargetStatus()
        {
            return GetTargetID() > 0;
        }
        public bool WaitForTarget(int MaxWaitTimeMS)
        {
            DateTime endTime = DateTime.Now.AddMilliseconds(MaxWaitTimeMS);
            while (GetTargetID() == 0 && DateTime.Now < endTime)
                Thread.Sleep(10);

            return (DateTime.Now < endTime && GetTargetID() > 0);
        }
        public void CancelTarget()
        {
            _client.SendPacket(PacketType.SCCancelTarget);
        }
        public void TargetToObject(uint ObjectID)
        {
            _client.SendPacket(PacketType.SCTargetToObject, ObjectID);
        }
        public void TargetToXYZ(ushort x, ushort y, sbyte z)
        {
            _client.SendPacket(PacketType.SCTargetToXYZ, x, y, z);
        }
        public void TargetToTile(ushort tileModel, ushort x, ushort y, sbyte z)
        {
            _client.SendPacket(PacketType.SCTargetToTile, tileModel, x, y, z);
        }
        #endregion

        #region WaitTarget
        public void WaitTargetObject(uint objId)
        {
            _client.SendPacket(PacketType.SCWaitTargetObject, objId);
        }
        public void WaitTargetTile(ushort tile, ushort x, ushort y, sbyte z)
        {
            _client.SendPacket(PacketType.SCWaitTargetTile, tile, x, y, z);
        }
        public void WaitTargetXYZ(ushort x, ushort y, sbyte z)
        {
            _client.SendPacket(PacketType.SCWaitTargetXYZ, x, y, z);
        }
        public void WaitTargetSelf()
        {
            _client.SendPacket(PacketType.SCWaitTargetSelf);

        }
        public void WaitTargetType(ushort objType)
        {
            _client.SendPacket(PacketType.SCWaitTargetType, objType);
        }
        public void CancelWaitTarget()
        {
            _client.SendPacket(PacketType.SCCancelWaitTarget);

        }
        public void WaitTargetGround(ushort objType)
        {
            _client.SendPacket(PacketType.SCWaitTargetGround, objType);


        }
        public void WaitTargetLast()
        {
            _client.SendPacket(PacketType.SCWaitTargetLast);

        }
        #endregion

        #region Wait
        public void Wait(int WaitTimeMS)
        {
            Thread.Sleep(WaitTimeMS);
        }
        #endregion

        #region Ability
        public void UsePrimaryAbility()
        {
            _client.SendPacket(PacketType.SCUsePrimaryAbility);
        }
        public void UseSecondaryAbility()
        {
            _client.SendPacket(PacketType.SCUseSecondaryAbility);
        }
        public string GetAbility()
        {
            return _client.SendPacket<string>(PacketType.SCGetAbility);
        }
        public void ToggleFly()
        {
            _client.SendPacket(PacketType.SCToggleFly);
        }
        #endregion

        #region Skills Func
        #region GetSkillID
        public bool GetSkillID(Skill skill, out int skillId)
        {
            if (!skill.IsValidId)
                skill.Id = _client.SendPacket<int>(PacketType.SCGetSkillID, skill.Value);

            skillId = skill.Id;
            return skill.IsValidId;
        }
        #endregion

        #region UseSkill
        public bool UseSkill(Skill skill)
        {
            int skillId;
            if (!GetSkillID(skill, out skillId))
            {
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Unknown skill name]");
                return false;
            }

            _client.SendPacket(PacketType.SCUseSkill, skillId);
            return true;
        }

        public bool UseSkill(SkillName ske)
        {
            switch (ske)
            {
                case SkillName.Alchemy:
                    return UseSkill(Skill.Alchemy);
                case SkillName.Anatomy:
                    return UseSkill(Skill.Anatomy);
                case SkillName.AnimalLore:
                    return UseSkill(Skill.AnimalLore);
                case SkillName.AnimalTaming:
                    return UseSkill(Skill.AnimalTaming);
                case SkillName.Archery:
                    return UseSkill(Skill.Archery);
                case SkillName.ArmsLore:
                    return UseSkill(Skill.Armslore);
                case SkillName.Begging:
                    return UseSkill(Skill.Begging);
                case SkillName.Blacksmith:
                    return UseSkill(Skill.Blacksmithy);
                case SkillName.Bushido:
                    return UseSkill(Skill.Bushido);
                case SkillName.Camping:
                    return UseSkill(Skill.Camping);
                case SkillName.Carpentry:
                    return UseSkill(Skill.Carpentry);
                case SkillName.Cartography:
                    return UseSkill(Skill.Cartography);
                case SkillName.Chivalry:
                    return UseSkill(Skill.Chivalry);
                case SkillName.Cooking:
                    return UseSkill(Skill.Cooking);
                case SkillName.DetectHidden:
                    return UseSkill(Skill.DetectHidden);
                case SkillName.Discordance:
                    return UseSkill(Skill.Discordance);
                case SkillName.EvalInt:
                    return UseSkill(Skill.EvaluateIntelligence);
                case SkillName.Fencing:
                    return UseSkill(Skill.Fencing);
                case SkillName.Fishing:
                    return UseSkill(Skill.Fishing);
                case SkillName.Fletching:
                    return UseSkill(Skill.Bowcraft);
                case SkillName.Focus:
                    return UseSkill(Skill.Focus);
                case SkillName.Forensics:
                    return UseSkill(Skill.Forensic);
                case SkillName.Healing:
                    return UseSkill(Skill.Healing);
                case SkillName.Herding:
                    return UseSkill(Skill.Herding);
                case SkillName.Hiding:
                    return UseSkill(Skill.Hiding);
                case SkillName.Imbuing:
                    return UseSkill(Skill.Imbuing);
                case SkillName.Inscribe:
                    return UseSkill(Skill.Inscription);
                case SkillName.ItemID:
                    return UseSkill(Skill.ItemIdentification);
                case SkillName.Lockpicking:
                    return UseSkill(Skill.Lockpicking);
                case SkillName.Lumberjacking:
                    return UseSkill(Skill.Lumberjacking);
                case SkillName.Macing:
                    return UseSkill(Skill.MaceFighting);
                case SkillName.Magery:
                    return UseSkill(Skill.Magery);
                case SkillName.MagicResist:
                    return UseSkill(Skill.ResistingSpells);
                case SkillName.Meditation:
                    return UseSkill(Skill.Meditation);
                case SkillName.Mining:
                    return UseSkill(Skill.Mining);
                case SkillName.Musicianship:
                    return UseSkill(Skill.Musicianship);
                case SkillName.Mysticism:
                    return UseSkill(Skill.Mysticism);
                case SkillName.Necromancy:
                    return UseSkill(Skill.Necromancy);
                case SkillName.Ninjitsu:
                    return UseSkill(Skill.Ninjitsu);
                case SkillName.RemoveTrap:
                    return UseSkill(Skill.RemoveTrap);
                case SkillName.Stealth:
                    return UseSkill(Skill.Stealth);
                case SkillName.Snooping:
                    return UseSkill(Skill.Snooping);
                case SkillName.Spellweaving:
                    return UseSkill(Skill.Spellweaving);
                case SkillName.SpiritSpeak:
                    return UseSkill(Skill.SpiritSpeak);
                case SkillName.Stealing:
                    return UseSkill(Skill.Stealing);
                case SkillName.Swords:
                    return UseSkill(Skill.Swordsmanship);
                case SkillName.Tactics:
                    return UseSkill(Skill.Tactics);
                case SkillName.Tailoring:
                    return UseSkill(Skill.Tailoring);
                case SkillName.TasteID:
                    return UseSkill(Skill.TasteIdentification);
                case SkillName.Throwing:
                    return UseSkill(Skill.Throwing);
                case SkillName.Tinkering:
                    return UseSkill(Skill.Tinkering);
                case SkillName.Tracking:
                    return UseSkill(Skill.Tracking);
                case SkillName.Veterinary:
                    return UseSkill(Skill.Veterinary);
                case SkillName.Wrestling:
                    return UseSkill(Skill.Wrestling);
            }

            return false;
        }
        #endregion

        #region ChangeSkillLockState
        public void ChangeSkillLockState(Skill skill, byte skillState)
        {
            int skillId;
            if (!GetSkillID(skill, out skillId))
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Unknown skill name]");
            else
                _client.SendPacket(PacketType.SCChangeSkillLockState, skillId, skillState);
        }

        public void ChangeSkillLockState(SkillName ske, byte skillState)
        {
            Skill sk = new Skill();

            switch (ske)
            {
                case SkillName.Alchemy:
                    ChangeSkillLockState(Skill.Alchemy, skillState);
                    break;
                case SkillName.Anatomy:
                    ChangeSkillLockState(Skill.Anatomy, skillState);
                    break;
                case SkillName.AnimalLore:
                    ChangeSkillLockState(Skill.AnimalLore, skillState);
                    break;
                case SkillName.AnimalTaming:
                    ChangeSkillLockState(Skill.AnimalTaming, skillState);
                    break;
                case SkillName.Archery:
                    ChangeSkillLockState(Skill.Archery, skillState);
                    break;
                case SkillName.ArmsLore:
                    ChangeSkillLockState(Skill.Armslore, skillState);
                    break;
                case SkillName.Begging:
                    ChangeSkillLockState(Skill.Begging, skillState);
                    break;
                case SkillName.Blacksmith:
                    ChangeSkillLockState(Skill.Blacksmithy, skillState);
                    break;
                case SkillName.Bushido:
                    ChangeSkillLockState(Skill.Bushido, skillState);
                    break;
                case SkillName.Camping:
                    ChangeSkillLockState(Skill.Camping, skillState);
                    break;
                case SkillName.Carpentry:
                    ChangeSkillLockState(Skill.Carpentry, skillState);
                    break;
                case SkillName.Cartography:
                    ChangeSkillLockState(Skill.Cartography, skillState);
                    break;
                case SkillName.Chivalry:
                    ChangeSkillLockState(Skill.Chivalry, skillState);
                    break;
                case SkillName.Cooking:
                    ChangeSkillLockState(Skill.Cooking, skillState);
                    break;
                case SkillName.DetectHidden:
                    ChangeSkillLockState(Skill.DetectHidden, skillState);
                    break;
                case SkillName.Discordance:
                    ChangeSkillLockState(Skill.Discordance, skillState);
                    break;
                case SkillName.EvalInt:
                    ChangeSkillLockState(Skill.EvaluateIntelligence, skillState);
                    break;
                case SkillName.Fencing:
                    ChangeSkillLockState(Skill.Fencing, skillState);
                    break;
                case SkillName.Fishing:
                    ChangeSkillLockState(Skill.Fishing, skillState);
                    break;
                case SkillName.Fletching:
                    ChangeSkillLockState(Skill.Bowcraft, skillState);
                    break;
                case SkillName.Focus:
                    ChangeSkillLockState(Skill.Focus, skillState);
                    break;
                case SkillName.Forensics:
                    ChangeSkillLockState(Skill.Forensic, skillState);
                    break;
                case SkillName.Healing:
                    ChangeSkillLockState(Skill.Healing, skillState);
                    break;
                case SkillName.Herding:
                    ChangeSkillLockState(Skill.Herding, skillState);
                    break;
                case SkillName.Hiding:
                    ChangeSkillLockState(Skill.Hiding, skillState);
                    break;
                case SkillName.Imbuing:
                    ChangeSkillLockState(Skill.Imbuing, skillState);
                    break;
                case SkillName.Inscribe:
                    ChangeSkillLockState(Skill.Inscription, skillState);
                    break;
                case SkillName.ItemID:
                    ChangeSkillLockState(Skill.ItemIdentification, skillState);
                    break;
                case SkillName.Lockpicking:
                    ChangeSkillLockState(Skill.Lockpicking, skillState);
                    break;
                case SkillName.Lumberjacking:
                    ChangeSkillLockState(Skill.Lumberjacking, skillState);
                    break;
                case SkillName.Macing:
                    ChangeSkillLockState(Skill.MaceFighting, skillState);
                    break;
                case SkillName.Magery:
                    ChangeSkillLockState(Skill.Magery, skillState);
                    break;
                case SkillName.MagicResist:
                    ChangeSkillLockState(Skill.ResistingSpells, skillState);
                    break;
                case SkillName.Meditation:
                    ChangeSkillLockState(Skill.Meditation, skillState);
                    break;
                case SkillName.Mining:
                    ChangeSkillLockState(Skill.Mining, skillState);
                    break;
                case SkillName.Musicianship:
                    ChangeSkillLockState(Skill.Musicianship, skillState);
                    break;
                case SkillName.Mysticism:
                    ChangeSkillLockState(Skill.Mysticism, skillState);
                    break;
                case SkillName.Necromancy:
                    ChangeSkillLockState(Skill.Necromancy, skillState);
                    break;
                case SkillName.Ninjitsu:
                    ChangeSkillLockState(Skill.Ninjitsu, skillState);
                    break;
                case SkillName.RemoveTrap:
                    ChangeSkillLockState(Skill.RemoveTrap, skillState);
                    break;
                case SkillName.Stealth:
                    ChangeSkillLockState(Skill.Stealth, skillState);
                    break;
                case SkillName.Snooping:
                    ChangeSkillLockState(Skill.Snooping, skillState);
                    break;
                case SkillName.Spellweaving:
                    ChangeSkillLockState(Skill.Spellweaving, skillState);
                    break;
                case SkillName.SpiritSpeak:
                    ChangeSkillLockState(Skill.SpiritSpeak, skillState);
                    break;
                case SkillName.Stealing:
                    ChangeSkillLockState(Skill.Stealing, skillState);
                    break;
                case SkillName.Swords:
                    ChangeSkillLockState(Skill.Swordsmanship, skillState);
                    break;
                case SkillName.Tactics:
                    ChangeSkillLockState(Skill.Tactics, skillState);
                    break;
                case SkillName.Tailoring:
                    ChangeSkillLockState(Skill.Tailoring, skillState);
                    break;
                case SkillName.TasteID:
                    ChangeSkillLockState(Skill.TasteIdentification, skillState);
                    break;
                case SkillName.Throwing:
                    ChangeSkillLockState(Skill.Throwing, skillState);
                    break;
                case SkillName.Tinkering:
                    ChangeSkillLockState(Skill.Tinkering, skillState);
                    break;
                case SkillName.Tracking:
                    ChangeSkillLockState(Skill.Tracking, skillState);
                    break;
                case SkillName.Veterinary:
                    ChangeSkillLockState(Skill.Veterinary, skillState);
                    break;
                case SkillName.Wrestling:
                    ChangeSkillLockState(Skill.Wrestling, skillState);
                    break;
            }
        }
        #endregion

        #region GetSkillCap

        public double GetSkillCap(SkillName ske)
        {
            Skill sk = new Skill();

            switch (ske)
            {
                case SkillName.Alchemy:
                    return GetSkillCap(Skill.Alchemy);
                case SkillName.Anatomy:
                    return GetSkillCap(Skill.Anatomy);
                case SkillName.AnimalLore:
                    return GetSkillCap(Skill.AnimalLore);
                case SkillName.AnimalTaming:
                    return GetSkillCap(Skill.AnimalTaming);
                case SkillName.Archery:
                    return GetSkillCap(Skill.Archery);
                case SkillName.ArmsLore:
                    return GetSkillCap(Skill.Armslore);
                case SkillName.Begging:
                    return GetSkillCap(Skill.Begging);
                case SkillName.Blacksmith:
                    return GetSkillCap(Skill.Blacksmithy);
                case SkillName.Bushido:
                    return GetSkillCap(Skill.Bushido);
                case SkillName.Camping:
                    return GetSkillCap(Skill.Camping);
                case SkillName.Carpentry:
                    return GetSkillCap(Skill.Carpentry);
                case SkillName.Cartography:
                    return GetSkillCap(Skill.Cartography);
                case SkillName.Chivalry:
                    return GetSkillCap(Skill.Chivalry);
                case SkillName.Cooking:
                    return GetSkillCap(Skill.Cooking);
                case SkillName.DetectHidden:
                    return GetSkillCap(Skill.DetectHidden);
                case SkillName.Discordance:
                    return GetSkillCap(Skill.Discordance);
                case SkillName.EvalInt:
                    return GetSkillCap(Skill.EvaluateIntelligence);
                case SkillName.Fencing:
                    return GetSkillCap(Skill.Fencing);
                case SkillName.Fishing:
                    return GetSkillCap(Skill.Fishing);
                case SkillName.Fletching:
                    return GetSkillCap(Skill.Bowcraft);
                case SkillName.Focus:
                    return GetSkillCap(Skill.Focus);
                case SkillName.Forensics:
                    return GetSkillCap(Skill.Forensic);
                case SkillName.Healing:
                    return GetSkillCap(Skill.Healing);
                case SkillName.Herding:
                    return GetSkillCap(Skill.Herding);
                case SkillName.Hiding:
                    return GetSkillCap(Skill.Hiding);
                case SkillName.Imbuing:
                    return GetSkillCap(Skill.Imbuing);
                case SkillName.Inscribe:
                    return GetSkillCap(Skill.Inscription);
                case SkillName.ItemID:
                    return GetSkillCap(Skill.ItemIdentification);
                case SkillName.Lockpicking:
                    return GetSkillCap(Skill.Lockpicking);
                case SkillName.Lumberjacking:
                    return GetSkillCap(Skill.Lumberjacking);
                case SkillName.Macing:
                    return GetSkillCap(Skill.MaceFighting);
                case SkillName.Magery:
                    return GetSkillCap(Skill.Magery);
                case SkillName.MagicResist:
                    return GetSkillCap(Skill.ResistingSpells);
                case SkillName.Meditation:
                    return GetSkillCap(Skill.Meditation);
                case SkillName.Mining:
                    return GetSkillCap(Skill.Mining);
                case SkillName.Musicianship:
                    return GetSkillCap(Skill.Musicianship);
                case SkillName.Mysticism:
                    return GetSkillCap(Skill.Mysticism);
                case SkillName.Necromancy:
                    return GetSkillCap(Skill.Necromancy);
                case SkillName.Ninjitsu:
                    return GetSkillCap(Skill.Ninjitsu);
                case SkillName.RemoveTrap:
                    return GetSkillCap(Skill.RemoveTrap);
                case SkillName.Stealth:
                    return GetSkillCap(Skill.Stealth);
                case SkillName.Snooping:
                    return GetSkillCap(Skill.Snooping);
                case SkillName.Spellweaving:
                    return GetSkillCap(Skill.Spellweaving);
                case SkillName.SpiritSpeak:
                    return GetSkillCap(Skill.SpiritSpeak);
                case SkillName.Stealing:
                    return GetSkillCap(Skill.Stealing);
                case SkillName.Swords:
                    return GetSkillCap(Skill.Swordsmanship);
                case SkillName.Tactics:
                    return GetSkillCap(Skill.Tactics);
                case SkillName.Tailoring:
                    return GetSkillCap(Skill.Tailoring);
                case SkillName.TasteID:
                    return GetSkillCap(Skill.TasteIdentification);
                case SkillName.Throwing:
                    return GetSkillCap(Skill.Throwing);
                case SkillName.Tinkering:
                    return GetSkillCap(Skill.Tinkering);
                case SkillName.Tracking:
                    return GetSkillCap(Skill.Tracking);
                case SkillName.Veterinary:
                    return GetSkillCap(Skill.Veterinary);
                case SkillName.Wrestling:
                    return GetSkillCap(Skill.Wrestling);
            }
            return 0.0;
        }

        public double GetSkillCap(Skill skill)
        {
            int skillId;
            if (!GetSkillID(skill, out skillId))
            {
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Unknown skill name]");
                return -1;
            }
            return _client.SendPacket<double>(PacketType.SCGetSkillCap, skillId);
        }

        #endregion

        #region GetSkillValue
        public double GetSkillValue(Skill skill)
        {
            int skillId;
            if (!GetSkillID(skill, out skillId))
            {
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Unknown skill name]");
                return -1;
            }
            return _client.SendPacket<double>(PacketType.SCSkillValue, skillId);
        }

        public double GetSkillValue(SkillName ske)
        {
            Skill sk = new Skill();

            switch (ske)
            {
                case SkillName.Alchemy:
                    return GetSkillValue(Skill.Alchemy);
                case SkillName.Anatomy:
                    return GetSkillValue(Skill.Anatomy);
                case SkillName.AnimalLore:
                    return GetSkillValue(Skill.AnimalLore);
                case SkillName.AnimalTaming:
                    return GetSkillValue(Skill.AnimalTaming);
                case SkillName.Archery:
                    return GetSkillValue(Skill.Archery);
                case SkillName.ArmsLore:
                    return GetSkillValue(Skill.Armslore);
                case SkillName.Begging:
                    return GetSkillValue(Skill.Begging);
                case SkillName.Blacksmith:
                    return GetSkillValue(Skill.Blacksmithy);
                case SkillName.Bushido:
                    return GetSkillValue(Skill.Bushido);
                case SkillName.Camping:
                    return GetSkillValue(Skill.Camping);
                case SkillName.Carpentry:
                    return GetSkillValue(Skill.Carpentry);
                case SkillName.Cartography:
                    return GetSkillValue(Skill.Cartography);
                case SkillName.Chivalry:
                    return GetSkillValue(Skill.Chivalry);
                case SkillName.Cooking:
                    return GetSkillValue(Skill.Cooking);
                case SkillName.DetectHidden:
                    return GetSkillValue(Skill.DetectHidden);
                case SkillName.Discordance:
                    return GetSkillValue(Skill.Discordance);
                case SkillName.EvalInt:
                    return GetSkillValue(Skill.EvaluateIntelligence);
                case SkillName.Fencing:
                    return GetSkillValue(Skill.Fencing);
                case SkillName.Fishing:
                    return GetSkillValue(Skill.Fishing);
                case SkillName.Fletching:
                    return GetSkillValue(Skill.Bowcraft);
                case SkillName.Focus:
                    return GetSkillValue(Skill.Focus);
                case SkillName.Forensics:
                    return GetSkillValue(Skill.Forensic);
                case SkillName.Healing:
                    return GetSkillValue(Skill.Healing);
                case SkillName.Herding:
                    return GetSkillValue(Skill.Herding);
                case SkillName.Hiding:
                    return GetSkillValue(Skill.Hiding);
                case SkillName.Imbuing:
                    return GetSkillValue(Skill.Imbuing);
                case SkillName.Inscribe:
                    return GetSkillValue(Skill.Inscription);
                case SkillName.ItemID:
                    return GetSkillValue(Skill.ItemIdentification);
                case SkillName.Lockpicking:
                    return GetSkillValue(Skill.Lockpicking);
                case SkillName.Lumberjacking:
                    return GetSkillValue(Skill.Lumberjacking);
                case SkillName.Macing:
                    return GetSkillValue(Skill.MaceFighting);
                case SkillName.Magery:
                    return GetSkillValue(Skill.Magery);
                case SkillName.MagicResist:
                    return GetSkillValue(Skill.ResistingSpells);
                case SkillName.Meditation:
                    return GetSkillValue(Skill.Meditation);
                case SkillName.Mining:
                    return GetSkillValue(Skill.Mining);
                case SkillName.Musicianship:
                    return GetSkillValue(Skill.Musicianship);
                case SkillName.Mysticism:
                    return GetSkillValue(Skill.Mysticism);
                case SkillName.Necromancy:
                    return GetSkillValue(Skill.Necromancy);
                case SkillName.Ninjitsu:
                    return GetSkillValue(Skill.Ninjitsu);
                case SkillName.RemoveTrap:
                    return GetSkillValue(Skill.RemoveTrap);
                case SkillName.Stealth:
                    return GetSkillValue(Skill.Stealth);
                case SkillName.Snooping:
                    return GetSkillValue(Skill.Snooping);
                case SkillName.Spellweaving:
                    return GetSkillValue(Skill.Spellweaving);
                case SkillName.SpiritSpeak:
                    return GetSkillValue(Skill.SpiritSpeak);
                case SkillName.Stealing:
                    return GetSkillValue(Skill.Stealing);
                case SkillName.Swords:
                    return GetSkillValue(Skill.Swordsmanship);
                case SkillName.Tactics:
                    return GetSkillValue(Skill.Tactics);
                case SkillName.Tailoring:
                    return GetSkillValue(Skill.Tailoring);
                case SkillName.TasteID:
                    return GetSkillValue(Skill.TasteIdentification);
                case SkillName.Throwing:
                    return GetSkillValue(Skill.Throwing);
                case SkillName.Tinkering:
                    return GetSkillValue(Skill.Tinkering);
                case SkillName.Tracking:
                    return GetSkillValue(Skill.Tracking);
                case SkillName.Veterinary:
                    return GetSkillValue(Skill.Veterinary);
                case SkillName.Wrestling:
                    return GetSkillValue(Skill.Wrestling);
            }
            return 0.0;
        }

        public double GetSkillCurrentValue(Skill skill)
        {
            int skillId;
            if (!GetSkillID(skill, out skillId))
            {
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Unknown skill name]");
                return -1;
            }
            return _client.SendPacket<double>(PacketType.SCSkillCurrentValue, skillId);
        }

        public double GetSkillCurrentValue(SkillName ske)
        {
            Skill sk = new Skill();

            switch (ske)
            {
                case SkillName.Alchemy:
                    return GetSkillCurrentValue(Skill.Alchemy);
                case SkillName.Anatomy:
                    return GetSkillCurrentValue(Skill.Anatomy);
                case SkillName.AnimalLore:
                    return GetSkillCurrentValue(Skill.AnimalLore);
                case SkillName.AnimalTaming:
                    return GetSkillCurrentValue(Skill.AnimalTaming);
                case SkillName.Archery:
                    return GetSkillCurrentValue(Skill.Archery);
                case SkillName.ArmsLore:
                    return GetSkillCurrentValue(Skill.Armslore);
                case SkillName.Begging:
                    return GetSkillCurrentValue(Skill.Begging);
                case SkillName.Blacksmith:
                    return GetSkillCurrentValue(Skill.Blacksmithy);
                case SkillName.Bushido:
                    return GetSkillCurrentValue(Skill.Bushido);
                case SkillName.Camping:
                    return GetSkillCurrentValue(Skill.Camping);
                case SkillName.Carpentry:
                    return GetSkillCurrentValue(Skill.Carpentry);
                case SkillName.Cartography:
                    return GetSkillCurrentValue(Skill.Cartography);
                case SkillName.Chivalry:
                    return GetSkillCurrentValue(Skill.Chivalry);
                case SkillName.Cooking:
                    return GetSkillCurrentValue(Skill.Cooking);
                case SkillName.DetectHidden:
                    return GetSkillCurrentValue(Skill.DetectHidden);
                case SkillName.Discordance:
                    return GetSkillCurrentValue(Skill.Discordance);
                case SkillName.EvalInt:
                    return GetSkillCurrentValue(Skill.EvaluateIntelligence);
                case SkillName.Fencing:
                    return GetSkillCurrentValue(Skill.Fencing);
                case SkillName.Fishing:
                    return GetSkillCurrentValue(Skill.Fishing);
                case SkillName.Fletching:
                    return GetSkillCurrentValue(Skill.Bowcraft);
                case SkillName.Focus:
                    return GetSkillCurrentValue(Skill.Focus);
                case SkillName.Forensics:
                    return GetSkillCurrentValue(Skill.Forensic);
                case SkillName.Healing:
                    return GetSkillCurrentValue(Skill.Healing);
                case SkillName.Herding:
                    return GetSkillCurrentValue(Skill.Herding);
                case SkillName.Hiding:
                    return GetSkillCurrentValue(Skill.Hiding);
                case SkillName.Imbuing:
                    return GetSkillCurrentValue(Skill.Imbuing);
                case SkillName.Inscribe:
                    return GetSkillCurrentValue(Skill.Inscription);
                case SkillName.ItemID:
                    return GetSkillCurrentValue(Skill.ItemIdentification);
                case SkillName.Lockpicking:
                    return GetSkillCurrentValue(Skill.Lockpicking);
                case SkillName.Lumberjacking:
                    return GetSkillCurrentValue(Skill.Lumberjacking);
                case SkillName.Macing:
                    return GetSkillCurrentValue(Skill.MaceFighting);
                case SkillName.Magery:
                    return GetSkillCurrentValue(Skill.Magery);
                case SkillName.MagicResist:
                    return GetSkillCurrentValue(Skill.ResistingSpells);
                case SkillName.Meditation:
                    return GetSkillCurrentValue(Skill.Meditation);
                case SkillName.Mining:
                    return GetSkillCurrentValue(Skill.Mining);
                case SkillName.Musicianship:
                    return GetSkillCurrentValue(Skill.Musicianship);
                case SkillName.Mysticism:
                    return GetSkillCurrentValue(Skill.Mysticism);
                case SkillName.Necromancy:
                    return GetSkillCurrentValue(Skill.Necromancy);
                case SkillName.Ninjitsu:
                    return GetSkillCurrentValue(Skill.Ninjitsu);
                case SkillName.RemoveTrap:
                    return GetSkillCurrentValue(Skill.RemoveTrap);
                case SkillName.Stealth:
                    return GetSkillCurrentValue(Skill.Stealth);
                case SkillName.Snooping:
                    return GetSkillCurrentValue(Skill.Snooping);
                case SkillName.Spellweaving:
                    return GetSkillCurrentValue(Skill.Spellweaving);
                case SkillName.SpiritSpeak:
                    return GetSkillCurrentValue(Skill.SpiritSpeak);
                case SkillName.Stealing:
                    return GetSkillCurrentValue(Skill.Stealing);
                case SkillName.Swords:
                    return GetSkillCurrentValue(Skill.Swordsmanship);
                case SkillName.Tactics:
                    return GetSkillCurrentValue(Skill.Tactics);
                case SkillName.Tailoring:
                    return GetSkillCurrentValue(Skill.Tailoring);
                case SkillName.TasteID:
                    return GetSkillCurrentValue(Skill.TasteIdentification);
                case SkillName.Throwing:
                    return GetSkillCurrentValue(Skill.Throwing);
                case SkillName.Tinkering:
                    return GetSkillCurrentValue(Skill.Tinkering);
                case SkillName.Tracking:
                    return GetSkillCurrentValue(Skill.Tracking);
                case SkillName.Veterinary:
                    return GetSkillCurrentValue(Skill.Veterinary);
                case SkillName.Wrestling:
                    return GetSkillCurrentValue(Skill.Wrestling);
            }
            return 0.0;
        }
        #endregion
        #endregion

        #region Virtues
        public void ReqVirtuesGump()
        {
            _client.SendPacket(PacketType.SCReqVirtuesGump);
        }

        public void UseVirtue(string virtueName)
        {
            Virtue virtue;
            if (virtueName.GetEnum(out virtue))
                UseVirtue(virtue);
            else
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Unknown virtue name " + virtueName + "]");
        }

        public void UseVirtue(Virtue virtue)
        {
            _client.SendPacket(PacketType.SCUseVirtue, (uint)virtue);
        }
        #endregion

        #region Cast Spell
        public uint GetSpellID(string spellName)
        {
            Spells spell;
            if (spellName.GetEnum(out spell))
                return (uint)spell;
            return 0;
        }

        public bool CastSpell(string spellName)
        {
            uint spellId = GetSpellID(spellName);

            if (spellId == 0)
            {
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Unknown spell name " + spellName + "]");
                return false;
            }

            _client.SendPacket(PacketType.SCCastSpell, spellId);

            return true;
        }
        public bool CastSpellToObj(string spellName, uint objId)
        {
            WaitTargetObject(objId);
            return CastSpell(spellName);
        }
        public bool IsActiveSpellAbility(string spellName)
        {
            uint spellId = GetSpellID(spellName);

            if (spellId == 0)
            {
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Unknown spell name " + spellName + "]");
                return false;
            }

            return _client.SendPacket<bool>(PacketType.SCIsActiveSpellAbility, spellId);
        }
        #endregion

        #region SetCatchBag
        public void UnsetCatchBag()
        {
            _client.SendPacket(PacketType.SCUnsetCatchBag);
        }
        public byte SetCatchBag(uint objectId)
        {
            return _client.SendPacket<byte>(PacketType.SCSetCatchBag, objectId);
        }
        #endregion

        #region UseObject
        public void UseObject(uint objectId)
        {
            _client.SendPacket(PacketType.SCUseObject, objectId);
        }
        public uint UseType(ushort objType, ushort color)
        {
            return _client.SendPacket<uint>(PacketType.SCUseType, objType, color);
        }
        public uint UseType(ushort objType)
        {
            return UseType(objType, 0xFFFF);
        }
        public uint UseFromGround(ushort objType, ushort color)
        {
            return _client.SendPacket<uint>(PacketType.SCUseFromGround, objType, color);
        }
        #endregion

        #region ClickOnObject
        public void ClickOnObject(uint objectId)
        {
            _client.SendPacket(PacketType.SCClickOnObject, objectId);
        }
        #endregion

        #region Line Fields
        public int GetFoundedParamID()
        {
            return _client.SendPacket<int>(PacketType.SCGetFoundedParamID);
        }
        public uint GetLineID()
        {
            return _client.SendPacket<uint>(PacketType.SCGetLineID);
        }
        public ushort GetLineType()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetLineType);
        }
        public string GetLineName()
        {
            return _client.SendPacket<string>(PacketType.SCGetLineName);
        }
        public DateTime GetLineTime()
        {
            return _client.SendPacket<DateTime>(PacketType.SCGetLineTime);
        }
        public byte GetLineMsgType()
        {
            return _client.SendPacket<byte>(PacketType.SCGetLineMsgType);
        }
        public ushort GetLineTextColor()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetLineTextColor);
        }
        public ushort GetLineTextFont()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetLineTextFont);
        }
        public int GetLineIndex()
        {
            return _client.SendPacket<int>(PacketType.SCGetLineIndex);
        }
        public int GetLineCount()
        {
            return _client.SendPacket<int>(PacketType.SCGetLineCount);
        }
        #endregion

        #region Journal
        public void AddJournalIgnore(string str)
        {
            _client.SendPacket(PacketType.SCAddJournalIgnore, str);
        }
        public void ClearJournalIgnore()
        {
            _client.SendPacket(PacketType.SCClearJournalIgnore);
        }
        public void AddChatUserIgnore(string user)
        {
            _client.SendPacket(PacketType.SCUAddChatUserIgnore, user);
        }
        public void ClearChatUserIgnore()
        {
            _client.SendPacket(PacketType.SCClearChatUserIgnore);
        }
        public void ClearJournal()
        {
            _client.SendPacket(PacketType.SCClearJournal);
        }
        public string LastJournalMessage()
        {
            return _client.SendPacket<string>(PacketType.SCLastJournalMessage);
        }
        public int InJournal(string str)
        {
            return _client.SendPacket<int>(PacketType.SCInJournal, str);
        }
        public int InJournalBetweenTimes(string str, DateTime timeBegin, DateTime timeEnd)
        {
            return _client.SendPacket<int>(PacketType.SCInJournalBetweenTimes, str, timeBegin, timeEnd);
        }
        public string Journal(uint stringIndex)
        {
            return _client.SendPacket<string>(PacketType.SCJournal, stringIndex);
        }
        public void SetJournalLine(uint stringIndex, string text)
        {
            _client.SendPacket(PacketType.SCSetJournalLine, stringIndex, text);
        }
        public int LowJournal()
        {
            return _client.SendPacket<int>(PacketType.SCLowJournal);
        }
        public int HighJournal()
        {
            return _client.SendPacket<int>(PacketType.SCHighJournal);
        }
        public bool WaitJournalLine(DateTime startTime, string str, int maxWaitTimeMS)
        {
            bool infinite = maxWaitTimeMS <= 0;

            var StopTime = startTime.AddMilliseconds(maxWaitTimeMS);

            do
            {
                if (InJournalBetweenTimes(str, startTime, infinite ? DateTime.Now : StopTime) >= 0)
                    return true;
                else
                    Wait(100);
            }
            while (infinite || (StopTime > DateTime.Now));
            return false;
        }
        public bool WaitJournalLineSystem(DateTime startTime, string str, int maxWaitTimeMS)
        {
            bool infinite = maxWaitTimeMS <= 0;

            var StopTime = maxWaitTimeMS > 0 ? startTime.AddMilliseconds(maxWaitTimeMS) : DateTime.Now;


            while (infinite || (StopTime > DateTime.Now))
            {
                if (infinite) StopTime = DateTime.Now;
                if ((InJournalBetweenTimes(str, startTime, StopTime) >= 0) && GetLineName().Equals("System"))
                    return true;
                else
                    Wait(100);

            }
            return false;
        }

        public void AddToJournal(string msg)
        {
            _client.SendPacket(PacketType.SCAddToJournal, msg);
        }
        #endregion

        #region Objects
        public void SetFindDistance(uint value)
        {
            _client.SendPacket(PacketType.SCSetFindDistance, value);
        }
        public uint GetFindDistance()
        {
            return _client.SendPacket<uint>(PacketType.SCGetFindDistance);
        }
        public void SetFindVertical(uint value)
        {
            _client.SendPacket(PacketType.SCSetFindVertical, value);
        }
        public uint GetFindVertical()
        {
            return _client.SendPacket<uint>(PacketType.SCGetFindVertical);
        }
        public void SetFindInNulPoint(bool value)
        {
            _client.SendPacket(PacketType.SCSetFindInNulPoint, value);
        }
        public bool GetFindInNulPoint()
        {
            return _client.SendPacket<bool>(PacketType.SCGetFindInNulPoint);
        }
        public uint FindTypeEx(ushort objType, ushort color, uint container, bool inSub)
        {
            return _client.SendPacket<uint>(PacketType.SCFindTypeEx, objType, color, container, inSub);
        }
        public uint FindType(ushort objType, uint container)
        {
            return FindTypeEx(objType, 0xFFFF, container, false);
        }
        public uint FindTypesArrayEx(IEnumerable<ushort> objTypes, IEnumerable<ushort> colors, IEnumerable<uint> containers, bool inSub)
        {
            return _client.SendPacket<uint>(PacketType.SCFindTypesArrayEx, objTypes.ToArray(), colors.ToArray(), containers.ToArray(), inSub);
        }
        public uint FindNotoriety(ushort objType, byte notoriety)
        {
            return _client.SendPacket<uint>(PacketType.SCFindNotoriety, objType, notoriety);
        }
        public uint FindAtCoord(ushort x, ushort y)
        {
            return _client.SendPacket<uint>(PacketType.SCFindAtCoord, x, y);
        }
        public void Ignore(uint objId)
        {
            _client.SendPacket(PacketType.SCIgnore, objId);
        }
        public void IgnoreOff(uint objId)
        {
            _client.SendPacket(PacketType.SCIgnoreOff, objId);
        }
        public void IgnoreReset()
        {
            _client.SendPacket(PacketType.SCIgnoreReset);
        }
        public List<uint> GetIgnoreList()
        {
            return _client.SendPacket<List<uint>>(PacketType.SCGetIgnoreList);
        }
        public List<uint> GetFindList() // Renamed to correct grammar ( GetFindedlist )
        {
            return _client.SendPacket<List<uint>>(PacketType.SCGetFindedList);
        }
        public uint GetFindItem()
        {
            return _client.SendPacket<uint>(PacketType.SCGetFindItem);
        }
        public int GetFindCount()
        {
            return _client.SendPacket<int>(PacketType.SCGetFindCount);
        }
        public int GetFindQuantity()
        {
            return _client.SendPacket<int>(PacketType.SCGetFindQuantity);
        }
        public int GetFindFullQuantity()
        {
            return _client.SendPacket<int>(PacketType.SCGetFindFullQuantity);
        }
        public ushort PredictedX()
        {
            return _client.SendPacket<ushort>(PacketType.SCPredictedX);
        }
        public ushort PredictedY()
        {
            return _client.SendPacket<ushort>(PacketType.SCPredictedY);
        }
        public sbyte PredictedZ()
        {
            return _client.SendPacket<sbyte>(PacketType.SCPredictedZ);
        }
        public byte PredictedDirection()
        {
            return _client.SendPacket<byte>(PacketType.SCPredictedDirection);
        }
        public int GetX(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetX, objId);
        }
        public int GetY(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetY, objId);
        }
        public sbyte GetZ(uint objId)
        {
            return _client.SendPacket<sbyte>(PacketType.SCGetZ, objId);
        }
        public string GetName(uint objId)
        {
            return _client.SendPacket<string>(PacketType.SCGetName, objId);
        }
        public string GetAltName(uint objId)
        {
            return _client.SendPacket<string>(PacketType.SCGetAltName, objId);
        }
        public string GetTitle(uint objId)
        {
            return _client.SendPacket<string>(PacketType.SCGetTitle, objId);
        }
        public string GetTooltip(uint objId)
        {
            return _client.SendPacket<string>(PacketType.SCGetCliloc, objId);
        }
        public string GetTooltip(uint objId, int tryUntilMS)
        {
            DateTime now = DateTime.Now;
            TimeSpan span;
            string ret;
            do
            {
                ret = GetTooltip(objId);
                span = DateTime.Now - now;
                Wait(10);
            } while (string.IsNullOrEmpty(ret) && span.TotalMilliseconds < tryUntilMS);
            return ret;
        }
        public ushort GetType(uint objId)
        {
            return _client.SendPacket<ushort>(PacketType.SCGetType, objId);
        }

        public List<MultiItem> GetMultis()
        {
            return _client.SendPacket<List<MultiItem>>(PacketType.SCGetMultis);
        }

        public ContextMenu GetContextMenuRec()
        {
            return _client.SendPacket<ContextMenu>(PacketType.SCGetContextMenuRec);
        }

        public List<ClilocItemRec> GetClilocRec(uint objId)
        {
            return _client.SendPacket<List<ClilocItemRec>>(PacketType.SCGetToolTipRec, objId);
        }

        public string GetClilocByID(uint clilocId)
        {
            return _client.SendPacket<string>(PacketType.SCGetClilocByID, clilocId);
        }
        public int GetQuantity(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetQuantity, objId);
        }
        public bool IsObjectExists(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsObjectExists, objId);
        }
        public bool IsNPC(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsNPC, objId);
        }
        public uint GetPrice(uint objId)
        {
            return _client.SendPacket<uint>(PacketType.SCGetPrice, objId);
        }
        public byte GetDirection(uint objId)
        {
            return _client.SendPacket<byte>(PacketType.SCGetDirection, objId);
        }
        public int GetDistance(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetDistance, objId);
        }
        public ushort GetColor(uint objId)
        {
            return _client.SendPacket<ushort>(PacketType.SCGetColor, objId);
        }
        public int GetStr(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetStr, objId);
        }
        public int GetInt(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetInt, objId);
        }
        public int GetDex(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetDex, objId);
        }
        public int GetHP(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetHP, objId);
        }
        public int GetMaxHP(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetMaxHP, objId);
        }
        public int GetMana(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetMana, objId);
        }
        public int GetMaxMana(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetMaxMana, objId);
        }
        public int GetStam(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetStam, objId);
        }
        public int GetMaxStam(uint objId)
        {
            return _client.SendPacket<int>(PacketType.SCGetMaxStam, objId);
        }
        public byte GetNotoriety(uint objId)
        {
            return _client.SendPacket<byte>(PacketType.SCGetNotoriety, objId);
        }
        public uint GetParent(uint objId)
        {
            return _client.SendPacket<uint>(PacketType.SCGetParent, objId);
        }
        public bool IsWarMode(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsWarMode, objId);
        }
        public bool IsDead(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsDead, objId);
        }
        public bool IsRunning(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsRunning, objId);
        }
        public bool IsContainer(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsContainer, objId);
        }
        public bool IsHidden(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsHidden, objId);
        }
        public bool IsMovable(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsMovable, objId);
        }
        public bool IsYellowHits(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsYellowHits, objId);
        }
        public bool IsPoisoned(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsPoisoned, objId);
        }
        public bool IsParalyzed(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsParalyzed, objId);
        }
        public bool IsFemale(uint objId)
        {
            return _client.SendPacket<bool>(PacketType.SCIsFemale, objId);
        }
        #endregion

        #region Actions
        public void OpenDoor()
        {
            _client.SendPacket(PacketType.SCOpenDoor);

        }
        public void Bow()
        {
            _client.SendPacket(PacketType.SCBow);

        }
        public void Salute()
        {
            _client.SendPacket(PacketType.SCSalute);

        }
        #endregion

        #region Move Items
        public uint GetPickedUpItem()
        {
            return _client.SendPacket<uint>(PacketType.SCGetPickupedItem);
        }
        public void SetPickedUpItem(uint objId)
        {
            _client.SendPacket(PacketType.SCSetPickupedItem, objId);
        }
        public bool GetDropCheckCoord()
        {
            return _client.SendPacket<bool>(PacketType.SCGetDropCheckCoord);
        }
        public void SetDropCheckCoord(bool value)
        {
            _client.SendPacket(PacketType.SCSetDropCheckCoord, value);
        }
        public uint GetDropDelay()
        {
            return _dropDelay;
        }
        public void SetDropDelay(uint delay)
        {
            _dropDelay = delay;
        }
        public bool DragItem(uint itemId, int count)
        {
            int rescount = count;

            if (IsDead(GetSelfID()))
            {
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Character is dead]");
                return false;
            }

            uint pickedUpItem = GetPickedUpItem();

            if (pickedUpItem != 0 && IsObjectExists(pickedUpItem))
            {
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Must drop current item before dragging a new one]");
                return false;
            }

            int quantity = GetQuantity(itemId);

            if (!IsObjectExists(itemId))
            {
                AddToSystemJournal("Error: " + MethodBase.GetCurrentMethod() + " [Object not found]");
                return false;
            }

            if (count <= 0 || count > quantity) rescount = quantity;

            _client.SendPacket(PacketType.SCDragItem, itemId, rescount);
            return true;
        }
        public bool DropItem(uint moveIntoId, int x, int y, int z)
        {
            _client.SendPacket(PacketType.SCDropItem, moveIntoId, x, y, z);
            return true;
        }
        public bool MoveItem(uint itemId, int count, uint moveIntoId, int x, int y, int z)
        {
            if (DragItem(itemId, count))
                return DropItem(moveIntoId, x, y, z);
            return false;
        }
        public bool Grab(uint itemId, int count)
        {
            return MoveItem(itemId, count, GetBackpackID(), 0, 0, 0);
        }
        public bool Drop(uint itemId, int count, int x, int y, int z)
        {
            return MoveItem(itemId, count, GetGroundID(), x, y, z);
        }
        public bool DropHere(uint itemId)
        {
            return MoveItem(itemId, 0, GetGroundID(), 0, 0, 0);
        }

        internal bool MoveItemsInternal(uint container, ushort itemsType, ushort itemsColor, uint moveIntoId, int x, int y, int z, int delayMs, int maxCount = -1)
        {
            int moveItemsCount;

            FindTypeEx(itemsType, itemsColor, container, false);

            List<uint> foundList = GetFindList();
            if (foundList == null)
                return false;

            if (GetDropDelay() < 50)
                SetDropDelay(50);
            else if (GetDropDelay() > 10000)
                SetDropDelay(10000);

            if (GetDropDelay() > delayMs)
                delayMs = 0;

            if (maxCount == -1 || maxCount > foundList.Count)
                moveItemsCount = foundList.Count;
            else
                moveItemsCount = maxCount;

            for (int i = 0; i < moveItemsCount; i++)
            {
                uint id = foundList[i];
                MoveItem(id, 0, moveIntoId, x, y, z);
                Wait(delayMs);
            }

            return true;
        }
        public bool MoveItems(uint container, ushort itemsType, ushort itemsColor, uint moveIntoId, int x, int y, int z, int delayMs)
        {
            return MoveItemsInternal(container, itemsType, itemsColor, moveIntoId, x, y, z, delayMs);
        }
        public bool EmptyContainer(uint container, uint destContainer, ushort delay_ms)
        {
            return MoveItemsInternal(container, 0xFFFF, 0xFFFF, destContainer, 0xFFFF, 0xFFFF, 0, delay_ms);
        }
        #endregion

        #region ContextMenus
        public void RequestContextMenu(uint id)
        {
            _client.SendPacket(PacketType.SCRequestContextMenu, id);
        }
        public void SetContextMenuHook(uint menuId, byte entryNumber)
        {
            _client.SendPacket(PacketType.SCContextMenuHook, menuId, entryNumber);
        }
        public string GetContextMenu()
        {
            return _client.SendPacket<string>(PacketType.SCGetContextMenu);
        }
        public void ClearContextMenu()
        {
            _client.SendPacket(PacketType.SCClearContextMenu);
        }
        #endregion

        #region Secure Trade
        public bool CheckTradeState()
        {
            return _client.SendPacket<bool>(PacketType.SCCheckTradeState);
        }
        public uint GetTradeContainer(byte tradeNum, byte num)
        {
            return _client.SendPacket<uint>(PacketType.SCGetTradeContainer, tradeNum, num);
        }
        public uint GetTradeOpponent(byte tradeNum)
        {
            return _client.SendPacket<uint>(PacketType.SCGetTradeOpponent, tradeNum);
        }
        public byte GetTradeCount()
        {
            return _client.SendPacket<byte>(PacketType.SCGetTradeCount);
        }
        public string GetTradeOpponentName(byte tradeNum)
        {
            return _client.SendPacket<string>(PacketType.SCGetTradeOpponentName, tradeNum);
        }
        public bool TradeCheck(byte tradeNum, byte num)
        {
            return _client.SendPacket<bool>(PacketType.SCTradeCheck, tradeNum, num);
        }
        public void ConfirmTrade(byte tradeNum)
        {
            _client.SendPacket(PacketType.SCConfirmTrade, tradeNum);
        }
        public bool CancelTrade(byte tradeNum)
        {
            return _client.SendPacket<bool>(PacketType.SCCancelTrade, tradeNum);
        }
        #endregion

        #region Menus
        public void WaitMenu(string menuCaption, string elementCaption)
        {
            _client.SendPacket(PacketType.SCWaitMenu, menuCaption, elementCaption);
        }
        public void AutoMenu(string menuCaption, string elementCaption)
        {
            _client.SendPacket(PacketType.SCAutoMenu, menuCaption, elementCaption);
        }
        public bool MenuHookPresent()
        {
            return _client.SendPacket<bool>(PacketType.SCMenuHookPresent);
        }
        public bool MenuPresent()
        {
            return _client.SendPacket<bool>(PacketType.SCMenuPresent);
        }
        public void CancelMenu()
        {
            _client.SendPacket(PacketType.SCCancelMenu);
        }
        public void CloseMenu()
        {
            _client.SendPacket(PacketType.SCCloseMenu);
        }
        public string GetMenuItems(string menuCaption)
        {
            return _client.SendPacket<string>(PacketType.SCGetMenuItems, menuCaption);
        }
        public string GetLastMenuItems()
        {
            return _client.SendPacket<string>(PacketType.SCGetLastMenuItems);
        }
        #endregion

        #region Gumps
        internal void WaitGumpInt(int value)
        {
            _client.SendPacket(PacketType.SCWaitGumpInt, value);
        }
        internal void WaitGump(string value)
        {
            if (!string.IsNullOrEmpty(value))
                WaitGumpInt(BitConverter.ToInt32(Encoding.Unicode.GetBytes(value.Trim()), 0));
        }
        internal void WaitGumpTextEntry(string value)
        {
            _client.SendPacket(PacketType.SCWaitGumpTextEntry, value);
        }
        internal void GumpAutoTextEntry(int textEntryId, string value)
        {
            _client.SendPacket(PacketType.SCGumpAutoTextEntry, textEntryId, value);
        }
        internal void GumpAutoRadiobutton(int radioButtonId, int value)
        {
            _client.SendPacket(PacketType.SCGumpAutoRadiobutton, radioButtonId, value);
        }
        internal void GumpAutoCheckBox(int checkBoxId, int value)
        {
            _client.SendPacket(PacketType.SCGumpAutoCheckBox, checkBoxId, value);
        }
        internal bool NumGumpButton(ushort gumpIndex, int value)
        {
            return _client.SendPacket<bool>(PacketType.SCNumGumpButton, gumpIndex, value);
        }
        internal bool NumGumpTextEntry(ushort gumpIndex, int textEntryId, string value)
        {
            return _client.SendPacket<bool>(PacketType.SCNumGumpTextEntry, gumpIndex, textEntryId, value);
        }
        internal bool NumGumpRadiobutton(ushort gumpIndex, int radioButtonId, int value)
        {
            return _client.SendPacket<bool>(PacketType.SCNumGumpRadiobutton, gumpIndex, radioButtonId, value);
        }
        internal bool NumGumpCheckBox(ushort gumpIndex, int checkBoxId, int value)
        {
            return _client.SendPacket<bool>(PacketType.SCNumGumpCheckBox, gumpIndex, checkBoxId, value);
        }
        internal uint GetGumpsCount()
        {
            return _client.SendPacket<uint>(PacketType.SCGetGumpsCount);
        }
        internal void CloseSimpleGump(ushort gumpIndex)
        {
            _client.SendPacket(PacketType.SCCloseSimpleGump, gumpIndex);
        }
        internal bool IsGump()
        {
            return GetGumpsCount() > 0;
        }
        internal uint GetGumpSerial(ushort gumpIndex)
        {
            return _client.SendPacket<uint>(PacketType.SCGetGumpSerial, gumpIndex);
        }
        internal uint GetGumpID(ushort gumpIndex)
        {
            return _client.SendPacket<uint>(PacketType.SCGetGumpID, gumpIndex);
        }
        internal bool GetGumpNoClose(ushort gumpIndex)
        {
            return _client.SendPacket<bool>(PacketType.SCGetGumpNoClose, gumpIndex);
        }
        internal List<string> GetGumpTextLines(ushort gumpIndex)
        {
            return _client.SendPacket<List<string>>(PacketType.SCGetGumpTextLines, gumpIndex);
        }
        internal List<string> GetGumpFullLines(ushort gumpIndex)
        {
            return _client.SendPacket<List<string>>(PacketType.SCGetGumpFullLines, gumpIndex);
        }
        internal List<string> GetGumpShortLines(ushort gumpIndex)
        {
            return _client.SendPacket<List<string>>(PacketType.SCGetGumpShortLines, gumpIndex);
        }
        internal List<string> GetGumpButtonsDescription(ushort gumpIndex)
        {
            return _client.SendPacket<List<string>>(PacketType.SCGetGumpButtonsDescription, gumpIndex);
        }
        internal GumpInfo GetGumpInfo(ushort gumpIndex)
        {
            return _client.SendPacket<GumpInfo>(PacketType.SCGetGumpInfo, gumpIndex);
        }

        internal void AddGumpIgnoreByID(uint id)
        {
            _client.SendPacket(PacketType.SCAddGumpIgnoreByID, id);
        }
        internal void AddGumpIgnoreBySerial(uint serial)
        {
            _client.SendPacket(PacketType.SCAddGumpIgnoreBySerial, serial);
        }
        internal void ClearGumpsIgnore()
        {
            _client.SendPacket(PacketType.SCClearGumpsIgnore);
        }
        #endregion

        #region Layers Names

        public static byte GetRhandLayer()
        {
            return (byte)Layers.RHand;
        }

        public static byte GetLhandLayer()
        {
            return (byte)Layers.LHand;
        }

        public static byte GetShoesLayer()
        {
            return (byte)Layers.Shoes;
        }

        public static byte GetPantsLayer()
        {
            return (byte)Layers.Pants;
        }

        public static byte GetShirtLayer()
        {
            return (byte)Layers.Shirt;
        }

        public static byte GetHatLayer()
        {
            return (byte)Layers.Hat;
        }

        public static byte GetGlovesLayer()
        {
            return (byte)Layers.Gloves;
        }

        public static byte GetRingLayer()
        {
            return (byte)Layers.Ring;
        }

        public static byte GetTalismanLayer()
        {
            return (byte)Layers.Talisman;
        }

        public static byte GetNeckLayer()
        {
            return (byte)Layers.Neck;
        }

        public static byte GetHairLayer()
        {
            return (byte)Layers.Hair;
        }

        public static byte GetWaistLayer()
        {
            return (byte)Layers.Waist;
        }

        public static byte GetTorsoLayer()
        {
            return (byte)Layers.Torso;
        }

        public static byte GetBraceLayer()
        {
            return (byte)Layers.Brace;
        }

        public static byte GetBeardLayer()
        {
            return (byte)Layers.Beard;
        }

        public static byte GetTorsoHLayer()
        {
            return (byte)Layers.TorsoH;
        }

        public static byte GetEarLayer()
        {
            return (byte)Layers.Ear;
        }

        public static byte GetArmsLayer()
        {
            return (byte)Layers.Arms;
        }

        public static byte GetCloakLayer()
        {
            return (byte)Layers.Cloak;
        }

        public static byte GetBpackLayer()
        {
            return (byte)Layers.Bpack;
        }

        public static byte GetRobeLayer()
        {
            return (byte)Layers.Robe;
        }

        public static byte GetEggsLayer()
        {
            return (byte)Layers.Eggs;
        }

        public static byte GetLegsLayer()
        {
            return (byte)Layers.Legs;
        }

        public static byte GetHorseLayer()
        {
            return (byte)Layers.Horse;
        }

        public static byte GetRstkLayer()
        {
            return (byte)Layers.Rstk;
        }

        public static byte GetNRstkLayer()
        {
            return (byte)Layers.NRstk;
        }

        public static byte GetSellLayer()
        {
            return (byte)Layers.Sell;
        }

        public static byte GetBankLayer()
        {
            return (byte)Layers.Bank;
        }

        #endregion

        #region LayerInfo
        public uint ObjAtLayerEx(byte layerType, uint playerId)
        {
            if (layerType == 0)
                return 0;

            return _client.SendPacket<uint>(PacketType.SCObjAtLayerEx, layerType, playerId);
        }
        public uint ObjAtLayer(byte layerType)
        {
            return ObjAtLayerEx(layerType, GetSelfID());
        }
        public byte GetLayer(uint obj)
        {
            return _client.SendPacket<byte>(PacketType.SCGetLayer, obj);
        }
        #endregion

        #region layer dress/undress
        public bool WearItem(byte layer, uint obj)
        {
            if (GetPickedUpItem() == 0x00)
                return false;

            if (layer == 0) return false;
            if (GetSelfID() == 0x00) return false;

            _client.SendPacket(PacketType.SCWearItem, layer, obj);
            SetPickedUpItem(0x00);
            return true;
        }
        public bool Disarm()
        {
            bool lh = true;
            bool rh = true;

            uint objId = ObjAtLayerEx(GetLhandLayer(), GetSelfID());
            if (objId != 0)
                lh = MoveItem(objId, 1, GetBackpackID(), 0xff, 0xff, 0);

            objId = ObjAtLayerEx(GetRhandLayer(), GetSelfID());
            if (objId != 0)
                rh = MoveItem(objId, 1, GetBackpackID(), 0xff, 0xff, 0);

            return lh && rh;
        }
        public bool Equip(byte layer, uint obj)
        {
            if (layer == 0)
                return false;

            if (!DragItem(obj, 1))
                return false;

            Wait(5);

            return WearItem(layer, obj);
        }
        public bool EquipType(byte layer, ushort objType)
        {
            uint obj = FindType(objType, GetBackpackID());
            if (obj == 0 || layer == 0)
                return false;

            if (!DragItem(obj, 1))
                return false;

            return WearItem(layer, obj);
        }
        public bool Unequip(byte layer)
        {
            if (layer == 0) return false;
            uint objId = ObjAtLayerEx(layer, GetSelfID());
            if (objId != 0)
                return MoveItem(objId, 1, GetBackpackID(), 0, 0, 0);
            else
                return false;
        }
        public ushort GetDressSpeed()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetDressSpeed);
        }
        public void SetDressSpeed(ushort value)
        {
            _client.SendPacket(PacketType.SCSetDressSpeed);
        }
        public bool Undress()
        {
            bool result = true;
            byte i;
            ushort dressSpeed = GetDressSpeed();

            for (i = 0; i < 0x18; i++)
            {
                if (Enumerable.Range(1, 8).Contains(i) ||
                    Enumerable.Range(0x0C, 0x0E).Contains(i) ||
                    Enumerable.Range(0x11, 0x14).Contains(i) ||
                    Enumerable.Range(0x16, 0x18).Contains(i) ||
                    i == 0x0A)
                {
                    if (ObjAtLayerEx(i, GetSelfID()) > 0)
                    {
                        result = Unequip(0x18);
                        Wait(dressSpeed);
                    }
                }
            }
            return result;
        }

        public void SetDress()
        {
            _client.SendPacket(PacketType.SCSetDress);
        }

        #region Crome : 07.01.2015

        public bool Dress()
        {
            return _client.SendPacket<bool>(PacketType.SCGetDressSet);
        }
        #endregion
        #endregion

        #region Count/CountGround
        public int Count(ushort objType)
        {
            FindType(objType, GetBackpackID());
            return GetFindFullQuantity();
        }
        public int CountGround(ushort objType)
        {
            FindType(objType, GetGroundID());
            return GetFindFullQuantity();
        }
        public int CountEx(ushort objType, ushort color, uint container)
        {
            FindTypeEx(objType, color, container, false);
            return GetFindFullQuantity();
        }
        #endregion

        #region Reagents

        public ushort ConstBP()
        {
            return (ushort)Reagents.BP;
        }
        public ushort ConstBM()
        {
            return (ushort)Reagents.BM;
        }
        public ushort ConstGA()
        {
            return (ushort)Reagents.GA;
        }
        public ushort ConstGS()
        {
            return (ushort)Reagents.GS;
        }
        public ushort ConstMR()
        {
            return (ushort)Reagents.MR;
        }
        public ushort ConstNS()
        {
            return (ushort)Reagents.NS;
        }
        public ushort ConstSA()
        {
            return (ushort)Reagents.SA;
        }
        public ushort ConstSS()
        {
            return (ushort)Reagents.SS;
        }
        public ushort ConstBPCount()
        {
            FindTypeEx(ConstMR(), 0x0000, GetBackpackID(), true);
            return Convert.ToUInt16(GetFindFullQuantity());
        }
        public ushort ConstBMCount()
        {
            FindTypeEx(ConstBM(), 0x0000, GetBackpackID(), true);
            return Convert.ToUInt16(GetFindFullQuantity());
        }
        public ushort ConstGACount()
        {
            FindTypeEx(ConstGA(), 0x0000, GetBackpackID(), true);
            return Convert.ToUInt16(GetFindFullQuantity());
        }
        public ushort ConstGSCount()
        {
            FindTypeEx(ConstGS(), 0x0000, GetBackpackID(), true);
            return Convert.ToUInt16(GetFindFullQuantity());
        }
        public ushort ConstMRCount()
        {
            FindTypeEx(ConstMR(), 0x0000, GetBackpackID(), true);
            return Convert.ToUInt16(GetFindFullQuantity());
        }
        public ushort ConstNSCount()
        {
            FindTypeEx(ConstNS(), 0x0000, GetBackpackID(), true);
            return Convert.ToUInt16(GetFindFullQuantity());
        }
        public ushort ConstSACount()
        {
            FindTypeEx(ConstSA(), 0x0000, GetBackpackID(), true);
            return Convert.ToUInt16(GetFindFullQuantity());
        }
        public ushort ConstSSCount()
        {
            FindTypeEx(ConstSS(), 0x0000, GetBackpackID(), true);
            return Convert.ToUInt16(GetFindFullQuantity());
        }

        #endregion

        #region Shop
        public void AutoBuy(ushort itemType, ushort itemColor, ushort quantity)
        {
            _client.SendPacket(PacketType.SCAutoBuy, itemType, itemColor, quantity);
        }
        public string GetShopList()
        {
            return _client.SendPacket<string>(PacketType.SCGetShopList);
        }
        public void ClearShopList()
        {
            _client.SendPacket(PacketType.SCClearShopList);
        }
        public void AutoBuyEx(ushort itemType, ushort itemColor, ushort quantity, uint price, string name)
        {
            _client.SendPacket(PacketType.SCAutoBuyEx, itemType, itemColor, quantity, price, name);
        }
        public ushort GetAutoBuyDelay()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetAutoBuyDelay);
        }
        public void SetAutoBuyDelay(ushort value)
        {
            _client.SendPacket(PacketType.SCSetAutoBuyDelay, value);
        }
        public ushort GetAutoSellDelay()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetAutoSellDelay);
        }
        public void SetAutoSellDelay(ushort value)
        {
            _client.SendPacket(PacketType.SCSetAutoSellDelay, value);
        }
        public void AutoSell(ushort itemType, ushort itemColor, ushort quantity)
        {
            _client.SendPacket(PacketType.SCAutoSell, itemType, itemColor, quantity);
        }
        #endregion

        #region Other
        public bool PlayWav(string fileName)
        {
            SoundPlayer simpleSound = new SoundPlayer(fileName);
            simpleSound.Play();
            return true;
        }
        public void RequestStats(uint objId)
        {
            _client.SendPacket(PacketType.SCRequestStats, objId);
        }
        public void HelpRequest()
        {
            _client.SendPacket(PacketType.SCHelpRequest);
        }
        public void QuestRequest()
        {
            _client.SendPacket(PacketType.SCQuestRequest);
        }
        public void RenameMobile(uint mobId, string newName)
        {
            _client.SendPacket(PacketType.SCRenameMobile, mobId, newName);
        }
        public bool MobileCanBeRenamed(uint mobId)
        {
            return _client.SendPacket<bool>(PacketType.SCMobileCanBeRenamed, mobId);
        }
        public void ChangeStatLockState(byte statNum, byte statState)
        {
            _client.SendPacket(PacketType.SCChangeStatLockState, statNum, statState);
        }
        public System.Drawing.Bitmap GetStaticArtBitmap(uint id, ushort hue)
        {
            var res = _client.SendPacket<byte[]>(PacketType.SCGetStaticArtBitmap, id, hue);

            if (res == null || res.Length == 0)
                return null;

            MemoryStream ms = new MemoryStream(res);
            return new System.Drawing.Bitmap(ms);
        }

        //TODO:this shit don't work at server
        public void PrintScriptMethodsList(string fileName, bool sortedList)
        {
            _client.SendPacket(PacketType.SCPrintScriptMethodsList, fileName, sortedList);
        }
        public void SetAlarm()
        {
            _client.SendPacket(PacketType.SCSetAlarm);
        }
        public bool CheckLag(int timeoutMS)
        {
            var result = false;
            _client.SendPacket(PacketType.SCCheckLagBegin);
            var stopTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, timeoutMS);
            bool checkLagEndRes;
            do
            {
                Wait(20);
                checkLagEndRes = _client.SendPacket<bool>(PacketType.SCIsCheckLagEnd);
            } while (DateTime.Now <= stopTime && !checkLagEndRes);
            if (checkLagEndRes)
            {
                result = true;
            }
            _client.SendPacket(PacketType.SCCheckLagEnd);

            return result;
        }
        public void SendTextToUO(string text)
        {
            _client.SendPacket(PacketType.SCSendTextToUO, text);
        }
        public void SendTextToUOColor(string text, ushort color)
        {
            _client.SendPacket(PacketType.SCSendTextToUOColor, text, color);
        }
        public void SetGlobal(VarRegion globalRegion, string varName, string varValue)
        {
            _client.SendPacket(PacketType.SCSetGlobal, globalRegion, varName, varValue);
        }
        public string GetGlobal(VarRegion globalRegion, string varName)
        {
            return _client.SendPacket<string>(PacketType.SCGetGlobal, globalRegion, varName);
        }
        public void ConsoleEntryReply(string text)
        {
            _client.SendPacket(PacketType.SCConsoleEntryReply, text);
        }
        public void ConsoleEntryUnicodeReply(string text)
        {
            _client.SendPacket(PacketType.SCConsoleEntryUnicodeReply, text);
        }
        public string GameServerIPstring()
        {
            return _client.SendPacket<string>(PacketType.SCGameServerIPString);
        }
        public void CloseClientGump(uint gumpId)
        {
            _client.SendPacket(PacketType.SCCloseClientGump, gumpId);
        }

        public void ClearInfoWindow()
        {
            _client.SendPacket(PacketType.SCClearInfoWindow);
        }

        #endregion

        #region EasyUO Working
        public void SetEasyUO(int num, string regValue)
        {
            var euokey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"\Software\EasyUO", false);
            if (euokey != null)
                euokey.SetValue("*" + num.ToString(), regValue);
        }
        public string GetEasyUO(int num)
        {
            var euokey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"\Software\EasyUO", false);
            if (euokey != null)
                return euokey.GetValue("*" + num).ToString();
            return string.Empty;
        }

        public ushort EUO2StealthType(string EUO)
        {
            uint a = 0;
            uint i = 1;
            foreach (var c in EUO)
            {
                a += ((c - (uint)65) * i);
                i *= 26;
            }
            a = (a - 7) ^ 0x45;
            if (a > 0xFFFF)
                return 0;

            return (ushort)a;
        }

        public uint EUO2StealthID(string EUO)
        {
            uint ret = 0;
            uint i = 1;
            foreach (var c in EUO)
            {
                ret += (c - (uint)65) * i;
                i *= 26;
            }
            return (ret - 7) ^ 0x45;
        }
        #endregion

        #region HttpWorking
        public void HTTP_Get(string url)
        {
            _client.SendPacket(PacketType.SCHTTP_Get, url);
        }
        public string HTTP_Post(string url, string postData)
        {
            return _client.SendPacket<string>(PacketType.SCHTTP_Post, url, postData);
        }
        public string HTTP_Body()
        {
            return _client.SendPacket<string>(PacketType.SCHTTP_Body);
        }
        public string HTTP_Header()
        {
            return _client.SendPacket<string>(PacketType.SCHTTP_Header);
        }
        #endregion

        #region Party
        public void InviteToParty(uint id)
        {
            _client.SendPacket(PacketType.SCInviteToParty, id);
        }
        public void RemoveFromParty(uint id)
        {
            _client.SendPacket(PacketType.SCRemoveFromParty, id);
        }
        public void PartyMessageTo(uint id, string msg)
        {
            _client.SendPacket(PacketType.SCPartyMessageTo, id, msg);
        }
        public void PartySay(string msg)
        {
            _client.SendPacket(PacketType.SCPartySay, msg);
        }
        public void PartyCanLootMe(bool value)
        {
            _client.SendPacket(PacketType.SCPartyCanLootMe, value);
        }
        public void PartyAcceptInvite()
        {
            _client.SendPacket(PacketType.SCPartyAcceptInvite);
        }
        public void PartyDeclineInvite()
        {
            _client.SendPacket(PacketType.SCPartyDeclineInvite);
        }
        public void PartyLeave()
        {
            _client.SendPacket(PacketType.SCPartyLeave);
        }
        public bool InParty()
        {
            return _client.SendPacket<bool>(PacketType.SCInParty);
        }
        public uint[] PartyMembersList()
        {
            return _client.SendPacket<uint[]>(PacketType.SCPartyMembersList);
            /*byte[] barray = resultArray();
            if (barray == null || barray.Length == 0)
                return null;
            uint[] uarray = new uint[barray.Length / 4];
            Buffer.BlockCopy(barray, 0, uarray, 0, barray.Length);
            return uarray;*/
        }

        #endregion

        #region ICQ
        public bool ICQ_GetConnectedStatus()
        {
            return _client.SendPacket<bool>(PacketType.SCICQ_GetConnectedStatus);
        }
        public void ICQ_Connect(uint uin, string password)
        {
            _client.SendPacket(PacketType.SCICQ_Connect, uin, password);
        }
        public void ICQ_Disconnect()
        {
            _client.SendPacket(PacketType.SCICQ_Disconnect);

        }
        public void ICQ_SetStatus(byte num)
        {
            _client.SendPacket(PacketType.SCICQ_SetStatus, num);
        }
        public void ICQ_SetXStatus(byte num)
        {
            _client.SendPacket(PacketType.SCICQ_SetXStatus, num);
        }
        public void ICQ_SendText(uint toUin, string text)
        {
            _client.SendPacket(PacketType.SCICQ_SendText, toUin, text);
        }
        #endregion

        #region tile Working
        public TileDataFlags ConvertIntegerToFlags(byte group, int flags)
        {
            return _client.SendPacket<TileDataFlags>(PacketType.SCConvertIntegerToFlags, group, flags);
        }

        public uint GetTileFlags(TileFlagsType tileGroup, ushort tile)
        {
            return _client.SendPacket<uint>(PacketType.SCGetTileFlags, tileGroup, tile);
        }
        public TileDataFlags ConvertFlagsToFlagSet(TileFlagsType tileGroup, uint flags)
        {
            return _client.SendPacket<TileDataFlags>(PacketType.SCConvertFlagsToFlagSet, tileGroup, flags);
        }
        public LandTileData GetLandTileData(ushort tile)
        {
            return _client.SendPacket<LandTileData>(PacketType.SCGetLandTileData, tile);
        }
        public StaticTileData GetStaticTileData(ushort tile)
        {
            return _client.SendPacket<StaticTileData>(PacketType.SCGetStaticTileData, tile);
        }
        public MapCell GetCell(ushort x, ushort y, byte worldNum)
        {
            return _client.SendPacket<MapCell>(PacketType.SCGetCell, x, y, worldNum);
        }
        public byte GetLayerCount(ushort x, ushort y, byte worldNum)
        {
            return _client.SendPacket<byte>(PacketType.SCGetLayerCount, x, y, worldNum);
        }
        public List<StaticItemRealXY> ReadStaticsXY(ushort x, ushort y, byte worldNum)
        {
            return _client.SendPacket<List<StaticItemRealXY>>(PacketType.SCReadStaticsXY, x, y, worldNum);
            // Needs testing
        }
        public sbyte GetSurfaceZ(ushort x, ushort y, byte worldNum)
        {
            return _client.SendPacket<sbyte>(PacketType.SCGetSurfaceZ, x, y, worldNum);
        }
        public bool IsWorldCellPassable(ushort currX, ushort currY, sbyte currZ, ushort destX, ushort destY, out sbyte destZ, byte worldNum)
        {
            var res = _client.SendPacket<byte[]>(PacketType.SCIsWorldCellPassable, currX, currY, currZ, destX, destY, worldNum);

            bool ret = BitConverter.ToBoolean(res, 0);
            destZ = (sbyte)res[1];
            return ret;
        }
        public List<FoundTile> GetStaticTilesArray(ushort xMin, ushort yMin, ushort xMax, ushort yMax, byte worldNum, ushort tileType)
        {
            return _client.SendPacket<List<FoundTile>>(PacketType.SCGetStaticTilesArray, xMin, yMin, xMax, yMax, worldNum, tileType);
        }
        public List<FoundTile> GetLandTilesArray(ushort xMin, ushort yMin, ushort xMax, ushort yMax, byte worldNum, ushort tileType)
        {
            return _client.SendPacket<List<FoundTile>>(PacketType.SCGetLandTilesArray, xMin, yMin, xMax, yMax, worldNum, tileType);
        }
        #endregion

        #region Client work
        public void ClientPrint(string text)
        {
            _client.SendPacket(PacketType.SCClientPrint, text);
        }
        public void ClientPrintEx(uint senderId, ushort color, ushort font, string text)
        {
            _client.SendPacket(PacketType.SCClientPrintEx, senderId, color, font, text);
        }

        public void CloseClientUIWindow(UIWindowType uiWindowType, uint id)
        {
            _client.SendPacket(PacketType.SCCloseClientUIWindow, uiWindowType, id);
        }
        public void ClientRequestObjectTarget()
        {
            _client.SendPacket(PacketType.SCClientRequestObjectTarget);
        }
        public void ClientRequestTileTarget()
        {
            _client.SendPacket(PacketType.SCClientRequestTileTarget);
        }
        public bool ClientTargetResponsePresent()
        {
            return _client.SendPacket<bool>(PacketType.SCClientTargetResponsePresent);
        }
        public TargetInfo ClientTargetResponse()
        {
            return _client.SendPacket<TargetInfo>(PacketType.SCClientTargetResponse);
        }
        public bool WaitForClientTargetResponse(int MaxWaitTimeMS)
        {
            DateTime enddate = DateTime.Now.AddMilliseconds(MaxWaitTimeMS);

            while (DateTime.Now < enddate && !ClientTargetResponsePresent())
            {
                Wait(100);
            }

            return (DateTime.Now < enddate && ClientTargetResponsePresent());
        }
        #endregion

        #region QuestArrow
        public Point GetQuestArrow()
        {
            return _client.SendPacket<Point>(PacketType.SCGetQuestArrow);
        }
        #endregion

        #region FillNewWindow
        public bool GetSilentMode()
        {
            return _client.SendPacket<bool>(PacketType.SCGetSilentMode);
        }
        public void SetSilentMode(bool value)
        {
            _client.SendPacket(PacketType.SCSetSilentMode, value);
        }
        public void FillInfoWindow(string str)
        {
            _client.SendPacket(PacketType.SCFillNewWindow, str);
        }
        #endregion

        #region Path
        public string GetStealthPath()
        {
            return _client.SendPacket<string>(PacketType.SCGetStealthPath);
        }

        //TODO:Need test
        public string GetCurrentScriptPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        public string GetStealthProfilePath()
        {
            return _client.SendPacket<string>(PacketType.SCGetStealthProfilePath);
        }
        public string GetShardPath()
        {
            return _client.SendPacket<string>(PacketType.SCGetShardPath);
        }
        #endregion

        #region BuffBarInfo
        public List<BuffIcon> GetBuffBarInfo()
        {
            return _client.SendPacket<List<BuffIcon>>(PacketType.SCGetBuffBarInfo);
        }
        #endregion

        #region Mover
        public uint GetLastStepQUsedDoor()
        {
            return _client.SendPacket<uint>(PacketType.SCGetLastStepQUsedDoor);
        }
        public byte Step(byte direction, bool running)
        {
            return _client.SendPacket<byte>(PacketType.SCStep, direction, running);
        }
        public int StepQ(byte direction, bool running)
        {
            return _client.SendPacket<int>(PacketType.SCStepQ, direction, running);
        }
        public bool MoveXYZ(ushort xDst, ushort yDst, sbyte zDst, int accuracyXY, int accuracyZ, bool running)
        {
            return _client.SendPacket<bool>(PacketType.SCMoveXYZ, xDst, yDst, zDst, accuracyXY, accuracyZ, running);
        }
        public bool newMoveXY(ushort xDst, ushort yDst, bool optimized, int accuracy, bool running)
        {
            return MoveXYZ(xDst, yDst, 0, accuracy, 255, running);
        }
        public bool MoveXY(ushort xDst, ushort yDst, bool optimized, int accuracy, bool running)
        {
            return _client.SendPacket<bool>(PacketType.SCMoveXY, xDst, yDst, optimized, accuracy, running);
        }
        public void SetBadLocation(ushort x, ushort y)
        {
            _client.SendPacket(PacketType.SCSetBadLocation, x, y);
        }
        public void SetGoodLocation(ushort x, ushort y)
        {
            _client.SendPacket(PacketType.SCSetGoodLocation, x, y);
        }
        public void ClearBadLocationList()
        {
            _client.SendPacket(PacketType.SCClearBadLocationList);
        }
        public void SetBadObject(ushort objType, ushort color, byte radius)
        {
            _client.SendPacket(PacketType.SCSetBadObjects, objType, color, radius);
        }
        public void ClearBadObjectList()
        {
            _client.SendPacket(PacketType.SCClearBadObjectList);
        }
        public bool CheckLOS(int xf, int yf, sbyte zf, int xt, int yt, sbyte zt, byte worldNum)
        {
            return _client.SendPacket<bool>(PacketType.SCCheckLOS, xf, yf, zf, xt, yt, zt, worldNum);
        }
        //TODO: Needs testing
        public List<MyPoint> GetPathArray(ushort destX, ushort destY, bool optimized, int accuracy)
        {
            return _client.SendPacket<List<MyPoint>>(PacketType.SCGetPathArray, destX, destY, optimized, accuracy);
        }
        //TODO: Needs testing
        public List<MyPoint> GetPathArray3D(ushort startX, ushort startY, sbyte startZ, ushort finishX, ushort finishY, sbyte finishZ, byte worldNum, int accuracyXY, int accuracyZ, bool run)
        {
            return _client.SendPacket<List<MyPoint>>(PacketType.SCGetPathArray3D, startX, startY, startZ, finishX, finishY, finishZ, worldNum, accuracyXY, accuracyZ, run);
        }

        public short Dist(short x1, short y1, short x2, short y2)
        {
            short dx = (short)Math.Abs(x1 - x2);
            short dy = (short)Math.Abs(y1 - y2);

            short ret = (dx > dy) ? dy : dx;
            short my = (short)(Math.Abs(dx - dy));
            return (short)(ret + my);
        }

        public void CalcCoord(int x, int y, byte dir, out int x2, out int y2)
        {
            x2 = x;
            y2 = y;

            if ((dir == 1) || (dir == 2) || (dir == 3)) x2 = x + 1;
            if ((dir == 5) || (dir == 6) || (dir == 7)) x2 = x - 1;
            if ((dir == 0) || (dir == 4)) x2 = x;

            if ((dir == 3) || (dir == 4) || (dir == 5)) y2 = y + 1;
            if ((dir == 7) || (dir == 0) || (dir == 1)) y2 = y - 1;
            if ((dir == 2) || (dir == 6)) y2 = y;
        }

        public byte CalcDir(int xFrom, int yFrom, int xTo, int yTo) // Added by Crome
        {
            ushort diffx = (ushort)Math.Abs(xFrom - xTo);
            ushort diffy = (ushort)Math.Abs(yFrom - yTo);
            if (diffx == 0 && diffy == 0)
                return 100;
            if ((diffx / (diffy + 0.1)) >= 2)
            {
                if (xFrom > xTo) return 6;
                return 2;
            }
            if ((diffy / (diffx + 0.1)) >= 2)
            {
                if (yFrom > yTo) return 0;
                return 4;
            }
            if (xFrom > xTo && yFrom > yTo) return 7;
            if (xFrom > xTo && yFrom < yTo) return 5;
            if (xFrom < xTo && yFrom > yTo) return 1;
            if (xFrom < xTo && yFrom < yTo) return 3;
            return 0;
        }

        public void SetRunUnmountTimer(ushort value)
        {
            _client.SendPacket(PacketType.SCSetRunUnmountTimer, value);
        }
        public void SetWalkMountTimer(ushort value)
        {
            _client.SendPacket(PacketType.SCSetWalkMountTimer, value);
        }
        public void SetRunMountTimer(ushort value)
        {
            _client.SendPacket(PacketType.SCSetRunMountTimer, value);
        }
        public void SetWalkUnmountTimer(ushort value)
        {
            _client.SendPacket(PacketType.SCSetWalkUnmountTimer, value);
        }
        public ushort GetRunMountTimer()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetRunMountTimer);
        }
        public ushort GetWalkMountTimer()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetWalkMountTimer);
        }
        public ushort GetRunUnmountTimer()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetRunUnmountTimer);
        }
        public ushort GetWalkUnmountTimer()
        {
            return _client.SendPacket<ushort>(PacketType.SCGetWalkUnmountTimer);
        }

        public void SetMoveOpenDoor(bool moveOpenDoor)
        {
            _client.SendPacket(PacketType.SCSetMoveOpenDoor, moveOpenDoor);
        }

        public bool GetMoveOpenDoor()
        {
            return _client.SendPacket<bool>(PacketType.SCGetMoveOpenDoor);
        }

        public void SetMoveThroughNPC(short moveThrougNPC)
        {
            _client.SendPacket(PacketType.SCSetMoveThroughNPC, moveThrougNPC);
        }

        public int GetMoveThroughNPC()
        {
            return _client.SendPacket<int>(PacketType.SCGetMoveThroughNPC);
        }

        public void SetMoveThroughCorner(bool moveThrougCorner)
        {
            _client.SendPacket(PacketType.SCSetMoveThroughCorner, moveThrougCorner);
        }

        public bool GetMoveThroughCorner()
        {
            return _client.SendPacket<bool>(PacketType.SCGetMoveThroughCorner);
        }

        public void SetMoveHeuristicMult(int moveHeuristicMult)
        {
            _client.SendPacket(PacketType.SCSetMoveHeuristicMult, moveHeuristicMult);
        }

        public int GetMoveHeuristicMult()
        {
            return _client.SendPacket<int>(PacketType.SCGetMoveHeuristicMult);
        }

        public void ClearSystemJournal()
        {
            _client.SendPacket(PacketType.SCClearSystemJournal);
        }
        #endregion

    }

}
