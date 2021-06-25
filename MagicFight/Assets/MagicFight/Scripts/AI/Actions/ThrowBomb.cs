﻿namespace AmazingTeam.MagicFight
{
    using Apex.AI;

    public sealed class ThrowBomb : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (SurvivalContext)context;
            
            var player = c.player;

            player.ThrowBomb();
        }
    }
}