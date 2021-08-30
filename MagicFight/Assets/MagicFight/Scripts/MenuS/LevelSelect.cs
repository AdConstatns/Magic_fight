namespace AmazingTeam.MagicFight {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class LevelSelect : SimpleMenu<LevelSelect> {
        public void OnAlphabetPressed(string _levelname) {
            //Go To Scene A
            SceneManager.LoadScene(_levelname);
        }

        public override void OnBackPressed() {
            MainMenu.Show();
        }

        public void OnAudioButtonPressed() {
            AudioMenu.Show();
        }

        public void OnSettingsButtonPressed() {
            SettingsMenu.Show();
        }
    }
}
