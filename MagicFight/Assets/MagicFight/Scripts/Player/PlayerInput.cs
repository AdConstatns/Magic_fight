namespace AmazingTeam.MagicFight{

    using UnityEngine;
    using System;

    [AddComponentMenu("MagicFight/Player/PlayerInput", 1)]
    public class PlayerInput : MonoBehaviour
    {
        private Player _player;
        private PlayerAIMovement _playerAIMovement;
        private InputReceiverMobile LeftStick;
        private PlayerHealth _playerHealth;
        private PlayerShooting _playerShooting;
        private FieldOfView _fieldOfView;
       
      
        bool OnceEnableMeshCollider = true;

       // public static event Action<bool> OnPlayerTakeDamageEventHandler;


        void Awake()
        {
            _playerAIMovement = this.GetComponent<PlayerAIMovement>();
            _playerHealth = this.GetComponent<PlayerHealth>();
            _playerShooting = this.GetComponentInChildren<PlayerShooting>();
            LeftStick = this.GetComponent<InputReceiverMobile>();
            _fieldOfView = this.GetComponentInChildren<FieldOfView>();
            _player = this.GetComponent<Player>();
           
        }

        private void OnEnable() {
            _playerAIMovement.enabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(LeftStick.IsPressed)
                _playerAIMovement.Move( this.transform.position + LeftStick.LeftStickDirection);

            if (LeftStick.WasReleased && _playerHealth.currentBandAids > 0 && (_player.attackTarget != null)) {
                _player.StartFiring();               
                //Invoke("DiablePlayerShooting", 1f);
            }          
        }

        void DiablePlayerShooting() {
            _player.StopFiring();
        }
    }
}
