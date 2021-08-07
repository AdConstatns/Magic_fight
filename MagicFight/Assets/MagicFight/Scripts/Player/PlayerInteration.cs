namespace AmazingTeam.MagicFight {

    using UnityEngine;
    using Apex.Services;
    using System;

    public class PlayerInteration : MonoBehaviour
    {
        public PlayerCollectable _playerCollectable;
        
        Player _player;

        string layerName;

        // Start is called before the first frame update
        void Awake()
        {
            _player = GetComponent<Player>();
        }

        // Update is called once per frame
        void Update() {

            // Shift 1 to the 9th place from the right.
            //int bitmask = 1 << 9;

            //if (Input.GetKeyDown(KeyCode.X))
            //    Debug.Log(Convert.ToString(bitmask, 2).PadLeft(32, '0'));

            if (_player.PowerUpCount < _playerCollectable.Maximum) {
                // Change layer to PlayerAI so that player will be able to interact with power.
                if (gameObject.CompareTag("PlayerAI"))
                    gameObject.layer = LayerMask.NameToLayer("PlayerAI");
                else if (gameObject.CompareTag("Player")) {
                    gameObject.layer = LayerMask.NameToLayer("Player");

                    // Sending message to the HUD about the powerup message
                    HUDState.DisplayMaxPowerupMessage(false);
                }                
                   
            } else {
                // Change layer to Player-NoPowerup so that player will not be able to interact with power.
                gameObject.layer = LayerMask.NameToLayer("Player-NoPowerup");

                if(gameObject.CompareTag("Player"))
                // Sending message to the HUD about the powerup message
                HUDState.DisplayMaxPowerupMessage(true);

            }           
        }
       
    }
}
