using System.Collections.Generic;
using System.Linq;
using StealthAPI;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// EasyUOHelper stores different converter and transmission functions.
    /// </summary>
    public static class EasyUOHelper
    {
        /// <summary>
        /// Sets data to registry, where EASYUO could read. SetData(1,Text) is equilavent to "set *1 Text".
        /// </summary>
        /// <param name="nspace"></param>
        /// <param name="value"></param>
        public static void SetData(int nspace, string value)
        {
            Stealth.Client.SetEasyUO(nspace, value);
        }

        /// <summary>
        /// Gets data from registry, where EASYUO could write. GetData(1) is reading from data stored through "set *1 Text".
        /// </summary>
        /// <param name="nspace"></param>
        /// <returns></returns>
        public static string GetData(int nspace)
        {
            return Stealth.Client.GetEasyUO(nspace);
        }

        /// <summary>
        /// Function allows to convert EUO-Type to Stealth Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ushort ConvertToStealthType(string value)
        {
            return Stealth.Client.EUO2StealthType(value);
        }

        /// <summary>
        /// Function allows to convert a list of EUO-Types to a list of Stealth-Types.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<ushort> ConvertToStealthType(List<string> values)
        {
            return values.Select(ConvertToStealthType).ToList();
        }

        /// <summary>
        /// Function allows to convert EUO-ID to Stealth ID.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static uint ConvertToStealthID(string value)
        {
            return Stealth.Client.EUO2StealthID(value);
        }

        /// <summary>
        /// Function allows to convert a list of EUO-ID´s to a list of Stealth-ID´s.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<uint> ConvertToStealthID(List<string> values)
        {
            return values.Select(ConvertToStealthID).ToList();
        }
    }
}