namespace AmazingTeam.MagicFight
{
    using System.Collections.Generic;
    using Apex.AI;
    using UnityEngine;

    public sealed class SurvivalContext : IAIContext
    {
        public SurvivalContext(Player entity)
        {          
            this.player = entity;
            this.Players = new List<LivingEntity>();          
            this.enemies = new List<LivingEntity>();
            this.sampledPositions = new List<Vector3>();
            this.powerups = new List<IEntity>();
        }     

        public Player player
        {
            get;
            private set;
        }

        public List<LivingEntity> Players {
            get;
            private set;
        }     

        public List<LivingEntity> enemies
        {
            get;
            private set;
        }

        public List<IEntity> powerups
        {
            get;
            private set;
        }

        public List<Vector3> sampledPositions
        {
            get;
            private set;
        }
    }
}
