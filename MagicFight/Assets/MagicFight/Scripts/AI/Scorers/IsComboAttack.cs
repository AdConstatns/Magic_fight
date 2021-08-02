namespace AmazingTeam.MagicFight
{
    using Apex.AI;
    using Apex.Serialization;

    public class IsComboAttack : ContextualScorerBase {

        [ApexSerialization(defaultValue = false)]
        public bool not = false;  
        public override float Score(IAIContext context) {

            var c = (SurvivalContext)context;

            var player = c.player;

            // Test for negative. i.e Player has no combo Attack. i.e no multiple attack.
            if (!player.IsComboAttack) {
                return this.not ? this.score : 0f;
            }

            return this.not ? 0f : this.score;
        }
    }
}
