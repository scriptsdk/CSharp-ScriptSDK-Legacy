#if RebirthUO
using System.ComponentModel;

#endif

namespace ScriptSDK.Data
{
    /// <summary>
    ///     Describes the state of a target class
    /// </summary>
    public enum TargetState
    {
#pragma warning disable 1591
        New,
        Busy,
        Timeout,
        Canceled,
        Finished
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration is used for multiple actions in range of journal, such as sending, parsing and reading messages.
    /// </summary>
    public enum JournalType
    {
#pragma warning disable 1591
        Journal,
        System,
        Debug,
        Chat
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes the different beings of target cursors used by Targethelper and virtual targets.
    /// </summary>
    public enum TargetType
    {
#pragma warning disable 1591
        Object,
        Location,
        TileLocation,
        VirtualObject,
        VirtualLocation,
        VirtualTileLocation,
        Ground
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes the kind of target action on Targethelper.
    /// </summary>
    public enum TargetActionType
    {
#pragma warning disable 1591
        Request,
        Targeting,
        AutoTargeting
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes the kind of reply on gumpevents.
    /// </summary>
    public enum GumpReplyType
    {
#pragma warning disable 1591
        Button,
        RadioButton,
        CheckBox,
        TextEdit,
        TextEditLimited
#pragma warning restore 1591
    }

    /// <summary>
    /// Context menu flags for context entries.
    /// </summary>
    public enum CMEFlags : ushort
    {
#pragma warning disable 1591
        None = 0x00,
        Disabled = 0x01,
        Arrow = 0x02,
        Highlighted = 0x04,
        Colored = 0x20
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes the lockstate of skills and stats.
    /// </summary>
    public enum SkillLock : byte
    {
#pragma warning disable 1591
        Up = 0,
        Down = 1,
        Locked = 2
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes stats.
    /// </summary>
    public enum Stats : byte
    {
#pragma warning disable 1591
        Str = 0,
        Dex = 1,
        Int = 2
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes the notoriety of a mobile.
    /// </summary>
    public enum Notoriety : byte
    {
#pragma warning disable 1591
        Innocent = 1,
        Ally = 2,
        CanBeAttacked = 3,
        Criminal = 4,
        Enemy = 5,
        Murderer = 6,
        Invulnerable = 7,
        Invalid = 127
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes direction for targethelper.
    /// </summary>
    public enum HarvestType
    {
#pragma warning disable 1591
        Mining_Cave,
        Mining_Mountain,
        Mining_Sand
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration used inlined by UAC-Helper.
    /// </summary>
    public enum TOKEN_ELEVATION_TYPE
    {
#pragma warning disable 1591
        TokenElevationTypeDefault = 1,
        TokenElevationTypeFull,
        TokenElevationTypeLimited
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration used inlined by UAC-Helper.
    /// </summary>
    public enum TOKEN_INFORMATION_CLASS
    {
#pragma warning disable 1591
        TokenUser = 1,
        TokenGroups,
        TokenPrivileges,
        TokenOwner,
        TokenPrimaryGroup,
        TokenDefaultDacl,
        TokenSource,
        TokenType,
        TokenImpersonationLevel,
        TokenStatistics,
        TokenRestrictedSids,
        TokenSessionId,
        TokenGroupsAndPrivileges,
        TokenSessionReference,
        TokenSandBoxInert,
        TokenAuditPolicy,
        TokenOrigin,
        TokenElevationType,
        TokenLinkedToken,
        TokenElevation,
        TokenHasRestrictions,
        TokenAccessInformation,
        TokenVirtualizationAllowed,
        TokenVirtualizationEnabled,
        TokenIntegrityLevel,
        TokenUIAccess,
        TokenMandatoryPolicy,
        TokenLogonSid,
        MaxTokenInfoClass
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes skills as skillname. Returns 999 if unknown skill.
    /// </summary>
    public enum SkillName
    {
#pragma warning disable 1591
        Alchemy = 0,
        Anatomy = 1,
        AnimalLore = 2,
        ItemID = 3,
        ArmsLore = 4,
        Parry = 5,
        Begging = 6,
        Blacksmith = 7,
        Fletching = 8,
        Peacemaking = 9,
        Camping = 10,
        Carpentry = 11,
        Cartography = 12,
        Cooking = 13,
        DetectHidden = 14,
        Discordance = 15,
        EvalInt = 16,
        Healing = 17,
        Fishing = 18,
        Forensics = 19,
        Herding = 20,
        Hiding = 21,
        Provocation = 22,
        Inscribe = 23,
        Lockpicking = 24,
        Magery = 25,
        MagicResist = 26,
        Tactics = 27,
        Snooping = 28,
        Musicianship = 29,
        Poisoning = 30,
        Archery = 31,
        SpiritSpeak = 32,
        Stealing = 33,
        Tailoring = 34,
        AnimalTaming = 35,
        TasteID = 36,
        Tinkering = 37,
        Tracking = 38,
        Veterinary = 39,
        Swords = 40,
        Macing = 41,
        Fencing = 42,
        Wrestling = 43,
        Lumberjacking = 44,
        Mining = 45,
        Meditation = 46,
        Stealth = 47,
        RemoveTrap = 48,
        Necromancy = 49,
        Focus = 50,
        Chivalry = 51,
        Bushido = 52,
        Ninjitsu = 53,
        Spellweaving = 54,
        Mysticism = 55,
        Imbuing = 56,
        Throwing = 57,
        Invalid = 999
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes the Buff exposed via ID from server.
    /// </summary>
    public enum BuffIconSrv : short
    {
#pragma warning disable 1591
        DismountPrevention = 0x3E9,
        NoRearm = 0x3EA,
        NightSight = 0x3ED,
        DeathStrike,
        EvilOmen,
        UnknownStandingSwirl,
        UnknownKneelingSword,
        DivineFury,
        EnemyOfOne,
        HidingAndOrStealth,
        ActiveMeditation,
        BloodOathCaster,
        BloodOathCurse,
        CorpseSkin,
        Mindrot,
        PainSpike,
        Strangle,
        GiftOfRenewal,
        AttuneWeapon,
        Thunderstorm,
        EssenceOfWind,
        EtherealVoyage,
        GiftOfLife,
        ArcaneEmpowerment,
        MortalStrike,
        ReactiveArmor,
        Protection,
        ArchProtection,
        MagicReflection,
        Incognito,
        Disguised,
        AnimalForm,
        Polymorph,
        Invisibility,
        Paralyze,
        Poison,
        Bleed,
        Clumsy,
        FeebleMind,
        Weaken,
        Curse,
        MassCurse,
        Agility,
        Cunning,
        Strength,
        Bless,
        Sleep,
        StoneForm,
        SpellPlague,
        SpellTrigger,
        NetherBolt,
        Fly,
        Inspire,
        Invigorate,
        Resilience,
        Perseverance,
        TribulationTarget,
        DespairTarget,
        MagicFish = 0x426,
        HitLowerAttack,
        HitLowerDefense,
        DualWield,
        Block,
        DefenseMastery,
        DespairCaster,
        Healing,
        SpellFocusingBuff,
        SpellFocusingDebuff,
        RageFocusingDebuff,
        RageFocusingBuff,
        Warding,
        TribulationCaster,
        ForceArrow,
        Disarm,
        Surge,
        Feint,
        TalonStrike,
        PsychicAttack,
        GrapesOfWrath,
        EnemyOfOneDebuff,
        HorrificBeast,
        LichForm,
        VampiricEmbrace,
        CurseWeapon,
        ReaperForm,
        ImmolatingWeapon,
        Enchant,
        HonorableExecution,
        Confidence,
        Evasion,
        CounterAttack,
        LightningStrike,
        MomentumStrike,
        OrangePetals,
        RoseOfTrinsic,
        PoisonImmunity,
        Veterinary,
        Perfection,
        Honored,
        ManaPhase,
        FanDancerFanFire,
        Rage,
        Webbing,
        MedusaStone,
        TrueFear,
        AuraOfNausea,
        HowlOfCacophony,
        GazeDespair,
        HiryuPhysicalResistance,
        RuneBeetleCorruption,
        BloodwormAnemia,
        RotwormBloodDisease,
        SkillUseDelay,
        FactionStatLoss,
        HeatOfBattleStatus,
        CriminalStatus,
        ArmorPierce,
        SplinteringEffect,
        SwingSpeedDebuff,
        WraithForm,
        CityTradeDeal = 0x466,
        Tribulation = 1075,
        ConsecrateWeapon = 1082
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes the location of player. Returns 0x7F - Internal when unknown.
    /// </summary>
    public enum Map : byte
    {
#pragma warning disable 1591
        Felucca = 0,
        Trammel = 1,
        Ilshenar = 2,
        Malas = 3,
        Tokuno = 4,
        TerMur = 5,
        Internal = 0x7F
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes the movement and view direction of an entity.
    /// </summary>
    public enum Direction : byte
    {
#pragma warning disable 1591
        North = 0x0,
        Right = 0x1,
        East = 0x2,
        Down = 0x3,
        South = 0x4,
        Left = 0x5,
        West = 0x6,
        Up = 0x7,
        Invalid = 0xFF
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes different layer on mobile paperdoll.
    /// </summary>
    public enum Layer : byte
    {
#pragma warning disable 1591
        Invalid = 0x00,
        OneHanded = 0x01,
        TwoHanded = 0x02,
        Shoes = 0x03,
        Pants = 0x04,
        Shirt = 0x05,
        Helm = 0x06,
        Gloves = 0x07,
        Ring = 0x08,
        Talisman = 0x09,
        Neck = 0x0A,
        Hair = 0x0B,
        Waist = 0x0C,
        InnerTorso = 0x0D,
        Bracelet = 0x0E,
        Face = 0x0F,
        FacialHair = 0x10,
        MiddleTorso = 0x11,
        Earrings = 0x12,
        Arms = 0x13,
        Cloak = 0x14,
        Backpack = 0x15,
        OuterTorso = 0x16,
        OuterLegs = 0x17,
        InnerLegs = 0x18,
        Mount = 0x19,
        ShopBuy = 0x1A,
        ShopResale = 0x1B,
        ShopSell = 0x1C,
        Bank = 0x1D
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes the race of a mobile.
    /// </summary>
    public enum Race : byte
    {
#pragma warning disable 1591
        Human = 0,
        Elv = 1,
        Gargoyle = 2,
        None = 127
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration to store and expose genders.
    /// </summary>
    public enum Gender : byte
    {
#pragma warning disable 1591
        Female,
        Male
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration describes types of requests on paperdoll.
    /// </summary>
    public enum RequestType : byte
    {
#pragma warning disable 1591
        Help,
        Quest,
        Virtues
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes the property of an Item wich can be blessed, secured, or cursed.
    /// </summary>
    public enum LootType : byte
    {
#pragma warning disable 1591
        Regular = 0,
        Newbied = 1,
        Blessed = 2,
        Cursed = 3,
        Insured = 4
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes the standart Securelevel readable by native client.
    /// </summary>
    public enum SecureLevel
    {
#pragma warning disable 1591
        Regular,
        Locked,
        Secured
#pragma warning restore 1591
    }
}