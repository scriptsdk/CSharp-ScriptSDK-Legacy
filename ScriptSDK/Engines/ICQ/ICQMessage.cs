using System;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// Structure for handle ICQ messages.
    /// </summary>
    public class ICQMessage
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="SenderID"></param>
        /// <param name="Msg"></param>
        public ICQMessage(uint SenderID, string Msg)
        {
            Sender = SenderID;
            Message = Msg;
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Stores informations about sender\receivant.
        /// </summary>
        public virtual uint Sender { get; private set; }
        /// <summary>
        /// Stores text message.
        /// </summary>
        public virtual string Message { get; private set; }
        /// <summary>
        /// Stores timestamp of message.
        /// </summary>
        public virtual DateTime Timestamp { get; private set; }
    }
}