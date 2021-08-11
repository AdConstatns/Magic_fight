namespace AmazingTeam.MagicFight
{
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// An AI scorer for evaluating whether the entity has an attack target or not.
    /// </summary>
    public class HasMultipleAttackTargets : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Not", "If set to true, the logic is reversed, e.g. used if the desire is to score when there is no attack target (in this case)")]
        public bool not = false;

        public override float Score(IAIContext context) {
            var c = (SurvivalContext)context;              

            if (c.PlayersInsideStrike.Count <= 0) {  // c.Players.Count
                // No Players available make the input shooting false.
                c.player.IsPlayerShooting = false;
                return this.not ? this.score : 0f;
            }

            return this.not ? 0f : this.score;
        }
    }
}
