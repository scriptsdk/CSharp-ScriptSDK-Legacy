using System;
using ScriptSDK.Data;
using ScriptSDK.Engines;
using ScriptSDK.Items;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Paperdoll class is a dynamic class allowing the refered mobile to 
    /// </summary>
    public sealed class Paperdoll
    {
        private Paperdoll(Mobile owner)
        {
            Owner = owner;
        }

        private Mobile Owner { get; set; }

        /// <summary>
        /// Returns object found Left-Hand Layer
        /// </summary>
        public UOEntity OneHanded
        {
            get { return GetObject(Layer.OneHanded); }
        }
        /// <summary>
        /// Returns object found Right-Hand Layer
        /// </summary>
        public UOEntity TwoHanded
        {
            get { return GetObject(Layer.TwoHanded); }
        }
        /// <summary>
        /// Returns object found Shoes Layer
        /// </summary>
        public UOEntity Shoes
        {
            get { return GetObject(Layer.Shoes); }
        }
        /// <summary>
        /// Returns object found Pants Layer
        /// </summary>
        public UOEntity Pants
        {
            get { return GetObject(Layer.Pants); }
        }
        /// <summary>
        /// Returns object found Shirt Layer
        /// </summary>
        public UOEntity Shirt
        {
            get { return GetObject(Layer.Shirt); }
        }
        /// <summary>
        /// Returns object found Helm Layer
        /// </summary>
        public UOEntity Helm
        {
            get { return GetObject(Layer.Helm); }
        }
        /// <summary>
        /// Returns object found Gloves Layer
        /// </summary>
        public UOEntity Gloves
        {
            get { return GetObject(Layer.Gloves); }
        }
        /// <summary>
        /// Returns object found Ring Layer
        /// </summary>
        public UOEntity Ring
        {
            get { return GetObject(Layer.Ring); }
        }
        /// <summary>
        /// Returns object found Talisman Layer
        /// </summary>
        public UOEntity Talisman
        {
            get { return GetObject(Layer.Talisman); }
        }
        /// <summary>
        /// Returns object found Neck Layer
        /// </summary>
        public UOEntity Neck
        {
            get { return GetObject(Layer.Neck); }
        }
        /// <summary>
        /// Returns object found Hair Layer
        /// </summary>
        public UOEntity Hair
        {
            get { return GetObject(Layer.Hair); }
        }
        /// <summary>
        /// Returns object found Waist Layer
        /// </summary>
        public UOEntity Waist
        {
            get { return GetObject(Layer.Waist); }
        }
        /// <summary>
        /// Returns object found Inner Torso Layer
        /// </summary>
        public UOEntity InnerTorso
        {
            get { return GetObject(Layer.InnerTorso); }
        }
        /// <summary>
        /// Returns object found Bracelet Layer
        /// </summary>
        public UOEntity Bracelet
        {
            get { return GetObject(Layer.Bracelet); }
        }
        /// <summary>
        /// Returns object found Face Layer
        /// </summary>
        public UOEntity Face
        {
            get { return GetObject(Layer.Face); }
        }
        /// <summary>
        /// Returns object found Facial Hair Layer
        /// </summary>
        public UOEntity FacialHair
        {
            get { return GetObject(Layer.FacialHair); }
        }
        /// <summary>
        /// Returns object found Middle Torso Layer
        /// </summary>
        public UOEntity MiddleTorso
        {
            get { return GetObject(Layer.MiddleTorso); }
        }
        /// <summary>
        /// Returns object found Earrings Layer
        /// </summary>
        public UOEntity Earrings
        {
            get { return GetObject(Layer.Earrings); }
        }
        /// <summary>
        /// Returns object found Arms Layer
        /// </summary>
        public UOEntity Arms
        {
            get { return GetObject(Layer.Arms); }
        }
        /// <summary>
        /// Returns object found Cloak Layer
        /// </summary>
        public UOEntity Cloak
        {
            get { return GetObject(Layer.Cloak); }
        }
        /// <summary>
        /// Returns object found Backpack Layer
        /// </summary>
        public Container Backpack
        {
            get { return new Container(GetObject(Layer.Backpack).Serial); }
        }
        /// <summary>
        /// Returns object found Outer Torso Layer
        /// </summary>
        public UOEntity OuterTorso
        {
            get { return GetObject(Layer.OuterTorso); }
        }
        /// <summary>
        /// Returns object found Outer Legs Layer
        /// </summary>
        public UOEntity OuterLegs
        {
            get { return GetObject(Layer.OuterLegs); }
        }
        /// <summary>
        /// Returns object found Inner Legs Layer
        /// </summary>
        public UOEntity InnerLegs
        {
            get { return GetObject(Layer.InnerLegs); }
        }
        /// <summary>
        /// Returns object found Mount Layer
        /// </summary>
        public UOEntity Mount
        {
            get { return GetObject(Layer.Mount); }
        }
        /// <summary>
        /// Returns object found Shop (Buy-View) Layer
        /// </summary>
        public UOEntity ShopBuy
        {
            get { return GetObject(Layer.ShopBuy); }
        }
        /// <summary>
        /// Returns object found Shop (Resale-View) Layer
        /// </summary>
        public UOEntity ShopResale
        {
            get { return GetObject(Layer.ShopResale); }
        }
        /// <summary>
        /// Returns object found Shop (Sell-View) Layer
        /// </summary>
        public UOEntity ShopSell
        {
            get { return GetObject(Layer.ShopSell); }
        }
        /// <summary>
        /// Returns object found Bank Layer
        /// </summary>
        public UOEntity Bank
        {
            get { return GetObject(Layer.Bank); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public UOEntity GetObject(Layer layer)
        {
            if (Owner is PlayerMobile)
                return new UOEntity(new Serial(Stealth.Client.ObjAtLayer((byte) layer)));
            return new UOEntity(new Serial(Stealth.Client.ObjAtLayerEx((byte) layer, Owner.Serial.Value)));
        }
        /// <summary>
        /// Singleton designed function to get reference of paperdoll object.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static Paperdoll GetPaperdoll(Mobile owner)
        {
            return new Paperdoll(owner);
        }
        /// <summary>
        /// Returns total counted amount of Lower Mana Cost through paperdoll.
        /// </summary>
        public int TotalLMC
        {
            get
            {
                var value = 0;

                foreach (Layer e in Enum.GetValues(typeof(Layer)))
                {
                    var o = GetObject(e);
                    o.UpdateLocalizedProperties();
                    value += (ClilocHelper.Contains(o.Properties, 1060433)) ? ClilocHelper.GetParams(o.Properties, 1060433)[0] : 0;
                }
                return value;
            }
        }
        /// <summary>
        /// Returns total counted amount of Faster Casting through paperdoll.
        /// </summary>
        public int TotalFC
        {
            get
            {
                var value = 0;

                foreach (Layer e in Enum.GetValues(typeof(Layer)))
                {
                    var o = GetObject(e);
                    o.UpdateLocalizedProperties();
                    value += (ClilocHelper.Contains(o.Properties, 1060413)) ? ClilocHelper.GetParams(o.Properties, 1060413)[0] : 0;
                }
                return value;
            }
        }
        /// <summary>
        /// Returns total counted amount of Faster Cast Recovery through paperdoll.
        /// </summary>
        public int TotalFCR
        {
            get
            {
                var value = 0;

                foreach (Layer e in Enum.GetValues(typeof(Layer)))
                {
                    var o = GetObject(e);
                    o.UpdateLocalizedProperties();
                    value += (ClilocHelper.Contains(o.Properties, 1060412)) ? ClilocHelper.GetParams(o.Properties, 1060412)[0] : 0;
                }
                return value;
            }
        }
    }
}