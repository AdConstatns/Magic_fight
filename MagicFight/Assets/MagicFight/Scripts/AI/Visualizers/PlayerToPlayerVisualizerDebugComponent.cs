namespace AmazingTeam.MagicFight {
    using System;
    using System.Collections.Generic;
    using Apex.AI;
    using Apex.AI.Components;
    using Apex.AI.Visualization;
    using UnityEngine;
    public class PlayerToPlayerVisualizerDebugComponent : ContextGizmoVisualizerComponent {

        public Color color = Color.green;
        protected override void DrawGizmos(IAIContext context) {
            var ctx = (SurvivalContext)context;

            Gizmos.color = color;
            var playerPos = ctx.player.position;

            var players = ctx.Players;
            var count = players.Count;
            for (int i = 0; i < count; i++) {
                var otherPlayers = players[i];
                
                Gizmos.DrawLine(playerPos, otherPlayers.position);
                Gizmos.DrawSphere(otherPlayers.position, 1f);
            }
        }

        protected override void GetContextsToVisualize(List<IAIContext> contextsBuffer, Guid relevantAIId) {
            var players = EntityManager.instance.players;
            if (players != null) {
                var count = players.Count;
                for (int i = 0; i < count; i++) {
                    contextsBuffer.Add(((IContextProvider)players[i]).GetContext(relevantAIId));
                }
            }
        }


    }
}