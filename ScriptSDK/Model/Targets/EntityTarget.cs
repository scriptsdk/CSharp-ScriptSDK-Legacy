/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/
using ScriptSDK.Attributes;

namespace ScriptSDK.Targets
{
    /// <summary>
    /// EntityTarget class supports the script writer to handle native targets towards objects.
    /// </summary>		
    /// <example>
    ///     Sample describes, how to use an axe together with EntityTarget
    ///     and convert logs to boards.
    ///     Then print out the result.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     EntityTarget target = new EntityTarget(PlayerMobile.GetPlayer(),1500);
    ///     Item axe = new Item(0x00000007);
    ///     Item log = new Item(0x00000127);
    ///     bool result = axe.DoubleClick();
    ///     if(result)
    ///         result = target.Action(log); //Target to specific object
    ///     Console.WriteLine(string.Format("Operation result is {0}",result);
    /// ]]>
    /// </code>
    /// </example>
    public class EntityTarget : Target
    {
        /// <summary>
        ///Object designer constructor.
        /// </summary>
        protected EntityTarget(UOEntity source, int delay) : base(source, delay)
        {
        }

        /// <summary>
        ///Default constructor.
        /// </summary>
        public EntityTarget(int delay) : base(delay)
        {
        }

        /// <summary>
        ///Overrides base.OnTarget and handles entity targets.
        ///      Allowed parameters are :
        ///       * Object inherited from UOEntity
        ///       * Object inherited from Serial
        ///       * ID of datatype "uint"
        /// </summary>
        protected override bool OnTarget(params object[] args)
        {
            if (base.OnTarget(args))
                if (args != null)
                    if (args.Length.Equals(1))
                    {
                        if (args[0] is UOEntity)
                        {
                            return TargetHelper.GetTarget().TargetTo(((UOEntity) args[0]).Serial);
                        }
                        if (args[0] is Serial)
                        {
                            return TargetHelper.GetTarget().TargetTo((Serial) args[0]);
                        }
                        if (args[0] is uint)
                        {
                            return TargetHelper.GetTarget().TargetTo(new Serial((uint) args[0]));
                        }
                    }
            return false;
        }
    }

}