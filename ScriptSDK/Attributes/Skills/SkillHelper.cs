using System;
using System.Collections.Generic;
using System.Linq;
using ScriptSDK.Data;
using ScriptSDK.Mobiles;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Skillhelper exposes handles, actions and properties about skill system.
    /// </summary>
    public class SkillHelper
    {
        #region Constructor

        /// <exclude />
        protected SkillHelper(PlayerMobile owner)
        {
            _owner = owner;
            _uskills = new Dictionary<SkillName, UseableSkill>
            {
                {SkillName.Anatomy, new UseableSkill(this, "Anatomy", SkillName.Anatomy, new TimeSpan(10000))},
                {
                    SkillName.AnimalTaming,
                    new UseableSkill(this, "Animal Taming", SkillName.AnimalTaming, new TimeSpan(10000))
                },
                {SkillName.ArmsLore, new UseableSkill(this, "Arms Lore", SkillName.ArmsLore, new TimeSpan(10000))},
                {SkillName.Begging, new UseableSkill(this, "Begging", SkillName.Begging, new TimeSpan(10000))},
                {SkillName.DetectHidden,new UseableSkill(this, "Detect Hidden", SkillName.DetectHidden, new TimeSpan(1000))},
                {SkillName.Discordance,new UseableSkill(this, "Discordance", SkillName.Discordance, new TimeSpan(10000))},
                {SkillName.EvalInt,new UseableSkill(this, "Evaluating Intelligence", SkillName.EvalInt, new TimeSpan(10000))},
                {SkillName.Forensics,new UseableSkill(this, "Forensic Evaluatio", SkillName.Forensics, new TimeSpan(10000))},
                {SkillName.Herding, new UseableSkill(this, "Herding", SkillName.Herding, new TimeSpan(10000))},
                {SkillName.Meditation, new UseableSkill(this, "Meditation", SkillName.Meditation, new TimeSpan(10000))},
                {SkillName.Stealth, new UseableSkill(this, "Stealth", SkillName.Stealth, new TimeSpan(10000))},
                {SkillName.RemoveTrap, new UseableSkill(this, "Remove Trap", SkillName.RemoveTrap, new TimeSpan(10000))},
                {SkillName.Imbuing, new UseableSkill(this, "Imbuing", SkillName.Imbuing, new TimeSpan(10000))},
                {SkillName.Snooping, new UseableSkill(this, "Snooping", SkillName.Snooping, new TimeSpan(10000))},
                {SkillName.AnimalLore, new UseableSkill(this, "Animal Lore", SkillName.AnimalLore, new TimeSpan(10000))},
                {SkillName.ItemID,new UseableSkill(this, "Item Identification", SkillName.ItemID, new TimeSpan(10000))},
                {SkillName.Peacemaking,new UseableSkill(this, "Peacemaking", SkillName.Peacemaking, new TimeSpan(10000))},
                {SkillName.Hiding, new UseableSkill(this, "Hiding", SkillName.Hiding, new TimeSpan(10000))},
                {SkillName.Provocation,new UseableSkill(this, "Provocation", SkillName.Provocation, new TimeSpan(10000))},
                {SkillName.Inscribe, new UseableSkill(this, "Inscription", SkillName.Inscribe, new TimeSpan(10000))},
                {SkillName.Poisoning, new UseableSkill(this, "Poisoning", SkillName.Poisoning, new TimeSpan(10000))},
                {SkillName.SpiritSpeak,new UseableSkill(this, "Spirit Speak", SkillName.SpiritSpeak, new TimeSpan(10000))},
                {SkillName.Stealing, new UseableSkill(this, "Stealing", SkillName.Stealing, new TimeSpan(10000))},
                {SkillName.TasteID,new UseableSkill(this, "Taste Identification", SkillName.TasteID, new TimeSpan(10000))},
                {SkillName.Tracking, new UseableSkill(this, "Tracking", SkillName.Tracking, new TimeSpan(10000))}
            };

            _skills = new Dictionary<SkillName, Skill>
            {
                {SkillName.Alchemy, new Skill(this, "Alchemy", SkillName.Alchemy)},
                {SkillName.Parry, new Skill(this, "Parry", SkillName.Parry)},
                {SkillName.Blacksmith, new Skill(this, "Blacksmith", SkillName.Blacksmith)},
                {SkillName.Fletching, new Skill(this, "Fletching", SkillName.Fletching)},
                {SkillName.Camping, new Skill(this, "Camping", SkillName.Camping)},
                {SkillName.Carpentry, new Skill(this, "Carpentry", SkillName.Carpentry)},
                {SkillName.Cartography, new Skill(this, "Cartography", SkillName.Cartography)},
                {SkillName.Cooking, new Skill(this, "Cooking", SkillName.Cooking)},
                {SkillName.Healing, new Skill(this, "Healing", SkillName.Healing)},
                {SkillName.Fishing, new Skill(this, "Fishing", SkillName.Fishing)},
                {SkillName.Lockpicking, new Skill(this, "Lockpicking", SkillName.Lockpicking)},
                {SkillName.Magery, new Skill(this, "Magery", SkillName.Magery)},
                {SkillName.MagicResist, new Skill(this, "Magic Resist", SkillName.MagicResist)},
                {SkillName.Tactics, new Skill(this, "Tactics", SkillName.Tactics)},
                {SkillName.Musicianship, new Skill(this, "Musicianship", SkillName.Musicianship)},
                {SkillName.Archery, new Skill(this, "Archery", SkillName.Archery)},
                {SkillName.Tailoring, new Skill(this, "Tailoring", SkillName.Tailoring)},
                {SkillName.Tinkering, new Skill(this, "Tinkering", SkillName.Tinkering)},
                {SkillName.Veterinary, new Skill(this, "Veterinary", SkillName.Veterinary)},
                {SkillName.Swords, new Skill(this, "Swords", SkillName.Swords)},
                {SkillName.Macing, new Skill(this, "Macing", SkillName.Macing)},
                {SkillName.Fencing, new Skill(this, "Fencing", SkillName.Fencing)},
                {SkillName.Wrestling, new Skill(this, "Wrestling", SkillName.Wrestling)},
                {SkillName.Lumberjacking, new Skill(this, "Lumberjacking", SkillName.Lumberjacking)},
                {SkillName.Mining, new Skill(this, "Mining", SkillName.Mining)},
                {SkillName.Necromancy, new Skill(this, "Necromancy", SkillName.Necromancy)},
                {SkillName.Focus, new Skill(this, "Focus", SkillName.Focus)},
                {SkillName.Chivalry, new Skill(this, "Chivalry", SkillName.Chivalry)},
                {SkillName.Bushido, new Skill(this, "Bushido", SkillName.Bushido)},
                {SkillName.Ninjitsu, new Skill(this, "Ninjitsu", SkillName.Ninjitsu)},
                {SkillName.Spellweaving, new Skill(this, "Spellweaving", SkillName.Spellweaving)},
                {SkillName.Mysticism, new Skill(this, "Mysticism", SkillName.Mysticism)},
                {SkillName.Throwing, new Skill(this, "Throwing", SkillName.Throwing)}
            };
        }

        #endregion

        #region Vars

        /// <exclude />
        protected static SkillHelper _instance { get; set; }

        /// <exclude />
        protected Dictionary<SkillName, Skill> _skills { get; set; }

        /// <exclude />
        protected Dictionary<SkillName, UseableSkill> _uskills { get; set; }

        /// <exclude />
        protected PlayerMobile _owner { get; set; }

        /// <summary>
        /// returns reference to last used skill.
        /// </summary>
        public virtual Skill LastSkill { get; protected set; }

        #endregion

        #region Skills
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Anatomy
        {
            get { return _uskills[SkillName.Anatomy]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill AnimalTaming
        {
            get { return _uskills[SkillName.AnimalTaming]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill ArmsLore
        {
            get { return _uskills[SkillName.ArmsLore]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Begging
        {
            get { return _uskills[SkillName.Begging]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill DetectHidden
        {
            get { return _uskills[SkillName.DetectHidden]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Discordance
        {
            get { return _uskills[SkillName.Discordance]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill EvaluatingIntelligence
        {
            get { return _uskills[SkillName.EvalInt]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Forensics
        {
            get { return _uskills[SkillName.Forensics]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Herding
        {
            get { return _uskills[SkillName.Herding]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Meditation
        {
            get { return _uskills[SkillName.Meditation]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Stealth
        {
            get { return _uskills[SkillName.Stealth]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill RemoveTrap
        {
            get { return _uskills[SkillName.RemoveTrap]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Imbuing
        {
            get { return _uskills[SkillName.Imbuing]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Snooping
        {
            get { return _uskills[SkillName.Snooping]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill AnimalLore
        {
            get { return _uskills[SkillName.AnimalLore]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill ItemID
        {
            get { return _uskills[SkillName.ItemID]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Peacemaking
        {
            get { return _uskills[SkillName.Peacemaking]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Hiding
        {
            get { return _uskills[SkillName.Hiding]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Provocation
        {
            get { return _uskills[SkillName.Provocation]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Inscription
        {
            get { return _uskills[SkillName.Inscribe]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Poisoning
        {
            get { return _uskills[SkillName.Poisoning]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill SpiritSpeak
        {
            get { return _uskills[SkillName.SpiritSpeak]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Stealing
        {
            get { return _uskills[SkillName.Stealing]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill TasteIdentification
        {
            get { return _uskills[SkillName.TasteID]; }
        }
        /// <summary>
        /// Stores useable skill data.
        /// </summary>
        public virtual UseableSkill Tracking
        {
            get { return _uskills[SkillName.Tracking]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Alchemy
        {
            get { return _skills[SkillName.Alchemy]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Parry
        {
            get { return _skills[SkillName.Parry]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Blacksmith
        {
            get { return _skills[SkillName.Blacksmith]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Fletching
        {
            get { return _skills[SkillName.Fletching]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Camping
        {
            get { return _skills[SkillName.Camping]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Carpentry
        {
            get { return _skills[SkillName.Carpentry]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Cartography
        {
            get { return _skills[SkillName.Cartography]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Cooking
        {
            get { return _skills[SkillName.Cooking]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Healing
        {
            get { return _skills[SkillName.Healing]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Fishing
        {
            get { return _skills[SkillName.Fishing]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Lockpicking
        {
            get { return _skills[SkillName.Lockpicking]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Magery
        {
            get { return _skills[SkillName.Magery]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill MagicResist
        {
            get { return _skills[SkillName.MagicResist]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Tactics
        {
            get { return _skills[SkillName.Tactics]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Musicianship
        {
            get { return _skills[SkillName.Musicianship]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Archery
        {
            get { return _skills[SkillName.Archery]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Tailoring
        {
            get { return _skills[SkillName.Tailoring]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Tinkering
        {
            get { return _skills[SkillName.Tinkering]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Veterinary
        {
            get { return _skills[SkillName.Veterinary]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Swords
        {
            get { return _skills[SkillName.Swords]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Macing
        {
            get { return _skills[SkillName.Macing]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Fencing
        {
            get { return _skills[SkillName.Fencing]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Wrestling
        {
            get { return _skills[SkillName.Wrestling]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Lumberjacking
        {
            get { return _skills[SkillName.Lumberjacking]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Mining
        {
            get { return _skills[SkillName.Mining]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Necromancy
        {
            get { return _skills[SkillName.Necromancy]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Focus
        {
            get { return _skills[SkillName.Focus]; }
        }

        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Chivalry
        {
            get { return _skills[SkillName.Chivalry]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Bushido
        {
            get { return _skills[SkillName.Bushido]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Ninjitsu
        {
            get { return _skills[SkillName.Ninjitsu]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Spellweaving
        {
            get { return _skills[SkillName.Spellweaving]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Mysticism
        {
            get { return _skills[SkillName.Mysticism]; }
        }
        /// <summary>
        /// Stores non useable skill data.
        /// </summary>
        public virtual Skill Throwing
        {
            get { return _skills[SkillName.Throwing]; }
        }

        #endregion

        #region Functions
        /// <summary>
        /// Stores total amount of used skillpoints.
        /// </summary>
        public virtual double TotalValue
        {
            get { return _skills.Values.Sum(s => s.Value) + _uskills.Values.Sum(s => s.Value); }
        }

        /// <summary>
        /// Returns reference to skill system.
        /// </summary>
        /// <returns></returns>
        public static SkillHelper GetSkills()
        {
            return _instance ?? (_instance = new SkillHelper(PlayerMobile.GetPlayer()));
        }

        /// <summary>
        /// Function wich calls a Skill action (only works on useable skills).
        /// </summary>
        /// <param name="sk"></param>
        /// <returns></returns>
        public virtual bool UseSkill(SkillName sk)
        {
            return _uskills.ContainsKey(sk) && _uskills[sk].Use();
        }

        #endregion
    }
}