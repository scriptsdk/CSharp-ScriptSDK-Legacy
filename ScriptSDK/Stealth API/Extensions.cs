using System;
using System.ComponentModel;
#pragma warning disable 1591


namespace StealthAPI
{
    public static class Extensions
    {
        public static bool GetEnum<T>(this string name, out T result) where T : struct
        {
            return Enum.TryParse<T>(name.Replace(" ", ""), true, out result);
        }
    }
}
