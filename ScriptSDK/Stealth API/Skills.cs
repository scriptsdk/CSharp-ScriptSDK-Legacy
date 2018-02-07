#pragma warning disable 1591

namespace StealthAPI
{
    public class Skill
    {
        public Skill()
        {
            Id = -1;
        }

        public string Value { get; set; }
        public int Id { get; set; }
        public bool IsValidId { get { return Id >= 0 && Id < 250; } }

        public static readonly Skill Alchemy = new Skill() { Value = "Alchemy" };
        public static readonly Skill Anatomy = new Skill() { Value = "Anatomy" };
        public static readonly Skill AnimalLore = new Skill() { Value = "Animal Lore" };
        public static readonly Skill AnimalTaming = new Skill() { Value = "Animal Taming" };
        public static readonly Skill Archery = new Skill() { Value = "Archery" };
        public static readonly Skill Armslore = new Skill() { Value = "Armslore" };
        public static readonly Skill Begging = new Skill() { Value = "Begging" };
        public static readonly Skill Blacksmithy = new Skill() { Value = "Blacksmithy" };
        public static readonly Skill Bowcraft = new Skill() { Value = "Bowcraft" };
        public static readonly Skill Bushido = new Skill() { Value = "Bushido" };
        public static readonly Skill Camping = new Skill() { Value = "Camping" };
        public static readonly Skill Carpentry = new Skill() { Value = "Carpentry" };
        public static readonly Skill Cartography = new Skill() { Value = "Cartography" };
        public static readonly Skill Chivalry = new Skill() { Value = "Chivalry" };
        public static readonly Skill Cooking = new Skill() { Value = "Cooking" };
        public static readonly Skill DetectHidden = new Skill() { Value = "Detect Hidden" };
        public static readonly Skill Discordance = new Skill() { Value = "Discordance" };
        public static readonly Skill EvaluateIntelligence = new Skill() { Value = "Evaluate Intelligence" };
        public static readonly Skill Fencing = new Skill() { Value = "Fencing" };
        public static readonly Skill Fishing = new Skill() { Value = "Fishing" };
        public static readonly Skill Focus = new Skill() { Value = "Focus" };
        public static readonly Skill Forensic = new Skill() { Value = "Forensic" };
        public static readonly Skill Healing = new Skill() { Value = "Healing" };
        public static readonly Skill Herding = new Skill() { Value = "Herding" };
        public static readonly Skill Hiding = new Skill() { Value = "Hiding" };
        public static readonly Skill Inscription = new Skill() { Value = "Inscription" };
        public static readonly Skill ItemIdentification = new Skill() { Value = "Item Identification" };
        public static readonly Skill Imbuing = new Skill() { Value = "Imbuing" };
        public static readonly Skill Lockpicking = new Skill() { Value = "Lockpicking" };
        public static readonly Skill Lumberjacking = new Skill() { Value = "Lumberjacking" };
        public static readonly Skill MaceFighting = new Skill() { Value = "Mace Fighting" };
        public static readonly Skill Magery = new Skill() { Value = "Magery" };
        public static readonly Skill Meditation = new Skill() { Value = "Meditation" };
        public static readonly Skill Mining = new Skill() { Value = "Mining" };
        public static readonly Skill Musicianship = new Skill() { Value = "Musicianship" };
        public static readonly Skill Mysticism = new Skill() { Value = "Mysticism" };
        public static readonly Skill Necromancy = new Skill() { Value = "Necromancy" };
        public static readonly Skill Ninjitsu = new Skill() { Value = "Ninjitsu" };
        public static readonly Skill Parrying = new Skill() { Value = "Parrying" };
        public static readonly Skill Peacemaking = new Skill() { Value = "Peacemaking" };
        public static readonly Skill Poisoning = new Skill() { Value = "Poisoning" };
        public static readonly Skill Provocation = new Skill() { Value = "Provocation" };
        public static readonly Skill RemoveTrap = new Skill() { Value = "Remove Tra" };
        public static readonly Skill ResistingSpells = new Skill() { Value = "Resisting Spells" };
        public static readonly Skill Snooping = new Skill() { Value = "Snooping" };
        public static readonly Skill Spellweaving = new Skill() { Value = "Spellweaving" };
        public static readonly Skill SpiritSpeak = new Skill() { Value = "Spirit Speak" };
        public static readonly Skill Stealing = new Skill() { Value = "Stealing" };
        public static readonly Skill Stealth = new Skill() { Value = "Stealth" };
        public static readonly Skill Swordsmanship = new Skill() { Value = "Swordsmanship" };
        public static readonly Skill Tactics = new Skill() { Value = "Tactics" };
        public static readonly Skill Tailoring = new Skill() { Value = "Tailoring" };
        public static readonly Skill TasteIdentification = new Skill() { Value = "Taste Identification" };
        public static readonly Skill Tinkering = new Skill() { Value = "Tinkering" };
        public static readonly Skill Throwing = new Skill() { Value = "Throwing" };
        public static readonly Skill Tracking = new Skill() { Value = "Tracking" };
        public static readonly Skill Veterinary = new Skill() { Value = "Veterinary" };
        public static readonly Skill Wrestling = new Skill() { Value = "Wrestling" };
    }
}
