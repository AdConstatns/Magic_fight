namespace AmazingTeam.MagicFight {
    using Apex.AI;
    using Apex.Serialization;
    public class SetBestAttackPlayer : ActionWithOptions<LivingEntity> {
        [ApexSerialization(defaultValue = 0.0f), FriendlyName("Target Name", "Name of the target to attack")]
        public string AttackTargetName = string.Empty;

        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;
            var player = c.player;

            var players = c.players;

            var best = this.GetBest(context, players);
            if (best != null) {
                // Set the attack target
                player.attackTarget = best;

                AttackTargetName = player.attackTarget.name;
            }
        }
    }
}
