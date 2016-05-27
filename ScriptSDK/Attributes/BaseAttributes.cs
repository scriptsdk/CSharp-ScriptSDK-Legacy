using System;
using System.Collections.Generic;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Another way to store properties which was inspired by runuo.
    /// </summary>
    /// <example>
    /// Sample shows how to make a custom attributes storing some armor informations.
    /// <code language="CSharp">
    /// <![CDATA[
    /// public enum ArmorAttribute
    /// {
    ///     LowerStatReq = 0x00000001,
    ///     SelfRepair = 0x00000002,
    ///     MageArmor = 0x00000004,
    ///     DurabilityBonus = 0x00000008,
    ///     SoulCharge = 0x00000010,
    ///     ReactiveParalyze = 0x00000020
    /// }
    /// 
    /// public sealed class ArmorAttributes : BaseAttributes
    /// {
    ///     #region Parser
    /// 
    ///     protected override void Parse()
    ///     {
    ///         var x = Enum.GetValues(typeof(ArmorAttribute));
    ///         foreach (var e in x)
    ///         {
    ///             _data.Add((ArmorAttribute)e, 0);
    ///         }
    /// 
    ///         this[ArmorAttribute.MageArmor] = (ClilocHelper.Contains(_lastmetatable, 1060437));
    ///         this[ArmorAttribute.SelfRepair] = (ClilocHelper.GetIndex(_lastmetatable, 1060450) > -1) ? ClilocHelper.GetParams(_lastmetatable, 1060450)[0] : 0;
    ///         this[ArmorAttribute.SoulCharge] = (ClilocHelper.GetIndex(_lastmetatable, 1113630) > -1) ? ClilocHelper.GetParams(_lastmetatable, 1113630)[0] : 0;
    ///         this[ArmorAttribute.ReactiveParalyze] = (ClilocHelper.Contains(_lastmetatable, 1112364));
    ///         this[ArmorAttribute.LowerStatReq] = (ClilocHelper.GetIndex(_lastmetatable, 1061170) > -1) ? ClilocHelper.GetParams(_lastmetatable, 1061170)[0] : 0;
    ///         base.Parse();
    ///    } 
    ///     #endregion
    /// 
    ///     #region Constructors 
    ///     public ArmorAttributes(UOEntity owner, List<ClilocItemRec> reader) : base(owner, reader)
    ///     { }
    /// 
    ///     public ArmorAttributes(UOEntity owner) : base(owner, owner.Properties)
    ///     { }  
    ///     #endregion
    /// 
    ///     #region Properties 
    ///     public int LowerStatReq { get { return this[ArmorAttribute.LowerStatReq]; } }
    /// 
    ///     public int SelfRepair { get { return this[ArmorAttribute.SelfRepair]; } }
    /// 
    ///     public bool MageArmor { get { return this[ArmorAttribute.MageArmor]; } }
    /// 
    ///     public int DurabilityBonus { get { return this[ArmorAttribute.DurabilityBonus]; } }
    /// 
    ///     public int SoulCharge { get { return this[ArmorAttribute.SoulCharge]; } } 
    /// 
    ///     public int ReactiveParalyze { get { return this[ArmorAttribute.ReactiveParalyze]; } } 
    ///     #endregion
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public abstract class BaseAttributes
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="reader"></param>
        public BaseAttributes(UOEntity owner, List<ClilocItemRec> reader)
        {
            Owner = owner;
            if (reader == null)
                reader = new List<ClilocItemRec>();
            _lastmetatable = reader;
            _data = new Dictionary<Enum, dynamic>();
            Parse();
        }

        /// <summary>
        /// Stores data internal for the property mapping.
        /// </summary>
        protected Dictionary<Enum, dynamic> _data { get; set; }

        /// <summary>
        /// Stores the refered owner.
        /// </summary>
        public UOEntity Owner { get; private set; }

        /// <summary>
        /// Stores a copy of last properties passed to the parser
        /// </summary>
        protected List<ClilocItemRec> _lastmetatable { get; set; }

        /// <summary>
        /// Accessor for generic property mapper.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        protected dynamic this[Enum attribute]
        {
            get { return _data.ContainsKey(attribute) ? _data[attribute] : null; }
            set { if (_data.ContainsKey(attribute)) _data[attribute] = value; }
        }

        /// <summary>
        /// Generic Parser method for later datamapping 
        /// </summary>
        protected virtual void Parse()
        { }
    }
}