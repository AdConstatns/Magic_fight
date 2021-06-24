#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;

    public sealed class MemMemoryCleanup : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (MemContext)context;

            var observations = c.entity.memory.allObservations;
            var count = observations.Count;
            if (count == 0)
            {
                return;
            }

            // must loop backwards to enable removing elements from the list while iterating through it
            for (int i = count - 1; i >= 0; i--)
            {
                var obs = observations[i];
                if (obs != null)
                {
                    continue;
                }

                observations.RemoveAt(i);
            }
        }
    }
}