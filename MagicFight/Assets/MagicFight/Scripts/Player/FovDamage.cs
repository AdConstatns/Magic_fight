/// <summary>
/// Author: Tholkappiyan
/// </summary>
namespace AmazingTeam.MagicFight {
    using UnityEngine;
    using System.Collections.Generic;

    public class FovDamage : MonoBehaviour {
        private Player _cPlayer;
        private PlayerShooting _playershooting;

        private List<Collider> _playerToAttackList;

        private ColliderContainer _ColliderContainer;

        private void Awake() {
            _cPlayer = GetComponentInParent<Player>();
           _playershooting = _cPlayer.gameObject.GetComponentInChildren<PlayerShooting>();
            _playerToAttackList = new List<Collider>();
        }

        private void Start() {
            _ColliderContainer = new ColliderContainer();
        }

        //Upon collision with another GameObject, 
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                //   var player = other.gameObject.GetComponent<Player>();
               // _playerToAttackList.Add(other);
               // _ColliderContainer.colliders.Add(other);
                
            }
        }

        private void OnTriggerStay(Collider other) {

            // Commented by Tholkappiyan-----------------------------------------------------------
            // If you want to attack single player then this is fine. for multipler target not ok.
            if (other.CompareTag("Player")) {
                var otherPlayer = other.gameObject.GetComponent<Player>();
                var playershooting = _cPlayer.gameObject.GetComponentInChildren<PlayerShooting>();
                // If attack target is not null.
                if (otherPlayer != null) {
                    //Set the attack target to the current player.
                    _cPlayer.attackTarget = otherPlayer;

                    //If attack target is null
                    if (_cPlayer.attackTarget == null) {
                       // Stop Shooting.
                        _cPlayer.StopFiring();
                        return;
                    }

                    //If Shooting is true
                    if (playershooting.shooting) {
                       // Bring the hurt to the player.
                        otherPlayer.TakeDamage(playershooting.damagePerShot);
                        // Commented by Tholkappiyan
                        // Not needed
                        // Reduce the current Ammo.
                       playershooting.currentAmmo--;
                    }
                }
            }
            //-----------------------------------------------------------------------------------

            //if (_ColliderContainer.colliders.Count > 0) {
            //    Debug.Log("Player Count: "+ _ColliderContainer.colliders.Count);
            //}

            //if (!other.CompareTag("Player"))
            //    return;
            //var toRemove = new List<Collider>();
            //foreach (var col in _playerToAttackList) {
            //    var otherPlayer = col.gameObject.GetComponent<Player>();


            //    // If attack target is not null.
            //    if (otherPlayer != null) {
            //        // Set the attack target to the current player.
            //        _cPlayer.attackTarget = otherPlayer;

            //        // If attack target is null
            //        if (_cPlayer.attackTarget == null) {
            //            // Stop Shooting.
            //            _cPlayer.StopFiring();
            //            return;
            //        }

            //        // If Shooting is true
            //        if (_playershooting.shooting) {
            //            //Bring the hurt to the player.
            //            otherPlayer.TakeDamage(_playershooting.damagePerShot);
            //            // Remove the collider from the list of attack
            //            toRemove.Add(col);
            //            //_playerToAttackList.Remove(col);
            //            // Commented by Tholkappiyan 
            //            // Not needed 
            //            // Reduce the current Ammo.
            //            //playershooting.currentAmmo--;
            //        }

            //    }
            //}

            //// Remove the players are dead.
            //foreach (var collider in toRemove) {
            //    _playerToAttackList.Remove(collider);
            //}

            //toRemove.Clear();

        }



        private void AttackPlayer() {

            // If Shooting is true
            if (_playershooting.shooting) {

                foreach (var col in _playerToAttackList) {
                    var otherPlayer = col.gameObject.GetComponent<Player>();

                    // If attack target is not null.
                    if (otherPlayer != null) {
                        // Set the attack target to the current player.
                        _cPlayer.attackTarget = otherPlayer;

                        // If attack target is null
                        if (_cPlayer.attackTarget == null) {
                            // Stop Shooting.
                            _cPlayer.StopFiring();
                            return;
                        }

                        // If Shooting is true
                        if (_playershooting.shooting) {
                            //Bring the hurt to the player.
                            otherPlayer.TakeDamage(_playershooting.damagePerShot);
                            // Remove the collider from the list of attack
                            _playerToAttackList.Remove(col);
                            // Commented by Tholkappiyan 
                            // Not needed 
                            // Reduce the current Ammo.
                            //playershooting.currentAmmo--;
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other) {

            // Commented by tholkappiyan--------------------------------------
            // UnComment for single player attack.
            //For single attack it is fine. for multiple attack it is not ok.
            // if exit is a player
            if (other.CompareTag("Player")) {
                    // Attack target is not null
                    if (_cPlayer.attackTarget == null)
                        return;

                    // Get the other Player
                    var otherPlayer = other.gameObject.GetComponent<Player>();

                    // Check whether the other player is same as attack target.
                    if (_cPlayer.attackTarget.gameObject == otherPlayer.gameObject) {
                        // Clear the attack target
                        _cPlayer.attackTarget = null;
                    }
                }
            //------------------------------------------------------------------------

            //RemovePlayer(other);
        }

        private void RemovePlayer(Collider other) {
            if (other.CompareTag("Player")) {
                //   var player = other.gameObject.GetComponent<Player>();             
                if (_playerToAttackList.Contains(other))
                    _playerToAttackList.Remove(other);
            }
        }

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
