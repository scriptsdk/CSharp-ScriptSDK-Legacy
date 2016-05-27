/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/
using ScriptSDK.Items;
using StealthAPI;

namespace ScriptSDK.Configuration
{
    /// <summary>
    /// ObjectOptions is a configuration class for UOEntities.
    /// </summary>
    public static class ObjectOptions
    {
        /// <summary>
        ///Stores delay (ms) used by Stealth for drag and drop actions.
        /// </summary>
        public static uint DropDelay
        {
            get { return Stealth.Client.GetDropDelay(); }
            set { Stealth.Client.SetDropDelay(value); }
        }

        /// <summary>
        ///Stores delay (ms) for tooltip parser which is used by Stealth client.
        /// </summary>
        public static int ToolTipDelay { get; set; }

        /// <summary>
        /// Function assignes the cached "Catchbag".
        /// </summary>
        public static byte SetCatchBag(Container item)
        {
            return SetCatchBag(item.Serial);
        }

        /// <summary>
        /// Function assignes the cached "Catchbag".
        /// </summary>
        public static byte SetCatchBag(Serial serial)
        {
            return SetCatchBag(serial.Value);
        }

        /// <summary>
        /// Function assignes the cached "Catchbag".
        /// </summary>
        public static byte SetCatchBag(uint ObjectID)
        {
            return Stealth.Client.SetCatchBag(ObjectID);
        }

        /// <summary>
        /// Function releases the cached "Catchbag".
        /// </summary>
        public static void ReleaseCatchBag()
        {
            Stealth.Client.UnsetCatchBag();
        }
    }


}
