/// <summary>
/// Author: Tholkappiyan
/// </summary>
namespace AmazingTeam.MagicFight
{
    using UnityEngine;

    public class FovDamage : MonoBehaviour
    {
        //Upon collision with another GameObject, 
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                var player = other.gameObject.GetComponent<Player>();
                var playershooting = other.gameObject.GetComponentInChildren<PlayerShooting>();
                //Bring the hurt
                if (player != null)
                    player.TakeDamage(playershooting.damagePerShot);
                playershooting.currentAmmo--;
            }

        }
    }
}
