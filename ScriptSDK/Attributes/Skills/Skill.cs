using ScriptSDK.Data;
using StealthAPI;

namespace ScriptSDK.Attributes
{
    /// <summary>
    /// Skill class describes a single non useable skill with handles and properties.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="name"></param>
        /// <param name="skillname"></param>
        public Skill(SkillHelper owner, string name, SkillName skillname)
        {
            Name = name;
            SkillName = skillname;
            _owner = owner;
        }

        /// <summary>
        /// Stores reference to skill list.
        /// </summary>
        protected virtual SkillHelper _owner { get; set; }
        /// <summary>
        /// Stores internal skill lock state.
        /// </summary>
        protected virtual SkillLock _lock { get; set; }
        /// <summary>
        /// Stores skill name as text.
        /// </summary>
        public virtual string Name { get; private set; }
        /// <summary>
        /// Stores skill name as enumeration.
        /// </summary>
        public virtual SkillName SkillName { get; private set; }

        /// <summary>
        /// Returns the skill cap.
        /// </summary>
        public virtual double Cap
        {
            get { return Stealth.Client.GetSkillCap(Name); }
        }

        /// <summary>
        /// Returns skill value.
        /// </summary>
        public virtual double Value
        {
            get { return Stealth.Client.GetSkillValue(Name); }
        }

        /// <summary>
        /// Gets or set skill lock state. Getter only can be used when once setted per script instance.
        /// </summary>
        public virtual SkillLock SkillLock
        {
            get { return _lock; }
            set
            {
                _lock = value;
                Stealth.Client.ChangeSkillLockState(Name, (byte) value);
            }
        }

        /// <summary>
        /// Returns skill properly parsed as text.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}: {1}/{2}]", Name, Value, Cap);
        }
    }
}