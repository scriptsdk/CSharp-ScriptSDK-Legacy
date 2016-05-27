using System;
using System.Collections.Generic;
#pragma warning disable 1591

namespace StealthAPI
{
    public class Character
    {
        private Stealth _stealth;
        internal Character(Stealth stealth)
        {
            _stealth = stealth;
        }

        public string Name { get { return _stealth.GetCharName(); } }
        public uint ID { get { return _stealth.GetSelfID(); } }
        public uint BackpackId { get { return _stealth.GetBackpackID(); } }
        public byte Sex { get { return _stealth.GetSelfSex(); } }
        public string Title { get { return _stealth.GetCharTitle(); } }
        public uint Gold { get { return _stealth.GetSelfGold(); } }
        public ushort PhysicalResist { get { return _stealth.GetSelfPhysicalResist(); } }
        public ushort Armor { get { return _stealth.GetSelfArmor(); } } // Legacy Stealth support
        public ushort Weight { get { return _stealth.GetSelfWeight(); } }
        public ushort MaxWeight { get { return _stealth.GetSelfMaxWeight(); } }
        public byte WorldNum { get { return _stealth.GetWorldNum(); } }
        public byte Race { get { return _stealth.GetSelfRace(); } }
        public byte PetsMax { get { return _stealth.GetSelfPetsMax(); } }
        public byte PetsCurrent { get { return _stealth.GetSelfPetsCurrent(); } }
        public ushort FireResist { get { return _stealth.GetSelfFireResist(); } }
        public ushort ColdResist { get { return _stealth.GetSelfColdResist(); } }
        public ushort PoisonResist { get { return _stealth.GetSelfPoisonResist(); } }
        public ushort EnergyResist { get { return _stealth.GetSelfEnergyResist(); } }
        public DateTime ConnectedTime { get { return _stealth.GetConnectedTime(); } }
        public DateTime DisconnectedTime { get { return _stealth.GetDisconnectedTime(); } }
        public uint LastContainer { get { return _stealth.GetLastContainer(); } }
        public uint LastTarget { get { return _stealth.GetLastTarget(); } }
        public uint LastAttack { get { return _stealth.GetLastAttack(); } }
        public uint LastStatus { get { return _stealth.GetLastStatus(); } }
        public uint LastObject { get { return _stealth.GetLastObject(); } }

        public int Str { get { return _stealth.GetSelfStr(); } }
        public int Int { get { return _stealth.GetSelfInt(); } }
        public int Dex { get { return _stealth.GetSelfDex(); } }
        public int Life { get { return _stealth.GetSelfLife(); } }
        public int Mana { get { return _stealth.GetSelfMana(); } }
        public int Stam { get { return _stealth.GetSelfStam(); } }
        public int MaxLife { get { return _stealth.GetSelfMaxLife(); } }
        public int MaxMana { get { return _stealth.GetSelfMaxMana(); } }
        public int MaxStam { get { return _stealth.GetSelfMaxStam(); } }
        public int Luck { get { return _stealth.GetSelfLuck(); } }
        public ExtendedInfo ExtInfo { get { return _stealth.GetExtInfo(); } }

        public bool IsHidden { get { return _stealth.GetHiddenStatus(); } }
        public bool IsPoisoned { get { return _stealth.GetPoisonedStatus(); } }
        public bool IsParalized { get { return _stealth.GetParalyzedStatus(); } }
        public bool IsDead { get { return _stealth.GetDeadStatus(); } }
        public string Ability { get { return _stealth.GetAbility(); } }

        public List<BuffIcon> BuffBarInfo
        {
            get
            {
                return _stealth.GetBuffBarInfo();
            }
        }

        public bool IsInWarMode
        {
            get { return _stealth.GetWarModeStatus(); }
            set { _stealth.SetWarMode(value); }
        }

        public uint WarTarget
        {
            get { return _stealth.GetWarTarget(); }
            set { _stealth.Attack(value); }
        }

        public void UsePrimaryAbility()
        {
            _stealth.UsePrimaryAbility();
        }
        public void UseSecondaryAbility()
        {
            _stealth.UseSecondaryAbility();
        }
        public void ToggleFly()
        {
            _stealth.ToggleFly();
        }

        public void Attack(uint attackedID)
        {
            WarTarget = attackedID;
        }

        public void UseSelfPaperdollScroll()
        {
            _stealth.UseSelfPaperdollScroll();
        }
        public void UseOtherPaperdollScroll(uint id)
        {
            _stealth.UseOtherPaperdollScroll(id);
        }

        public uint ObjectAtLayer(Layers layer)
        {
            return _stealth.ObjAtLayer((byte)layer);
        }
    }
}
