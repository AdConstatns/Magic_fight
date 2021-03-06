namespace AmazingTeam.MagicFight
{
    using Apex.AI;
    using Apex.Serialization;

    public class HasMaximumPowerup : ContextualScorerBase {
        [ApexSerialization(defaultValue = false)]
        public bool not = false;

        public override float Score(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;

            // Check for the maximum powerup collected.
            if (player.PowerUpCount < player.CollectedPowerup.Maximum && c.PlayersInsideStrike.Count <= 0) {
                return this.not ? this.score : 0f;
            }

            return this.not ? 0f : this.score;
        }
    }
}
