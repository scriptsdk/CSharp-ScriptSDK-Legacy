/*
███████╗ ██████╗██████╗ ██╗██████╗ ████████╗███████╗██████╗ ██╗  ██╗
██╔════╝██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██║ ██╔╝
███████╗██║     ██████╔╝██║██████╔╝   ██║   ███████╗██║  ██║█████╔╝ 
╚════██║██║     ██╔══██╗██║██╔═══╝    ██║   ╚════██║██║  ██║██╔═██╗ 
███████║╚██████╗██║  ██║██║██║        ██║   ███████║██████╔╝██║  ██╗
╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝        ╚═╝   ╚══════╝╚═════╝ ╚═╝  ╚═╝
*/

using System;
using System.Collections.Generic;
using System.Linq;
using ScriptSDK.Data;
using ScriptSDK.Engines;
using ScriptSDK.Mobiles;
using StealthAPI;

namespace ScriptSDK.Items
{

    /// <summary>
    /// Item class expose functions and properties to handle and manage actions about any mobile.
    /// </summary>	
    /// <example>
    /// Example #1 Doing a generic search via Item.Find.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     List<uint> locations = new List<uint>();
    ///     locations.Add(0x0); // Ground
    ///     //We designed a Class Dagger wich inherited from Item and had search params
    ///     List<Item> search = Item.Find(typeof(Dagger),locations,true;
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Example #2 Another way of doing a generic search via Item.Find.   
    /// <code language="CSharp">
    /// <![CDATA[
    ///     List<uint> locations = new List<uint>();
    ///     locations.Add(0x0); // Ground
    ///     List<Type> types = new List<Type>();
    ///     types.Add(typeof(Dagger));
    ///     types.Add(typeof(Sword)); 
    ///     //We designed a Class Dagger and a Class Sword which inherited from Item and had search params
    ///     List<Item> search = Item.Find(types,locations,true;
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Example #3 Yet another way of generic search via Item.Find.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     //We designed a Class Dagger and a class Sword which inherited from Item and had search params
    ///     List<Item> search = Item.Find(typeof(Dagger),0x0,true;
    /// ]]>
    /// </code>
    /// </example>      
    /// <example>
    /// Example #4 Last way of generic search via Item.Find
    /// <code language="CSharp">
    /// <![CDATA[
    ///     List<Type> types = new List<Type>();
    ///     types.Add(typeof(Sword));
    ///     types.Add(typeof(Dagger)); 
    ///     //We designed a Class Sword and a Class Dagger which inherited from Item and had search params
    ///     List<Item> search = Item.Find(types,0x0,true;
    /// ]]>
    /// </code>
    /// </example>
    public class Item : UOEntity
    {
        /// <summary>
        /// Alternative constructor.
        /// </summary>
        /// <param name="ObjectID"></param>
        public Item(uint ObjectID) : this(new Serial(ObjectID))
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serial"></param>
        public Item(Serial serial) : base(serial)
        {
        }
        
        /// <summary>
        /// Returns the loottype of item object.
        /// </summary>
        public virtual LootType LootType
        {
            get
            {
                if (!Valid)
                    return LootType.Regular;
                var props = Properties;
                if (ClilocHelper.GetIndex(props, 1038021) > -1)
                    return LootType.Blessed;
                if (ClilocHelper.GetIndex(props, 1049643) > -1)
                    return LootType.Cursed;
                if (ClilocHelper.GetIndex(props, 1061682) > -1)
                    return LootType.Insured;
                return LootType.Regular;
            }
        }

        /// <summary>
        /// Returns current race requirements.
        /// </summary>
        public virtual Race RequiredRace
        {
            get
            {
                if (!Valid)
                    return Race.None;
                var props = Properties;
                if (ClilocHelper.GetIndex(props, 1075086) > -1)
                    return Race.Elv;
                return ClilocHelper.GetIndex(props, 1111709) > -1 ? Race.Gargoyle : Race.None;
            }
        }

        /// <summary>
        /// Returns current state, if item is a quest item.
        /// </summary>
        public virtual bool IsQuestItem
        {
            get
            {
                if (!Valid)
                    return false;
                var props = Properties;
                return ClilocHelper.GetIndex(props, 1075086) > -1;
            }
        }

        /// <summary>
        /// Returns current secure level.
        /// </summary>
        public virtual SecureLevel SecureLevel
        {
            get
            {
                if (!Valid)
                    return SecureLevel.Regular;
                var props = Properties;

                if (ClilocHelper.GetIndex(props, 501644) > -1)
                    return SecureLevel.Secured;
                return ClilocHelper.GetIndex(props, 501643) > -1 ? SecureLevel.Locked : SecureLevel.Regular;
            }
        }

        /// <summary>
        /// Returns current weigth.
        /// </summary>
        public virtual double Weight
        {
            get
            {
                if (!Valid)
                    return 0.0;
                var props = Properties;
                if (ClilocHelper.GetIndex(props, 1072788) > -1)
                    return ClilocHelper.GetParams(props, 1072788)[0];
                return ClilocHelper.GetIndex(props, 1072789) > -1 ? ClilocHelper.GetParams(props, 1072789)[0] : 0.0;
            }
        }

        /// <summary>
        /// Returns current amount.
        /// </summary>
        public virtual int Amount
        {
            get { return !Valid ? 0 : Stealth.Client.GetQuantity(Serial.Value); }
        }

        /// <summary>
        /// Returns current informations about personal blessing.
        /// </summary>
        public virtual string BlessedFor
        {
            get
            {
                if (!Valid)
                    return string.Empty;
                var props = Properties;
                return (ClilocHelper.GetIndex(props, 1062203) > -1) ? ClilocHelper.GetParams(props, 1062203)[0] : "";
            }
        }

        /// <summary>
        /// Returns current price, if item is on vendor list (NPC).
        /// </summary>
        public virtual uint Price
        {
            get { return !Valid ? 0 : Stealth.Client.GetPrice(Serial.Value); }
        }

        /// <summary>
        /// Returns if object contains idenfitication as container.
        /// </summary>
        public virtual bool IsContainer
        {
            get { return Valid && Stealth.Client.IsContainer(Serial.Value); }
        }

        /// <summary>
        /// Returns if item is insured
        /// </summary>
        public virtual bool Insured
        {
            get { return LootType.Equals(LootType.Insured) || LootType.Equals(LootType.Blessed); }
        }

        /// <summary>
        /// Returns if item is secured.
        /// </summary>
        public virtual bool IsSecured
        {
            get { return SecureLevel.Equals(SecureLevel.Secured); }
        }

        /// <summary>
        /// Returns if item is lockdown.
        /// </summary>
        public virtual bool IsLockedDown
        {
            get { return SecureLevel.Equals(SecureLevel.Secured) || SecureLevel.Equals(SecureLevel.Locked); }
        }

        /// <summary>
        /// Returns the default labelnumber based on graphic type.
        /// </summary>
        public virtual int DefaultLabelNumber
        {
            get { return (ObjectType < 0x4000) ? 1020000 + ObjectType : 1078872 + ObjectType; }
        }

        /// <summary>
        /// Stores the default color for quest markup.
        /// </summary>
        public virtual int QuestItemHue
        {
            get { return 0x04EA; }
        }

        /// <summary>
        /// Stores if item can be transfered.
        /// </summary>
        public virtual bool NonTransferable
        {
            get { return IsQuestItem; }
        }

        /// <summary>
        /// Returns if gargoyles could wear the item.
        /// </summary>
        public virtual bool WearableByGargoyles
        {
            get { return RequiredRace.Equals(Race.Gargoyle); }
        }

        /// <summary>
        /// Returns if elvs could wear the item.
        /// </summary>
        public virtual bool WearableByElves
        {
            get { return RequiredRace.Equals(Race.Elv); }
        }

        /// <summary>
        /// Sets or gets item to "I must be picked as next" for drag operations.
        /// </summary>
        public virtual Item PickedItem
        {
            get { return new Item(new Serial(Stealth.Client.GetPickedUpItem())); }
            set { Stealth.Client.SetPickedUpItem(value.Serial.Value); }
        }

        /// <summary>
        /// Gets or sets if the location must be validated before allowing drop actions.
        /// </summary>
        public virtual bool CheckLocationBeforeDrop
        {
            get { return Stealth.Client.GetDropCheckCoord(); }
            set { Stealth.Client.SetDropCheckCoord(value); }
        }

        /// <summary>
        /// Gets or sets the dropdelay which is equivalent to ObjectOptions.DropDelay.
        /// </summary>
        public virtual uint DropDelay
        {
            get { return Stealth.Client.GetDropDelay(); }
            set { Stealth.Client.SetDropDelay(value); }
        }

        /// <summary>
        /// Returns always false because it´s a dummy for further inheritence.
        /// </summary>
        public virtual bool AllowEquipedCast
        {
            get { return false; }
        }

        /// <summary>
        /// Returns if the object theoretical could be moved.
        /// </summary>
        public virtual bool Movable
        {
            get { return Valid && Stealth.Client.IsMovable(Serial.Value); }
        }
        
        /// <summary>
        /// Returns if the object is blessed for passed object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool CheckBlessed(object obj)
        {
            return CheckBlessed(obj as Mobile);
        }

        /// <summary>
        /// Returns if the object is blessed for passed mobile.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public virtual bool CheckBlessed(Mobile m)
        {
            return (LootType == LootType.Blessed || Insured) || (m != null && m.Name == BlessedFor);
        }

        /// <summary>
        /// Pass item and formats it properly to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("0x{0:X} \"{1}\"", Serial.Value, GetType().Name);
        }

        /// <summary>
        /// Returns true if object is right under players feet.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected virtual bool IsUnderYourFeet(UOEntity target)
        {
            return PlayerMobile.GetPlayer().Location.Equals(target.Location);
        }
        /// <summary> 
        /// Generic Search for Item.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<Item> Find(Type type, List<uint> Locations, bool SubSearch)
        {
            if (type == null)
                return new List<Item>();
            if ((!typeof (Item).IsAssignableFrom(type)) && (!(type.IsInterface)))
                return new List<Item>();

            if (Locations == null)
                return new List<Item>();
            if (Locations.Count < 1)
                return new List<Item>();

            try
            {
                if (!type.IsInterface)
                {
                    var tester = (Item) Activator.CreateInstance(type, new Serial(0));
                    if (tester == null)
                        return new List<Item>();
                }
            }
            catch
            {
                return new List<Item>();
            }

            var cattributes = type.GetCustomAttributes(false);

            var _sublist = new List<Item>();


            foreach (var attrib in cattributes)
            {
                if (attrib is QuerySearchAttribute)
                {
                    var x = (QuerySearchAttribute) attrib;

                    var xlist = Scanner.Find<Item>(x.Graphics, x.Colors, Locations, SubSearch);

                    var handle = true;

                    var labels = x.Labels;

                    labels.Remove(0);

                    if (x.Labels.Count > 0)
                    {
                        xlist = xlist.Where(i => ClilocHelper.ContainsAny(i.Properties, x.Labels)).ToList();
                        handle = xlist.Count > 0;
                    }

                    if (handle)
                        _sublist.AddRange(xlist.Select(xo => Activator.CreateInstance(type, xo.Serial) as Item));
                }
                else if (attrib is QueryTypeAttribute)
                {
                    var x = (QueryTypeAttribute) attrib;

                    foreach (var a in x.Types)
                    {
                        var xlist = Find(a, Locations, SubSearch);
                        if (xlist.Count > 0)
                            _sublist.AddRange(xlist);
                    }
                }
            }

            var rlist = new List<Item>();
            if (_sublist.Count > 0)
                rlist.AddRange(_sublist);

            return rlist.Distinct().ToList();
        }

        /// <summary>
        /// Generic Search for Item.
        /// </summary>
        /// <param name="types"></param>
        /// <param name="Locations"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<Item> Find(List<Type> types, List<uint> Locations, bool SubSearch)
        {
            if (types == null)
                return new List<Item>();
            if (types.Count < 1)
                return new List<Item>();

            var rlist = new List<Item>();
            foreach (var e in types)
            {
                var sublist = Find(e, Locations, SubSearch);
                if ((sublist != null) && (sublist.Count > 0))
                    rlist.AddRange(sublist);
            }
            //Remove dubs is missing
            return rlist.Distinct().ToList();
        }

        /// <summary>
        /// Generic Search for Item.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Location"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<Item> Find(Type type, uint Location, bool SubSearch)
        {
            return Find(type, new List<uint> {Location}, SubSearch);
        }

        /// <summary>
        /// Generic Search for Item.
        /// </summary>
        /// <param name="types"></param>
        /// <param name="Location"></param>
        /// <param name="SubSearch"></param>
        /// <returns></returns>
        public static List<Item> Find(List<Type> types, uint Location, bool SubSearch)
        {
            return Find(types, new List<uint> {Location}, SubSearch);
        }

        /// <summary>
        /// Returns if item theoreticly could be moved.
        /// </summary>
        public virtual bool CanBeMoved
        {
            get
            {

                var v = Valid;
                //var m = Movable;
                var s = SecureLevel.Equals(SecureLevel.Regular);

                return v /*&& m*/ && s;
            }
        }

        /// <summary>
        /// Performs a drop action towards ground.
        /// </summary>
        /// <returns></returns>
        public virtual bool DropHere()
        {
            return CanBeMoved && Stealth.Client.DropHere(Serial.Value);
        }

        /// <summary>
        /// Performs a drop action towards backpack with random location and stacking (if object allows).
        /// </summary>
        /// <returns></returns>
        public virtual bool Drop()
        {
            return Drop(new Point3D(0, 0, 0));
        }

        /// <summary>
        /// Performs a drop action towards backpack with location without stacking.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual bool Drop(Point3D location)
        {
            return Drop(Amount, location);
        }

        /// <summary>
        /// Performs a drop action towards backpack with location without stacking but with customizeable amount.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual bool Drop(int amount, Point3D location)
        {
            return CanBeMoved && Stealth.Client.Drop(Serial.Value, amount, location.X, location.Y, location.Z);
        }

        /// <summary>
        /// Drops item into chosen container with random location and stacking (if object allows).
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool DropItem(Container target)
        {
            return DropItem(target, new Point3D(0, 0, 0));
        }

        /// <summary>
        /// Drops item into chosen container with chosen location and without stacking.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual bool DropItem(Container target, Point3D location)
        {
            return CanBeMoved && Stealth.Client.DropItem(target.Serial.Value, location.X, location.Y, location.Z);
        }

        /// <summary>
        /// Drags item with chosen amount.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual bool DragItem(int amount)
        {
            return CanBeMoved && Stealth.Client.DragItem(Serial.Value, amount);
        }

        /// <summary>
        /// Performs drag and drop action onto chosen container with random location and stacking(if object allows).
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool MoveItem(Container target)
        {
            return MoveItem(target, Amount, new Point3D(0, 0, 0));
        }

        /// <summary>
        /// Performs drag and drop action onto chosen container with random location and stacking(if object allows) and chosen amount.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual bool MoveItem(Container target, int amount)
        {
            return MoveItem(target, amount, new Point3D(0, 0, 0));
        }

        /// <summary>
        /// Performs drag and drop action onto chosen container with location and without stacking.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual bool MoveItem(Container target, Point3D location)
        {
            return MoveItem(target, Amount, location);
        }

        /// <summary>
        /// Performs drag and drop action onto chosen container with location and without stacking and chosen amount.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual bool MoveItem(Container target, int amount, Point3D location)
        {
            var m = CanBeMoved;

            return m && Stealth.Client.MoveItem(Serial.Value, amount, target.Serial.Value, location.X, location.Y, location.Z);
        }
        public virtual bool MoveItem(Serial target, int amount)
        {
            return MoveItem(target, amount, new Point3D(0, 0, 0));
        }
        public virtual bool MoveItem(Serial target, int amount, Point3D location)
        {
            var m = CanBeMoved;

            return m && Stealth.Client.MoveItem(Serial.Value, amount, target.Value, location.X, location.Y, location.Z);
        }
        /// <summary>
        /// Performs drag and drop action towards backpack.
        /// </summary>
        /// <returns></returns>
        public virtual bool Grab()
        {
            return CanBeMoved && Stealth.Client.Grab(Serial.Value, Amount);
        }

        /// <summary>
        /// Performs drag and drop action with chosen amount towards backpack.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual bool Grab(int amount)
        {
            return CanBeMoved && Stealth.Client.Grab(Serial.Value, Amount);
        }
    }
}