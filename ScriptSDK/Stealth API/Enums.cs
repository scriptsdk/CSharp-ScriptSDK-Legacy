#pragma warning disable 1591

namespace StealthAPI
{
    /// <summary>
    /// Event Enumeration
    /// </summary>
    public enum EventTypes : byte
    {
        ItemInfo = 0,
        ItemDeleted = 1,
        Speech = 2,
        DrawGamePlayer = 3,
        MoveRejection = 4,
        DrawContainer = 5,
        AddItemToContainer = 6,
        AddMultipleItemsInCont = 7,
        RejectMoveItem = 8,
        UpdateChar = 9,
        DrawObject = 10,
        Menu = 11,
        MapMessage = 12,
        Allow_RefuseAttack = 13,
        ClilocSpeech = 14,
        ClilocSpeechAffix = 15,
        UnicodeSpeech = 16,
        Buff_DebuffSystem = 17,
        ClientSendResync = 18,
        CharAnimation = 19,
        ICQDisconnect = 20,
        ICQConnect = 21,
        ICQIncomingText = 22,
        ICQError = 23,
        IncomingGump = 24,
        Timer1 = 25,
        Timer2 = 26,
        WindowsMessage = 27,
        Sound = 28,
        Death = 29,
        QuestArrow = 30,
        PartyInvite = 31,
        MapPin = 32,
        GumpTextEntry = 33,
        GraphicalEffect = 34,
        IRCIncomingText = 35,
        //SkypeEvent = 36,
        MessengerEvent = 36,
        SetGlobalVar = 37,
        UpdateObjStats = 38
    }
    public enum MessangerType : byte
    {
        Unknown = 0,
        Telegram = 1,
        Viber = 2
    }
    public enum MessangerEventType : byte
    {
        Connected = 0,
        Disconnected = 1,
        Message = 2,
        Error = 3
    }
    public enum RejectMoveItemReasons : byte
    {
        CanNotPickUp = 0,
        TooFarAway = 1,
        OutOfSight = 2,
        DoesNotBelong = 3,
        AlreadyHolding = 4,
        MustWait = 5
    }

    public enum Virtue : uint
    {
        Compassion = 0x69,
        Honesty = 0x6A,
        Honor = 0x6B,
        Humility = 0x6C,
        Justice = 0x6D,
        Sacrifice = 0x6E,
        Spirituality = 0x6F,
        Valor = 0x70
    };

    public enum Spells : uint
    {
        none = 0,
        // Magery Spells
        Clumsy = 1,
        CreateFood = 2,
        Feeblemind = 3,
        Heal = 4,
        MagicArrow = 5,
        NightSight = 6,
        ReactiveArmor = 7,
        Weaken = 8,
        Agility = 9,
        Cunning = 10,
        Cure = 11,
        Harm = 12,
        MagicTrap = 13,
        MagicUntrap = 14,
        Protection = 15,
        Strength = 16,
        Bless = 17,
        Fireball = 18,
        MagicLock = 19,
        Poison = 20,
        Telekinesis = 21,
        Teleport = 22,
        Unlock = 23,
        WallOfStone = 24,
        ArchCure = 25,
        ArchProtection = 26,
        Curse = 27,
        FireField = 28,
        GreaterHeal = 29,
        Lightning = 30,
        ManaDrain = 31,
        Recall = 32,
        BladeSpirit = 33,
        DispelField = 34,
        Incognito = 35,
        MagicReflection = 36,
        SpellReflection = MagicReflection,
        MindBlast = 37,
        Paralyze = 38,
        PoisonField = 39,
        SummonCreature = 40,
        Dispel = 41,
        EnergyBolt = 42,
        Explosion = 43,
        Invisibility = 44,
        Mark = 45,
        MassCurse = 46,
        ParalyzeField = 47,
        Reveal = 48,
        ChainLightning = 49,
        EnergyField = 50,
        FlameStrike = 51,
        GateTravel = 52,
        ManaVampire = 53,
        MassDispel = 54,
        MeteorSwarm = 55,
        Polymorph = 56,
        Earthquake = 57,
        EnergyVortex = 58,
        Resurrection = 59,
        SummonAirElemental = 60,
        SummonDaemon = 61,
        SummonEarthElemental = 62,
        SummonFireElemental = 63,
        SummonWaterElemental = 64,
        // Necromancy
        AnimateDead = 101,
        BloodOath = 102,
        CorpseSkin = 103,
        CurseWeapon = 104,
        EvilOmen = 105,
        HorrificBeast = 106,
        LichForm = 107,
        MindRot = 108,
        PainSpike = 109,
        PoisonStrike = 110,
        Strangle = 111,
        SummonFamiliar = 112,
        VampiricEmbrace = 113,
        VengefulSpirit = 114,
        Wither = 115,
        WraithForm = 116,
        Exorcism = 117,
        // Chivalry
        CleanseByFire = 201,
        CloseWounds = 202,
        ConsecrateWeapon = 203,
        DispelEvil = 204,
        DivineFury = 205,
        EnemyOfOne = 206,
        HolyLight = 207,
        NobleSacrifice = 208,
        RemoveCurse = 209,
        SacredJourney = 210,
        // Bushido
        HonorableExecution = 401,
        Confidence = 402,
        Evasion = 403,
        CounterAttack = 404,
        LightningStrike = 405,
        MomentumStrike = 406,
        // Ninjitsu
        FocusAttack = 501,
        DeathStrike = 502,
        AnimalForm = 503,
        KiAttack = 504,
        SupriseAttack = 505,
        Backstab = 506,
        Shadowjump = 507,
        MirrorImage = 508,
        // Spellweaving
        ArcaneCircle = 601,
        GiftOfRenewal = 602,
        ImmolatingWeapon = 603,
        Attunement = 604,
        Thunderstorm = 605,
        NatureFury = 606,
        SummonFey = 607,
        SummonFiend = 608,
        ReaperForm = 609,
        Wildfire = 610,
        EssenceOfWind = 611,
        DryadAllure = 612,
        EtherealVoyage = 613,
        WordOfDeath = 614,
        GiftOfLife = 615,
        ArcaneEmpowerment = 616,
        // Mysticism
        NetherBolt = 678,
        HealingStone = 679,
        PureMagic = 680,
        Enchant = 681,
        Sleep = 682,
        EagleStrike = 683,
        AnimatedWeapon = 684,
        StoneForm = 685,
        SpellTrigger = 686,
        MassSleep = 687,
        CleansingWinds = 688,
        Bombard = 689,
        SpellPlague = 690,
        HailStorm = 691,
        NetherCyclone = 692,
        RisingColossus = 693,
        // Bard Mastery
        Inspire = 701,
        Invigorate = 702,
        Resilience = 703,
        Perseverance = 704,
        Tribulation = 705,
        Despair = 706
    };

    public enum TileFlagsType
    {
        Land,
        Static
    }

    public enum TileDataFlags
    {
        StaticBackground = 0,
        StaticWeapon,
        StaticTransparent,
        StaticTranslucent,
        StaticWall,
        StaticDamaging,
        StaticImpassable,
        StaticWet,
        StaticUnknown,
        StaticSurface,
        StaticBridge,
        StaticGeneric,
        StaticWindow,
        StaticNoShoot,
        StaticPrefixA,
        StaticPrefixAn,
        StaticInternal,
        StaticFoliage,
        StaticPartialHue,
        StaticUnknown1,
        StaticMap,
        StaticContainer,
        StaticWearable,
        StaticLightSource,
        StaticAnimated,
        StaticNoDiagonal,
        StaticUnknown2,
        StaticArmor,
        StaticRoof,
        StaticDoor,
        StaticStairBack,
        StaticStairRight,
        LandTranslucent,
        LandWall,
        LandDamaging,
        LandImpassable,
        LandWet,
        LandSurface,
        LandBridge,
        LandPrefixA,
        LandPrefixAn,
        LandInternal,
        LandMap,
        LandUnknown3,
    }

    public enum Layers : byte
    {
        RHand = 0x01,
        LHand = 0x02,
        Shoes = 0x03,
        Pants = 0x04,
        Shirt = 0x05,
        Hat = 0x06,
        Gloves = 0x07,
        Ring = 0x08,
        Talisman = 0x09,
        Neck = 0x0A,
        Hair = 0x0B,
        Waist = 0x0C,
        Torso = 0x0D,
        Brace = 0x0E,
        Beard = 0x10,
        TorsoH = 0x11,
        Ear = 0x12,
        Arms = 0x13,
        Cloak = 0x14,
        Bpack = 0x15,
        Robe = 0x16,
        Eggs = 0x17,
        Legs = 0x18,
        Horse = 0x19,
        Rstk = 0x1A,
        NRstk = 0x1B,
        Sell = 0x1C,
        Bank = 0x1D
    }

    public enum Reagents : ushort
    {
        BP = 0xF7A,
        BM = 0xF7B,
        GA = 0xF84,
        GS = 0xF85,
        MR = 0xF86,
        NS = 0xF88,
        SA = 0xF8C,
        SS = 0xF8D
    }

    public enum VarRegion
    {
        RegStealth,
        RegChar
    }

    public enum UIWindowType
    {
        Paperdoll = 0,
        Status,
        CharProfile,
        Container
    }
    public enum Buffs : int
    {
        //MAGERY
        Clumsy = 117706755,
        FeebleMind = 117707011,
        NightSight = 0,
        ReactiveArmor = 117703683,
        Weaken = 117707267,
        Agility = 117708035,
        Cunning = 117708291,
        Protection = 117703939,
        Strength = 117708547,
        Bless = 117708803,
        Poison = 117706243,
        Curse = 117707523,
        Incognito = 117704707,
        MagicReflection = 117704451,
        Paralyse = 117705987,
        Invisibility = 117699587,
        //NECROMANCY
        BloodOath = 117700099,
        CorpseSkin = 117700611,
        CurseWeapon = 117719043,
        HorrificBeast = 117718275,
        LichForm = 117718531,
        MindRot = 117700867,
        PainSpike = 117701123,
        Strangle = 117701379,
        VampiricEmbrace = 117718787,
        WraithForm = 117728259
    }
    /*
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TPage
    {

        public int Page;
        public int ElemNum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TMasterGump
    {

        public uint ID;
        public int ElemNum;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TUnknownItem
    {

        public StringBuilder CmdName;
        public StringBuilder Arguments;
        public int ElemNum;
    }*/
}
