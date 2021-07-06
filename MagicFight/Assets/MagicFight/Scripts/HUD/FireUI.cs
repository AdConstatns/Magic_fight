namespace AmazingTeam.MagicFight
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class FireUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake() {
            _text = GetComponentInChildren<TextMeshProUGUI>();

        }

        private void Start() {
            SetFires(0);
        }

        public void SetFires(int currentFires) {
            if (_text != null)
                _text.SetText(currentFires.ToString());
        }
    }
}
