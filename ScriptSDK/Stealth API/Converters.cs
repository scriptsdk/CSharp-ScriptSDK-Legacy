using System;
using System.Collections.Generic;
using System.Linq;
#pragma warning disable 1591


namespace StealthAPI
{
    public static class Converters
    {
        /// <summary>
        /// Converts a TDateTime from Delphi to a DateTime in .NET
        /// For more info see: http://docs.embarcadero.com/products/rad_studio/delphiAndcpp2009/HelpUpdate2/EN/html/delphivclwin32/System_TDateTime.html
        /// </summary>
        /// <param name="tDateTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this float tDateTime)
        {
            DateTime startDate = new DateTime(1899, 12, 30);
            var days = (int)tDateTime;
            var hours = 24 * (tDateTime - days);
            return startDate.AddDays(days).AddHours(hours);
        }

        /// <summary>
        /// Converts a TDateTime from Delphi to a DateTime in .NET
        /// For more info see: http://docs.embarcadero.com/products/rad_studio/delphiAndcpp2009/HelpUpdate2/EN/html/delphivclwin32/System_TDateTime.html
        /// </summary>
        /// <param name="tDateTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this double tDateTime)
        {
            DateTime startDate = new DateTime(1899, 12, 30);
            var days = (int)tDateTime;
            var hours = 24 * (tDateTime - days);
            return startDate.AddDays(days).AddHours(hours);
        }

        /// <summary>
        /// Converts a datetime.datetime from Python to a TDateTimein Delphi.
        /// For more info see: http://docs.embarcadero.com/products/rad_studio/delphiAndcpp2009/HelpUpdate2/EN/html/delphivclwin32/System_TDateTime.html
        /// </summary>
        /// <param name="dateTime"></param>
        public static double ToDouble(this DateTime dateTime)
        {

            DateTime startDate = new DateTime(1899, 12, 30);
            var deltaDate = (dateTime - startDate);

            var days = deltaDate.Days;
            deltaDate -= new TimeSpan(days, 0, 0, 0);

            var hours = ((deltaDate.TotalSeconds) / 3600.0) / 24;

            return days + hours;
        }

        //public static string stringFromMemory(this int address)
        //{

        //    outLen = c_uint()
        //    memmove(addressof(outLen), address, sizeof(c_uint))
        //    if outLen.value > 0:
        //        return wstring_at(address + 4, outLen.value) 
        //    else:
        //        return ''
        //}
        /// <summary>
        /// A generator to divide a sequence into chunks of n units.
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> SplitN<T>(this IEnumerable<T> seq, int n)
        {
            while (seq.Count() > 0)
            {
                yield return seq.Take(n);
                seq = seq.Skip(n);
            }
        }
    }
}
