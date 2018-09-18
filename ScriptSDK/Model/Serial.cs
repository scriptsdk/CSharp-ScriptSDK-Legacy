/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/
using System;
using System.CodeDom;

namespace ScriptSDK
{
    /// <summary>
    /// Serial class contains functions and properties to handle the unique instanced ID of objects in UO.
    /// </summary>		
    public class Serial : IComparable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serial"></param>
        public Serial(uint serial)
        {
            Value = serial;
        }

        /// <summary>
        /// Nulled constructor.
        /// </summary>
        public Serial()
            : this(0)
        {

        }
        public static implicit operator Serial( uint a )
        {
            return new Serial( a );
        }
        /// <summary>
        /// Stores unique ID of object.
        /// </summary>
        public virtual uint Value { get; set; }

        /// <summary>
        /// Allows to Compare if the value equals this objects value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Equals(uint value)
        {
            return Value.Equals(value);
        }

        /// <summary>
        /// Returns the Value parsed to a proper text.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("0x{0:X8}", Value);
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return (((obj is Serial) && Equals((Serial)obj))) ? 0 : -1;
        }

        /// <summary>
        /// Function returns 0 if passed serial can be compared.
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public int CompareTo(Serial serial)
        {
            return (serial.Value.Equals(serial.Value)) ? 0 : -1;
        }
    }
}