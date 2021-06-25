namespace AmazingTeam.MagicFight {
    using Apex.AI;
    using UnityEngine;

    public class LineOfSightToClosestPlayer : CustomScorer<Vector3> {
        public override float Score(IAIContext context, Vector3 position) {
            var c = (SurvivalContext)context;
            var player = c.player;

            //To be a bit more precise, we check line of sight as seen from the tip of the gun
            position = position + player.gunTipOffset;

            var players = c.players;
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

            var dir = (nearest - position);
            var range = dir.magnitude;
            var ray = new Ray(position + Vector3.up, dir);

            if (!Physics.Raycast(ray, range, Layers.cover)) {
                return this.score;
            }

            return 0f;
        }
    }
}
