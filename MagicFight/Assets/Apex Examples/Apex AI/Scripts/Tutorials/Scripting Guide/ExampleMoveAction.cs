#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using UnityEngine;

    public sealed class ExampleMoveAction : ActionWithOptions<Vector3>
    {
        public override void Execute(IAIContext context)
        {
            var c = (ExampleContext)context;

            var best = this.GetBest(c, c.sampledPositions);
            // Move to the best position..
        }
    }
}