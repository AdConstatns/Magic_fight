    
/// <summary>
/// Author Tholkappiyan
/// </summary>
namespace AmazingTeam.MagicFight {
    using Apex.AI;
    using Apex.Serialization;

    public class HasPowerups : ContextualScorerBase {

        [ApexSerialization(defaultValue = false)]
        public bool not = false;

        public override float Score(IAIContext context) {
            var c = (SurvivalContext)context;

            if (c.powerups.Count <= 0) {
                return this.not ? this.score : 0f;
            }

            return this.not ? 0f : this.score;
        }
    }
}
