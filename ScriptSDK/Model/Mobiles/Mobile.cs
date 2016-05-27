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
using ScriptSDK.Attributes;
using ScriptSDK.Data;
using ScriptSDK.Engines;
using StealthAPI;

namespace ScriptSDK.Mobiles
{
    /// <summary>
    /// Mobile class expose functions and properties to handle and manage actions about any mobile.
    /// </summary>
    /// <example>
    /// Example #1 Doing a generic search via Mobile.Find.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     List<uint> locations = new List<uint>();
    ///     locations.Add(0x0); // Ground
    ///     //We designed a Class Cow wich inherited from Mobile and had search params
    ///     List<Mobile> mobilesearch = Mobile.Find(typeof(Cow),locations,true;
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Example #2 Another way of doing a generic search via Mobile.Find.   
    /// <code language="CSharp">
    /// <![CDATA[
    ///     List<uint> locations = new List<uint>();
    ///     locations.Add(0x0); // Ground
    ///     List<Type> types = new List<Type>();
    ///     types.Add(typeof(Cow));
    ///     types.Add(typeof(Dog)); 
    ///     //We designed a Class Cow and a Class Dog which inherited from Mobile and had search params
    ///     List<Mobile> mobilesearch = Mobile.Find(types,locations,true;
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// Example #3 Yet another way of generic search via Mobile.Find.
    /// <code language="CSharp">
    /// <![CDATA[
    ///     //We designed a Class Cow and a Class Dog which inherited from Mobile and had search params
    ///     List<Mobile> mobilesearch = Mobile.Find(typeof(Dog),0x0,true;
    /// ]]>
    /// </code>
    /// </example>      
    /// <example>
    /// Example #4 Last way of generic search via Mobile.Find
    /// <code language="CSharp">
    /// <![CDATA[
    ///     List<Type> types = new List<Type>();
    ///     types.Add(typeof(Cow));
    ///     types.Add(typeof(Dog)); 
    ///     //We designed a Class Cow and a Class Dog which inherited from Mobile and had search params
    ///     List<Mobile> mobilesearch = Mobile.Find(types,0x0,true;
    /// ]]>
    /// </code>
    /// </example>
    public class Mobile : UOEntity
    {
        /// <summary>
        ///  Alternate constructor.
        /// </summary>
        /// <param name="ObjectID"></param>
        public Mobile(uint ObjectID)
            : this(new Serial(ObjectID))
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serial"></param>
        public Mobile(Serial serial)
            : base(serial)
        {
            Paperdoll = Paperdoll.GetPaperdoll(this);
        }

        /// <summary>
        ///Returns if mobile object is running.
        /// </summary>
        public virtual bool IsRunning
        {
            get { return Stealth.Client.IsRunning(Serial.Value); }
        }

        /// <summary>
        ///Returns if mobile object is immortal or mortaled.
        /// </summary>
        public virtual bool YellowHits
        {
            get { return Stealth.Client.IsYellowHits(Serial.Value); }
        }

        /// <summary>
        /// Returns if mobile object is poisoned.
        /// </summary>
        public virtual bool Poisoned
        {
            get { return Stealth.Client.IsPoisoned(Serial.Value); }
        }

        /// <summary>
        ///Returns if mobile object is paralyzed.
        /// </summary>
        public virtual bool Paralyzed
        {
            get { return Stealth.Client.IsParalyzed(Serial.Value); }
        }

        /// <summary>
        ///Returns if mobile object is invisible.
        /// </summary>
        public virtual bool Hidden
        {
            get { return Stealth.Client.IsHidden(Serial.Value); }
        }

        /// <summary>
        ///Returns if mobile object is in combat mode.
        /// </summary>
        public virtual bool WarMode
        {
            get { return Stealth.Client.IsWarMode(Serial.Value); }
        }

        /// <summary>
        ///Returns current hit points.
        /// </summary>
        public virtual int Hits
        {
            get { return Stealth.Client.GetHP(Serial.Value); }
        }

        /// <summary>
        ///Returns maximum hit points.
        /// </summary>
        public virtual int MaxHits
        {
            get { return Stealth.Client.GetMaxHP(Serial.Value); }
        }

        /// <summary>
        ///Returns current mana.
        /// </summary>
        public virtual int Mana
        {
            get { return Stealth.Client.GetMana(Serial.Value); }
        }

        /// <summary>
        ///Returns maximum mana.
        /// </summary>
        public virtual int MaxMana
        {
            get { return Stealth.Client.GetMaxMana(Serial.Value); }
        }

        /// <summary>
        ///Returns current stamina.
        /// </summary>
        public virtual int Stamina
        {
            get { return Stealth.Client.GetStam(Serial.Value); }
        }

        /// <summary>
        ///Returns maximum stamina.
        /// </summary>
        public virtual int MaxStamina
        {
            get { return Stealth.Client.GetMaxStam(Serial.Value); }
        }

        /// <summary>
        ///Returns current notoriety.
        /// </summary>
        public virtual Notoriety Notoriety
        {
            get
            {
                var b = Stealth.Client.GetNotoriety(Serial.Value);
                var l = Enum.GetValues(typeof(Notoriety)) as byte[];
                if (l == null)
                    return Notoriety.Invalid;
                if (l.Contains(b))
                    return (Notoriety)b;
                return Notoriety.Invalid;
            }
        }

        /// <summary>
        ///Returns current name.
        /// </summary>
        public virtual string Name
        {
            get { return Stealth.Client.GetName(Serial.Value); }
        }

        /// <summary>
        ///Returns current title.
        /// </summary>
        public virtual string Title
        {
            get { return Stealth.Client.GetTitle(Serial.Value); }
        }

        /// <summary>
        ///Returns current alternate name.
        /// </summary>
        public virtual string AltName
        {
            get { return Stealth.Client.GetAltName(Serial.Value); }
        }

        /// <summary>
        ///Returns current gender.
        /// </summary>
        public virtual Gender Gender
        {
            get { return Stealth.Client.IsFemale(Serial.Value) ? Gender.Female : Gender.Male; }
        }

        /// <summary>
        ///Returns if mobile object is dead.
        /// </summary>
        public virtual bool Dead
        {
            get { return Stealth.Client.IsDead(Serial.Value); }
        }

        /// <summary>
        ///Returns if mobile object gets identificated as NPC.
        /// </summary>
        public virtual bool IsNPC
        {
            get { return Stealth.Client.IsNPC(Serial.Value); }
        }

        /// <summary>
        ///Returns current strength.
        /// </summary>
        public virtual int Strength
        {
            get { return Stealth.Client.GetStr(Serial.Value); }
        }

        /// <summary>
        ///Returns current intelligence.
        /// </summary>
        public virtual int Intelligence
        {
            get { return Stealth.Client.GetInt(Serial.Value); }
        }

        /// <summary>
        ///Returns current dexterity.
        /// </summary>
        public virtual int Dexterity
        {
            get { return Stealth.Client.GetDex(Serial.Value); }
        }

        /// <summary>
        ///Stores reference to paperdoll of mobile.
        /// </summary>
        public virtual Paperdoll Paperdoll { get; private set; }

        /// <summary>
        ///Returns percentage how much hitpoints towards maximum hitpoints.
        /// </summary>
        public virtual double HealthPercent
        {
            get
            {
                var max = MaxHits;
                var cur = Hits;
                if (max > 0.0 && cur > 0.0)
                    return (cur * 100.0 / max);
                return 0.0;
            }
        }

        /// <summary>
        ///Returns percentage how much stamina towards maximum stamina.
        /// </summary>
        public virtual double StaminaPercent
        {
            get
            {
                var max = MaxStamina;
                var cur = Stamina;
                if (max > 0.0 && cur > 0.0)
                    return (cur * 100.0 / max);
                return 0.0;
            }
        }

        /// <summary>
        ///Returns percentage how much mana towards maximum mana.
        /// </summary>
        public virtual double ManaPercent
        {
            get
            {
                var max = MaxMana;
                var cur = Mana;
                if (max > 0.0 && cur > 0.0)
                    return (cur * 100.0 / max);
                return 0.0;
            }
        }

        /// <summary>
        ///Performs a mobile update packet request.
        /// </summary>
        public virtual bool RequestStats()
        {
            if (!Valid)
                return false;
            Stealth.Client.RequestStats(Serial.Value);
            return true;
        }

        /// <summary>
        ///Performs a request to open paperdoll.
        /// </summary>
        public virtual bool OpenPaperdoll()
        {
            if (!Valid)
                return false;

            if (this is PlayerMobile)
            {
                Stealth.Client.UseSelfPaperdollScroll();
                return true;
            }
            Stealth.Client.UseOtherPaperdollScroll(Serial.Value);
            return true;
        }

        /// <summary>
        ///Generic Search for mobiles.
        /// </summary>
        public static List<Mobile> Find(Type type, List<uint> Locations, bool SubSearch)
        {
            if (type == null)
                return new List<Mobile>();
            if ((!typeof(Mobile).IsAssignableFrom(type)) && (!(type.IsInterface)))
                return new List<Mobile>();

            if (Locations == null)
                return new List<Mobile>();
            if (Locations.Count < 1)
                return new List<Mobile>();

            try
            {
                if (!type.IsInterface)
                {
                    var tester = (Mobile)Activator.CreateInstance(type, new Serial(0));
                    if (tester == null)
                        return new List<Mobile>();
                }
            }
            catch
            {
                return new List<Mobile>();
            }

            var cattributes = type.GetCustomAttributes(false);

            var _sublist = new List<Mobile>();


            foreach (var attrib in cattributes)
            {
                if (attrib is QuerySearchAttribute)
                {
                    var x = (QuerySearchAttribute)attrib;

                    var xlist = Scanner.Find<Mobile>(x.Graphics, x.Colors, Locations, SubSearch);

                    var handle = true;

                    var labels = x.Labels;

                    labels.Remove(0);

                    if (x.Labels.Count > 0)
                    {
                        xlist = xlist.Where(i => ClilocHelper.ContainsAny(i.Properties, x.Labels)).ToList();
                        handle = xlist.Count > 0;
                    }

                    if (handle)
                        _sublist.AddRange(xlist.Select(xo => Activator.CreateInstance(type, xo.Serial) as Mobile));
                }
                else if (attrib is QueryTypeAttribute)
                {
                    var x = (QueryTypeAttribute)attrib;

                    foreach (var xlist in x.Types.Select(a => Find(a, Locations, SubSearch)).Where(xlist => xlist.Count > 0))
                    {
                        _sublist.AddRange(xlist);
                    }
                }
            }

            var rlist = new List<Mobile>();
            if (_sublist.Count > 0)
                rlist.AddRange(_sublist);

            return rlist.Distinct().ToList();
        }

        /// <summary>
        ///Generic Search for mobiles.
        /// </summary>
        public static List<Mobile> Find(List<Type> types, List<uint> Locations, bool SubSearch)
        {
            if (types == null)
                return new List<Mobile>();
            if (types.Count < 1)
                return new List<Mobile>();

            var rlist = new List<Mobile>();
            foreach (
                var sublist in
                    types.Select(e => Find(e, Locations, SubSearch))
                        .Where(sublist => (sublist != null) && (sublist.Count > 0)))
            {
                rlist.AddRange(sublist);
            }
            return rlist.Distinct().ToList();
        }

        /// <summary>
        ///Generic Search for mobiles.
        /// </summary>
        public static List<Mobile> Find(Type type, uint Location, bool SubSearch)
        {
            return Find(type, new List<uint> { Location }, SubSearch);
        }

        /// <summary>
        ///Generic Search for mobiles.
        /// </summary>
        public static List<Mobile> Find(List<Type> types, uint Location, bool SubSearch)
        {
            return Find(types, new List<uint> { Location }, SubSearch);
        }

    }
}