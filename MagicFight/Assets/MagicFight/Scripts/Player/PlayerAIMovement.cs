namespace AmazingTeam.MagicFight
{
    using Apex;
    using Apex.Units;
    using UnityEngine;
    using Apex.Steering.Behaviours;

    [AddComponentMenu("MagicFight/Player/PlayerAIMovement", 2)]
    public class PlayerAIMovement : MonoBehaviour
    {
        private Animator _anim;
        private Transform _lookAtTransform;
        private IUnitFacade _unit;
        private PlayerAnimation _playerAnimation;
        private Player _player;

        private float radius;
        private float minimumDistance;
        private float lingerForSeconds;


        int Walk = Animator.StringToHash("IS_WALK");
        int Attack = Animator.StringToHash("IS_ATTACK");

        private void Awake()
        {
            // Set up references.
            _anim = GetComponentInChildren<Animator>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _player = GetComponent<Player>();           
            _unit = this.GetUnitFacade();         
        }

        private void OnDisable()
        {
            _unit.Stop();            
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.R)) {
               var unit = this.GetUnitFacade<ExtendedUnitFacade>();
                unit.Run();
            }
        }

        private void FixedUpdate()
        {
            if (_lookAtTransform != null)
            {
                _unit.lookAt = _lookAtTransform.position;
            }

            // Animate the player.
            Animating();
        }

        public void Move(Vector3 destination)
        {
            _unit.MoveTo(destination, false);
           
        }

        public void Stop()
        {
            _unit.Stop();            
        }

        public void StopWander() {

            _unit.StopWander();
        }

        public void StartWander() {
            // _unit.Wander(10.0f, 4.0f, 0.0f);
            _unit.Wander(12.0f, 8.0f, 0.0f);
        }        

        /// <summary>
        /// Set the position to look at. Set to null is the Ai should stop looking
        /// </summary>
        /// <param name="lookAtTransform"></param>
        public void LookAt(Transform lookAtTransform)
        {
            _lookAtTransform = lookAtTransform;
            if (_lookAtTransform == null)
            {
                _unit.lookAt = null;
            }
        }

        private void Animating()
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = false;

            // Commented by tholkappiyan           
            //if (_unit.velocity.sqrMagnitude > 10) {
            //    walking = true;
            //}

            // Add by tholkappiyan       
            if (Mathf.Abs(_unit.velocity.x) > 2.0f || Mathf.Abs(_unit.velocity.z) > 2.0f) {

                walking = true;
            }


            // Tell the animator whether or not the player is walking.
            // Commented by Tholkappiyan. Implemented below.
            //_anim.SetBool("IsWalking", walking);
            _playerAnimation.SetAnimationBool(Walk, walking);

            //if (_player.IsPlayerShooting) {
            //    _playerAnimation.SetAnimationBool(Attack, true);
            //}
            //else {             
            //    _playerAnimation.SetAnimationBool(Attack, false);
            //}

//#if UNITY_EDITOR

//            if (gameObject.CompareTag("Player")) {
//                Debug.LogWarning($"<color=green><b>Player Animation: Velocity:</b></color>" + _unit.velocity.sqrMagnitude);
//                Debug.LogWarning($"<color=green><b>Player Animation: Walk velocity of x, z: </b></color>" + _unit.velocity.x + "  " + _unit.velocity.z);
//            }

//#endif

//#if UNITY_EDITOR
//            Debug.LogWarning($"<color=green><b>Player Animation: Walk Status:</b></color>"+ walking);
//            Debug.LogWarning($"<color=green><b>Player Animation: Walk velocity of x, z: </b></color>" + _unit.velocity.x + "  "+ _unit.velocity.z);
//#endif
        }
    }
}