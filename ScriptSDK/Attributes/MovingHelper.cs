using System.Collections.Generic;
using ScriptSDK.Data;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Moving helper class expose handles, actions and properties, wich allow a player to pathfind around the world.
    /// </summary>
    public class MovingHelper
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly PlayerMobile _Owner;

        private MovingHelper(PlayerMobile owner)
        {
            _Owner = owner;
        }

        private static MovingHelper _instance { get; set; }

        /// <summary>
        /// Gets or Sets if player should move over NPC. Value describes the amount of required stamina.
        /// </summary>
        public int MoveThroughNPC
        {
            get { return Stealth.Client.GetMoveThroughNPC(); }
            set { Stealth.Client.SetMoveThroughNPC((short) value); }
        }

        /// <summary>
        /// Enable or disable if player automaticly should open doors.
        /// </summary>
        public bool AutoOpenDoors
        {
            get { return Stealth.Client.GetMoveOpenDoor(); }
            set { Stealth.Client.SetMoveOpenDoor(value); }
        }

        /// <summary>
        /// Returns instance of pathfinding engine.
        /// </summary>
        /// <returns></returns>
        public static MovingHelper GetMovingHelper()
        {
            return _instance ?? (_instance = new MovingHelper(PlayerMobile.GetPlayer()));
        }

        /// <summary>
        /// Generates a path array about waypoint from player to destination.
        /// </summary>
        /// <param name="destX"></param>
        /// <param name="destY"></param>
        /// <param name="optimized"></param>
        /// <param name="accuracy"></param>
        /// <returns></returns>
        public List<MyPoint> GetPathArray(ushort destX, ushort destY, bool optimized, int accuracy)
        {
            return Stealth.Client.GetPathArray(destX, destY, optimized, accuracy);
        }
        /// <summary>
        /// Generates a path array about waypoint from start to finish.
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="startZ"></param>
        /// <param name="finishX"></param>
        /// <param name="finishY"></param>
        /// <param name="finishZ"></param>
        /// <param name="worldNum"></param>
        /// <param name="accuracyXY"></param>
        /// <param name="accuracyZ"></param>
        /// <param name="run"></param>
        /// <returns></returns>
        public List<MyPoint> GetPathArray3D(ushort startX, ushort startY, sbyte startZ, ushort finishX, ushort finishY,
            sbyte finishZ, byte worldNum, int accuracyXY, int accuracyZ, bool run)
        {
            return Stealth.Client.GetPathArray3D(startX, startY, startZ, finishX, finishY, finishZ, worldNum,
                accuracyXY, accuracyZ, run);
        }
        /// <summary>
        /// Generates a path array about waypoint from start to finish.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="World"></param>
        /// <param name="accuracyXY"></param>
        /// <param name="accuracyZ"></param>
        /// <param name="run"></param>
        /// <returns></returns>
        public List<MyPoint> GetPathArray3D(Point3D start, Point3D end, byte World, int accuracyXY, int accuracyZ,
            bool run)
        {
            return GetPathArray3D((ushort) start.X, (ushort) start.Y, (sbyte) start.Z, (ushort) end.X, (ushort) end.Y,
                (sbyte) end.Z, World, accuracyXY, accuracyZ, run);
        }

        /// <summary>
        /// Calculates next coords from your location and direction.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dir"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void CalcCoord(int x, int y, byte dir, out int x2, out int y2)
        {
            Stealth.Client.CalcCoord(x, y, dir, out x2, out y2);
        }

        /// <summary>
        /// Clears the bad location list.
        /// </summary>
        public void ClearBadLocationList()
        {
            Stealth.Client.ClearBadLocationList();
        }
        /// <summary>
        /// Clears the bad object list.
        /// </summary>
        public void ClearBadObjectList()
        {
            Stealth.Client.ClearBadObjectList();
        }

        /// <summary>
        /// Performs a single step in desired direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="running"></param>
        /// <returns></returns>
        public byte Step(byte direction, bool running)
        {
            return Stealth.Client.Step(direction, running);
        }
        /// <summary>
        /// Performs a single step in desired direction.<br/> This is pure guess but i guess Q stands for silent moving?
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="running"></param>
        /// <returns></returns>
        public int StepQ(byte direction, bool running)
        {
            return Stealth.Client.StepQ(direction, running);
        }

        /// <summary>
        /// Predicts next Y.
        /// </summary>
        /// <returns></returns>
        public ushort PredictedY()
        {
            return Stealth.Client.PredictedY();
        }

        /// <summary>
        /// Predicts next Z.
        /// </summary>
        /// <returns></returns>
        public sbyte PredictedZ()
        {
            return Stealth.Client.PredictedZ();
        }

        /// <summary>
        /// Predicts next X.
        /// </summary>
        /// <returns></returns>
        public ushort PredictedX()
        {
            return Stealth.Client.PredictedX();
        }

        /// <summary>
        /// Returns ID of last opened door.
        /// </summary>
        /// <returns></returns>
        public uint GetLastStepQUsedDoor()
        {
            return Stealth.Client.GetLastStepQUsedDoor();
        }

        /// <summary>
        /// Sets point as bad location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetBadLocation(ushort x, ushort y)
        {
            Stealth.Client.SetBadLocation(x, y);
        }

        /// <summary>
        /// Sets point as good location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetGoodLocation(ushort x, ushort y)
        {
            Stealth.Client.SetGoodLocation(x, y);
        }
        /// <summary>
        /// Sets all objects of type\color within radius as bad object.
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="color"></param>
        /// <param name="radius"></param>
        public void SetBadObject(ushort objType, ushort color, byte radius)
        {
            Stealth.Client.SetBadObject(objType, color, radius);
        }

        /// <summary>
        /// Moves player to destined location.
        /// </summary>
        /// <param name="xDst"></param>
        /// <param name="yDst"></param>
        /// <param name="zDst"></param>
        /// <param name="accuracyXY"></param>
        /// <param name="accuracyZ"></param>
        /// <param name="running"></param>
        /// <returns></returns>
        public bool MoveXYZ(ushort xDst, ushort yDst, sbyte zDst, int accuracyXY, int accuracyZ, bool running)
        {
            return Stealth.Client.MoveXYZ(xDst, yDst, zDst, accuracyXY, accuracyZ, running);
        }

        /// <summary>
        /// Moves player to destined location with better precision.
        /// </summary>
        /// <param name="xDst"></param>
        /// <param name="yDst"></param>
        /// <param name="optimized"></param>
        /// <param name="accuracy"></param>
        /// <param name="running"></param>
        /// <returns></returns>
        public bool newMoveXY(ushort xDst, ushort yDst, bool optimized, int accuracy, bool running)
        {
            return Stealth.Client.newMoveXY(xDst, yDst, optimized, accuracy, running);
        }

        /// <summary>
        /// Moves player to destined location.
        /// </summary>
        /// <param name="xDst"></param>
        /// <param name="yDst"></param>
        /// <param name="optimized"></param>
        /// <param name="accuracy"></param>
        /// <param name="running"></param>
        /// <returns></returns>
        public bool MoveXY(ushort xDst, ushort yDst, bool optimized, int accuracy, bool running)
        {
            return Stealth.Client.MoveXY(xDst, yDst, optimized, accuracy, running);
        }

        /// <summary>
        /// Predict next direction
        /// </summary>
        /// <returns></returns>
        public byte PredictedDirection()
        {
            return Stealth.Client.PredictedDirection();
        }
        /// <summary>
        /// Calculates direction.
        /// </summary>
        /// <param name="xFrom"></param>
        /// <param name="yFrom"></param>
        /// <param name="xTo"></param>
        /// <param name="yTo"></param>
        /// <returns></returns>
        public byte CalcDir(int xFrom, int yFrom, int xTo, int yTo)
        {
            return Stealth.Client.CalcDir(xFrom, yFrom, xTo, yTo);
        }
    }
}