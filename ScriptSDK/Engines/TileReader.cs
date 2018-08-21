using System;
using System.Collections.Generic;
using System.Linq;
using ScriptSDK.Data;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// Tilereader class is a very extended class allowing to read and handle actions towards tiles.
    /// </summary>
    public static class TileReader
    {
        static TileReader()
        {
            Initialize();
        }
        /// <summary>
        /// This function should be always called once before handling the tilereader.
        /// Not required to be called when SDK.Initialize has been called before.
        /// </summary>
        public static void Initialize()
        {
            TreeTiles = new List<ushort>
            {
                3274,
                3275,
                3277,
                3280,
                3283,
                3286,
                3288,
                3290,
                3293,
                3296,
                3299,
                3302,
                3320,
                3323,
                3326,
                3329,
                3393,
                3394,
                3395,
                3396,
                3415,
                3416,
                3417,
                3418,
                3419,
                3438,
                3439,
                3440,
                3441,
                3442,
                3460,
                3461,
                3462,
                3476,
                3478,
                3480,
                3482,
                3484,
                3492,
                3496
            };
            MountainTiles = new List<ushort>
            {
                220,
                221,
                222,
                223,
                224,
                225,
                226,
                227,
                228,
                229,
                230,
                231,
                236,
                237,
                238,
                239,
                240,
                241,
                242,
                243,
                244,
                245,
                246,
                247,
                252,
                253,
                254,
                255,
                256,
                257,
                258,
                259,
                260,
                261,
                262,
                263,
                268,
                269,
                270,
                271,
                272,
                273,
                274,
                275,
                276,
                277,
                278,
                279,
                286,
                287,
                288,
                289,
                290,
                291,
                292,
                293,
                294,
                296,
                296,
                297,
                321,
                322,
                323,
                324,
                467,
                468,
                469,
                470,
                471,
                472,
                473,
                474,
                476,
                477,
                478,
                479,
                480,
                481,
                482,
                483,
                484,
                485,
                486,
                487,
                492,
                493,
                494,
                495,
                543,
                544,
                545,
                546,
                547,
                548,
                549,
                550,
                551,
                552,
                553,
                554,
                555,
                556,
                557,
                558,
                559,
                560,
                561,
                562,
                563,
                564,
                565,
                566,
                567,
                568,
                569,
                570,
                571,
                572,
                573,
                574,
                575,
                576,
                577,
                578,
                579,
                581,
                582,
                583,
                584,
                585,
                586,
                587,
                588,
                589,
                590,
                591,
                592,
                593,
                594,
                595,
                596,
                597,
                598,
                599,
                600,
                601,
                610,
                611,
                612,
                613,
                1010,
                1741,
                1742,
                1743,
                1744,
                1745,
                1746,
                1747,
                1748,
                1749,
                1750,
                1751,
                1752,
                1753,
                1754,
                1755,
                1756,
                1757,
                1771,
                1772,
                1773,
                1774,
                1775,
                1776,
                1777,
                1778,
                1779,
                1780,
                1781,
                1782,
                1783,
                1784,
                1785,
                1786,
                1787,
                1788,
                1789,
                1790,
                1801,
                1802,
                1803,
                1804,
                1805,
                1806,
                1807,
                1808,
                1809,
                1811,
                1812,
                1813,
                1814,
                1815,
                1816,
                1817,
                1818,
                1819,
                1820,
                1821,
                1822,
                1823,
                1824,
                1831,
                1832,
                1833,
                1834,
                1835,
                1836,
                1837,
                1838,
                1839,
                1840,
                1841,
                1842,
                1843,
                1844,
                1845,
                1846,
                1847,
                1848,
                1849,
                1850,
                1851,
                1852,
                1853,
                1854,
                1861,
                1862,
                1863,
                1864,
                1865,
                1866,
                1867,
                1868,
                1869,
                1870,
                1871,
                1872,
                1873,
                1874,
                1875,
                1876,
                1877,
                1878,
                1879,
                1880,
                1881,
                1882,
                1883,
                1884,
                1981,
                1982,
                1983,
                1984,
                1985,
                1986,
                1987,
                1988,
                1989,
                1990,
                1991,
                1992,
                1993,
                1994,
                1995,
                1996,
                1997,
                1998,
                1999,
                2000,
                2001,
                2002,
                2003,
                2004,
                2028,
                2029,
                2030,
                2031,
                2032,
                2033,
                2100,
                2101,
                2102,
                2103,
                2104,
                2105,
                17723,
                17724,
                17725,
                17726,
                17727,
                17728,
                17729,
                17730,
                17731,
                17732,
                17733,
                17734,
                17735,
                17736,
                17737,
                17738,
                17739,
                17740,
                17741,
                17742,
                17743
            };


            CaveTiles = new List<ushort>
            {
                1339,
                1340,
                1341,
                1342,
                1343,
                1344,
                1345,
                1346,
                1347,
                1348,
                1349,
                1350,
                1351,
                1352,
                1353,
                1354,
                1355,
                1356,
                1357,
                1358,
                1359
            };

            SandTiles = new List<ushort>
            {
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
                32,
                33,
                34,
                35,
                36,
                37,
                38,
                39,
                40,
                41,
                42,
                43,
                44,
                45,
                46,
                47,
                48,
                49,
                50,
                51,
                52,
                53,
                54,
                55,
                56,
                57,
                58,
                59,
                60,
                61,
                62,
                68,
                69,
                70,
                71,
                72,
                73,
                74,
                75,
                286,
                287,
                288,
                289,
                290,
                291,
                292,
                293,
                294,
                295,
                296,
                297,
                298,
                299,
                300,
                301,
                402,
                424,
                425,
                426,
                427,
                441,
                442,
                443,
                444,
                445,
                446,
                447,
                448,
                449,
                450,
                451,
                452,
                453,
                454,
                455,
                456,
                457,
                458,
                459,
                460,
                461,
                462,
                463,
                464,
                465,
                642,
                643,
                644,
                645,
                650,
                651,
                652,
                653,
                654,
                655,
                656,
                657,
                821,
                822,
                823,
                824,
                825,
                826,
                827,
                828,
                833,
                834,
                835,
                836,
                845,
                846,
                847,
                848,
                849,
                850,
                851,
                852,
                857,
                858,
                859,
                860,
                951,
                952,
                953,
                954,
                955,
                956,
                957,
                958,
                967,
                968,
                969,
                970,
                1447,
                1448,
                1449,
                1450,
                1451,
                1452,
                1453,
                1454,
                1455,
                1456,
                1457,
                1458,
                1611,
                1612,
                1613,
                1614,
                1615,
                1616,
                1617,
                1618,
                1623,
                1624,
                1625,
                1626,
                1635,
                1636,
                1637,
                1638,
                1639,
                1640,
                1641,
                1642,
                1647,
                1648,
                1649,
                1650
            };
            StealthOffset = 256;
        }

        /// <summary>
        /// Stores a list of valid tree tiles. Preset via Initialize().
        /// </summary>
        public static List<ushort> TreeTiles { get; set; }
        /// <summary>
        ///  Stores a list of valid mountain tiles. Preset via Initialize().
        /// </summary>
        public static List<ushort> MountainTiles { get; set; }
        /// <summary>
        ///  Stores a list of valid cave tiles. Preset via Initialize().
        /// </summary>
        public static List<ushort> CaveTiles { get; set; }
        /// <summary>
        ///  Stores a list of valid sand tiles. Preset via Initialize().
        /// </summary>
        public static List<ushort> SandTiles { get; set; }

        /// <summary>
        /// This is an offset the developer can set when tiles arent found. By some reasons 
        /// It seems some shards having an offset which can differ. Preset use the base of ServUO\JustUO 
        /// core. May differ on other servers.
        /// </summary>
        public static int StealthOffset { get; set; }

        /// <summary>
        /// Function try to parse useable locations for mining around a distance from player.
        /// </summary>
        /// <param name="Distance"></param>
        /// <returns></returns>
        public static Dictionary<HarvestType, List<StaticItemRealXY>> GetMiningSpots(int Distance)
        {
            var loc = PlayerMobile.GetPlayer().Location;
            return GetMiningSpots(new Point2D(loc.X - Distance, loc.Y - Distance),
                new Point2D(loc.X + Distance, loc.Y + Distance));
        }
        /// <summary>
        /// Function try to parse useable locations for mining on a certain area.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<StaticItemRealXY> GetOreSpots(Point2D start, Point2D end)
        {
            var px = start.X >= end.X ? new Point2D(end.X, start.X) : new Point2D(start.X, end.X);
            var py = start.Y >= end.Y ? new Point2D(end.Y, start.Y) : new Point2D(start.Y, end.Y);
            var w = Stealth.Client.GetWorldNum();

            //assumes fel for now


            var list = new List<StaticItemRealXY>();

            for (var x = px.X; x < px.Y + 1; x++)
            {
                for (var y = py.X; y < py.Y + 1; y++)
                {
                    var tiles = Ultima.Map.Felucca.Tiles.GetStaticTiles(x, y);
                    if (tiles.Count(t => CaveTiles.Contains(t.ID)) == 0)
                        continue;
                    var tree = tiles.FirstOrDefault(t => CaveTiles.Contains(t.ID));
                    list.Add(new StaticItemRealXY() { X = (ushort)x, Y = (ushort)y, Z = (byte)tree.Z, Tile = tree.ID });
                    var info = ReadStaticsXY((ushort)x, (ushort)y, w);
                    list.AddRange(info.Where(e => CaveTiles.Contains(e.Tile)));
                }
            }

            return list;
        }
        /// <summary>
        /// Function try to parse useable locations for mining on a certain area.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Dictionary<HarvestType, List<StaticItemRealXY>> GetMiningSpots(Point2D start, Point2D end)
        {
            var px = start.X >= end.X ? new Point2D(end.X, start.X) : new Point2D(start.X, end.X);
            var py = start.Y >= end.Y ? new Point2D(end.Y, start.Y) : new Point2D(start.Y, end.Y);
            var w = Stealth.Client.GetWorldNum();
            var id = Stealth.Client.GetSelfID();
            var list = new Dictionary<HarvestType, List<StaticItemRealXY>>
            {
                {HarvestType.Mining_Mountain, new List<StaticItemRealXY>()},
                {HarvestType.Mining_Cave, new List<StaticItemRealXY>()},
                {HarvestType.Mining_Sand, new List<StaticItemRealXY>()}
            };

            var z = Stealth.Client.GetZ(id);

            for (var x = px.X; x < px.Y + 1; x++)
            {
                for (var y = py.X; y < py.Y + 1; y++)
                {
                    /*Mountain*/
                    var mc = GetCell((ushort) x, (ushort) y, w);

                    if ((MountainTiles.Contains(mc.Tile)) && (Math.Abs(z - (mc.Z - StealthOffset)) < 15))
                    {
                        var obj = new StaticItemRealXY
                        {
                            X = (ushort) x,
                            Y = (ushort) y,
                            Z = mc.Z,
                            Color = 0,
                            Tile = mc.Tile
                        };
                        list[HarvestType.Mining_Mountain].Add(obj);
                    }

                    /*Sand*/
                    if ((SandTiles.Contains(mc.Tile)) && (Math.Abs(z - (mc.Z)) < 15))
                    {
                        var obj = new StaticItemRealXY
                        {
                            X = (ushort) x,
                            Y = (ushort) y,
                            Z = mc.Z,
                            Color = 0,
                            Tile = mc.Tile
                        };
                        list[HarvestType.Mining_Sand].Add(obj);
                    }
                    /*Cave*/
                    var sc = ReadStaticsXY((ushort) x, (ushort) y, w);
                    if (sc.Count > 0)
                    {
                        foreach (var e in sc.Where(e => CaveTiles.Contains(e.Tile)))
                        {
                            list[HarvestType.Mining_Cave].Add(e);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Function try to parse useable locations for lumberjacking around a range from player.
        /// </summary>
        /// <param name="Distance"></param>
        /// <returns></returns>
        public static List<StaticItemRealXY> GetLumberSpots(int Distance)
        {
            var loc = PlayerMobile.GetPlayer().Location;
            return GetLumberSpots(new Point2D(loc.X - Distance, loc.Y - Distance),
                new Point2D(loc.X + Distance, loc.Y + Distance));
        }

        /// <summary>
        /// Function try to parse useable locations for mining on a certain area.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<StaticItemRealXY> GetLumberSpots(Point2D start, Point2D end)
        {
            var px = start.X >= end.X ? new Point2D(end.X, start.X) : new Point2D(start.X, end.X);
            var py = start.Y >= end.Y ? new Point2D(end.Y, start.Y) : new Point2D(start.Y, end.Y);
            var w = Stealth.Client.GetWorldNum();

            //assumes fel for now
            

            var list = new List<StaticItemRealXY>();

            for (var x = px.X; x < px.Y + 1; x++)
            {
                for (var y = py.X; y < py.Y + 1; y++)
                {
                    var tiles = Ultima.Map.Felucca.Tiles.GetStaticTiles(x, y);
                    if (tiles.Count(t => TreeTiles.Contains(t.ID)) == 0)
                        continue;
                    var tree = tiles.FirstOrDefault(t => TreeTiles.Contains(t.ID));
                    list.Add(new StaticItemRealXY() { X = (ushort)x, Y = (ushort)y, Z = (byte)tree.Z, Tile = tree.ID });
                    var info = ReadStaticsXY((ushort) x, (ushort) y, w);
                    list.AddRange(info.Where(e => TreeTiles.Contains(e.Tile)));
                }
            }

            return list;
        }

        /// <summary>
        /// Reads Tileflags from specific tile on uo data.
        /// </summary>
        /// <param name="tileGroup"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static uint GetTileFlags(TileFlagsType tileGroup, ushort tile)
        {
            return Stealth.Client.GetTileFlags(tileGroup, tile);
        }

        /// <summary>
        /// Reads Tileflags from specific tile on uo data and converts them.
        /// </summary>
        /// <param name="tileGroup"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static TileDataFlags ConvertFlagsToFlagSet(TileFlagsType tileGroup, ushort tile)
        {
            return Stealth.Client.ConvertFlagsToFlagSet(tileGroup, tile);
        }

        /// <summary>
        /// Allows to read Landtiledata from uo data through passed tile.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static LandTileData GetLandTileData(ushort tile)
        {
            return Stealth.Client.GetLandTileData(tile);
        }

        /// <summary>
        /// Allows to read static tile data from uo data through passed tile.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static StaticTileData GetStaticTileData(ushort tile)
        {
            return Stealth.Client.GetStaticTileData(tile);
        }

        /// <summary>
        /// Reads static tile data on passed area. Requires to parse uo data (UOP or MUL).
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="tileType"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<FoundTile> GetStaticTilesArray(ushort xMin, ushort yMin, ushort xMax, ushort yMax,ushort tileType, Map map)
        {
            return GetStaticTilesArray(xMin, yMin, xMax, yMax, (byte) map, tileType);
        }

        /// <summary>
        ///Reads static tile data on passed area. Requires to parse uo data (UOP or MUL).
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="worldnum"></param>
        /// <param name="tileType"></param>
        /// <returns></returns>
        public static List<FoundTile> GetStaticTilesArray(ushort xMin, ushort yMin, ushort xMax, ushort yMax,byte worldnum, ushort tileType)
        {
            return Stealth.Client.GetStaticTilesArray(xMin, yMin, xMax, yMax, worldnum, tileType);
        }

        /// <summary>
        /// Reads land static data on passed area. Requires to parse uo data (UOP or MUL).
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="tileType"></param>
        /// <returns></returns>
        public static List<FoundTile> GetStaticTilesArray(ushort xMin, ushort yMin, ushort xMax, ushort yMax, ushort tileType)
        {
            return GetStaticTilesArray(xMin, yMin, xMax, yMax, Stealth.Client.GetWorldNum(), tileType);
        }

        /// <summary>
        /// Reads land tile data on passed area. Requires to parse uo data (UOP or MUL).
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="tileType"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<FoundTile> GetLandTilesArray(ushort xMin, ushort yMin, ushort xMax, ushort yMax,
            ushort tileType, Map map)
        {
            return GetLandTilesArray(xMin, yMin, xMax, yMax, tileType, (byte) map);
        }

        /// <summary>
        /// Reads land tile data on passed area. Requires to parse uo data (UOP or MUL).
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="tileType"></param>
        /// <param name="worldnum"></param>
        /// <returns></returns>
        public static List<FoundTile> GetLandTilesArray(ushort xMin, ushort yMin, ushort xMax, ushort yMax,
            ushort tileType, byte worldnum)
        {
            return Stealth.Client.GetLandTilesArray(xMin, yMin, xMax, yMax, worldnum, tileType);
        }

        /// <summary>
        /// Reads land tile data on passed area. Requires to parse uo data (UOP or MUL).
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="tileType"></param>
        /// <returns></returns>
        public static List<FoundTile> GetLandTilesArray(ushort xMin, ushort yMin, ushort xMax, ushort yMax,
            ushort tileType)
        {
            return GetLandTilesArray(xMin, yMin, xMax, yMax, tileType, Stealth.Client.GetWorldNum());
        }

        /// <summary>
        /// Reads MapCell from specific map point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="worldnum"></param>
        /// <returns></returns>
        public static MapCell GetCell(ushort x, ushort y, byte worldnum)
        {
            return Stealth.Client.GetCell(x, y, Stealth.Client.GetWorldNum());
        }

        /// <summary> 
        /// Reads MapCell from specific map point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static MapCell GetCell(ushort x, ushort y, Map map)
        {
            return GetCell(x, y, (byte) map);
        }

        /// <summary> 
        /// Reads MapCell from specific map point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static MapCell GetCell(ushort x, ushort y)
        {
            return GetCell(x, y, Stealth.Client.GetWorldNum());
        }

        /// <summary>
        /// Returns amount of layers on specific map point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static byte GetLayerCount(ushort x, ushort y, Map map)
        {
            return GetLayerCount(x, y, (byte) map);
        }

        /// <summary>
        /// Returns amount of layers on specific map point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="worldnum"></param>
        /// <returns></returns>
        public static byte GetLayerCount(ushort x, ushort y, byte worldnum)
        {
            return Stealth.Client.GetLayerCount(x, y, worldnum);
        }

        /// <summary>
        /// Returns amount of layers on specific map point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static byte GetLayerCount(ushort x, ushort y)
        {
            return Stealth.Client.GetLayerCount(x, y, Stealth.Client.GetWorldNum());
        }

        /// <summary>
        /// Allows to read static item on x/y-axis.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static List<StaticItemRealXY> ReadStaticsXY(ushort x, ushort y)
        {
            return ReadStaticsXY(x, y, Stealth.Client.GetWorldNum());
        }

        /// <summary>
        /// Allows to read static item on x/y-axis.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="worldnum"></param>
        /// <returns></returns>
        public static List<StaticItemRealXY> ReadStaticsXY(ushort x, ushort y, byte worldnum)
        {
            return Stealth.Client.ReadStaticsXY(x, y, worldnum);
        }

        /// <summary>
        /// Allows to read static item on x/y-axis.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<StaticItemRealXY> ReadStaticsXY(ushort x, ushort y, Map map)
        {
            return ReadStaticsXY(x, y, (byte) map);
        }

        /// <summary>
        /// Reads the surfaced z from destined point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static sbyte GetSurfaceZ(ushort x, ushort y)
        {
            return GetSurfaceZ(x, y, Stealth.Client.GetWorldNum());
        }

        /// <summary>
        /// Reads the surfaced z from destined point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="worldnum"></param>
        /// <returns></returns>
        public static sbyte GetSurfaceZ(ushort x, ushort y, byte worldnum)
        {
            return Stealth.Client.GetSurfaceZ(x, y, worldnum);
        }

        /// <summary>
        /// Reads the surfaced z from destined point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static sbyte GetSurfaceZ(ushort x, ushort y, Map map)
        {
            return GetSurfaceZ(x, y, (byte) map);
        }

        /// <summary>
        /// Function returns if destined cell is passable. I have to guess because there is no proper documentation about this code.
        /// I think this function is something like a line of sight check from your location to next location.
        /// </summary>
        /// <param name="currX"></param>
        /// <param name="currY"></param>
        /// <param name="currZ"></param>
        /// <param name="destX"></param>
        /// <param name="destY"></param>
        /// <param name="destZ"></param>
        /// <returns></returns>
        public static bool IsWorldCellPassable(ushort currX, ushort currY, sbyte currZ, ushort destX, ushort destY,
            out sbyte destZ)
        {
            return IsWorldCellPassable(currX, currY, currZ, destX, destY, out destZ, Stealth.Client.GetWorldNum());
        }

        /// <summary>
        /// Function returns if destined cell is passable. I have to guess because there is no proper documentation about this code.
        /// I think this function is something like a line of sight check from your location to next location.
        /// </summary>
        /// <param name="currX"></param>
        /// <param name="currY"></param>
        /// <param name="currZ"></param>
        /// <param name="destX"></param>
        /// <param name="destY"></param>
        /// <param name="destZ"></param>
        /// <param name="WorldNum"></param>
        /// <returns></returns>
        public static bool IsWorldCellPassable(ushort currX, ushort currY, sbyte currZ, ushort destX, ushort destY,
            out sbyte destZ, byte WorldNum)
        {
            return Stealth.Client.IsWorldCellPassable(currX, currY, currZ, destX, destY, out destZ, WorldNum);
        }

        /// <summary>
        /// Function returns if destined cell is passable. I have to guess because there is no proper documentation about this code.
        /// I think this function is something like a line of sight check from your location to next location.
        /// </summary>
        /// <param name="currX"></param>
        /// <param name="currY"></param>
        /// <param name="currZ"></param>
        /// <param name="destX"></param>
        /// <param name="destY"></param>
        /// <param name="destZ"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static bool IsWorldCellPassable(ushort currX, ushort currY, sbyte currZ, ushort destX, ushort destY,
            out sbyte destZ, Map map)
        {
            return IsWorldCellPassable(currX, currY, currZ, destX, destY, out destZ, (byte) map);
        }
    }
}