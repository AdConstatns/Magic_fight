#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using UnityEngine;

    [FriendlyName("Tutorial Set Best Attack Target")]
    public sealed class SetBestAttackTarget : ActionWithOptions<GameObject>
    {
        public override void Execute(IAIContext context)
        {
            var c = (ExampleContext)context;

            // get all observations from memory
            var observations = c.observations;
            if (observations.Count == 0)
            {
                return;
            }

            var best = this.GetBest(context, observations);
            if (best != null)
            {
                // set the attack target through method, property, field or any way desired
                c.attackTarget = best;
            }
        }
    }
}