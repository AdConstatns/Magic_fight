namespace AmazingTeam.MagicFight {
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;

    public class ProximityToNearestPlayer : CustomScorer<Vector3> {
        [ApexSerialization(defaultValue = 14f)]
        public float desiredRange = 14f;

        public override float Score(IAIContext context, Vector3 position) {
            var c = (SurvivalContext)context;
            var players = c.AIPlayers;
            var count = players.Count;
            if (count == 0) {
                return 0f;
            }

            var nearest = Vector3.zero;
            var shortest = float.MaxValue;

            for (int i = 0; i < count; i++) {
                var otherPlayer = players[i];

                var distance = (position - otherPlayer.position).sqrMagnitude;
                if (distance < shortest) {
                    shortest = distance;
                    nearest = otherPlayer.position;
                }
            }

            if (nearest.sqrMagnitude == 0f) {
                return 0f;
            }

            var range = (position - nearest).magnitude;
            return Mathf.Max(0f, (this.score - Mathf.Abs(this.desiredRange - range)));
        }
    }
}
