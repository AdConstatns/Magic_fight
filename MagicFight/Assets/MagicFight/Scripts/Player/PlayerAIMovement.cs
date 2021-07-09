﻿namespace AmazingTeam.MagicFight
{
    using Apex;
    using Apex.Units;
    using UnityEngine;

    [AddComponentMenu("MagicFight/Player/PlayerAIMovement", 2)]
    public class PlayerAIMovement : MonoBehaviour
    {
        private Animator _anim;
        private Transform _lookAtTransform;
        private IUnitFacade _unit;
        private PlayerAnimation _playerAnimation;
     
        int Walk = Animator.StringToHash("IS_WALK");

        private void Awake()
        {
            // Set up references.
            _anim = GetComponentInChildren<Animator>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _unit = this.GetUnitFacade();
        }

        private void OnDisable()
        {
            _unit.Stop();
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


            // modified by tholkappiyan
            if (_unit.velocity.x > 0f || _unit.velocity.z > 0)  //_unit.velocity.sqrMagnitude
            {
                walking = true;
            }

            // Tell the animator whether or not the player is walking.
            // Commented by Tholkappiyan. Implemented below.
            //_anim.SetBool("IsWalking", walking);
            _playerAnimation.SetAnimationBool(Walk, walking);

#if UNITY_EDITOR
            Debug.Log("Player Animation: Walk Status:"+ walking);
#endif


        }
    }
}