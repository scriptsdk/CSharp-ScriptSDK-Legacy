using System.Collections.Generic;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Equivalent designed class to UOStringAttributes but designed for localized properties.
    /// </summary>
    /// <example>
    /// <code language="CSharp">
    /// <![CDATA[
    /// ///<summary>
    /// ///Code Sample object for assigning a UOClilocAttributes as property.
    /// ///</summary>
    /// public class TestItem : Item
    /// {
    ///     #pragma warning disable 1591
    ///     public TestItem(uint ObjectID) : base(ObjectID)
    ///     #pragma warning restore 1591
    ///     { }
    /// 
    ///     #pragma warning disable 1591
    ///     public TestItem(Serial serial) : base(serial)
    ///     #pragma warning restore 1591
    ///     { }
    /// 
    ///     /// <summary>
    ///     /// Stores specialattributes
    ///     /// </summary>
    ///     public FreedomAttributes SpecialAttributes { get; protected set; }
    /// 
    ///     /// <summary>
    ///     /// Overrides UpdateLocalizedProperties and assign SpecialAttributes.
    ///     /// </summary>
    ///     /// <returns></returns>
    ///     public override bool UpdateLocalizedProperties()
    ///     {
    ///         var result = base.UpdateLocalizedProperties();
    ///         SpecialAttributes = new FreedomAttributes(Tooltip, this);
    ///         return result;
    ///     }
    /// }
    /// 
    /// ///<summary>
    /// ///Very free sample of making UOClilocAttributes
    /// ///</summary>
    /// public class FreedomAttributes : UOClilocAttributes
    /// {
    ///     /// <summary>
    ///     /// Stores if the Item is a custom special item
    ///     /// </summary>
    ///     public bool IsSpecialItem { get; protected set; }
    /// 
    ///     private FreedomAttributes(List<ClilocItemRec> properties) : base(properties)
    ///     { }
    /// 
    ///     /// <summary>
    ///     /// Default constructor.
    ///     /// </summary>
    ///     /// <param name="properties"></param>
    ///     /// <param name="owner"></param>
    ///     public FreedomAttributes(List<ClilocItemRec> properties, UOEntity owner = null) : base(properties, owner)
    ///     { }
    /// 
    ///     /// <summary>
    ///     /// Parsing string and finds out if item tooltip contains the wording "special item".
    ///     /// </summary> 
    ///     /// <param name="properties"></param>
    ///     protected override void Parse(List<ClilocItemRec> properties)
    ///     {
    ///         if (properties[0].ClilocID = 12345678)
    ///             IsSpecialItem = true;
    ///         else
    ///             IsSpecialItem = false;
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public abstract class UOClilocAttributes
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="owner"></param>
        public UOClilocAttributes(List<ClilocItemRec> properties, UOEntity owner = null)
            : this(properties)
        {
            _uoeowner = owner;
        }

        /// <summary>
        /// More simpler constructor but doesnt store any owner.
        /// </summary>
        /// <param name="properties"></param>
        public UOClilocAttributes(List<ClilocItemRec> properties)
        {
            Parse(properties);
        }

        /// <summary>
        /// Stores the owner of attribute.
        /// </summary>
        protected UOEntity _uoeowner { get; set; }

        /// <summary>
        /// Designed generic function to parse localized properties to custom properties.
        /// </summary>
        /// <param name="properties"></param>
        protected abstract void Parse(List<ClilocItemRec> properties);
    }
}