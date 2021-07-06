namespace AmazingTeam.MagicFight { 

    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class ThunderUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake() {
            _text = GetComponentInChildren<TextMeshProUGUI>();

        }

        private void Start() {
            SetThunders(0);
        }

        public void SetThunders(int currentThunders) {
            if (_text != null)
                _text.SetText(currentThunders.ToString());
        }
    }
}
