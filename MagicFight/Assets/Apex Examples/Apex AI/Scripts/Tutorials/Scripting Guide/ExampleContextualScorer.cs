#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;

    public sealed class ExampleContextualScorer : ContextualScorerBase
    {
        public override float Score(IAIContext context)
        {
            // Cast the provided context to your concrete context type
            var concreteContext = (ExampleContext)context;

            // Put scoring logic here
            return this.score;
        }
    }
}