namespace AmazingTeam.MagicFight
{
    using Apex.AI;

    public  class MoveToClosestPlayer : ActionWithOptions<LivingEntity> {
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var best = this.GetBest(context, c.Players);
            if (best == null) {
                // no valid position found
                return;
            }

            c.player.MoveTo(best.position);
        }
    }
}
