using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;

        int SpeedParam = Animator.StringToHash("H_SPEED");
        int Idle = Animator.StringToHash("IS_IDLE");
        int Walk  = Animator.StringToHash("IS_WALK");
        int Run = Animator.StringToHash("IS_RUN");
        int GetHit = Animator.StringToHash("IS_GETHIT");
        int Attack1 = Animator.StringToHash("IS_ATTACK1");
        int Attack2 = Animator.StringToHash("IS_ATTACK2");
        int Die = Animator.StringToHash("IS_DIE");
        int Victory = Animator.StringToHash("IS_VICTORY");

        // Start is called before the first frame update
        void Awake() {
            _animator = GetComponent<Animator>();
        }

        public void SetAnimationBool(int Param, bool value) {
            _animator.SetBool(Param, value);
        }       

        public void TriggerAnimation(int Param) {
            _animator.SetTrigger(Param);
        }

       

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
