namespace AmazingTeam.MagicFight {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class SceneASettings : MonoBehaviour {

        public MenuManager MenuManagerPrefab;

        // Start is called before the first frame update
        void Awake() {
            PlayerPrefs.SetString("Scene", "MagicFight_2(Villege)");
            Instantiate(MenuManagerPrefab);
        }       
    }

}



