/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ScriptSDK.Data;

namespace ScriptSDK.Utils
{
    /// <summary>
    /// Utility class contains random generators and text handlers.
    /// </summary>		
    public static class Utility
    {
        private static UTF8Encoding _utf8;
        private static UTF8Encoding _utfw8;
        private static Random _random;

        /// <summary>
        /// Stores RandomGenerator engine.
        /// </summary>
        public static Random RandomGenerator
        {
            get { return _random ?? (_random = new Random()); }
        }

        /// <summary>
        /// Stores text encoder with UTF8 but without encoding.
        /// </summary>
        public static Encoding UTF8
        {
            get { return _utf8 ?? (_utf8 = new UTF8Encoding(false, false)); }
        }

        /// <summary>
        /// Stores text encoder with UTF8 but with encoding.
        /// </summary>
        public static Encoding UTF8WithEncoding
        {
            get { return _utfw8 ?? (_utfw8 = new UTF8Encoding(true, false)); }
        }

        /// <summary>
        /// Stores insensitive text comperator engine.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int InsensitiveCompare(string first, string second)
        {
            return Insensitive.Compare(first, second);
        }

        /// <summary>
        /// Function returns true if "first" text starts with "second" text.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool InsensitiveStartsWith(string first, string second)
        {
            return Insensitive.StartsWith(first, second);
        }

        /// <summary>
        /// Function adds string to stringbuilder object together with a seperator between old and new text.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        /// <param name="seperator"></param>
        public static void Seperate(StringBuilder sb, string value, string seperator)
        {
            if (sb.Length > 0)
                sb.Append(seperator);

            sb.Append(value);
        }

        /// <summary>
        /// Function fixes html-tags and return passed text.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FixHtml(string str)
        {
            if (str == null)
                return "";

            var hasOpen = (str.IndexOf('<') >= 0);
            var hasClose = (str.IndexOf('>') >= 0);
            var hasPound = (str.IndexOf('#') >= 0);

            if (!hasOpen && !hasClose && !hasPound)
                return str;

            var sb = new StringBuilder(str);

            if (hasOpen)
                sb.Replace('<', '(');

            if (hasClose)
                sb.Replace('>', ')');

            if (hasPound)
                sb.Replace('#', '-');

            return sb.ToString();
        }

        /// <summary>
        /// Function will perform method for each object in enumerable and pass value as parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eable"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> eable, Action<T> action)
        {
            foreach (var obj in eable)
            {
                action(obj);
            }
        }


        /// <summary>
        /// Extension converts DateTime to proper UnixTime.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToUnixTime(this DateTime value)
        {
            return (int)(value - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }


        /// <summary>
        /// Function returns passed amount of totalbytes to proper data size format as text.
        /// </summary>
        /// <param name="totalBytes"></param>
        /// <returns></returns>
        public static string FormatByteAmount(long totalBytes)
        {
            if (totalBytes > 1000000000000)
                return string.Format("{0:F1} TB", totalBytes / ((1024.0 * 1024.0) * (1024.0 * 1024.0)));

            if (totalBytes > 1000000000)
                return string.Format("{0:F1} GB", (double)totalBytes / (1024 * 1024 * 1024));

            if (totalBytes > 1000000)
                return string.Format("{0:F1} MB", (double)totalBytes / (1024 * 1024));

            if (totalBytes > 1000)
                return string.Format("{0:F1} KB", (double)totalBytes / 1024);

            return string.Format("{0} Bytes", totalBytes);
        }

        /// <summary>
        /// Function returns custom attributes stored on a class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static T[] GetCustomAttributes<T>(this MemberInfo info, bool inherit) where T : Attribute
        {
            return (T[])info.GetCustomAttributes(typeof(T), inherit);
        }


        /// <summary>
        /// Function returns array as enumerateable object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static IEnumerator<T> GetEnumerator<T>(this T[] array)
        {
            return array.AsEnumerable().GetEnumerator();
        }


        /// <summary>
        /// Function removes html-tags and return passed text.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveHtml(string str)
        {
            return str.Replace("<", "").Replace(">", "").Trim();
        }


        /// <summary>
        /// Function returns if passed text is numeric only.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            return !Regex.IsMatch(str, "[^0-9]");
        }


        /// <summary>
        /// Function returns if passed text is alphabetical only.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAlpha(string str)
        {
            return !Regex.IsMatch(str, "[^a-z]", RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// Function returns if passed text is alpha numeric.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(string str)
        {
            return !Regex.IsMatch(str, "[^a-z0-9]", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Extension allows to shuffle values in passed generic array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T[] Shuffle<T>(this T[] source)
        {
            return Shuffle(source, 0, source.Length);
        }

        /// <summary>
        /// Extension allows to shuffle values in passed generic array from passed index up to passed length.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] Shuffle<T>(this T[] source, int index, int length)
        {
            var sorted = new T[source.Length];
            var randoms = new byte[sorted.Length];

            source.CopyTo(sorted, 0);

            RandomGenerator.NextBytes(randoms);
            Array.Sort(randoms, sorted, index, length);

            return sorted;
        }

        /// <summary>
        /// Function returns squared distance of area between two points.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetDistanceToSqrt(Point3D p1, Point3D p2)
        {
            var xDelta = p1.X - p2.X;
            var yDelta = p1.Y - p2.Y;

            return Math.Sqrt((xDelta * xDelta) + (yDelta * yDelta));
        }

        /// <summary>
        /// Function returns random value between 0.* and 1.0.
        /// </summary>
        /// <returns></returns>
        public static double RandomDouble()
        {
            return RandomGenerator.NextDouble();
        }

        /// <summary>
        /// Function calculates the direction point "from" takes towards point "to".
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Direction GetDirection(IPoint2D from, IPoint2D to)
        {
            var dx = to.X - from.X;
            var dy = to.Y - from.Y;

            var adx = Math.Abs(dx);
            var ady = Math.Abs(dy);

            if (adx >= ady * 3)
            {
                if (dx > 0)
                    return Direction.East;
                return Direction.West;
            }
            if (ady >= adx * 3)
            {
                if (dy > 0)
                    return Direction.South;
                return Direction.North;
            }
            if (dx > 0)
            {
                if (dy > 0)
                    return Direction.Down;
                return Direction.Right;
            }
            return dy > 0 ? Direction.Left : Direction.Up;
        }

        /// <summary>
        /// Function returns the entry on highest array index.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static object GetArrayCap(Array array, int index)
        {
            return GetArrayCap(array, index, null);
        }

        /// <summary>
        /// Function returns the entry on highest array index or emptyvalue if not possible.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="emptyValue"></param>
        /// <returns></returns>
        public static object GetArrayCap(Array array, int index, object emptyValue)
        {
            if (array != null)
            {
                if (array.Length > 0)
                {
                    if (index < 0)
                    {
                        index = 0;
                    }
                    else if (index >= array.Length)
                    {
                        index = array.Length - 1;
                    }

                    return array.GetValue(index);
                }
            }
            return emptyValue;
        }

        /// <summary>
        /// Rolls passed amount of dices with passed amount of sides and adds additional bonus.
        /// </summary>
        /// <param name="numDice"></param>
        /// <param name="numSides"></param>
        /// <param name="bonus"></param>
        /// <returns></returns>
        public static int Dice(int numDice, int numSides, int bonus)
        {
            var total = 0;
            for (var i = 0; i < numDice; ++i)
                total += Random(numSides) + 1;
            total += bonus;
            return total;
        }

        /// <summary>
        /// Function returns random text entry within passed list.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string RandomList(params string[] list)
        {
            return list[RandomGenerator.Next(list.Length)];
        }

        /// <summary>
        /// Function returns random numeric entry within passed list.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int RandomList(params int[] list)
        {
            return list[RandomGenerator.Next(list.Length)];
        }

        /// <summary>
        /// Function returns random generic entry within passed list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomList<T>(params T[] list)
        {
            return list[RandomGenerator.Next(list.Length)];
        }

        /// <summary>
        /// Function returns random boolean value.
        /// </summary>
        /// <returns></returns>
        public static bool RandomBool()
        {
            return (RandomGenerator.Next(2) == 0);
        }

        /// <summary>
        /// Function returns random numeric value between two bounds.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomMinMax(int min, int max)
        {
            if (min > max)
            {
                var copy = min;
                min = max;
                max = copy;
            }
            else if (min == max)
            {
                return min;
            }

            return min + RandomGenerator.Next((max - min) + 1);
        }

        /// <summary>
        /// Function returns random numeric value. if count is 0 it returns from , count bigger 0 returns 
        /// random * 1 and count smaller 0 returns random * -1.  
        /// </summary>
        /// <param name="from"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int Random(int from, int count)
        {
            if (count == 0)
            {
                return from;
            }
            if (count > 0)
            {
                return @from + RandomGenerator.Next(count);
            }
            return @from - RandomGenerator.Next(-count);
        }

        /// <summary>
        /// Function returns random numeric value.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int Random(int count)
        {
            return RandomGenerator.Next(count);
        }

        /// <summary>
        /// Function checks if the range between two points matchs passed range.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool RangeCheck(IPoint2D p1, IPoint2D p2, int range)
        {
            return (p1.X >= (p2.X - range))
                   && (p1.X <= (p2.X + range))
                   && (p1.Y >= (p2.Y - range))
                   && (p2.Y <= (p2.Y + range));
        }

        /// <summary>
        /// Function allows to check if value is between two bounds.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="bound1"></param>
        /// <param name="bound2"></param>
        /// <param name="allowance"></param>
        /// <returns></returns>
        public static bool NumberBetween(double num, int bound1, int bound2, double allowance)
        {
            if (bound1 > bound2)
            {
                var i = bound1;
                bound1 = bound2;
                bound2 = i;
            }

            return (num < bound2 + allowance && num > bound1 - allowance);
        }
    }
}