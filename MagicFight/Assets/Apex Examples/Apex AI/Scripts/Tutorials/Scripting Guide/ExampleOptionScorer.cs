#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using UnityEngine;

    public sealed class ExampleOptionScorer : OptionScorerBase<Vector3>
    {
        public override float Score(IAIContext context, Vector3 data)
        {
            /* Put logic here */

            return 1f; // return some calculated score
        }
    }
}