/// <summary>
/// Author: Tholkappiyan
/// </summary>
namespace AmazingTeam.MagicFight {
    using UnityEngine;
    using System.Collections.Generic;
    using System;

    public class FovDamage : MonoBehaviour {
        private Player _cPlayer;
        private PlayerShooting _playershooting;
        private PlayerAnimation _playerAnimation;

        private ColliderContainer _ColliderContainer;

        [SerializeField]
        private int Count;

        int Attack = Animator.StringToHash("IS_ATTACK");



        private void Awake() {
            _cPlayer = GetComponentInParent<Player>();
            _playershooting = _cPlayer.gameObject.GetComponentInChildren<PlayerShooting>();
            _playerAnimation = GetComponentInParent<PlayerAnimation>();
        }

        private void Start() {
            _ColliderContainer = new ColliderContainer();
        }

        #region Trigger      

        //Upon collision with another GameObject, 
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _ColliderContainer.colliders.Add(other);
                Count = _ColliderContainer.colliders.Count;
                // Shooting Status.
#if UNITY_2020
                Debug.LogWarning($"<color=green><b>Trigger Enter - Shooting Status: { _playershooting.shooting }</b></color>");
#endif

            }
        }

        private void OnTriggerStay(Collider other) {           

            // If players are available. Go Inside.
            if (!other.CompareTag("Player"))
                return;

            if (_ColliderContainer.colliders.Count > 0) {
                Debug.Log("Player Count: " + _ColliderContainer.colliders.Count);
            }

            // Get the Colliders.
            HashSet<Collider> colliderList = _ColliderContainer.GetColliders();
            // Cannot remove the collider at the time of iteration. So temporarily store the collider to be removed.
            var toRemove = new HashSet<Collider>();

            foreach (var col in colliderList) {
                var otherPlayer = col.gameObject.GetComponent<Player>();

                //If attack target is not null.
                if (otherPlayer != null) {
                    // Set the attack target to the current player.
                    _cPlayer.AttackTarget.Add(otherPlayer);

                    // If attack target is null
                    if (_cPlayer.AttackTarget.Count == 0) {
                        // Stop Shooting.
                        _cPlayer.StopFiring();
                        return;
                    }

                    // If Shooting is true
                    if (_playershooting.shooting) {                       
                            _playerAnimation.TriggerAnimation(Attack);
                        //Bring the hurt to the player.                       
                        otherPlayer.TakeDamage(_playershooting.damagePerShot);
                        //for each player dead shooting will be set to false.
                        // Enable shooting to true. to shoot another enemies.           
                        _playershooting.shooting = true;
                        // Remove the Dead players from the attackTarget list.
                        _cPlayer.AttackTarget.Remove(other.gameObject.GetComponent<Player>());
                        _cPlayer.AttackTarget.Clear();
#if UNITY_2020
                        Debug.LogWarning($"<color=green><b>Trigger Stay - Target Removed: {other.gameObject } Attack Count: { _cPlayer.AttackTarget.Count}</b></color>");
#endif
                        // Remove the collider from the list of attack
                        toRemove.Add(col);
                        // Commented by Tholkappiyan 
                        // Not needed 
                        // Reduce the current Ammo.
                        //playershooting.currentAmmo--;
                    }
                }
            }

            // After finishing the loop make the shooting false.
            _playershooting.shooting = false;
            // Clear all AttackTargets after deleting.
            // _cPlayer.AttackTarget.Clear();

            // Remove the players are dead.
            foreach (var collider in toRemove) {
                _ColliderContainer.colliders.Remove(collider);
                //_playerToAttackList.Remove(collider);
            }
            toRemove.Clear();

#if UNITY_2020
            Debug.LogWarning($"<color=green><b>Trigger Stay - Shooting Status: { _playershooting.shooting }</b></color>");
#endif

        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player")) {
                Count--;
                // If player does not shoot then make the attack target null.
                _cPlayer.AttackTarget.Remove(other.gameObject.GetComponent<Player>());

              // Remove the other player that are not Shoot by curent player.         
                if (_ColliderContainer.colliders.Contains(other))
                    _ColliderContainer.colliders.Remove(other);

#if UNITY_2020
                Debug.LogWarning($"<color=green><b>Trigger Exit - Shooting Status: { _playershooting.shooting }</b></color>");
#endif
            }
        }

        #endregion

        public class ColliderContainer {

            // a HashSet can be used rather than a list,
            // which would yield a lower time complexity(make your code run faster).
            // While the.Remove() and.Contains() method for List runs in O(N^2) and O(N) respectively,
            // the.Remove() and.Contains() for HashSet are both O(1).

            public HashSet<Collider> colliders = new HashSet<Collider>();

            public HashSet<Collider> GetColliders() {
                return colliders;   //hashset automatically handles duplicates
            }
        }
    }
}
