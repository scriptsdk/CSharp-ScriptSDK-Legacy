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

        public static TResult SafeInvoke<T, TResult>(this T isi, Func<T, TResult> call) where T : ISynchronizeInvoke
        {
            if (!isi.InvokeRequired)
                return call(isi);
            IAsyncResult result = isi.BeginInvoke((Delegate)call, new object[1] { (object)isi });
            return (TResult)isi.EndInvoke(result);
        }

        public static void SafeInvoke<T>(this T isi, Action<T> call) where T : ISynchronizeInvoke
        {
            if (isi.InvokeRequired)
                isi.BeginInvoke((Delegate)call, new object[1] { (object)isi });
            else
                call(isi);
        }
    }
}
