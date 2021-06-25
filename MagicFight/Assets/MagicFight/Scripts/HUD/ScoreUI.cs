namespace AmazingTeam.MagicFight
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class ScoreUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private int _score;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _score = 0;
            AddScore(0);
        }

        public void AddScore(int score)
        {

#if UNITY_EDITOR
            Debug.Log($"<color=green><b>Set Score: {score} </b></color>");
#endif

            _score += score;
            _text.SetText("Score: " + _score);
        }
    }
}