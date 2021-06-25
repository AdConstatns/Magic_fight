using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight
{
    public class HealthPowerUpManualDetection : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                other.gameObject.GetComponent<Player>().AddBandAid(1);
                this.gameObject.SetActive(false);
            }
          
        }

        private void OnCollisionEnter(Collision other) {
            other.gameObject.GetComponent<Player>().AddBandAid(1);
        }
    }

    
}
