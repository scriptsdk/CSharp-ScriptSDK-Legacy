using System;
using System.Collections.Generic;
using System.Linq;
using ScriptSDK.Data;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// Generic scanner class wich expose the basical handlings for advanced queued searchs.
    /// Don't forget to call SDK.Initiliaze() if you want this class to use sane defaults.
    /// </summary>
    public static class Scanner
    {
        #region Init
        /// <summary>
        /// Do not use this. Check SDK.Initialize();
        /// </summary>
        public static void Initialize()
        {
            UseNullPoint = true;
            Range = 18;
            VerticalRange = 18;
            AutoGCMode = true;
            GCDelay = new TimeSpan(0, 0, 15, 0, 0);
            GCCache = new List<DateTime>();
        }

        #endregion

        #region Custom Garbage Collector

        /// <summary>
        /// Gets or sets the pseudo garbage collector for stealth.
        /// </summary>
        public static bool AutoGCMode { get; set; }

        /// <summary>
        /// Gets or sets the garbage collector delay.
        /// </summary>
        public static TimeSpan GCDelay { get; set; }

        private static List<DateTime> GCCache { get; set; }

        #endregion

        #region Configuration

        /// <summary>
        /// Gets or sets the vertical (Z-Axis) scanner range.
        /// </summary>
        public static uint VerticalRange
        {
            get { return Stealth.Client.GetFindVertical(); }
            set { Stealth.Client.SetFindVertical(value); }
        }
        /// <summary>
        /// Gets or sets the horizontal (X-Y-Axis) scanner range.
        /// </summary>
        public static uint Range
        {
            get { return Stealth.Client.GetFindDistance(); }
            set { Stealth.Client.SetFindDistance(value); }
        }

        /// <summary>
        /// Gets or sets if the nullpoint (e.q. under feet) can be used.
        /// </summary>
        public static bool UseNullPoint
        {
            get { return Stealth.Client.GetFindInNulPoint(); }
            set { Stealth.Client.SetFindInNulPoint(value); }
        }

        #endregion

        #region Base

        private static bool HandleGarbage()
        {
            if (AutoGCMode)
            {
                if (GCCache == null)
                    GCCache = new List<DateTime>();

                if (GCCache.Count > 0 && DateTime.UtcNow - GCCache.Last() > GCDelay)
                {
                    ClearIgnoreList();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Generic Search by graphics, colors, locations and allows recursive sub search in container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Colors"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<ushort> Graphics, List<ushort> Colors, List<uint> Locations, bool SubSearch)
            where T : UOEntity
        {
            HandleGarbage();

            #region Perform Query

            var list = new List<uint>();
            var vlist = new List<uint>();
            Graphics = Graphics.Distinct().ToList();
            Colors = Colors.Distinct().ToList();
            Locations = Locations.Distinct().ToList();
            foreach (var e in Graphics.SelectMany(te => Colors.SelectMany(ce =>
                (from le in Locations
                 where Stealth.Client.FindTypeEx(te, ce, le, false) > 0
                 where (vlist = Stealth.Client.GetFindList()).Count > 0
                 from e in
                     vlist.Where(e => !(list.Contains(e)))
                 select e))))
                list.Add(e);

            var Results =
                list.Distinct().ToList().Select(s => Activator.CreateInstance(typeof(T), new Serial(s)) as T).ToList();

            #endregion

            return Results;
        }
        /// <summary>
        /// Generic Search by graphics and notorieties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Notorieties"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<ushort> Graphics, List<Notoriety> Notorieties) where T : UOEntity
        {
            HandleGarbage();

            #region Perform Query

            var list = new List<uint>();
            var vlist = new List<uint>();
            Notorieties = Notorieties.Distinct().ToList();
            Graphics = Graphics.Distinct().ToList();
            foreach (var e in from t in Graphics
                              from n in Notorieties
                              where Stealth.Client.FindNotoriety(t, (byte)n) > 0
                              where
                                  (vlist = Stealth.Client.GetFindList()).Count > 0
                              from e in vlist
                              where !list.Contains(e)
                              select e)
                list.Add(e);

            var Results =
                list.Distinct().ToList().Select(s => Activator.CreateInstance(typeof(T), new Serial(s)) as T).ToList();

            #endregion

            return Results;
        }
        public static List<T> Find<T>(string objType, Serial containerID = null) where T : UOEntity
        {
            return Find<T>(EasyUOHelper.ConvertToStealthType(objType), containerID);
        }

        public static List<T> Find<T>(ushort objType, Serial containerID = null) where T : UOEntity
        {
            HandleGarbage();
            uint contID = uint.MaxValue;
            if (containerID != null)
                contID = containerID.Value;
            #region Perform Query
            var res = new List<T>();
            if (Stealth.Client.FindType(objType, contID) < 1)
                return res;
            foreach (var i in Stealth.Client.GetFindList())
                res.Add(Activator.CreateInstance(typeof(T), new Serial(i)) as T);
            
            return res;
        }

        /// <summary>
        /// Generic Search by map locations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Points"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<Point2D> Points) where T : UOEntity
        {
            HandleGarbage();

            #region Perform Query

            var list = new List<uint>();
            var vlist = new List<uint>();
            Points = Points.Distinct().ToList();
            foreach (var e in from p in Points
                              where Stealth.Client.FindAtCoord((ushort)p.X, (ushort)p.Y) > 0
                              where (vlist = Stealth.Client.GetFindList()).Count > 0
                              from e in vlist
                              where !list.Contains(e)
                              select e)
                list.Add(e);
            var Results =
                list.Distinct().ToList().Select(s => Activator.CreateInstance(typeof(T), new Serial(s)) as T).ToList();

            #endregion

            return Results;
        }

        /// <summary>
        /// Generic search by custom attributes such as QueryTypeAttribute and QuerySearchAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Types"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<Type> Types, List<uint> Locations, bool SubSearch) where T : UOEntity
        {
            HandleGarbage();

            #region Perform Query

            var subsearchlist = new List<T>();
            var subsearchglist = new List<T>();

            foreach (var attrs in Types.Select(Attribute.GetCustomAttributes).Where(attrs => attrs.Length > 0))
            {
                subsearchlist.Clear();
                foreach (var e in attrs)
                {
                    if (e is QuerySearchAttribute)
                    {
                        var query = (QuerySearchAttribute)e;
                        subsearchlist = Find<T>(query.Graphics, query.Colors, Locations, SubSearch);

                        var flist =
                            subsearchlist.Where(i => ClilocHelper.ContainsAny(i.Properties, query.Labels)).ToList();

                        if (flist.Count > 0)
                            subsearchlist.AddRange(flist);
                    }
                    else if (e is QueryTypeAttribute)
                    {
                        var flist = Find<T>(((QueryTypeAttribute)e).Types, Locations, SubSearch);
                        if (flist.Count > 0)
                            subsearchlist.AddRange(flist);
                    }


                    if (subsearchlist.Count > 0)
                        subsearchglist.AddRange(subsearchlist);
                }
            }
            var SuperResult = subsearchglist.GroupBy(e => e.Serial.Value).Select(g => g.First()).ToList();

            #endregion

            return SuperResult;
        }

        #endregion

        #region Object Search

        /// <summary>
        /// Generic Search by graphics, colors, locations and allows recursive sub search in container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Colors"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<ushort> Graphics, List<ushort> Colors, uint Locations, bool SubSearch)
            where T : UOEntity
        {
            return Find<T>(Graphics, Colors, new List<uint> { Locations }, SubSearch);
        }
        /// <summary>
        /// Generic Search by graphics, colors, locations and allows recursive sub search in container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Colors"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<ushort> Graphics, ushort Colors, List<uint> Locations, bool SubSearch)
            where T : UOEntity
        {
            return Find<T>(Graphics, new List<ushort> { Colors }, Locations, SubSearch);
        }
        /// <summary>
        /// Generic Search by graphics, colors, locations and allows recursive sub search in container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Colors"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<ushort> Graphics, ushort Colors, uint Locations, bool SubSearch)
            where T : UOEntity
        {
            return Find<T>(Graphics, new List<ushort> { Colors }, new List<uint> { Locations }, SubSearch);
        }
        /// <summary>
        /// Generic Search by graphics, colors, locations and allows recursive sub search in container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Colors"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(ushort Graphics, List<ushort> Colors, uint Locations, bool SubSearch)
            where T : UOEntity
        {
            return Find<T>(new List<ushort> { Graphics }, Colors, new List<uint> { Locations }, SubSearch);
        }
        /// <summary>
        /// Generic Search by graphics, colors, locations and allows recursive sub search in container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Colors"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(ushort Graphics, ushort Colors, List<uint> Locations, bool SubSearch)
            where T : UOEntity
        {
            return Find<T>(new List<ushort> { Graphics }, new List<ushort> { Colors }, Locations, SubSearch);
        }
        /// <summary>
        /// Generic Search by graphics, colors, locations and allows recursive sub search in container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Colors"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(ushort Graphics, ushort Colors, uint Locations, bool SubSearch) where T : UOEntity
        {
            return Find<T>(new List<ushort> { Graphics }, new List<ushort> { Colors }, new List<uint> { Locations }, SubSearch);
        }

        #endregion

        #region Notoriety Search

        /// <summary>
        /// Generic Search by graphics and notorieties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Notorieties"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<ushort> Graphics, Notoriety Notorieties) where T : UOEntity
        {
            return Find<T>(Graphics, new List<Notoriety> { Notorieties });
        }
        /// <summary>
        /// Generic Search by graphics and notorieties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Notorieties"></param>
        /// <returns></returns>
        public static List<T> Find<T>(ushort Graphics, List<Notoriety> Notorieties) where T : UOEntity
        {
            return Find<T>(new List<ushort> { Graphics }, Notorieties);
        }

        /// <summary>
        /// Generic Search by graphics and notorieties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Graphics"></param>
        /// <param name="Notorieties"></param>
        /// <returns></returns>
        public static List<T> Find<T>(ushort Graphics, Notoriety Notorieties) where T : UOEntity
        {
            return Find<T>(new List<ushort> { Graphics }, new List<Notoriety> { Notorieties });
        }

        #endregion

        #region Position Scan
        /// <summary>
        /// Generic Search by map locations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Point"></param>
        /// <returns></returns>
        public static List<T> Find<T>(Point2D Point) where T : UOEntity
        {
            return Find<T>(new List<Point2D> { Point });
        }

        /// <summary>
        /// Generic Search by map locations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static List<T> Find<T>(int X, int Y) where T : UOEntity
        {
            return Find<T>(new Point2D(X, Y));
        }

        /// <summary>
        /// Generic Search by map locations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="MixedPattern"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<int> X, List<int> Y, bool MixedPattern) where T : UOEntity
        {
            return Find<T>((from xe in X from ye in Y select new Point2D(xe, ye)).ToList());
        }

        /// <summary>
        /// Generic Search by map locations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<T> Find<T>(Point2D start, Point2D end) where T : UOEntity
        {
            var xstart = start.X > end.X ? end.X : start.X;
            var xend = start.X > end.X ? start.X : end.X;
            var ystart = start.Y > end.Y ? end.Y : start.Y;
            var yend = start.Y > end.Y ? start.Y : end.Y;
            var list = new List<Point2D>();
            for (var x = xstart; x < xend + 1; x++)
                for (var y = ystart; y < yend + 1; y++)
                    list.Add(new Point2D(x, y));
            return Find<T>(list);
        }

        #endregion

        #region Type Search
        /// <summary>
        /// Generic search by custom attributes such as QueryTypeAttribute and QuerySearchAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Types"></param>
        /// <param name="Location"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<Type> Types, uint Location, bool SubSearch) where T : UOEntity
        {
            return Find<T>(Types, new List<uint> { Location }, SubSearch);
        }
        /// <summary>
        /// Generic search by custom attributes such as QueryTypeAttribute and QuerySearchAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Type"></param>
        /// <param name="Location"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(Type Type, uint Location, bool SubSearch) where T : UOEntity
        {
            return Find<T>(new List<Type> { Type }, new List<uint> { Location }, SubSearch);
        }
        /// <summary>
        /// Generic search by custom attributes such as QueryTypeAttribute and QuerySearchAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Type"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(Type Type, List<uint> Locations, bool SubSearch) where T : UOEntity
        {
            return Find<T>(new List<Type> { Type }, Locations, SubSearch);
        }
        /// <summary>
        /// Generic search by custom attributes such as QueryTypeAttribute and QuerySearchAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Location"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(uint Location, bool SubSearch) where T : UOEntity
        {
            return Find<T>(new List<Type> { typeof(T) }, new List<uint> { Location }, SubSearch);
        }
        /// <summary>
        /// Generic search by custom attributes such as QueryTypeAttribute and QuerySearchAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<T> Find<T>(List<uint> Locations, bool SubSearch) where T : UOEntity
        {
            return Find<T>(new List<Type> { typeof(T) }, Locations, SubSearch);
        }

        #endregion

        #region Ignore Objects
        /// <summary>
        /// Adds object Id to ignore list.
        /// </summary>
        /// <param name="ID"></param>
        public static void Ignore(uint ID)
        {
            Ignore(new List<uint> { ID });
        }

        /// <summary>
        /// Adds serial to ignore list.
        /// </summary>
        /// <param name="serial"></param>
        public static void Ignore(Serial serial)
        {
            Ignore(serial.Value);
        }

        /// <summary>
        /// Adds entity to ignore list.
        /// </summary>
        /// <param name="entity"></param>
        public static void Ignore(UOEntity entity)
        {
            Ignore(entity.Serial);
        }

        /// <summary>
        /// Adds multiple object ID´s to ignore list.
        /// </summary>
        /// <param name="list"></param>
        public static void Ignore(List<uint> list)
        {
            HandleGarbage();
            foreach (var e in list)
                Stealth.Client.Ignore(e);
#if DEBUG
            ScriptLogger.WriteLine(string.Format("Adding {0} Items to Client Cache", list.Count));
#endif
            if (AutoGCMode)
            {
                if (GCCache == null)
                    GCCache = new List<DateTime>();

                var now = DateTime.UtcNow;
#if DEBUG
                ScriptLogger.WriteLine(string.Format("{0} New Items Cached  => {1}", list.Count, now));
#endif
                GCCache.Add(now);
            }
        }

        /// <summary>
        /// Adds multiple serials to ignore list.
        /// </summary>
        /// <param name="list"></param>
        public static void Ignore(List<Serial> list)
        {
            Ignore((from e in list where e.Value > 0 select e.Value).ToList());
        }
        /// <summary>
        /// Adds multiple entities to ignore list.
        /// </summary>
        /// <param name="list"></param>
        public static void Ignore(List<UOEntity> list)
        {
            Ignore((from e in list where e.Serial.Value > 0 select e.Serial).ToList());
        }

        /// <summary>
        /// clears ignorelist.
        /// </summary>
        public static void ClearIgnoreList()
        {
            if (AutoGCMode)
            {
                if (GCCache == null)
                    GCCache = new List<DateTime>();

                if (GCCache.Count > 0)
                {
#if DEBUG
                    ScriptLogger.WriteLine(string.Format("Clearing {0} Items from Client Cache", GCCache.Count));
#endif
                }
            }

            Stealth.Client.IgnoreReset();
        }

        /// <summary>
        /// Removes object ID from ignore list.
        /// </summary>
        /// <param name="ID"></param>
        public static void IgnoreRemove(uint ID)
        {
            IgnoreRemove(new List<uint> { ID });
        }
        /// <summary>
        ///  Removes serial from ignore list.
        /// </summary>
        /// <param name="serial"></param>
        public static void IgnoreRemove(Serial serial)
        {
            IgnoreRemove(new List<Serial> { serial });
        }

        /// <summary>
        ///  Removes entity from ignore list.
        /// </summary>
        /// <param name="entity"></param>
        public static void IgnoreRemove(UOEntity entity)
        {
            IgnoreRemove(new List<UOEntity> { entity });
        }
        /// <summary>
        ///  Removes multiple entities from ignore list.
        /// </summary>
        /// <param name="list"></param>
        public static void IgnoreRemove(List<UOEntity> list)
        {
            IgnoreRemove((from e in list where e.Serial.Value > 0 select e.Serial).ToList());
        }
        /// <summary>
        ///  Removes serials from ignore list.
        /// </summary>
        /// <param name="list"></param>
        public static void IgnoreRemove(List<Serial> list)
        {
            IgnoreRemove((from e in list where e.Value > 0 select e.Value).ToList());
        }
        /// <summary>
        ///  Removes multiple object ID´s from ignore list.
        /// </summary>
        /// <param name="list"></param>
        public static void IgnoreRemove(List<uint> list)
        {
            if (HandleGarbage())
                return;

            foreach (var e in list)
                Stealth.Client.IgnoreOff(e);
#if DEBUG
            ScriptLogger.WriteLine(string.Format("Removing {0} Items From Cache", list.Count));
#endif
        }

        #endregion

        #region World
        /// <summary>
        /// Function returns all valid items in range.
        /// </summary>
        /// <returns></returns>
        public static List<Item> FindItems()
        {
            return
                Find<Item>(0xFFFF, 0xFFFF, 0xFFFF, true)
                    .FindAll(e => e.Serial.Value >= 0x40000000 && e.Serial.Value <= 0x7FFFFFFF);
        }

        /// <summary>
        /// Function returns all valid mobiles in range.
        /// </summary>
        /// <returns></returns>
        public static List<Mobile> FindMobiles()
        {
            return
                Find<Mobile>(0xFFFF, 0xFFFF, 0x0, true)
                    .FindAll(e => e.Serial.Value < 0x40000000);
        }

        #endregion
    }
}