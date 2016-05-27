using StealthAPI;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// Structure is designed storing messages.
    /// </summary>
    public struct GameMessage
    {
        /// <summary>
        /// Stores message namespace
        /// </summary>
        public VarRegion Region { get; set; }
        /// <summary>
        /// Stores attribute name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Stores attribute value.
        /// </summary>
        public string Value { get; set; }
    }
}