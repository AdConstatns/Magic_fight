namespace AmazingTeam.MagicFight {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SettingsMenu : SimpleMenu<SettingsMenu> {
        // Close Button
        public override void OnBackPressed() {
            // Close the Current Menu    
            Hide();
        }
    }
}
