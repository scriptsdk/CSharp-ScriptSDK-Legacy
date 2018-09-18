using ScriptSDK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptSDK
{


    public class Pathfinder
    {
        public static List<Point3D> FindPath(Point3D start, Point3D dest, int accuracy = 0)
        {
            /*Bitmap bmp = new Bitmap(6128, 4096);
            for (int x = 1000; x < 1750; x++)
            {
                for (int y = 1555; y < 2444; y++)
                {
                    var tile = new Vector3(x, y);
                    bmp.SetPixel(x, y, tile.IsPassable() ? Color.Green : Color.Red);
                }
            }*/


            //Bitmap bmp = new Bitmap(6128, 4096);
            //todo weight less for diagonal
            // check diagonal move allowed.
            //var closedSet = new Vector3[6128,4096];
            var ClosedSet = new List<Point3D>();
            var OpenSet = new List<Point3D>();
            OpenSet.Add(start);

            Point3D curNode = null;
            int cnt = 0;
            while (OpenSet.Count > 0)
            {

                curNode = OpenSet.First();
                OpenSet.RemoveAt(0);
                ClosedSet.Add(curNode);
                //  bmp.SetPixel(curNode.X, curNode.Y, Color.Blue);
                if (curNode.Equals(dest))
                    break;
                var neighbours = GetNeighbours(curNode, dest);
                foreach (var n in neighbours)
                {

                    if (accuracy > 0)
                        if (n.Equals(dest))
                        {
                            OpenSet.Clear();
                            break;
                        }
                    //  closedSet[n.X, n.Y] == null
                    if (!n.IsPassable())
                        continue;
                    if (OpenSet.IndexOf(n) != -1)
                    {
                        var existing = OpenSet[OpenSet.IndexOf(n)];
                        if (existing.V > n.V)
                        {
                            OpenSet.Remove(existing);
                        }
                    }
                    if (ClosedSet.IndexOf(n) != -1)
                    {
                        var existing = ClosedSet[ClosedSet.IndexOf(n)];
                        if (existing.V > n.V)
                        {
                            ClosedSet.Remove(existing);
                        }
                    }

                    if (!ClosedSet.Contains(n) && !OpenSet.Contains(n) && n.IsPassable())
                    {
                        OpenSet.Add(n);
                    }
                }
                OpenSet.Sort();
                //closedSet[curNode.X, curNode.Y] = curNode;

                // if (ClosedSet.Count > 50000)
                //    return null;
                cnt++;
                //if (OpenSet.Count > 1000)
                //    OpenSet.RemoveRange(500, 1000);
                //bmp.Save("test.png", ImageFormat.Png);
                if (cnt > 25000)
                {
                    //    bmp.Save("test.png", ImageFormat.Png);
                    return null;
                }

            }


            var resultPath = new List<Point3D>();
            //curnode is Start
            //Bitmap bmp = new Bitmap(4096, 4096);
            while (curNode != null)
            {
                //  bmp.SetPixel(curNode.X, curNode.Y, Color.GhostWhite);
                resultPath.Add(new Point3D(curNode.X, curNode.Y));
                curNode = curNode.Parent;
            }
            // bmp.Save("test.png", ImageFormat.Png);
            //bmp.Save("path.png", ImageFormat.Png);
            resultPath.Reverse();
            return resultPath;

        }

        private static List<Point3D> GetNeighbours(Point3D curNode, Point3D dest)
        {
            var results = new List<Point3D>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    //if (Math.Abs(x) == Math.Abs(y))
                    //   continue;
                    //var heuristc = Tools.Get2DDistance(curNode.X + x, curNode.Y + y, dest.X, dest.Y);
                    var vec = new Point3D(curNode.X + x, curNode.Y + y) { Parent = curNode };

                    var h = diagonalDist(vec, dest) * 10;
                    int g = 5;
                    g = vec.ModifyG(g);
                    //if (Math.Abs(x) == Math.Abs(y))
                    //    g = 5;
                    vec.G = curNode.G + g;
                    vec.H = (int)h;

                    results.Add(vec);
                }
            }
            return results;
        }


        private static double diagonalDist(Point3D start, Point3D dest)
        {
            int dx = Math.Abs(start.X - dest.X);
            int dy = Math.Abs(start.Y - dest.Y);
            return 1 * (dx + dy) + (Math.Sqrt(2) - 2 * 1) * Math.Min(dx, dy);
        }
    }
    public static class GumpTypes
    {
        public static readonly string BANK_MENU_GUMP_TEXT = "Santiago Bank Menu";

    }
    public static class ItemTypes
    {
        public static readonly string AxeDouble = "NSF";
        public static readonly string PickAxe = "NPF";
        public static readonly string Forge = "JBG";
        public static readonly string Ore_Large = "DWJ";
        public static readonly string Ore_Small = "TVJ";
        public static readonly string Ore_SmallMed = "GWJ";
        public static readonly string Ore_Med = "EWJ";
        public static readonly string[] Ores = { "DWJ", "TVJ", "GWJ", "EWJ" };
        public static readonly string Tinker_Tool = "GTL";
        public static readonly string IngotIron = "RMK";
        public static readonly string IngotCopper = "NMK";
        public static readonly string IngotSilver = "XMK";
        public static readonly string IngotGold = "TMK";
        public static readonly string[] Ingots = { "RMK", "TMK", "XMK", "NMK" };
        public static readonly string[] MiningBankables = { "RMK", "TMK", "XMK", "NMK", "DWJ", "TVJ", "GWJ", "EWJ","ZLK" };
        public static string Hatchet = "FSF";
        public static string Logs = "ZLK";
        public static string BlackPearl = "KUF";
        public static string MandrakeRoot = "MZF";
        public static string BloodMoss = "JUF";
        public static string Map = "XVH";
        public static string Saw = "EGG";
        public static string Bottles = "WUF";
        public static readonly string NightShade = "WZF";
        public static readonly string Garlic = "KZF";
        public static readonly string Ginseng = "JZF";
        public static readonly string BlankScrolls = "MMF";
        public static readonly string Raw_Fish_Steak = "IND";
        public static readonly string Fish_Steak = "HND";
        public static readonly string SulfAsh = "SZF";
        public static readonly string ClockParts = "FCG";
        public static readonly string SpiderSilk = "RZF";
        public static readonly string Bandages = "ZLF";
        public static readonly string Dagger = "TSF";
        public static readonly string RecallScrolls ="WTL" ;
        public static readonly string EyeOfNewt = "LZF";
        public static readonly string WyrmHeart = "DAG";

    }
}
