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

        public readonly Skill Alchemy = new Skill() { Value = "Alchemy" };
        public readonly Skill Anatomy = new Skill() { Value = "Anatomy" };
        public readonly Skill AnimalLore = new Skill() { Value = "Animal Lore" };
        public readonly Skill AnimalTaming = new Skill() { Value = "Animal Taming" };
        public readonly Skill Archery = new Skill() { Value = "Archery" };
        public readonly Skill Armslore = new Skill() { Value = "Armslore" };
        public readonly Skill Begging = new Skill() { Value = "Begging" };
        public readonly Skill Blacksmithy = new Skill() { Value = "Blacksmithy" };
        public readonly Skill Bowcraft = new Skill() { Value = "Bowcraft" };
        public readonly Skill Bushido = new Skill() { Value = "Bushido" };
        public readonly Skill Camping = new Skill() { Value = "Camping" };
        public readonly Skill Carpentry = new Skill() { Value = "Carpentry" };
        public readonly Skill Cartography = new Skill() { Value = "Cartography" };
        public readonly Skill Chivalry = new Skill() { Value = "Chivalry" };
        public readonly Skill Cooking = new Skill() { Value = "Cooking" };
        public readonly Skill DetectHidden = new Skill() { Value = "Detect Hidden" };
        public readonly Skill Discordance = new Skill() { Value = "Discordance" };
        public readonly Skill EvaluateIntelligence = new Skill() { Value = "Evaluate Intelligence" };
        public readonly Skill Fencing = new Skill() { Value = "Fencing" };
        public readonly Skill Fishing = new Skill() { Value = "Fishing" };
        public readonly Skill Focus = new Skill() { Value = "Focus" };
        public readonly Skill Forensic = new Skill() { Value = "Forensic" };
        public readonly Skill Healing = new Skill() { Value = "Healing" };
        public readonly Skill Herding = new Skill() { Value = "Herding" };
        public readonly Skill Hiding = new Skill() { Value = "Hiding" };
        public readonly Skill Inscription = new Skill() { Value = "Inscription" };
        public readonly Skill ItemIdentification = new Skill() { Value = "Item Identification" };
        public readonly Skill Lockpicking = new Skill() { Value = "Lockpicking" };
        public readonly Skill Lumberjacking = new Skill() { Value = "Lumberjacking" };
        public readonly Skill MaceFighting = new Skill() { Value = "Mace Fighting" };
        public readonly Skill Magery = new Skill() { Value = "Magery" };
        public readonly Skill Meditation = new Skill() { Value = "Meditation" };
        public readonly Skill Mining = new Skill() { Value = "Mining" };
        public readonly Skill Musicianship = new Skill() { Value = "Musicianship" };
        public readonly Skill Necromancy = new Skill() { Value = "Necromancy" };
        public readonly Skill Ninjitsu = new Skill() { Value = "Ninjitsu" };
        public readonly Skill Parrying = new Skill() { Value = "Parrying" };
        public readonly Skill Peacemaking = new Skill() { Value = "Peacemaking" };
        public readonly Skill Poisoning = new Skill() { Value = "Poisoning" };
        public readonly Skill Provocation = new Skill() { Value = "Provocation" };
        public readonly Skill RemoveTrap = new Skill() { Value = "Remove Tra" };
        public readonly Skill ResistingSpells = new Skill() { Value = "Resisting Spells" };
        public readonly Skill Snooping = new Skill() { Value = "Snooping" };
        public readonly Skill Spellweaving = new Skill() { Value = "Spellweaving" };
        public readonly Skill SpiritSpeak = new Skill() { Value = "Spirit Speak" };
        public readonly Skill Stealing = new Skill() { Value = "Stealing" };
        public readonly Skill Stealth = new Skill() { Value = "Stealth" };
        public readonly Skill Swordsmanship = new Skill() { Value = "Swordsmanship" };
        public readonly Skill Tactics = new Skill() { Value = "Tactics" };
        public readonly Skill Tailoring = new Skill() { Value = "Tailoring" };
        public readonly Skill TasteIdentification = new Skill() { Value = "Taste Identification" };
        public readonly Skill Tinkering = new Skill() { Value = "Tinkering" };
        public readonly Skill Tracking = new Skill() { Value = "Tracking" };
        public readonly Skill Veterinary = new Skill() { Value = "Veterinary" };
        public readonly Skill Wrestling = new Skill() { Value = "Wrestling" };
    }
}
