/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/
using ScriptSDK.Attributes;
using ScriptSDK.Data;
using ScriptSDK.Mobiles;

namespace ScriptSDK.Targets
{
    /// <summary>
    /// Abstract target class offers basical workflow for targeting system.
    /// </summary>		
    public abstract class Target
    {
        /// <summary>
        ///Stores reference to target owner.
        /// </summary>
        public UOEntity Entity { get; protected set; }

        /// <summary>
        ///Stores target cursor delay.
        /// </summary>
        public int Delay { get; protected set; }

        /// <summary>
        ///Stores the state of target process.
        /// </summary>
        public TargetState TargetState { get; protected set; }

        /// <summary>
        ///Default constructor.
        /// </summary>
        public Target(int delay) : this(PlayerMobile.GetPlayer(), delay)
        {
        }

        /// <summary>
        ///Object designer constructor.
        /// </summary>
        protected Target(UOEntity source, int delay)
        {
            Entity = source;
            Delay = delay;
            TargetState = TargetState.New;
        }

        /// <summary>
        ///Process function when OnTarget() failed.
        /// </summary>
        protected virtual bool OnCancel(params object[] args)
        {
            TargetState = TargetState.Canceled;
            return true;
        }

        /// <summary>
        ///Process function when OnTarget() succeeded.
        /// </summary>
        protected virtual bool OnFinish(params object[] args)
        {
            TargetState = TargetState.Finished;
            return true;
        }

        /// <summary>
        ///Process function when target cursor appeared in time frame.
        /// </summary>
        protected virtual bool OnTarget(params object[] args)
        {
            TargetState = TargetState.Busy;
            return true;
        }

        /// <summary>
        ///Process function when target cursor appeared not in time frame.
        /// </summary>
        protected virtual bool OnTimeout(params object[] args)
        {
            TargetState = TargetState.Timeout;
            return true;
        }

        /// <summary>
        ///Function wich controls the targeting workflow.
        /// </summary>
        public virtual bool Action(params object[] args)
        {
            if (TargetHelper.GetTarget().WaitForTarget(Delay))
                return OnTarget(args) ? OnFinish(args) : OnCancel(args);
            return OnTimeout(args);
        }
    }
}