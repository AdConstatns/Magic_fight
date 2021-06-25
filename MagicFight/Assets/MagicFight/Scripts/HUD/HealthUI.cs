namespace AmazingTeam.MagicFight
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class HealthUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
           
        }
        private void Start() {
            SetHealth(0);
        }

        public void SetHealth(int currentHealth)
        {
#if UNITY_EDITOR
            Debug.Log($"<color=green><b>Set Health: {currentHealth} </b></color>");
#endif

            if(_text != null)             
                _text.SetText(currentHealth.ToString());
        }
    }
}