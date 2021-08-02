namespace AmazingTeam.MagicFight {

    using UnityEngine;
    using System;

    public class PlayerInteration : MonoBehaviour
    {
        public PlayerCollectable _playerCollectable;
        
        Player _player;      

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

            if (_player.PowerUpCount < _playerCollectable.Maximum ) {
                //Allow the collisions between layer 0 (default) and layer 8 (custom layer you set in Inspector window)
                //Physics.IgnoreLayerCollision(15, 16, false);
                gameObject.layer = LayerMask.NameToLayer("PlayerAI");
            } else {
                // Ignore
                //Physics.IgnoreLayerCollision(15, 16, true);
                gameObject.layer = LayerMask.NameToLayer("Player-NoPowerup");
               
            }           
        }
       
    }
}
