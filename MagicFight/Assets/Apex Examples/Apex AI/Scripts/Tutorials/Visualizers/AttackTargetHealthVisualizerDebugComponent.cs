namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.AI.Visualization;
    using Memory;
    using UnityEngine;

    public sealed class AttackTargetHealthVisualizerDebugComponent : ContextGizmoGUIVisualizerComponent
    {
        public Color gizmosColor = Color.red;

        [Range(0.5f, 4f)]
        public float sphereSize = 2f;

        protected override void DrawGizmos(IAIContext context)
        {
            var c = (AIContext)context;
            var attackTarget = c.entity.attackTarget;
            if (attackTarget != null)
            {
                Gizmos.color = this.gizmosColor;
                Gizmos.DrawWireSphere(attackTarget.position, this.sphereSize);
            }
        }

        protected override void DrawGUI(IAIContext context)
        {
            var c = (AIContext)context;
            var attackTarget = c.entity.attackTarget;
            if (attackTarget != null)
            {
                var health = attackTarget.currentHealth / attackTarget.maxHealth;
                GUI.color = new Color((1f - health), 0f, 0f, 1f);

                var pos = Camera.main.WorldToScreenPoint(attackTarget.position);
                pos.y = Screen.height - pos.y;
                GUI.Label(new Rect(pos.x, pos.y, 60f, 30f), string.Concat("HP: ", health.ToString("F1"), "%"));
            }
        }
    }
}