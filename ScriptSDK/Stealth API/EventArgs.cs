using System;
#pragma warning disable 1591

namespace StealthAPI
{
    public class StartStopEventArgs
    {
        public StartStopEventArgs(bool isStopped)
        {
            IsStopped = isStopped;
        }
        public bool IsStopped { get; private set; }
    }

    public class ItemEventArgs : EventArgs
    {
        public ItemEventArgs(uint itemId)
        {
            ItemId = itemId;
        }
        public uint ItemId { get; private set; }
    }
    public class ObjectEventArgs : EventArgs
    {
        public ObjectEventArgs(uint objectId)
        {
            ObjectId = objectId;
        }
        public uint ObjectId { get; private set; }
    }
    public class SpeechEventArgs : EventArgs
    {
        public SpeechEventArgs(string text, string senderName, uint senderId)
        {
            Text = text;
            SenderName = senderName;
            SenderId = senderId;
        }
        public string Text { get; private set; }
        public string SenderName { get; private set; }
        public uint SenderId { get; private set; }
    }

    public class MoveRejectionEventArgs : EventArgs
    {
        public MoveRejectionEventArgs(ushort xSource, ushort ySource, byte direction, ushort xDest, ushort yDest)
        {
            XSource = xSource;
            YSource = ySource;
            Direction = direction;
            XDest = xDest;
            YDest = yDest;
        }

        public ushort XSource { get; private set; }
        public ushort YSource { get; private set; }
        public byte Direction { get; private set; }
        public ushort XDest { get; private set; }
        public ushort YDest { get; private set; }
    }
    public class DrawContainerEventArgs : EventArgs
    {
        public DrawContainerEventArgs(uint containerId, ushort modelGump)
        {
            ContainerId = containerId;
            ModelGump = modelGump;
        }
        public uint ContainerId { get; private set; }
        public ushort ModelGump { get; private set; }
    }
    public class AddItemToContainerEventArgs : ItemEventArgs
    {
        public AddItemToContainerEventArgs(uint itemId, uint containerId)
            : base(itemId)
        {
            ContainerId = containerId;
        }
        public uint ContainerId { get; private set; }
    }
    public class AddMultipleItemsInContainerEventArgs : EventArgs
    {
        public AddMultipleItemsInContainerEventArgs(uint containerId)
        {
            ContainerId = containerId;
        }
        public uint ContainerId { get; private set; }
    }
    public class RejectMoveItemEventArgs : EventArgs
    {
        public RejectMoveItemEventArgs(RejectMoveItemReasons reason)
        {
            Reason = reason;
        }
        public RejectMoveItemReasons Reason { get; private set; }
    }
    public class MenuEventArgs : EventArgs
    {
        public MenuEventArgs(uint dialogId, ushort menuId)
        {
            DialogId = dialogId;
            MenuId = menuId;
        }
        public uint DialogId { get; private set; }
        public ushort MenuId { get; private set; }
    }
    public class MapMessageEventArgs : ItemEventArgs
    {
        public MapMessageEventArgs(uint itemId, int centerX, int centerY)
            : base(itemId)
        {
            CenterX = centerX;
            CenterY = centerY;
        }
        public int CenterX { get; private set; }
        public int CenterY { get; private set; }
    }
    public class AllowRefuseAttackEventArgs : EventArgs
    {
        public AllowRefuseAttackEventArgs(uint targetId, bool isAttackOk)
        {
            TargetId = targetId;
            IsAttackOK = isAttackOk;
        }
        public uint TargetId { get; private set; }
        public bool IsAttackOK { get; private set; }
    }
    public class ClilocSpeechEventArgs : EventArgs
    {
        public ClilocSpeechEventArgs(uint senderId, string senderName, uint clilocId, string text)
        {
            SenderId = senderId;
            SenderName = senderName;
            ClilocId = clilocId;
            Text = text;
        }
        public uint SenderId { get; private set; }
        public string SenderName { get; private set; }
        public uint ClilocId { get; private set; }
        public string Text { get; private set; }
    }
    public class ClilocSpeechAffixEventArgs : ClilocSpeechEventArgs
    {
        public ClilocSpeechAffixEventArgs(uint senderId, string senderName, uint clilocId, string affix, string text)
            : base(senderId, senderName, clilocId, text)
        {
            Affix = affix;
        }
        public string Affix { get; private set; }
    }
    public class UnicodeSpeechEventArgs : EventArgs
    {
        public UnicodeSpeechEventArgs(string text, string senderName, uint senderId)
        {
            Text = text;
            SenderName = senderName;
            SenderId = senderId;
        }
        public string Text { get; private set; }
        public string SenderName { get; private set; }
        public uint SenderId { get; private set; }
    }
    public class Buff_DebuffSystemEventArgs : ObjectEventArgs
    {
        public Buff_DebuffSystemEventArgs(uint objectId, ushort attributeId, bool isEnabled)
            : base(objectId)
        {
            AttributeId = attributeId;
            IsEnabled = isEnabled;
        }
        public ushort AttributeId { get; private set; }
        public bool IsEnabled { get; private set; }
    }
    public class CharAnimationEventArgs : ObjectEventArgs
    {
        public CharAnimationEventArgs(uint objectId, uint action)
            : base(objectId)
        {
            Action = action;
        }
        public uint Action { get; private set; }
    }
    public class ICQIncomingTextEventArgs : EventArgs
    {
        public ICQIncomingTextEventArgs(uint uin, string text)
        {
            UIN = uin;
            Text = text;
        }
        public uint UIN { get; private set; }
        public string Text { get; private set; }
    }
    public class ICQErrorEventArgs : EventArgs
    {
        public ICQErrorEventArgs(string text)
        {
            Text = text;
        }
        public string Text { get; private set; }
    }
    public class IncomingGumpEventArgs : EventArgs
    {
        public IncomingGumpEventArgs(uint serial, uint gumpId, uint x, uint y)
        {
            Serial = serial;
            GumpId = gumpId;
            X = x;
            Y = y;
        }
        public uint Serial { get; private set; }
        public uint GumpId { get; private set; }
        public uint X { get; private set; }
        public uint Y { get; private set; }
    }
    public class WindowsMessageEventArgs : EventArgs
    {
        public WindowsMessageEventArgs(uint lParam)
        {
            LParam = lParam;
        }
        public uint LParam { get; private set; }
    }
    public class SoundEventArgs : EventArgs
    {
        public SoundEventArgs(ushort soundId, ushort x, ushort y, int z)
        {
            SoundId = soundId;
            X = x;
            Y = y;
            Z = z;
        }
        public ushort SoundId { get; private set; }
        public ushort X { get; private set; }
        public ushort Y { get; private set; }
        public int Z { get; private set; }
    }
    public class DeathEventArgs : EventArgs
    {
        public DeathEventArgs(bool isDead)
        {
            IsDead = isDead;
        }
        public bool IsDead { get; private set; }
    }
    public class QuestArrowEventArgs : EventArgs
    {
        public QuestArrowEventArgs(ushort x, ushort y, bool isActive)
        {
            X = x;
            Y = y;
            IsActive = isActive;
        }
        public ushort X { get; private set; }
        public ushort Y { get; private set; }
        public bool IsActive { get; private set; }
    }
    public class PartyInviteEventArgs : EventArgs
    {
        public PartyInviteEventArgs(uint inviterId)
        {
            InviterId = inviterId;
        }
        public uint InviterId { get; private set; }
    }
    public class MapPinEventArgs : EventArgs
    {
        public MapPinEventArgs(uint id, byte action,byte pinId,ushort x,ushort y)
        {
            ID = id;
            Action = action;
            PinId = pinId;
            X = x;
            Y = y;
        }

        public uint ID { get; private set; }
        public byte Action { get; set; }
        public byte PinId { get; set; }
        public ushort X { get; set; }
        public ushort Y { get; set; }
    }
    public class GumpTextEntryEventArgs : EventArgs
    {
        public GumpTextEntryEventArgs(uint gumpTextEntryId, string title,byte inputStyle, uint maxValue, string title2)
        {
            GumpTextEntryID = gumpTextEntryId;
            Title = title;
            InputStyle = inputStyle;
            MaxValue = maxValue;
            Title2 = title2;
        }

        public uint GumpTextEntryID { get; private set; }
        public string Title { get; set; }
        public byte InputStyle { get; set; }
        public uint MaxValue { get; set; }
        public string Title2 { get; set; }
    }
    public class GraphicalEffectEventArgs : EventArgs
    {
        public GraphicalEffectEventArgs(uint srcId, ushort srcX,ushort srcY,int srcZ,uint dstId, ushort dstX,ushort dstY,int dstZ, byte type, ushort itemId, byte fixedDir)
        {
            SrcId = srcId;
            SrcX = srcX;
            SrcY = srcY;
            SrcZ = srcZ;
            DstId = dstId;
            DstX = dstX;
            DstY = dstY;
            DstZ = dstZ;
            Type = type;
            ItemId = itemId;
            FixedDir = fixedDir;
        }

        public uint SrcId { get; private set; }
        public ushort SrcX { get; private set; }
        public ushort SrcY { get; private set; }
        public int SrcZ { get; private set; }
        public uint DstId { get; private set; }
        public ushort DstX { get; private set; }
        public ushort DstY { get; private set; }
        public int DstZ { get; private set; }
        public byte Type { get; private set; }
        public ushort ItemId { get; private set; }
        public byte FixedDir { get; private set; }
    }
}
