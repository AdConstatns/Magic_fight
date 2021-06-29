using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight
{
    public class FirePowerUp : PowerUp {
        protected override void OnPickup(Player p) {

            p.AddFire(1);
        }
    }
}
