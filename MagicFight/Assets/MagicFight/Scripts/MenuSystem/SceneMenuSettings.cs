namespace AmazingTeam.MagicFight {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SceneMenuSettings : MonoBehaviour {
        public MenuManager MenuManagerPrefab;

        // Start is called before the first frame update
        void Awake() {
            PlayerPrefs.SetString("Scene", "Menu");
            PlayerPrefs.SetInt("GameOver", 0);
            Instantiate(MenuManagerPrefab);
        }

        // Update is called once per frame
        void Update() {

        }
    }

}
