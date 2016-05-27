/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/
using System;
using System.Collections;

namespace ScriptSDK.Utils
{
    /// <summary>
    /// Insensitive class contains functions and properties to compare text insensitive.
    /// </summary>		
    public class Insensitive
    {
        private static readonly IComparer m_Comparer = CaseInsensitiveComparer.Default;

        private Insensitive()
        { }

        /// <summary>
        /// Stores insensitive Comperator.
        /// </summary>
        public static IComparer Comparer
        {
            get { return m_Comparer; }
        }

        /// <summary>
        /// Function returns amount of matching characters between two texts.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Compare(string a, string b)
        {
            return m_Comparer.Compare(a, b);
        }

        /// <summary>
        /// Function returns if two texts are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equals(string a, string b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null || a.Length != b.Length)
                return false;

            return (m_Comparer.Compare(a, b) == 0);
        }

        /// <summary>
        /// Function returns true if "first" text starts with "second" text.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool StartsWith(string a, string b)
        {
            if (a == null || b == null || a.Length < b.Length)
                return false;

            return (m_Comparer.Compare(a.Substring(0, b.Length), b) == 0);
        }

        /// <summary>
        /// Function returns true if "first" text ends with "second" text.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EndsWith(string a, string b)
        {
            if (a == null || b == null || a.Length < b.Length)
                return false;

            return (m_Comparer.Compare(a.Substring(a.Length - b.Length), b) == 0);
        }

        /// <summary>
        /// Function returns true if "first" text contains "second" text.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Contains(string a, string b)
        {
            if (a == null || b == null || a.Length < b.Length)
                return false;

            a = a.ToLower();
            b = b.ToLower();

            return (a.IndexOf(b, StringComparison.Ordinal) >= 0);
        }
    }
}