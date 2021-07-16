using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight
{
    [CreateAssetMenu(fileName ="MaxCollectable", menuName="MagicFight/ScriptableObject/MaxCollectable")]
    public class MaxCollectableCount : ScriptableObject
    {
        public int MaxCollectable = 0;

        // Start is called before the first frame update
        void Start()
        {
        
        }       
    }
}
