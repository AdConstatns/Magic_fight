using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight
{
    public class SceneArenaSettings : MonoBehaviour
    {
        public MenuManager MenuManagerPrefab;

        // Start is called before the first frame update
        void Awake() {
            Time.timeScale = 1;
            PlayerPrefs.SetString("Scene", "MagicFight_2(Arena)");
            Instantiate(MenuManagerPrefab);
        } 
    }
}
