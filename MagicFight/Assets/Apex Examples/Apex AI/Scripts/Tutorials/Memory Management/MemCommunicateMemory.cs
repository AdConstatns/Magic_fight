#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.Serialization;

    public sealed class ExampleCommunicateMemory : ActionBase
    {
        [ApexSerialization]
        public float maxCommunicationRange = 25f;

        public override void Execute(IAIContext context)
        {
            var c = (MemContext)context;

            var entity = c.entity;

            // get all observations
            var observations = entity.memory.allObservations;
            var count = observations.Count;
            if (count == 0)
            {
                // no observations available
                return;
            }

            // iterate through all observations - in order to figure out which allies should receive 'my' observations.
            var rangeSqr = this.maxCommunicationRange * this.maxCommunicationRange;
            var allies = MemEntityManager.instance.GetAllies(entity);
            int allyCount = allies.Count;
            for (int i = 0; i < allyCount; i++)
            {
                if ((allies[i].position - entity.position).sqrMagnitude > rangeSqr)
                {
                    // other entity is out of communication range
                    continue;
                }

                allies[i].ReceiveCommunicatedMemory(observations);
            }
        }
    }
}