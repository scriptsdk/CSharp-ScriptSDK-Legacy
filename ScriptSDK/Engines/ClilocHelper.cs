using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using StealthAPI;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// Clilochelper class is support class for localized messages.
    /// </summary>
    public static class ClilocHelper
    {
        private static Dictionary<int, string> _datatable { get; set; }

        /// <summary>
        /// Function allows to read out custom clilocfiles.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ReadClilocFile(string path, out string message)
        {
            var m_Buffer = new byte[1024];
            message = string.Empty;

            var list = new Dictionary<int, string>();

            try
            {
                using (var bin = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    bin.ReadInt32();
                    bin.ReadInt16();

                    while (bin.BaseStream.Length != bin.BaseStream.Position)
                    {
                        var number = bin.ReadInt32();
                        bin.ReadByte();
                        int length = bin.ReadInt16();

                        if (length > m_Buffer.Length)
                            m_Buffer = new byte[(length + 1023) & ~1023];

                        bin.Read(m_Buffer, 0, length);
                        var text = Encoding.UTF8.GetString(m_Buffer, 0, length);

                        list.Add(number, text);
                    }
                }
            }
            catch (Exception e)
            {
                message = e.ToString();
            }


            return list;
        }

        /// <summary>
        /// Function allows to load custom clilocfile into cache and decrease the parsertime of localized properties hugely,
        /// when cliloc exist in datatable.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool LoadClilocFileToCache(string path, out string msg)
        {
            _datatable = ReadClilocFile(path, out msg);

            return msg.Trim().Equals(string.Empty);
        }

        /// <summary>
        /// Removes\Release current cached cliloc file.
        /// </summary>
        public static void ClearClilocFileCache()
        {
            if (_datatable == null)
                _datatable = new Dictionary<int, string>();
            _datatable.Clear();
        }

        /// <summary>
        /// Function allows to get localized text either through stealth api or if included in cached datatable.
        /// </summary>
        /// <param name="ClilocID"></param>
        /// <returns></returns>
        public static string GetText(uint ClilocID)
        {
            if (_datatable == null)
                _datatable = new Dictionary<int, string>();

            if (_datatable.Values.Count > 0)
            {
                var key = (int)ClilocID;
                return _datatable.ContainsKey(key) ? _datatable[key] : string.Empty;
            }
            return Stealth.Client.GetClilocByID(ClilocID);
        }

        /// <summary>
        /// Function allows to get multiple localizedtexts. Function use GetText(Cliloc)
        /// </summary>
        /// <param name="ClilocList"></param>
        /// <returns></returns>
        public static List<string> GetText(List<uint> ClilocList)
        {
            return ClilocList.Select(GetText).ToList();
        }

        /// <summary>
        /// Function returns -1 if index not found else the index of list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ClilocID"></param>
        /// <returns></returns>
        public static int GetIndex(List<ClilocItemRec> list, uint ClilocID)
        {
            for (var i = 0; i < list.Count; i++)
                if (list[i].ClilocID.Equals(ClilocID))
                    return i;
            return -1;
        }

        /// <summary>
        /// function returns true if list contains cliloID.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ClilocID"></param>
        /// <returns></returns>
        public static bool Contains(List<ClilocItemRec> list, uint ClilocID)
        {
            return Contains(list, new List<uint> { ClilocID });
        }

        /// <summary>
        /// Function returns true when list contain all clilocID´s from clist.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="clist"></param>
        /// <returns></returns>
        public static bool Contains(List<ClilocItemRec> list, List<uint> clist)
        {
            return clist.All(e => GetIndex(list, e) >= 0);
        }

        /// <summary>
        /// Function returns true when list contains any ClilocID from clist.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="clist"></param>
        /// <returns></returns>
        public static bool ContainsAny(List<ClilocItemRec> list, List<uint> clist)
        {
            return clist.Any(e => GetIndex(list, e) > -1);
        }

        /// <summary>
        /// Function returns in proper datatype converted parameters of required cliloc.
        /// Allows to either get Text of subclilocs or ClilocID. 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ClilocID"></param>
        /// <param name="ConvertSubs"></param>
        /// <returns></returns>
        public static List<dynamic> GetParams(List<ClilocItemRec> list, uint ClilocID, bool ConvertSubs = false)
        {
            var index = GetIndex(list, ClilocID);

            if (index.Equals(-1))
                return new List<dynamic>();
            var tlist = list[index].Params;
            var rlist = new List<dynamic>();
            foreach (var e in tlist)
            {
                //ClilocID
                if (e.StartsWith("#"))
                {
                    var s = e.TrimStart('#');
                    uint o;
                    if (uint.TryParse(s, out o))
                    {
                        if (!ConvertSubs)
                            rlist.Add(o);
                        else
                            rlist.Add(GetText(o));
                        continue;
                    }
                }

                //Integer
                int i;
                if (int.TryParse(e, out i))
                {
                    rlist.Add(i);
                    continue;
                }

                //Double
                double d;
                if (double.TryParse(e, out d))
                {
                    rlist.Add(i);
                    continue;
                }

                //String
                rlist.Add(e);
            }
            return rlist;
        }

        /// <summary>
        /// Function allows to handle multiple params by utilizing GetParams(list,ClilocID,ConvertSubs).
        /// </summary>
        /// <param name="list"></param>
        /// <param name="clist"></param>
        /// <param name="ConvertSubs"></param>
        /// <returns></returns>
        public static Dictionary<uint, List<dynamic>> GetParams(List<ClilocItemRec> list, List<uint> clist, bool ConvertSubs = false)
        {
            return clist.ToDictionary(e => e, e => GetParams(list, e, ConvertSubs));
        }
    }
}