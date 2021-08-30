namespace AmazingTeam.MagicFight {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioMenu : SimpleMenu<AudioMenu> {
        // Close Button
        public override void OnBackPressed() {
            // Close the Current Menu    
            Hide();
        }
    }

}


