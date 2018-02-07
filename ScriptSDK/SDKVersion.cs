/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/


using ScriptSDK.Configuration;
using ScriptSDK.Engines;

namespace ScriptSDK
{
    /// <summary>
    /// SDK root class providing global initializers and informative constansts.
    /// </summary>		
    /// <example>
    /// <b>Example #1 :</b> Expose how to initialize all special systems.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     SDK.Initialize();
    /// ]]>
    /// </code>
    /// </example>
    public static class SDK
    {
        /// <summary>
        ///  Stores actual release build as text
        /// </summary>
        public const string Revision = "0.9.7";

        /// <summary>
        /// Stores timestamp of last success release build.
        /// </summary>
        public const string BuildDate = @"05.06.2016";

        /// <summary>
        /// Stores text about stealth and API compability.
        /// </summary>
        public const string CompatibleClient = @"Stealth 7.4 Rev 967 or above";

        /// <summary>
        /// SDK initializer which should be called one time only on first script call.
        /// </summary>
        public static void Initialize()
        {
            ContextOptions.Initialize();
            Scanner.Initialize();
            TileReader.Initialize();
            ScriptLogger.Initialize();

        }
    }
}