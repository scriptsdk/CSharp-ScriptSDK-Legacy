using System;
using System.Threading;
using ScriptSDK.Data;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Targethelper class exposes handles, actions and properties about targeting system.
    /// </summary>
    public class TargetHelper
    {
        #region WaitForTarget

        /// <summary>
        /// Functions allows dynamic to check if target cursor appear within delay.
        /// </summary>
        /// <param name="MaxWaitTimeMS"></param>
        /// <returns></returns>
        public bool WaitForTarget(int MaxWaitTimeMS)
        {
            return Stealth.Client.WaitForTarget(MaxWaitTimeMS);
        }

        #endregion

        #region Events

        private bool OnUse(TargetReplyEventArgs e)
        {
            var handler = OnTarget;
            if (handler != null)
            {
                handler(this, e);
            }
            return e.State;
        }

        #endregion

        #region Constructors

        private TargetHelper(PlayerMobile owner)
        {
            _owner = owner;
        }

        private PlayerMobile _owner { get; set; }
        private static TargetHelper _instance { get; set; }

        /// <summary>
        /// Event wich is fired on Targeting anything.
        /// </summary>
        public event EventHandler<TargetReplyEventArgs> OnTarget;

        /// <summary>
        /// Returns reference of target helper system.
        /// </summary>
        /// <returns></returns>
        public static TargetHelper GetTarget()
        {
            return _instance ?? (_instance = new TargetHelper(PlayerMobile.GetPlayer()));
        }

        #endregion

        #region TargetTo
        /// <summary>
        /// Method allows to target to an object via active cursor.
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public bool TargetTo(Serial serial)
        {
            var e = new UOEntity(serial);
            if (e.Valid)
            {
                Stealth.Client.TargetToObject(serial.Value);
                return
                    OnUse(new TargetReplyEventArgs
                    {
                        State = true,
                        ActionType = TargetActionType.Targeting,
                        ReplyType = TargetType.Object
                    });
            }
            return
                OnUse(new TargetReplyEventArgs
                {
                    State = false,
                    ActionType = TargetActionType.Targeting,
                    ReplyType = TargetType.Object
                });
        }
        /// <summary>
        /// Method allows to target to a location via active cursor.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool TargetTo(Point3D point)
        {
            Stealth.Client.TargetToXYZ((ushort) point.X, (ushort) point.Y, (sbyte) point.Z);
            return
                OnUse(new TargetReplyEventArgs
                {
                    State = true,
                    ActionType = TargetActionType.Targeting,
                    ReplyType = TargetType.Location
                });
        }
        /// <summary>
        /// Method allows to target to an tile location via active cursor.
        /// </summary>
        /// <param name="Tile"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool TargetTo(ushort Tile, Point3D point)
        {
            Stealth.Client.TargetToTile(Tile, (ushort) point.X, (ushort) point.Y, (sbyte) point.Z);
            return
                OnUse(new TargetReplyEventArgs
                {
                    State = true,
                    ActionType = TargetActionType.Targeting,
                    ReplyType = TargetType.TileLocation
                });
        }

        #endregion

        #region Autotarget

        /// <summary>
        /// Sets Autotarget onto serial wich will wait until cursor appears.
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public bool AutoTargetTo(Serial serial)
        {
            var e = new UOEntity(serial);
            if (e.Valid)
            {
                Stealth.Client.WaitTargetObject(serial.Value);
                return
                    OnUse(new TargetReplyEventArgs
                    {
                        State = true,
                        ActionType = TargetActionType.AutoTargeting,
                        ReplyType = TargetType.Object
                    });
            }
            return
                OnUse(new TargetReplyEventArgs
                {
                    State = false,
                    ActionType = TargetActionType.AutoTargeting,
                    ReplyType = TargetType.Object
                });
        }
        /// <summary>
        /// Sets Autotarget onto location wich will wait until cursor appears.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool AutoTargetTo(Point3D point)
        {
            Stealth.Client.WaitTargetXYZ((ushort) point.X, (ushort) point.Y, (sbyte) point.Z);
            return
                OnUse(new TargetReplyEventArgs
                {
                    State = true,
                    ActionType = TargetActionType.AutoTargeting,
                    ReplyType = TargetType.Location
                });
        }
        /// <summary>
        /// Sets Autotarget onto tile location wich will wait until cursor appears.
        /// </summary>
        /// <param name="Tile"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool AutoTargetTo(ushort Tile, Point3D point)
        {
            Stealth.Client.WaitTargetTile(Tile, (ushort) point.X, (ushort) point.Y, (sbyte) point.Z);
            return
                OnUse(new TargetReplyEventArgs
                {
                    State = true,
                    ActionType = TargetActionType.AutoTargeting,
                    ReplyType = TargetType.TileLocation
                });
        }
        /// <summary>
        /// Sets Autotarget onto players.
        /// </summary>
        /// <returns></returns>
        public bool AutoTargetSelf()
        {
            if (!_owner.Valid)
                return false;
            Stealth.Client.WaitTargetSelf();
            return
                OnUse(new TargetReplyEventArgs
                {
                    State = true,
                    ActionType = TargetActionType.AutoTargeting,
                    ReplyType = TargetType.Object
                });
        }

        /// <summary>
        /// TODO : NOT WORKING YET.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool AutoTargetType(ushort type)
        {
            return false;
        } //public void WaitTargetType(ushort objType);

        /// <summary>
        /// Performs autotarget to last active target.
        /// </summary>
        /// <returns></returns>
        public bool AutoTargetLast()
        {
            if (!_owner.Valid)
                return false;
            Stealth.Client.WaitTargetLast();
            return
                OnUse(new TargetReplyEventArgs
                {
                    State = true,
                    ActionType = TargetActionType.AutoTargeting,
                    ReplyType = TargetType.Object
                });
        }

        /// <summary>
        /// Applies auto target to random object by given type from ground.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool AutoTargetTypeFromGround(ushort type) //public void ;
        {
            if (!_owner.Valid)
                return false;
            Stealth.Client.WaitTargetGround(type);

            return
                OnUse(new TargetReplyEventArgs
                {
                    State = true,
                    ActionType = TargetActionType.AutoTargeting,
                    ReplyType = TargetType.Ground
                });
        }

        #endregion

        #region Virtual Targets

        /// <summary>
        /// Handles virtual target cursor onto objects.
        /// </summary>
        /// <returns></returns>
        public uint VCursorToObjectID()
        {
            Stealth.Client.ClientRequestObjectTarget();
            while (Stealth.Client.ClientTargetResponsePresent() == false)
                Thread.Sleep(25);
            return Stealth.Client.ClientTargetResponse().ID;
        }

        /// <summary>
        /// Handles virtual target cursor onto objects.
        /// </summary>
        /// <returns></returns>
        public UOEntity VCursorToEntity()
        {
            return new UOEntity(new Serial(VCursorToObjectID()));
        }

        /// <summary>
        /// Handles virtual target cursor onto objects.
        /// </summary>
        /// <returns></returns>
        public Item VCursorToItem()
        {
            return new Item(new Serial(VCursorToObjectID()));
        }

        /// <summary>
        /// Handles virtual target cursor onto objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T VCursorToGenericObject<T>() where T : UOEntity
        {
            T Obj;
            try
            {
                Obj = Activator.CreateInstance(typeof (T), new Serial(VCursorToObjectID())) as T;
            }
            catch (Exception)
            {
                return null;
            }
            return Obj;
        }
        /// <summary>
        /// Handles virtual target cursor onto objects.
        /// </summary>
        /// <returns></returns>
        public Mobile VCursorToMobile()
        {
            return new Mobile(new Serial(VCursorToObjectID()));
        }
        /// <summary>
        /// Handles virtual target cursor onto location.
        /// </summary>
        /// <returns></returns>
        public Point3D VCursorToLocation()
        {
            if (!_owner.Valid)
                return new Point3D(0, 0, 0);
            Stealth.Client.ClientRequestTileTarget();

            while (Stealth.Client.ClientTargetResponsePresent() == false)
                Thread.Sleep(25);

            var obj = Stealth.Client.ClientTargetResponse();

            return new Point3D(obj.X, obj.Y, obj.Z);
        }
        /// <summary>
        /// Handles virtual target cursor onto tile location.
        /// </summary>
        /// <returns></returns>
        public StaticItemRealXY VCursorToTile()
        {
            if (!_owner.Valid)
                return new StaticItemRealXY();
            Stealth.Client.ClientRequestTileTarget();

            while (Stealth.Client.ClientTargetResponsePresent() == false)
                Thread.Sleep(25);

            var obj = Stealth.Client.ClientTargetResponse();

            return new StaticItemRealXY {Tile = obj.Tile, X = obj.X, Y = obj.Y, Z = (byte) obj.Z};
        }

        #endregion
    }
}