namespace AmazingTeam.MagicFight
{
    using TMPro;
    using Apex;
    using Apex.Services;
    using UnityEngine;

    public class MaxPowerupUI : MonoBehaviour {

        private TextMeshProUGUI _text;

        private void Awake() {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start() {
            _text.enabled = true;
        }

        public void Display(bool enable) {
            if (_text != null) {
                if (enable)
                    _text.enabled = true;
                else
                    _text.enabled = false;
            }          
        }        
    }
}
