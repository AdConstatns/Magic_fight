namespace AmazingTeam.MagicFight{

    using UnityEngine;

    [AddComponentMenu("MagicFight/Player/PlayerInput", 1)]
    public class PlayerInput : MonoBehaviour
    {
        private PlayerAIMovement _playerAIMovement;
        private InputReceiverMobile LeftStick;
        private PlayerHealth _playerHealth;
        private PlayerShooting _playerShooting;
        private FieldOfView _fieldOfView;
        GameObject FOVCollider ;

        bool OnceEnableMeshCollider = true;


        void Awake()
        {
            _playerAIMovement = this.GetComponent<PlayerAIMovement>();
            _playerHealth = this.GetComponent<PlayerHealth>();
            _playerShooting = this.GetComponentInChildren<PlayerShooting>();
            LeftStick = this.GetComponent<InputReceiverMobile>();
            _fieldOfView = this.GetComponentInChildren<FieldOfView>();
            FOVCollider = GameObject.FindGameObjectWithTag("Sight");
        }

        private void OnEnable() {
            _playerAIMovement.enabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(LeftStick.IsPressed)
                _playerAIMovement.Move( this.transform.position + LeftStick.StickDirection);

            if (LeftStick.IsReleased && _playerHealth.currentBandAids > 0) {
                _playerShooting.shooting = true;
                _fieldOfView.enabled =true;
                Invoke("FOVColliderEnable", 0.5f);
            }          
        }

        void FOVColliderEnable() {
            FOVCollider.transform.GetChild(0).gameObject.SetActive(true);
        }


    }
}