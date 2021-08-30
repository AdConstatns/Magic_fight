using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight
{
    [CreateAssetMenu(fileName = "PlayerKilled", menuName = "MagicFight/ScriptableObject/PlayerKilled")]
    public class PlayerKilled : ScriptableObject
    {
        // Currently collected Powerup
        public int CurrentDeath = 0;
        // Maximum collected Powerup
        public int MaximumDeath = 3;

        // Start is called before the first frame update
        void Awake()
        {
            CurrentDeath = 0;
        }       
    }
}
