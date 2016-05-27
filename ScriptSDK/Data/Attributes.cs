using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptSDK.Data
{
    /// <summary>
    /// Custom attributes are able to store static data onto a class or interface without the requirement to instance the class.
    /// The SDK use this kind of attribute to store references as topdown principle. The root class stores the types of childclasses
    /// and the childclasses of their childclasses (full recursive). The lowest level of class attribute should be QueryTypeAttribute.
    /// Certain scanner functions are able to build search queries through this structure and generate a huge customized and filtered query 
    /// just by passing 1 single type.
    /// </summary>
    /// <example>
    /// <code language="CSharp">
    /// <![CDATA[
    /// [QueryType(typeof (BaseNecklace), typeof (BaseRing), typeof (BaseEarring), typeof (BaseBracelet))]
    /// public class BaseJewel : Item, IJewel
    /// {
    ///     public BaseJewel(uint ObjectID) : base(ObjectID)
    ///     { }
    /// 
    ///     public BaseJewel(Serial serial): base(serial)
    ///     { }
    /// } 
    /// ]]>
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class QueryTypeAttribute : Attribute
    {
        /// <summary>
        /// Constructor varuant to store types.
        /// </summary>
        public QueryTypeAttribute() : this(new List<Type>())
        {
        }

        /// <summary>
        /// Constructor varuant to store types.
        /// </summary>
        /// <param name="list"></param>
        public QueryTypeAttribute(List<Type> list)
        {
            Types = list;
        }

        /// <summary>
        /// Constructor varuant to store types.
        /// </summary>
        /// <param name="list"></param>
        public QueryTypeAttribute(params Type[] list) : this(list.ToList())
        {
        }

        /// <summary>
        /// Storage for implemented Types
        /// </summary>
        public List<Type> Types { get; private set; }
    }


    /// <summary>
    /// The QuerySearchAttribute describes a lowlevel attribute which allows to set filter attributes to a class inherited from UOEntity.
    /// The Scanner methods later on can build a query by this.
    /// Can be added in Combination of QueryTypeAttribute.
    /// </summary>
    /// <example>
    /// <code language="CSharp">
    /// <![CDATA[
    /// [QuerySearch(new ushort[] { 0x0EE9, 0x0E21 })]
    /// public class Bandage : Item
    /// {
    ///     public Bandage(Serial serial) : base(serial)
    ///     { }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class QuerySearchAttribute : Attribute
    {
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="colors"></param>
        /// <param name="labels"></param>
        public QuerySearchAttribute(ushort[] graphics, ushort[] colors, uint[] labels): this(graphics.ToList(), colors.ToList(), labels.ToList())
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="colors"></param>
        /// <param name="labels"></param>
        public QuerySearchAttribute(ushort graphic, ushort[] colors, uint[] labels): this(graphic, colors.ToList(), labels.ToList())
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="color"></param>
        /// <param name="labels"></param>
        public QuerySearchAttribute(ushort[] graphics, ushort color, uint[] labels): this(graphics.ToList(), color, labels.ToList())
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="colors"></param>
        /// <param name="label"></param>
        public QuerySearchAttribute(ushort[] graphics, ushort[] colors, uint label = 0): this(graphics.ToList(), colors.ToList(), label)
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="colors"></param>
        /// <param name="label"></param>
        public QuerySearchAttribute(ushort graphic, ushort[] colors, uint label = 0): this(graphic, colors.ToList(), label)
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="color"></param>
        /// <param name="label"></param>
        public QuerySearchAttribute(ushort[] graphics, ushort color = 0xFFFF, uint label = 0): this(graphics.ToList(), color, label)
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="color"></param>
        /// <param name="label"></param>
        public QuerySearchAttribute(ushort graphic = 0xFFFF, ushort color = 0xFFFF, uint label = 0): this(new List<ushort> {graphic}, new List<ushort> {color}, new List<uint> {label})
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="colors"></param>
        /// <param name="labels"></param>
        public QuerySearchAttribute(ushort graphic, List<ushort> colors, List<uint> labels): this(new List<ushort> {graphic}, colors, labels)
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="color"></param>
        /// <param name="labels"></param>
        public QuerySearchAttribute(List<ushort> graphics, ushort color, List<uint> labels): this(graphics, new List<ushort> {color}, labels)
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="colors"></param>
        /// <param name="label"></param>
        public QuerySearchAttribute(List<ushort> graphics, List<ushort> colors, uint label = 0): this(graphics, colors, new List<uint> {label})
        {
        }
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="colors"></param>
        /// <param name="label"></param>
        public QuerySearchAttribute(ushort graphic, List<ushort> colors, uint label = 0): this(new List<ushort> {graphic}, colors, new List<uint> {label})
        {
        }

        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="color"></param>
        /// <param name="label"></param>
        public QuerySearchAttribute(List<ushort> graphics, ushort color = 0xFFFF, uint label = 0): this(graphics, new List<ushort> {color}, new List<uint> {label})
        {
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="colors"></param>
        /// <param name="labels"></param>
        public QuerySearchAttribute(List<ushort> graphics, List<ushort> colors, List<uint> labels)
        {
            Graphics = graphics;
            Colors = colors;
            Labels = labels;
        }

        /// <summary>
        /// Stores a list of graphic types for search queries.
        /// </summary>
        public List<ushort> Graphics { get; private set; }

        /// <summary>
        /// Stores a list of colors for search queries.
        /// </summary>
        public List<ushort> Colors { get; private set; }

        /// <summary>
        /// Stores a list of localized ID´s fpr search queries.
        /// </summary>
        public List<uint> Labels { get; private set; }
    }
}