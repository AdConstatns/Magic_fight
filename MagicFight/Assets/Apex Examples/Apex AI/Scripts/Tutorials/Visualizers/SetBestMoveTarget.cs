#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using UnityEngine;

    public sealed class SetBestMoveTarget : ActionWithOptions<Vector3>
    {
        public override void Execute(IAIContext context)
        {
            var c = (ExampleContext)context;

            var best = this.GetBest(context, c.sampledPositions);
            if (best.sqrMagnitude == 0f)
            {
                return;
            }

            c.moveTarget = best;
        }
    }
}