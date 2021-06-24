#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.Examples.AI.Helpers;
    using Apex.Serialization;
    using UnityEngine;

    public sealed class ScanForObservations : ActionBase
    {
        [ApexSerialization, FriendlyName("Scan Range", "The scanning range used for the OverlapSphere check, i.e. only colliders within this range are scanned.")]
        public float scanRange = 10f;

        [ApexSerialization, FriendlyName("Observations Layer", "The layer in which the scannable units reside, i.e. only game objects in this layer can be scanned.")]
        public LayerMask observationsLayer;

        public override void Execute(IAIContext context)
        {
            var c = (ExampleContext)context;

            // Use Physics.OverlapSphere to get all relevant colliders within a set range
            var colliders = Physics.OverlapSphere(c.self.transform.position, this.scanRange, this.observationsLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                var col = colliders[i];
                if (object.ReferenceEquals(col.gameObject, c.self))
                {
                    // do not scan "self"
                    continue;
                }

                // GetIsVisible is basically just raycasting from self to col to see if any blocks are hit
                var visibility = Utilities.IsVisible(c.self.transform.position, col.transform.position, this.scanRange);

                // create the observation object with the visibility and recorded timestamp
                var observation = new ExampleObservation(col.gameObject, visibility, Time.time);
                c.AddOrUpdateObservation(observation);
            }
        }
    }
}