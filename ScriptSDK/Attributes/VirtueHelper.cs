using System;
using ScriptSDK.Data;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// Sample #1 standalone
    /// <code language="CSharp">
    /// <![CDATA[
    /// // Reads instance of Virtuehelper
    /// VirtueHelper helper = VirtueHelper.GetVirtues();
    /// 
    /// //Adding Event to VirtueHelper
    /// helper.OnUse += (sender, args) =>
    /// {
    ///     //Write Result to Console
    ///     Console.WriteLine("Result of using {0} is {1}",args.State,args.Virtue);
    /// };
    /// 
    /// // Setup the Parser Delay for latency overflow
    /// helper.ParserDelay = 500;
    /// 
    /// // Now we try to valor a Champion Skull
    /// if (helper.Use(Virtue.Valor))
    /// {
    ///     // We use the new Target system for targeting.
    ///     EntityTarget target = new EntityTarget(1000);
    ///     var result = target.Action(new UOEntity(0x00000017));
    ///     //Write Result to Console
    ///     Console.WriteLine("Result of summoning Champion Spawn is {0}",result);
    /// }
    /// ]]>
    /// </code>
    /// </example>
    /// 
    /// <example>
    /// Sample #2 using player
    /// <code language="CSharp">
    /// <![CDATA[
    /// // Reads instance of Virtuehelper
    /// PlayerMobile player = PlayerMobile.GetPlayer();
    /// 
    /// //Adding Event to VirtueHelper
    /// player.Virtues.OnUse += (sender, args) =>
    /// {
    ///     //Write Result to Console
    ///     Console.WriteLine("Result of using {0} is {1}", args.State, args.Virtue);
    /// };
    /// 
    /// // Setup the Parser Delay for latency overflow
    /// player.Virtues.ParserDelay = 500;
    /// 
    /// // Now we try to valor a Champion Skull
    /// if (player.Virtues.Use(Virtue.Valor))
    /// {
    ///     EntityTarget target = new EntityTarget(1000);
    ///     var result = target.Action(new UOEntity(0x00000017));
    ///     //Write Result to Console
    ///     Console.WriteLine("Result of summoning Champion Spawn is {0}", result);
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class VirtueHelper
    {
        private VirtueHelper(PlayerMobile owner)
        {
            _owner = owner;
        }

        private static VirtueHelper _instance { get; set; }
        private PlayerMobile _owner { get; set; }

        /// <summary>
        /// Property describes the parser delay, the system will use between request and use of virtues.
        /// </summary>
        public virtual int ParserDelay { get; set; }

        /// <summary>
        /// Function requests virtues gump via paperdoll, if the player is valid.
        /// </summary>
        /// <returns></returns>
        public virtual bool Request()
        {
            if (!_owner.Valid)
                return false;
            Stealth.Client.ReqVirtuesGump();
            return true;
        }

        /// <summary>
        /// Function calls <b>Request()</b> and based on the result click onto Virtues Icon. Then calls event(if assigned).
        /// Always exposes the current state.
        /// </summary>
        /// <param name="virtue"></param>
        /// <returns></returns>
        public virtual bool Use(Virtue virtue)
        {
            var state = Request();
            if (state)
            {
                if (ParserDelay > 0)
                    Stealth.Client.Wait(ParserDelay);
                Stealth.Client.UseVirtue(virtue);
            }
            return OnVirtueUse(new VirtuesEventArgs { Virtue = virtue, State = state });
        }

        /// <summary>
        /// Function expose singleton instanced reference of Virtues
        /// </summary>
        /// <returns></returns>
        public static VirtueHelper GetVirtues()
        {
            return _instance ?? (_instance = new VirtueHelper(PlayerMobile.GetPlayer()));
        }

        /// <summary>
        /// Event allows to add action after handling Use(Virtue)
        /// </summary>
        public event EventHandler<VirtuesEventArgs> OnUse;

        private bool OnVirtueUse(VirtuesEventArgs e)
        {
            var handler = OnUse;
            if (handler != null)
            {
                handler(this, e);
            }
            return e.State;
        }
    }
}