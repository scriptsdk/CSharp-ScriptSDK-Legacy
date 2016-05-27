using System;
using ScriptSDK.Data;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Object structure describes a useable skill with handler behind.
    /// </summary>
    public class UseableSkill : Skill
    {
        /// <summary>
        /// Default Constructor, should only be used by SkillHelper class or in exception for custom skills.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="name"></param>
        /// <param name="skillname"></param>
        /// <param name="delay"></param>
        public UseableSkill(SkillHelper owner, string name, SkillName skillname, TimeSpan delay) : base(owner, name, skillname)
        {
            Delay = delay;
            LastUsed = DateTime.Now.AddTicks(-1);
        }

        /// <summary>
        /// Skill action delay, can be customized by user input.
        /// </summary>
        public TimeSpan Delay { get; set; }

        /// <summary>
        /// Timestamp when skill last time has been used. In emergency cases can be customized.
        /// </summary>
        public DateTime LastUsed { get; set; }

        /// <summary>
        /// Returns if last usage + delay is is the past. True means skill "could" be used again.
        /// Be aware, that this values are setted up by SDK. They can differ against your used server.
        /// </summary>
        public bool Useable
        {
            get { return (DateTime.Now >= (LastUsed + Delay)); }
        }

        /// <summary>
        /// Use the Skill if possible, pass result to event and fires event if possible.
        /// </summary>
        /// <returns></returns>
        public bool Use()
        {
            var state = Useable && Stealth.Client.UseSkill(Name);
            var e = new SkillEventArgs {Skill = this, State = state};
            return Use(e);
        }

        private bool Use(SkillEventArgs e)
        {
            var handler = OnUse;
            if (handler != null)
            {
                handler(this, e);
            }
            return e.State;
        }

        /// <summary>
        /// Describes Event handled when skill has been used.
        /// </summary>
        public event EventHandler<SkillEventArgs> OnUse;
    }
}