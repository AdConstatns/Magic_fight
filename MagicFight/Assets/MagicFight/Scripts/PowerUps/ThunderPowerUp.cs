using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight
{
    public class ThunderPowerUp : PowerUp
    {
        protected override void OnPickup(Player p) {
            p.AddThunder(1);
        }       
    }
}
