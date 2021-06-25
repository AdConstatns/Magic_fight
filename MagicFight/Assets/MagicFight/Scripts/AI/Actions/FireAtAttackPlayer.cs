namespace AmazingTeam.MagicFight {
    using Apex.AI;

    public sealed class FireAtAttackPlayer : ActionBase {
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;

            var attackTarget = player.attackTarget;

            if (attackTarget == null) {
                player.StopFiring();

                // no valid attack target to attack -> so return
                return;
            }

            // Issue a 'fire at' command against the entity's current attack target
            player.StartFiring();
        }
    }
}