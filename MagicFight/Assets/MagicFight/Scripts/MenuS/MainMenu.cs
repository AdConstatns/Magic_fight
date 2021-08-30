namespace AmazingTeam.MagicFight {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MainMenu : SimpleMenu<MainMenu> {
        public PlayerKilled _playerKilled;
        public bool ignoreLevelSelection = false;
        public void OnPlayPressed() {

            if(ignoreLevelSelection)
                //Go To Scene MagicFight_2(Arena)           
                SceneManager.LoadScene("MagicFight_2(Arena)", LoadSceneMode.Single);          
            else
                LevelSelect.Show();
        }

        private void Start() {
            _playerKilled.CurrentDeath = 0;
        }


        public override void OnBackPressed() {
            Application.Quit();
        }
    }
}
