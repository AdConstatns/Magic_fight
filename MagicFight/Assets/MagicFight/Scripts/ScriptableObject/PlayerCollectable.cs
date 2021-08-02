namespace AmazingTeam.MagicFight
{   
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName ="PlayerCollectable", menuName="MagicFight/ScriptableObject/PlayerCollectable")]
    public class PlayerCollectable : ScriptableObject
    {
        // Currently collected Powerup
        public int Current = 0;
        // Maximum collected Powerup
        public int Maximum = 0;      

        // Start is called before the first frame update
        void Start()
        {
        
        }       
    }
}
