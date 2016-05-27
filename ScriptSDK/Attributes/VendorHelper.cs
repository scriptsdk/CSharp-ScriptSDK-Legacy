using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Vendorhelper is a singleton pattern class used by player.
    /// </summary>
    public class VendorHelper
    {
        private VendorHelper(PlayerMobile owner)
        {
            _owner = owner;
        }
        private static VendorHelper _instance { get; set; }
        
        /// <summary>
        /// Stores reference to player.
        /// </summary>
        protected PlayerMobile _owner { get; set; }

        /// <summary>
        /// Allows to get a reference of singleton pattern.
        /// </summary>
        /// <returns></returns>
        public static VendorHelper GetVendorHelper()
        {
            return _instance ?? (_instance = new VendorHelper(PlayerMobile.GetPlayer()));
        }

        /// <summary>
        /// Configuration for auto buy agent.
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemColor"></param>
        /// <param name="quantity"></param>
        public void AutoBuy(ushort itemType, ushort itemColor, ushort quantity)
        {
            Stealth.Client.AutoBuy(itemType, itemColor, quantity);
        }

        /// <summary>
        /// Returns shoplist as string.
        /// </summary>
        /// <returns></returns>
        public string GetShopList()
        {
            return Stealth.Client.GetShopList();
        }

        /// <summary>
        /// Clears shoplist.
        /// </summary>
        public void ClearShopList()
        {
            Stealth.Client.ClearShopList();
        }

        /// <summary>
        /// Allows to configurate the auto buy agent.
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemColor"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="name"></param>
        public void AutoBuyEx(ushort itemType, ushort itemColor, ushort quantity, uint price, string name)
        {
            Stealth.Client.AutoBuyEx(itemType, itemColor, quantity, price, name);
        }

        /// <summary>
        /// Allows to configurate the auto sell agent.
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemColor"></param>
        /// <param name="quantity"></param>
        public void AutoSell(ushort itemType, ushort itemColor, ushort quantity)
        {
            Stealth.Client.AutoSell(itemType, itemColor, quantity);
        }
    }
}