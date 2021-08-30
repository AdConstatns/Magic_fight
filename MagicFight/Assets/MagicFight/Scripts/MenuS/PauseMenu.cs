namespace AmazingTeam.MagicFight {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PauseMenu : SimpleMenu<PauseMenu> {
        public override void OnBackPressed() {
            ResetTimeScale();
            Hide();
        }

        public void OnQuitPressed() {
            
            ResetTimeScale();
            Hide();
            Destroy(this.gameObject); // This menu does not automatically destroy itself

            GameMenu.Hide();
            MainMenu.Show();

        }

        private void ResetTimeScale() {
            Time.timeScale = 1.0f;
        }
    }
}
