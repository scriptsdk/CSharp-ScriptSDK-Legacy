// /*
// ███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
// ██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
// ███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
// ╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
// ███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
// ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
// */

using System;

namespace ScriptSDK.Gumps
{
    /// <summary>
    /// Extends Gump instanced objects by further functions.
    /// </summary>
    public static class GumpExtensions
    {
        /// <summary> 
        /// Function describes a process where the user waits for a close action for certain gump type dynamicly but <br/>
        /// for a given timespan. Returns true if this event occured or false if not.
        /// </summary>
        /// <param name="gump"></param>
        /// <param name="MaxDelay"></param>
        /// <returns></returns>
        public static bool WaitForGumpClose(this Gump gump, double MaxDelay)
        {
            var start = DateTime.UtcNow;
            var finish = start.AddMilliseconds(MaxDelay);
            bool rstate;
            do
                rstate = gump.Index.Equals(-1);
            while (!rstate && DateTime.UtcNow < finish);
            return rstate;
        }
    }
}
