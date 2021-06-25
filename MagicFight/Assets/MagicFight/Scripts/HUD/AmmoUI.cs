namespace AmazingTeam.MagicFight
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class AmmoUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;                      

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
          
        }

        private void Start() {
            SetAmmo(0);
        }

        public void SetAmmo(int currentAmmo)
        {
            if(_text != null)
            _text.SetText(currentAmmo.ToString());
        }
    }
}