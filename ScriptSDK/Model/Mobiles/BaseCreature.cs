/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/
using StealthAPI;

namespace ScriptSDK.Mobiles
{
    /// <summary>
    /// BaseCreature class expose functions and properties to handle and manage actions about any creature wich is not a player.
    /// </summary>
    public class BaseCreature : Mobile
    {
        /// <summary>
        /// Function will perform a rename request and return the result.
        /// </summary>
        public virtual bool Rename(string name)
        {
            if ((!CanBeRenamed) || (!Valid) || (Distance >= 2))
                return false;
            Stealth.Client.RenameMobile(Serial.Value, name);
            return true;
        }

        /// <summary>
        /// Alternate constructor.
        /// </summary>
        public BaseCreature(uint ObjectID) : base(ObjectID)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseCreature(Serial serial) : base(serial)
        {
        }

        /// <summary>
        /// Returns if the creature can be renamed.
        /// </summary>
        public virtual bool CanBeRenamed
        {
            get { return Stealth.Client.MobileCanBeRenamed(Serial.Value); }
        }

        /// <summary>
        /// Returns if the creature can be damaged.
        /// </summary>
        public virtual bool CanBeDamaged
        {
            get { return !YellowHits; }
        }

    }
}