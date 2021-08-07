namespace AmazingTeam.MagicFight
{
    using Apex.Common;
    using UnityEngine;

    /// <summary>
    /// An Max Powerup  Message
    /// </summary>
    public class MessagePowerupChanged 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePowerupChanged"/> class.
        /// </summary>
        /// <param name="entity">The entity that this message concerns.</param>
        public MessagePowerupChanged(bool enableMessage) {
            this.enableMessage = enableMessage;
        }      

        /// <summary>
        /// Gets the entity that this message concerns.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public bool enableMessage {
            get;
            private set;
        }
    }
}
