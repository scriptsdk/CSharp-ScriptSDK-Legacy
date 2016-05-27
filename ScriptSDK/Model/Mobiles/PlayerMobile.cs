/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/
using System;
using System.Linq;
using ScriptSDK.Attributes;
using ScriptSDK.Data;
using ScriptSDK.Engines;
using ScriptSDK.Items;
using StealthAPI;

namespace ScriptSDK.Mobiles
{
    /// <summary>
    /// PlayerMobile class expose functions and properties to handle and manage actions about player.
    /// </summary>		
    public class PlayerMobile : Mobile
    {
        private PlayerMobile()
            : base(new Serial(Stealth.Client.GetSelfID()))
        {
        }

        private static PlayerMobile _instance { get; set; }

        /// <summary>
        /// Returns the unique instance of player.
        /// </summary>
        /// <returns></returns>
        public static PlayerMobile GetPlayer()
        {
            return _instance ?? (_instance = new PlayerMobile());
        }

        /// <summary>
        /// Returns physical resistence.
        /// </summary>
        public virtual int PhysResistence
        {
            get { return Stealth.Client.GetSelfArmor(); }
        }

        /// <summary>
        /// Returns fire resistence.
        /// </summary>
        public virtual int FireResistence
        {
            get { return Stealth.Client.GetSelfFireResist(); }
        }

        /// <summary>
        /// Returns cold resistence.
        /// </summary>
        public virtual int ColdResistence
        {
            get { return Stealth.Client.GetSelfColdResist(); }
        }

        /// <summary>
        /// Returns cold resistence.
        /// </summary>
        public virtual int PoisonResistence
        {
            get { return Stealth.Client.GetSelfPoisonResist(); }
        }

        /// <summary>
        /// Returns energy resistence.
        /// </summary>
        public virtual int EnergyResistence
        {
            get { return Stealth.Client.GetSelfEnergyResist(); }
        }

        /// <summary>
        /// Returns player race.
        /// </summary>
        public virtual Race Race
        {
            get { return (Race)Stealth.Client.GetSelfRace(); }
        }

        /// <summary>
        /// Returns current weight of backpack and paperdoll.
        /// </summary>
        public virtual int Weight
        {
            get { return Stealth.Client.GetSelfWeight(); }
        }

        /// <summary>
        /// Returns maximum capacable weight.
        /// </summary>
        public virtual int Maxweight
        {
            get { return Stealth.Client.GetSelfMaxWeight(); }
        }

        /// <summary>
        /// Gets or sets the timer how pathfinding system could run mounted.<br/>
        /// Doesnt allow to move faster then server allows!
        /// </summary>
        public virtual ushort RunMountTimer
        {
            get { return Stealth.Client.GetRunMountTimer(); }
            set { Stealth.Client.SetRunMountTimer(value); }
        }

        /// <summary>
        /// Gets or sets the timer how pathfinding system could run unmounted.<br/>
        /// Doesnt allow to move faster then server allows!
        /// </summary>
        public virtual ushort RunUnmountTimer
        {
            get { return Stealth.Client.GetRunUnmountTimer(); }
            set { Stealth.Client.SetRunUnmountTimer(value); }
        }

        /// <summary>
        /// Gets or sets the timer how pathfinding system could move mounted.<br/>
        /// Doesnt allow to move faster then server allows!
        /// </summary>
        public virtual ushort WalkMountTimer
        {
            get { return Stealth.Client.GetWalkMountTimer(); }
            set { Stealth.Client.SetWalkMountTimer(value); }
        }

        /// <summary>
        /// Gets or sets the timer how pathfinding system could move unmounted.<br/>
        /// Doesnt allow to move faster then server allows!
        /// </summary>
        public virtual ushort WalkUnmountTimer
        {
            get { return Stealth.Client.GetWalkUnmountTimer(); }
            set { Stealth.Client.SetWalkUnmountTimer(value); }
        }

        /// <summary>
        /// Returns reference to last used item.
        /// </summary>
        public virtual UOEntity LastObject
        {
            get { return new UOEntity(new Serial(Stealth.Client.GetLastObject())); }
        }

        /// <summary>
        /// Returns reference to last targeted entity.
        /// </summary>
        public virtual UOEntity LastTarget
        {
            get { return new UOEntity(new Serial(Stealth.Client.GetLastTarget())); }
        }

        /// <summary>
        /// Returns reference to last combatant.
        /// </summary>
        public virtual UOEntity LastAttackTarget
        {
            get { return new UOEntity(new Serial(Stealth.Client.GetLastAttack())); }
        }

        /// <summary>
        /// Returns reference to last used container.
        /// </summary>
        public virtual UOEntity LastContainer
        {
            get { return new UOEntity(new Serial(Stealth.Client.GetLastContainer())); }
        }

        /// <summary>
        /// Returns if player could regenerate hit points.
        /// </summary>
        public virtual bool CanRegenHits
        {
            get { return !Dead && (!Poisoned); }
        }

        /// <summary>
        /// Returns if player could regenerate stamina.
        /// </summary>
        public virtual bool CanRegenStam
        {
            get { return !Dead; }
        }

        /// <summary>
        /// Returns if player could regenerate mana.
        /// </summary>
        public virtual bool CanRegenMana
        {
            get { return !Dead; }
        }

        /// <summary>
        /// Returns the current active ability as text.
        /// </summary>
        public virtual string ActiveAbility
        {
            get { return Stealth.Client.GetAbility(); }
        }

        // TODO : Maybe make enumeration about it.

        /// <summary>
        /// Stores reference of party system
        /// </summary>
        public virtual PartyHelper Party
        {
            get { return PartyHelper.GetParty(); }
        }

        /// <summary>
        /// Stores reference of journal sysem.
        /// </summary>
        public virtual JournalHelper Journal
        {
            get { return JournalHelper.GetJournal(); }
        }

        /// <summary>
        /// Stores reference of trading system.
        /// </summary>
        public virtual TradeHelper Trade
        {
            get { return TradeHelper.GetTrade(); }
        }

        /// <summary>
        /// Stores reference of virtues system.
        /// </summary>
        public virtual VirtueHelper Virtues
        {
            get { return VirtueHelper.GetVirtues(); }
        }

        /// <summary>
        /// Stores reference of targeting system which includes virtual targets.
        /// </summary>
        public virtual TargetHelper Targeting
        {
            get { return TargetHelper.GetTarget(); }
        }
        /// <summary>
        /// Stores reference of skill system.
        /// </summary>
        public virtual SkillHelper Skills
        {
            get { return SkillHelper.GetSkills(); }
        }

        /// <summary>
        /// Stores cross over reference towards game client.
        /// </summary>
        public virtual GameClient Client
        {
            get { return GameClient.GetClient(); }
        }

        /// <summary>
        /// Stores reference of vendoring system.
        /// </summary>
        public virtual VendorHelper Vendoring
        {
            get { return VendorHelper.GetVendorHelper(); }
        }

        /// <summary>
        /// Stores reference of movement system.
        /// </summary>
        public virtual MovingHelper Movement
        {
            get { return MovingHelper.GetMovingHelper(); }
        }

        /// <summary>
        /// Returns player backpack object.
        /// </summary>
        public virtual Container Backpack
        {
            get { return new Container(new Serial(Stealth.Client.GetBackpackID())); }
        }

        /// <summary>
        /// Stores ID of last mobile wich got a recent status update via packet and is unequal player.
        /// </summary>
        public virtual uint LastStatus
        {
            get { return Stealth.Client.GetLastStatus(); }
        }

        /// <summary>
        /// Gets or sets warmode on player.
        /// </summary>
        public virtual bool Warmode
        {
            get { return Stealth.Client.GetWarModeStatus(); }
            set { Stealth.Client.SetWarMode(value); }
        }

        /// <summary>
        /// Returns the map location of player. Returns Map.Internal when unknown.
        /// </summary>
        public virtual Map Map
        {
            get
            {
                var b = Stealth.Client.GetWorldNum();
                var l = Enum.GetValues(typeof(Map)) as byte[];
                if (l == null)
                    return Map.Internal;
                var bl = l.ToList();
                if (bl.Contains(b))
                    return (Map)b;
                return Map.Internal;
            }
        }

        /// <summary>
        /// Function performs a request to open door next to player (if possible).
        /// </summary>
        /// <returns></returns>
        public virtual bool OpenDoor()
        {
            if (!Valid)
                return false;
            Stealth.Client.OpenDoor();
            return true;
        }

        /// <summary>
        /// Performs a bow action by player.
        /// </summary>
        /// <returns></returns>
        public virtual bool Bow()
        {
            if (!Valid)
                return false;
            Stealth.Client.Bow();
            return true;
        }

        /// <summary>
        /// Performs a salute action by player.
        /// </summary>
        /// <returns></returns>
        public virtual bool Salute()
        {
            if (!Valid)
                return false;
            Stealth.Client.Salute();
            return true;
        }

        /// <summary>
        /// Function requests passed gump type from paperdoll.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool Request(RequestType type)
        {
            if (!Valid)
                return false;

            if (type.Equals(RequestType.Help))
            {
                Stealth.Client.HelpRequest();
                return true;
            }

            if (type.Equals(RequestType.Quest))
            {
                Stealth.Client.QuestRequest();
                return true;
            }
            return type.Equals(RequestType.Virtues) && Virtues.Request();
        }

        /// <summary>
        /// Function allows to use an object by passed graphic type and color regardless of location.
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="hue"></param>
        /// <returns></returns>
        public virtual UOEntity Use(ushort Type, ushort hue)
        {
            return new UOEntity(new Serial(Stealth.Client.UseType(Type, hue)));
        }

        /// <summary>
        /// Function allows to use an object by passed graphic type regardless of location.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public virtual UOEntity Use(ushort Type)
        {
            return new UOEntity(new Serial(Stealth.Client.UseType(Type)));
        }

        // TODO : How about allowing lists? for Use
        // TODO : how about allowing Types and Recursive Types?

        /// <summary>
        /// Function allows to use an object by passed graphic type and color but only selecting items on ground.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="hue"></param>
        /// <returns></returns>
        public virtual UOEntity UseFromGround(ushort type = 65535, ushort hue = 65535)
        {
            return new UOEntity(new Serial(Stealth.Client.UseFromGround(type, hue)));
        }

        /// <summary>
        /// Performs a toggle or untoggle of flymodus.
        /// </summary>
        /// <returns></returns>
        public virtual bool Fly()
        {
            if (!Race.Equals(Race.Gargoyle))
                return false;
            Stealth.Client.ToggleFly();
            return true;
        }

        /// <summary>
        /// Performs a request to activate the primary ability.<br/>
        /// TODO : Still an open BUG to be fixed by Stealth client.
        /// </summary>
        public virtual void UsePrimaryAbility()
        {
            //TODO : Resolve if its a dll issue or Stealth
            Stealth.Client.UsePrimaryAbility();
        }

        /// <summary>
        /// Performs a request to activate the secondary ability.<br/>
        /// TODO : Still an open BUG to be fixed by Stealth client.
        /// </summary>
        public virtual void UseSecondaryAbility()
        {
            //TODO : Resolve if its a dll issue or Stealth
            Stealth.Client.UseSecondaryAbility();
        }

        /// <summary>
        /// Performs an attack towards passed mobile returns result of request.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public virtual bool Attack(Mobile m)
        {
            return m != null && Attack(m.Serial);
        }

        /// <summary>
        /// Performs an attack towards passed Serial returns result of request.
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public virtual bool Attack(Serial serial)
        {
            return Attack(serial.Value);
        }

        /// <summary>
        /// Performs an attack towards passed ID returns result of request.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual bool Attack(uint ID)
        {
            if ((ID.Equals(0)) || (!Valid))
                return false;
            Stealth.Client.Attack(ID);
            return true;
        }

        /// <summary>
        /// Performs an unequip on both hands.
        /// </summary>
        /// <returns></returns>
        public virtual bool ClearHands()
        {
            var a = Unequip(Layer.OneHanded);
            var b = Unequip(Layer.TwoHanded);
            return a && b;
        }

        /// <summary>
        /// Performs an equip on desired layer, returns result of process.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public virtual bool Equip(Layer l, UOEntity o)
        {
            if ((o == null) || (o.Serial.Value.Equals(0)))
                return false;
            return !l.Equals(Layer.Invalid) && Stealth.Client.WearItem((byte)l, o.Serial.Value);
        }

        /// <summary>
        /// performs an unequip command on desired layer, returns result of process.
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public virtual bool Unequip(Layer layer)
        {
            return Stealth.Client.Unequip((byte)layer);
        }

        /// <summary>
        /// Saves a copy of current equipped objects on paperdoll.
        /// </summary>
        public virtual void BackupPaperdoll()
        {
            Stealth.Client.SetDress();
        }

        /// <summary>
        /// Tries to restore the previous stored image of paperdoll through BackupPaperdoll().
        /// </summary>
        /// <returns></returns>
        public virtual bool RestorePaperdoll()
        {
            return Stealth.Client.Dress();
        }

        /// <summary>
        /// Describes the current used action delay on dress and undress actions.
        /// </summary>
        public ushort DressSpeed
        {
            get { return Stealth.Client.GetDressSpeed(); }
            set { Stealth.Client.SetDressSpeed(value); }
        }

        /// <summary>
        /// Undress whole character.
        /// </summary>
        /// <returns></returns>
        public virtual bool Undress()
        {
            return Stealth.Client.Undress();
        }

        /// <summary>
        /// Performs skill action and returns result of process.
        /// </summary>
        /// <param name="sk"></param>
        /// <returns></returns>
        public virtual bool UseSkill(SkillName sk)
        {
            return Skills.UseSkill(sk);
        }

        /// <summary>
        /// Function allows to send an ingame message without utilizing the console input. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public virtual void SendText(string text, ushort color = 0)
        {
            if (color > 0)
                Stealth.Client.SendTextToUO(text);
            else
                Stealth.Client.SendTextToUOColor(text, color);
        }

        /// <summary>
        /// Function allows to send an ingame message by utilizing the console input. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="unicode"></param>
        public virtual void SendText(string text, bool unicode = false)
        {
            if (unicode)
                Stealth.Client.ConsoleEntryUnicodeReply(text);
            else
                Stealth.Client.ConsoleEntryReply(text);
        }

        /// <summary>
        /// Function allows to change the lockstate of any stat such as STR,DEX,INT.
        /// </summary>
        /// <param name="statNum"></param>
        /// <param name="statState"></param>
        public virtual void ChangeStatLockState(byte statNum, byte statState)
        {
            Stealth.Client.ChangeStatLockState(statNum, statState);
        }
        /// <summary>
        /// Function allows to change the lockstate of any stat such as STR,DEX,INT.
        /// </summary>
        /// <param name="statNum"></param>
        /// <param name="statState"></param>
        public virtual void ChangeStatLockState(byte statNum, SkillLock statState)
        {
            ChangeStatLockState(statNum, (byte)statState);
        }

        /// <summary>
        /// Function allows to change the lockstate of any stat such as STR,DEX,INT.
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="statState"></param>
        public virtual void ChangeStatLockState(Stats stat, byte statState)
        {
            Stealth.Client.ChangeStatLockState((byte)stat, statState);
        }

        /// <summary>
        /// Function allows to change the lockstate of any stat such as STR,DEX,INT.
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="statState"></param>
        public virtual void ChangeStatLockState(Stats stat, SkillLock statState)
        {
            ChangeStatLockState((byte)stat, (byte)statState);
        }

        // TODO : Is there a way to READ Lockstate?

        /// <summary>
        /// Function returns true if the passed text (wich describes a spell) is activated.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool IActiveSpell(string name)
        {
            return Stealth.Client.IsActiveSpellAbility(name);
        }

        // TODO : An enumerated Spellcheck would be Super-Awesome!

        /// <summary>
        /// Function performs a cast request and returns true or false if the spell is valid.
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        public virtual bool Cast(string spell)
        {
            return Stealth.Client.CastSpell(spell);
        }

        /// Function performs a cast request and returns true or false if the spell is valid.
        public virtual bool Cast(string spell, uint targetID)
        {
            return Stealth.Client.CastSpellToObj(spell, targetID);
        }

        // TODO : Finish Casting System!

        /// <summary>
        /// Function performs a self disarm of wearing weapon.
        /// </summary>
        /// <returns></returns>
        public virtual bool Disarm()
        {
            return Stealth.Client.Disarm();
        }
    }
}