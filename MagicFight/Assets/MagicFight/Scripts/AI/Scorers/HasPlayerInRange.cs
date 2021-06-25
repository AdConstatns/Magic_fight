namespace AmazingTeam.MagicFight {

    using Apex.AI;
    using Apex.Serialization;

    public class HasPlayerInRange : ContextualScorerBase {
        [ApexSerialization(defaultValue = 0f)]
        public float range = 0f;

        [ApexSerialization(defaultValue = false)]
        public bool not;      

        public override float Score(IAIContext context) {
            var c = (SurvivalContext)context;

            var players = c.players;

            var count = players.Count;

            for (int i = 0; i < count; i++) {
                var otherPlayers = players[i];
                var sqrDist = (otherPlayers.position - c.player.position).sqrMagnitude;
              
                if (sqrDist <= range * range) {
                    if (not) {
                        return 0f;
                    }

                    return this.score;
                }
            }

            if (not) {
                return this.score;
            }

            return 0f;
        }
    }
}
