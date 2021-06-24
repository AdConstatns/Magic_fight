#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.AI.Visualization;
    using UnityEngine;

    public class TargetSelectionVisualizerDebugComponent : ContextGizmoVisualizerComponent
    {
        public Color gizmosColor = Color.red;

        [Range(0.5f, 4f)]
        public float sphereSize = 2f;

        protected override void DrawGizmos(IAIContext context)
        {
            var c = (ExampleContext)context;
            var attackTarget = c.attackTarget;
            if (attackTarget != null)
            {
                Gizmos.color = this.gizmosColor;
                Gizmos.DrawWireSphere(attackTarget.transform.position, this.sphereSize);
            }
        }
    }
}