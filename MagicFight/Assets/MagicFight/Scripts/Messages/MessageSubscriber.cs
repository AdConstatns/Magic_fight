namespace AmazingTeam.MagicFight
{
    using Apex;
    using Apex.Examples.Misc;
    using Apex.Services;
    using UnityEngine;

    public class MessageSubscriber : ExtendedMonoBehaviour, IHandleMessage<MessagePowerupChanged> {
        public void Handle(MessagePowerupChanged message) {          
          
        }

        protected override void OnStartAndEnable() {
            GameServices.messageBus.Subscribe(this);
        }

        private void OnDisable() {
            GameServices.messageBus.Unsubscribe(this);
        }
    }
}
