using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YsoCorp.GameUtils;

namespace AmazingTeam.MagicFight
{
    public class YSOCorpAnalytics : MonoBehaviour
    {
        #region Variables
        public static YSOCorpAnalytics Instance { get; private set; }
        public static int Level
        {
            get => PlayerPrefs.GetInt("Level", 0);
            set => PlayerPrefs.SetInt("Level", value);
        }
        #endregion

        #region UnityMethods
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GameStart();
        }
        #endregion

        #region Methods
        public void GameStart()
        {
            YCManager.instance.OnGameStarted(Level);

            Debug.Log("YSO: start");
        }

     public void GameFinish(bool win)
        {
            YCManager.instance.OnGameFinished(win);

            Debug.Log($"YSO: end {win}");
        }
        #endregion
    }
}
