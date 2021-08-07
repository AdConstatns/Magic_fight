namespace AmazingTeam.MagicFight
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using TMPro;

    public class GroundUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake() {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start() {
            SetGrounds(0);
        }

        public void SetGrounds(int currentGrounds) {
            if (_text != null)
                _text.SetText(currentGrounds.ToString());
        }
    }
}
