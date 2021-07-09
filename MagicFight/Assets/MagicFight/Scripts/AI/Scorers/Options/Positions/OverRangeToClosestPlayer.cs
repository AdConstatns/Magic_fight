namespace AmazingTeam.MagicFight {
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;
    public class OverRangeToClosestPlayer : CustomScorer<Vector3> {
        [ApexSerialization(defaultValue = 14f)]
        public float desiredRange = 14f;

        public override float Score(IAIContext context, Vector3 position) {
            var c = (SurvivalContext)context;
            var player = c.player;

            var players = c.AIPlayers;
            var count = players.Count;
            if (count == 0) {
                return 0f;
            }

            var nearest = Vector3.zero;
            var shortest = float.MaxValue;

            for (int i = 0; i < count; i++) {
                var otherPlayer = players[i];

                var distance = (player.position - otherPlayer.position).sqrMagnitude;
                if (distance < shortest) {
                    shortest = distance;
                    nearest = otherPlayer.position;
                }
            }

            var range = (position - nearest).magnitude;

            if (range > desiredRange) {
                return this.score;
            } else {
                return 0;
            }
        }
    }
}
