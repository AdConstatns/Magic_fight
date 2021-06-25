namespace AmazingTeam.MagicFight
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class BombUI : MonoBehaviour
    {
        private TextMeshProUGUI  _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
          
        }

        private void Start() {
            SetBombs(0);
        }

        public void SetBombs(int currentBombs)
        {
            if(_text != null)
            _text.SetText(currentBombs.ToString());
        }
    }
}