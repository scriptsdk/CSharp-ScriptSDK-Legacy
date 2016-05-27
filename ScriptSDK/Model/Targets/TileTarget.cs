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

namespace ScriptSDK.Targets
{
    /// <summary>
    /// TileTarget class supports the script writer to handle native targets towards locations or tile locations.
    /// </summary>		
    /// <example>
    /// Tile Targeting Sample #1 using native Tile and X\Y\Z coords.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     TileTarget target = new TileTarget(PlayerMobile.GetPlayer(),1500);
    ///     Item shovel = new Item(0x00000007);
    ///     bool result = shovel.DoubleClick();
    ///     if(result)
    ///         result = target.Action(1255, 1451, 900, -90);
    ///     Console.WriteLine(string.Format("Operation result is {0}",result);
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Tile Targeting Sample #2 using Tile and Point3D as coords.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     TileTarget target = new TileTarget(PlayerMobile.GetPlayer(),1500);
    ///     Item shovel = new Item(0x00000007);
    ///     Point3D p = new Point3D(1451,900,-90);
    ///     bool result = shovel.DoubleClick();
    ///     if(result)
    ///         result = target.Action(1255,p);
    ///     Console.WriteLine(string.Format("Operation result is {0}",result);
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Location Targeting Sample #1 using native X\Y\Z coords.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     TileTarget target = new TileTarget(PlayerMobile.GetPlayer(),1500);
    ///     Item shovel = new Item(0x00000007);
    ///     bool result = shovel.DoubleClick();
    ///     if(result)
    ///         result = target.Action(1451,900,-90);
    ///     Console.WriteLine(string.Format("Operation result is {0}",result);
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Tile Targeting Sample #2 using Point3D as coords.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     TileTarget target = new TileTarget(PlayerMobile.GetPlayer(),1500);
    ///     Item shovel = new Item(0x00000007);
    ///     Point3D p = new Point3D(1451,900,-90);
    ///     bool result = shovel.DoubleClick();
    ///     if(result)
    ///         result = target.Action(p);
    ///     Console.WriteLine(string.Format("Operation result is {0}",result);
    /// ]]>
    /// </code>
    /// </example>
    public class TileTarget : Target
    {
        /// <summary>
        ///Object designer constructor.
        /// </summary>
        protected TileTarget(UOEntity source, int delay) : base(source, delay)
        {
        }

        /// <summary>
        ///Default constructor.
        /// </summary>
        public TileTarget(int delay) : base(delay)
        {
        }

        /// <summary>
        ///Overrides base.OnTarget and handles tile and location targets.
        ///      Allowed parameters are :
        ///       * (Point3D Point)
        ///       * (ushort Tile, Point3D Point) 
        ///       * (int X, int Y, int Z)
        ///       * (Ushort Tile, int X, int Y, int Z "uint"
        /// </summary>
        protected override bool OnTarget(params object[] args)
        {
            if (base.OnTarget(args))
            {
                if (args != null)
                {
                    if (args.Length.Equals(1))
                    {
                        if (args[0] is Point3D)
                            return TargetHelper.GetTarget().TargetTo(((Point3D)args[0]));
                    }
                    if (args.Length.Equals(2))
                    {
                        if ((args[0] is ushort) && (args[1] is Point3D))
                            return TargetHelper.GetTarget().TargetTo((ushort)args[0], ((Point3D)args[1]));
                    }
                    if (args.Length.Equals(3))
                    {
                        if ((args[0] is int) && (args[1] is int) && (args[2] is int))
                            return TargetHelper.GetTarget().TargetTo(new Point3D((int)args[1], (int)args[2], (int)args[3]));
                    }
                    if (args.Length.Equals(4))
                    {
                        if ((args[0] is ushort) && (args[1] is int) && (args[2] is int) && (args[3] is int))
                            return TargetHelper.GetTarget().TargetTo((ushort)args[0], new Point3D((int)args[1], (int)args[2], (int)args[3]));
                    }
                }
            }
            return false;
        }
    }

}