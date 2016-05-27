/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/

using StealthAPI;

namespace ScriptSDK.Configuration
{
    /// <summary>
    /// ShopOptions is a configuration class for vendoring system.
    /// </summary>		
    public static class ShopOptions
    {
        /// <summary>
        ///Stores delay for auto buy on vendoring system.
        /// </summary>
        public static ushort AutoBuyDelay
        {
            get { return Stealth.Client.GetAutoBuyDelay(); }
            set { Stealth.Client.SetAutoBuyDelay(value); }
        }

        /// <summary>
        ///Stores delay for auto sell on vendoring system.
        /// </summary>
        public static ushort AutoSellDelay
        {
            get { return Stealth.Client.GetAutoSellDelay(); }
            set { Stealth.Client.SetAutoSellDelay(value); }
        }
    }
}
