namespace AmazingTeam.MagicFight {
    using Apex;
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;

    public class OverRangeToAnyPlayer : CustomScorer<Vector3> {
        [ApexSerialization(defaultValue = 14f)]
        public float desiredRange = 14f;

        public override float Score(IAIContext context, Vector3 position) {
            var c = (SurvivalContext)context;
            var player = c.player;

            var players = c.Players;
            var count = players.Count;
            if (count == 0) {
                return 0f;
            }

            var sqrDesiredRange = desiredRange * desiredRange;
            for (int i = 0; i < count; i++) {
                var otherPlayers = players[i];

                var dirPlayerToOtherPlayer = (otherPlayers.position - player.position).OnlyXZ();
                var dirPositionToOtherPlayer = (otherPlayers.position - position).OnlyXZ();

                //all positions behind the enemy or closer than the desired range are not of interest
                if (Vector3.Dot(dirPlayerToOtherPlayer, dirPositionToOtherPlayer) < 0f || dirPositionToOtherPlayer.sqrMagnitude < sqrDesiredRange) {
                    return 0f;
                }
            }

            return this.score;
        }
    }
}
