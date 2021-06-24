#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.AI.Visualization;
    using UnityEngine;

    public sealed class ObservationsVisualizerDebugComponent : ContextGizmoVisualizerComponent
    {
        public Color gizmosColor = Color.yellow;

        protected override void DrawGizmos(IAIContext context)
        {
            var c = (MemContext)context;
            var pos = c.entity.position;
            var observations = c.entity.memory.allObservations;
            var count = observations.Count;

            Gizmos.color = this.gizmosColor;
            for (int i = 0; i < count; i++)
            {
                Gizmos.DrawLine(pos, observations[i].position);
            }
        }
    }
}