#region References

using System;
using ScriptSDK.Data;
using StealthAPI;

#endregion

namespace ScriptSDK.Utils
{
    /// <summary>
    ///     Geometry class contains functions to handle geometrical tasks.
    /// </summary>
    public static class Geometry
    {
        /// <summary>
        ///     Function returns the squared distance between two points.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetDistanceToSqrt(this IPoint2D p1, IPoint2D p2)
        {
            var xDelta = p1.X - p2.X;
            var yDelta = p1.Y - p2.Y;

            return Math.Sqrt((xDelta*xDelta) + (yDelta*yDelta));
        }

        /// <summary>
        ///     Function returns if the two points are within passed range.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool InRange(this IPoint2D p1, IPoint2D p2, int range)
        {
            return (p1.X >= (p2.X - range))
                   && (p1.X <= (p2.X + range))
                   && (p1.Y >= (p2.Y - range))
                   && (p1.Y <= (p2.Y + range));
        }

        /// <summary>
        ///     Function returns if the two points are within the packet update range of 18.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool InUpdateRange(this IPoint2D p1, IPoint2D p2)
        {
            return InRange(p1, p2, 18);
        }

        /// <summary>
        ///     Extension returns direction object views towards passed point.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Direction GetDirectionTo(this IPoint2D p, int x, int y)
        {
            var dx = p.X - x;
            var dy = p.Y - y;

            var rx = (dx - dy)*44;
            var ry = (dx + dy)*44;

            var ax = Math.Abs(rx);
            var ay = Math.Abs(ry);

            Direction ret;

            if (((ay >> 1) - ax) >= 0)
                ret = (ry > 0) ? Direction.Up : Direction.Down;
            else if (((ax >> 1) - ay) >= 0)
                ret = (rx > 0) ? Direction.Left : Direction.Right;
            else if (rx >= 0 && ry >= 0)
                ret = Direction.West;
            else if (rx >= 0 && ry < 0)
                ret = Direction.South;
            else if (rx < 0 && ry < 0)
                ret = Direction.East;
            else
                ret = Direction.North;

            return ret;
        }

        /// <summary>
        ///     Extension returns direction object views towards passed point.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Direction GetDirectionTo(this IPoint2D p1, IPoint2D p2)
        {
            return p1.GetDirectionTo(p2.X, p2.Y);
        }

        /// <summary>
        ///     Function allows to parse coords to a specific point. This use the Runuo default acurate handling.
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Point3D CoordsToPoint(string coords, Map map)
        {
            var xEast = coords.Contains("E");
            var ySouth = coords.Contains("S");

            coords = coords.Replace((xEast ? 'E' : 'W'), ' ');
            coords = coords.Replace((char) 39, ' ');
            coords = coords.Trim();
            var a = coords.Split(ySouth ? 'S' : 'N');

            if (a.Length.Equals(2))
            {
                var vlat = a[0].Split('°');
                var vlong = a[1].Split('°');

                int yLat;
                int.TryParse(vlat[0].Trim(), out yLat);

                int yMins;
                int.TryParse(vlat[1].Trim(), out yMins);

                int xLong;
                int.TryParse(vlong[0].Trim(), out xLong);

                int xMins;
                int.TryParse(vlong[1].Trim(), out xMins);

                var p = ReverseLookup(map, xLong, yLat, xMins, yMins, xEast, ySouth);

                return p;
            }
            return new Point3D();
        }

        /// <summary>
        ///     Function allows to parse point to coords. This use the Runuo default acurate handling.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static string PointToCoords(Point3D point, Map map)
        {
            if (map == Map.Internal)
                return string.Empty; //return false;

            var x = point.X;
            var y = point.Y;
            int xCenter, yCenter;
            int xWidth, yHeight;

            if (!ComputeMapDetails(map, x, y, out xCenter, out yCenter, out xWidth, out yHeight))
                return string.Empty; //return false;

            var absLong = (double) ((x - xCenter)*360)/xWidth;
            var absLat = (double) ((y - yCenter)*360)/yHeight;

            if (absLong > 180.0)
                absLong = -180.0 + (absLong%180.0);

            if (absLat > 180.0)
                absLat = -180.0 + (absLat%180.0);

            bool east = (absLong >= 0), south = (absLat >= 0);

            if (absLong < 0.0)
                absLong = -absLong;

            if (absLat < 0.0)
                absLat = -absLat;

            var xLong = (int) absLong;
            var yLat = (int) absLat;

            var xMins = (int) ((absLong%1.0)*60);
            var yMins = (int) ((absLat%1.0)*60);

            var xEast = east;
            var ySouth = south;

            var location = string.Format("{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins,
                xEast ? "E" : "W");

            return location;
        }

        private static bool ComputeMapDetails(Map map, int x, int y, out int xCenter, out int yCenter, out int xWidth,
            out int yHeight)
        {
            xWidth = 5120;
            yHeight = 4096;

            if (map == Map.Trammel || map == Map.Felucca)
            {
                if (x >= 0 && y >= 0 && x < 5120 && y < 4096)
                {
                    xCenter = 1323;
                    yCenter = 1624;
                    return true;
                }
                if (x >= 5120 && y >= 2304 && x < 6144 && y < 4096)
                {
                    xCenter = 5936;
                    yCenter = 3112;
                    return true;
                }
            }
            xCenter = 1323;
            yCenter = 1624;
            return true;
        }

        private static Point3D ReverseLookup(Map map, int xLong, int yLat, int xMins, int yMins, bool xEast, bool ySouth)
        {
            if (map == Map.Internal)
                return Point3D.Zero;

            int xCenter, yCenter;
            int xWidth, yHeight;

            if (!ComputeMapDetails(map, 0, 0, out xCenter, out yCenter, out xWidth, out yHeight))
                return Point3D.Zero;

            var absLong = xLong + ((double) xMins/60);
            var absLat = yLat + ((double) yMins/60);

            if (!xEast)
                absLong = 360.0 - absLong;

            if (!ySouth)
                absLat = 360.0 - absLat;

            var x = xCenter + (int) ((absLong*xWidth)/360);
            var y = yCenter + (int) ((absLat*yHeight)/360);

            if (x < 0)
                x += xWidth;
            else if (x >= xWidth)
                x -= xWidth;

            if (y < 0)
                y += yHeight;
            else if (y >= yHeight)
                y -= yHeight;

            int z = Stealth.Client.GetSurfaceZ((ushort) x, (ushort) y, Stealth.Client.GetWorldNum());

            return new Point3D(x, y, z);
        }
    }
}