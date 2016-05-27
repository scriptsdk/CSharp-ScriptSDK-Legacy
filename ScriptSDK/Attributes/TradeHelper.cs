using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Object expose handles, actions and properties about trading system.
    /// </summary>
    public class TradeHelper
    {
        private TradeHelper(PlayerMobile owner)
        {
            _owner = owner;
        }

        private static TradeHelper _instance { get; set; }
        /// <summary>
        /// Stores reference to trading player.
        /// </summary>
        protected PlayerMobile _owner { get; set; }

        /// <summary>
        /// Returns amount of active trades.
        /// </summary>
        public byte ActiveTrades
        {
            get { return Stealth.Client.GetTradeCount(); }
        }

        /// <summary>
        /// Returns reference of trading system.
        /// </summary>
        /// <returns></returns>
        public static TradeHelper GetTrade()
        {
            return _instance ?? (_instance = new TradeHelper(PlayerMobile.GetPlayer()));
        }

        /// <summary>
        /// Returns reference to trading partner.
        /// </summary>
        /// <param name="TradeNumber"></param>
        /// <returns></returns>
        public Mobile GetOpponent(byte TradeNumber)
        {
            return
                new Mobile(new Serial(TradeNumber >= ActiveTrades ? 0 : Stealth.Client.GetTradeOpponent(TradeNumber)));
        }

        /// <summary>
        /// Confirms trade.
        /// </summary>
        /// <param name="TradeNumber"></param>
        /// <returns></returns>
        public bool Confirm(byte TradeNumber)
        {
            if (TradeNumber >= ActiveTrades)
                return false;
            Stealth.Client.ConfirmTrade(TradeNumber);
            return true;
        }

        /// <summary>
        /// Cancel trade.
        /// </summary>
        /// <param name="TradeNumber"></param>
        /// <returns></returns>
        public bool Cancel(byte TradeNumber)
        {
            if (TradeNumber >= ActiveTrades)
                return false;
            Stealth.Client.CancelTrade(TradeNumber);
            return true;
        }
    }
}