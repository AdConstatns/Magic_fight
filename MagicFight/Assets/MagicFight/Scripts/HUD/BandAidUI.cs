namespace AmazingTeam.MagicFight
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class BandAidUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            
        }

        private void Start() {
            SetBandAids(0);
        }

        public void SetBandAids(int currentBandAids)
        {
#if UNITY_EDITOR
            Debug.Log($"<color=green><b>Set BanAids: {currentBandAids} </b></color>");
#endif
            if (_text != null)
                _text.SetText(currentBandAids.ToString());
        }
    }
}