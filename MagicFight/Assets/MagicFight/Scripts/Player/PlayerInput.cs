namespace AmazingTeam.MagicFight{

    using UnityEngine;

    [AddComponentMenu("MagicFight/Player/PlayerInput", 1)]
    public class PlayerInput : MonoBehaviour
    {
        private PlayerAIMovement _playerAIMovement;
        private InputReceiverMobile LeftStick;

        void Awake()
        {
            _playerAIMovement = this.GetComponent<PlayerAIMovement>();
            LeftStick = this.GetComponent<InputReceiverMobile>();

        }

        private void OnEnable() {
            _playerAIMovement.enabled = true;
        }

        // Update is called once per frame
        void Update()
        {

            if(LeftStick.IsPressed)
                _playerAIMovement.Move( this.transform.position + LeftStick.StickDirection);
        }
    }
}
