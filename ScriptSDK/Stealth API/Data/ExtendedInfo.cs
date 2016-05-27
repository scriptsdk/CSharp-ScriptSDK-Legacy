using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace StealthAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ExtendedInfo
    {
        public ushort MaxWeight;
        public byte Race;
        public ushort StatCap;
        public byte PetsCurrent;
        public byte PetsMax;
        public ushort FireResist;
        public ushort ColdResist;
        public ushort PoisonResist;
        public ushort EnergyResist;
        public short Luck;
        public ushort DamageMin;
        public ushort DamageMax;
        public uint TithingPoints;
        public ushort HitChanceIncr;
        public ushort SwingSpeedIncr;
        public ushort DamageChanceIncr;
        public ushort LowerReagentCost;
        public ushort HPRegen;
        public ushort StamRegen;
        public ushort ManaRegen;
        public ushort ReflectPhysDamage;
        public ushort EnhancePotions;
        public ushort DefenseChanceIncr;
        public ushort SpellDamageIncr;
        public ushort FasterCastRecovery;
        public ushort FasterCasting;
        public ushort LowerManaCost;
        public ushort StrengthIncr;
        public ushort DextIncr;
        public ushort IntIncr;
        public ushort HPIncr;
        public ushort StamIncr;
        public ushort ManaIncr;
        public ushort MaxHPIncr;
        public ushort MaxStamIncr;
        public ushort MaxManaIncrease;
    }
}
