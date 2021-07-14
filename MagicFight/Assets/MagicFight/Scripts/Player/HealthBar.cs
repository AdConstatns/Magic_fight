using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AmazingTeam.MagicFight
{
    public class HealthBar : MonoBehaviour
    {
        public float UpdateSpeedSecond;
        public PlayerHealth _playerHealth;
        public Image forgroundImage;

        // Start is called before the first frame update
        void Awake()
        {
            //_playerHealth = GetComponentInParent<PlayerHealth>();
           // GetComponent<Canvas>().worldCamera = Camera.main;          
        }
       
        public void UpdateHealthBar() {
            StartCoroutine(ChangeToPct(GetHealthPercentage()));
        }

        IEnumerator ChangeToPct(float pct) {
            float preChangePct = forgroundImage.fillAmount;
            float elapsed = 0;

            while (elapsed < UpdateSpeedSecond) {
                elapsed += Time.deltaTime;
                forgroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / UpdateSpeedSecond);
                yield return null;
            }

            forgroundImage.fillAmount = pct;
        }

        public float GetHealthPercentage() {
            //return (float)health / healthMax;
            if(_playerHealth == null)
                return 0.0f;

            return (float)_playerHealth.currentHealth / _playerHealth.startingHealth;
        }

        private void LateUpdate() {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }

    }
}
