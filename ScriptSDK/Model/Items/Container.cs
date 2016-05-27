/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/
using ScriptSDK.Configuration;
using ScriptSDK.Data;
using StealthAPI;

namespace ScriptSDK.Items
{
    /// <summary>
    /// Container class expose functions and properties to handle and manage actions about any Container.
    /// </summary>
    public class Container : Item
    {

        /// <summary>
        /// Alternative constructor.
        /// </summary>
        /// <param name="ObjectID"></param>
        public Container(uint ObjectID) : base(ObjectID)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serial"></param>
        public Container(Serial serial) : base(serial)
        {
        }

        /// <summary>
        /// Function performs a mass moving from this container to another container, regardless of objects.
        /// </summary>
        /// <param name="Destination"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public virtual bool EmptyContainer(Container Destination, ushort delay)
        {
            return CanBeMoved && Stealth.Client.EmptyContainer(Serial.Value, Destination.Serial.Value, delay);
        }
        /// <summary> 
        /// Function performs a mass moving from this container to another container, regardless of objects.
        /// </summary>
        /// <param name="Destination"></param>
        /// <returns></returns>
        public virtual bool EmptyContainer(Container Destination)
        {
            return EmptyContainer(Destination, (ushort)ObjectOptions.DropDelay);
        }

        /// <summary> 
        /// Function performs a mass moving from this container to another container, regardless of objects.
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        public virtual bool MoveItems(Container dest)
        {
            return CanBeMoved && MoveItems(0xFFFF, 0xFFFF, dest, new Point3D(0, 0, 0), (int)ObjectOptions.DropDelay);
        }

        /// <summary> 
        /// Function performs a mass moving from this container to another container, regardless of objects.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public virtual bool MoveItems(Container dest, int delay)
        {
            return CanBeMoved && MoveItems(0xFFFF, 0xFFFF, dest, new Point3D(0, 0, 0), delay);
        }
        /// <summary> 
        /// Function performs a mass moving from this container to another container, regardless of objects.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual bool MoveItems(Container dest, Point3D location)
        {
            return CanBeMoved &&
                   MoveItems(0xFFFF, 0xFFFF, dest, new Point3D(location.X, location.Y, location.Z),
                       (int)ObjectOptions.DropDelay);
        }

        /// <summary> 
        /// Function performs a mass moving from this container to another container, regardless of objects.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="location"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public virtual bool MoveItems(Container dest, Point3D location, int delay)
        {
            return CanBeMoved &&
                   MoveItems(0xFFFF, 0xFFFF, dest, new Point3D(location.X, location.Y, location.Z),
                       (int)ObjectOptions.DropDelay);
        }

        /// <summary>
        /// Function performs a mass moving from this container to another container.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="color"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public virtual bool MoveItems(ushort type, ushort color, Container dest)
        {
            return CanBeMoved && MoveItems(type, color, dest, new Point3D(0, 0, 0), (int)ObjectOptions.DropDelay);
        }

        /// <summary>
        /// Function performs a mass moving from this container to another container.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="color"></param>
        /// <param name="dest"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public virtual bool MoveItems(ushort type, ushort color, Container dest, int delay)
        {
            return CanBeMoved && MoveItems(type, color, dest, new Point3D(0, 0, 0), delay);
        }

        /// <summary>
        /// Function performs a mass moving from this container to another container.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="color"></param>
        /// <param name="dest"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual bool MoveItems(ushort type, ushort color, Container dest, Point3D location)
        {
            return CanBeMoved &&
                   MoveItems(type, color, dest, new Point3D(location.X, location.Y, location.Z),
                       (int)ObjectOptions.DropDelay);
        }

        /// <summary>
        /// Function performs a mass moving from this container to another container.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="color"></param>
        /// <param name="dest"></param>
        /// <param name="location"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public virtual bool MoveItems(ushort type, ushort color, Container dest, Point3D location, int delay)
        {
            return CanBeMoved &&
                   Stealth.Client.MoveItems(Serial.Value, type, color, dest.Serial.Value, location.X, location.Y,
                       location.Z, delay);
        }

        //TODO : Allow to pass Object types
    }
}