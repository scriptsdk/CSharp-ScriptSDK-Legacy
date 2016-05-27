namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Model of custom string attribute allows a more free parsing of data to be attributes then localized attributes.
    /// </summary>
    /// <example>
    /// <code language="CSharp">
    /// <![CDATA[
    /// ///<summary>
    /// ///Code Sample object for assigning a UOStringAttribute as property.
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
    ///     /// Overrides UpdateTextProperties and assign SpecialAttributes.
    ///     /// </summary>
    ///     /// <returns></returns>
    ///     public override bool UpdateTextProperties()
    ///     {
    ///         var result = base.UpdateTextProperties();
    ///         SpecialAttributes = new FreedomAttributes(Tooltip, this);
    ///         return result;
    ///     }
    /// }
    /// 
    /// ///<summary>
    /// ///Very free sample of making UOStringAttribute
    /// ///</summary>
    /// public class FreedomAttributes : UOStringAttribute
    /// {
    ///     /// <summary>
    ///     /// Stores if the Item is a custom special item
    ///     /// </summary>
    ///     public bool IsSpecialItem { get; protected set; }
    /// 
    ///     private FreedomAttributes(string properties) : base(properties)
    ///     { }
    /// 
    ///     /// <summary>
    ///     /// Default constructor.
    ///     /// </summary>
    ///     /// <param name="properties"></param>
    ///     /// <param name="owner"></param>
    ///     public FreedomAttributes(string properties, UOEntity owner = null) : base(properties, owner)
    ///     { }
    /// 
    ///     /// <summary>
    ///     /// Parsing string and finds out if item tooltip contains the wording "special item".
    ///     /// </summary> 
    ///     /// <param name="properties"></param>
    ///     protected override void Parse(string properties)
    ///     {
    ///         if (Insensitive.Contains(properties, "special item"))
    ///             IsSpecialItem = true;
    ///         else
    ///             IsSpecialItem = false;
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>

    public abstract class UOStringAttribute
    {
        /// <summary>
        /// Constructor with more freedoms but 
        /// </summary>
        /// <param name="properties"></param>
        public UOStringAttribute(string properties)
        {
            Parse(properties);
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="owner"></param>
        public UOStringAttribute(string properties, UOEntity owner = null):this(properties)
        {
            _uoeowner = owner;
        }

        /// <summary>
        /// Stores the owner entity.
        /// </summary>
        protected UOEntity _uoeowner { get; set; }

        /// <summary>
        /// Designed generic function to parse string to properties.
        /// </summary>
        /// <param name="properties"></param>
        protected abstract void Parse(string properties);
    }
}