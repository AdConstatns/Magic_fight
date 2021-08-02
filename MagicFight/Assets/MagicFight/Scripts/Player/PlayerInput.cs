namespace AmazingTeam.MagicFight{

    using UnityEngine;
    using System;

    [AddComponentMenu("MagicFight/Player/PlayerInput", 1)]
    public class PlayerInput : MonoBehaviour {
        private Player _player;
        private PlayerAIMovement _playerAIMovement;
        private InputReceiverMobile LeftStick;
        private PlayerHealth _playerHealth;
        private PlayerShooting _playerShooting;
        private FieldOfView _fieldOfView;
        private PlayerFire _playerFire;
        private PlayerThunder _playerThunder;
        private PlayerLava _playerGround;




        // public static event Action<bool> OnPlayerTakeDamageEventHandler;


        void Awake() {
            _playerAIMovement = this.GetComponent<PlayerAIMovement>();
            _playerHealth = this.GetComponent<PlayerHealth>();
            _playerFire = this.GetComponentInChildren<PlayerFire>();
            _playerThunder = this.GetComponentInChildren<PlayerThunder>();
            _playerGround = this.GetComponentInChildren<PlayerLava>();
            _playerShooting = this.GetComponentInChildren<PlayerShooting>();
            LeftStick = this.GetComponent<InputReceiverMobile>();
            _fieldOfView = this.GetComponentInChildren<FieldOfView>();
            _player = this.GetComponent<Player>();

        }

        private void OnEnable() {
            _playerAIMovement.enabled = true;
        }

        // Update is called once per frame
        void Update() {

            // After finished shooting then only he can move.
            // Check Character is alive. Then he can move.
            if (LeftStick.IsPressed && _player.currentHealth > 0)
                _playerAIMovement.Move(this.transform.position + LeftStick.LeftStickDirection);    
            
            // Player should Finish shooting. Then only he can shoot again.
            if ((_playerHealth.currentHealth > 0) && LeftStick.WasReleased) {
                if (_playerFire.currentFires > 0 || _playerThunder.currentThunders > 0 || _playerGround.currentLavas > 0)
                    _player.IsPlayerShooting = true;
               

#if UNITY_EDITOR
                Debug.LogWarning($"<color=green><b>PlayerInput - Shooting Status: {  _player.IsPlayerShooting }</b></color>");
#endif

            } 
        }

        void DiablePlayerShooting() {
            _player.StopFiring();
        }
    }
}
