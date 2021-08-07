namespace AmazingTeam.MagicFight
{
    using Apex.Common;
    using Apex.Services;
    using UnityEngine;
   
    public class MessagePoster : MonoBehaviour
    {
        /// <summary>
        /// Called when MaxPowerup reached.
        /// </summary>
        /// <param name="previous">The previous attributes, before the change.</param>
        public void OnMaxPowerupReached() {
            GameServices.messageBus.Post(new MessagePowerupChanged(this.gameObject));
        }
    }
}
