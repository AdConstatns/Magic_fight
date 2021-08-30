namespace AmazingTeam.MagicFight {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameMenu : SimpleMenu<GameMenu> {
        public Text _Score;
        public Player _player;

        public void Start() {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _player = player.GetComponent<Player>();
        }

        public override void OnBackPressed() {
            Time.timeScale = 0.0f;
            PauseMenu.Show();
        }

        // Update is called once per frame
        void Update() {          

            if (_player.currentHealth <= 0 || PlayerPrefs.GetInt("GameOver") == 1) {
                GameOverMenu.Show();
            }
        }
    }
}
