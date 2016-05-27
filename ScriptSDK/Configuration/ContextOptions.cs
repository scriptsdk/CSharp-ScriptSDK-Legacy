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
    /// ContextOptions is a configuration class for context menu system.
    /// </summary>		
    /// <example>
    /// <b>Example #1 </b>
    /// <code language="CSharp">
    /// <![CDATA[
    ///     ContextOptions.Initialize();
    /// ]]>
    /// </code>
    /// </example>	
    public static class ContextOptions
    {
        /// <summary>
        ///Stores reference to current assigned object through context menu system.
        /// </summary>
        public static Serial AssignedObject { get; set; }

        /// <summary>
        /// Stores the character, the context menu entry parser use for splitting. <br/>
        /// Should be a character which never appear on tooltip.
        /// </summary>
        public static char ParserSymbol { get; set; }

        /// <summary>
        ///Stores the parser delay in ms which is used for parsing strings to objects.
        /// </summary>
        public static int ParserDelay { get; set; }

        /// <summary>
        /// Function initializes ContextMenuOptions  with default values.
        /// Obsolete when SDK.Initialize(); was used before.
        /// </summary>
        public static void Initialize()
        {
            AssignedObject = new Serial(0);
            ParserSymbol = '❦';
            ParserDelay = 1000;
            Stealth.Client.ClearContextMenu();
            Stealth.Client.RequestContextMenu(0);
            Stealth.Client.SetContextMenuHook(0, 0);
        }
    }
}
