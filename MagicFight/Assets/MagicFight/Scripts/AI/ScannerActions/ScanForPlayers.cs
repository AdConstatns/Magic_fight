namespace AmazingTeam.MagicFight {

    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;

    [FriendlyName("Scan for Players", "Scans the game world for Players and stores them in the context")]
    public sealed class ScanForPlayers : ActionBase {

        [ApexSerialization(defaultValue = 0.0f), FriendlyName("Player Count", "How large a radius points are sampled in, in a square with the entity in the center")]
        public int Count = 0;

        [ApexSerialization(defaultValue = 0.0f), FriendlyName("Strike Area Player Count", "How large a radius points are sampled in, in a square with the entity in the center")]
        public int StrikeAreaPlayerCount = 0;

        public override void Execute(IAIContext context) {
          
            var c = (SurvivalContext)context;

            var player = c.player;
            c.Players.Clear();

            // Use OverlapSphere for getting all relevant colliders within scan range, filtered by the scanning layer
            var colliders = Physics.OverlapSphere(player.position, player.scanRange, Layers.players);
          
            
            for (int i = 0; i < colliders.Length; i++) {
                var col = colliders[i];

                if (col.isTrigger) {
                    continue;
                }

                // Scan for the AI Player.
                var playerToAdd = EntityManager.instance.GetLivingEntityByGameObject(col.gameObject);

                if (playerToAdd == null) {
                    // Scan for the Non AI Player.
                    playerToAdd =  col.gameObject.GetComponent<Player>();
                    if(playerToAdd is null)
                        continue;
                }

                // Avoiding adding the own gameobject.
                if(c.player.gameObject != col.gameObject) {
                    c.Players.Add(playerToAdd);                  
                }               
            }
            // Visually display the Player Count
            Count = c.Players.Count;
          
            c.PlayersInsideStrike.Clear();

            // Use OverlapSphere for getting all relevant colliders within Strike range, filtered by the scanning layer
            var strikeAreacolliders = Physics.OverlapSphere(player.position, player.StrikeRange, Layers.players);

            for (int i = 0; i < strikeAreacolliders.Length; i++) {
                var col = strikeAreacolliders[i];

                if (col.isTrigger) {
                    continue;
                }

                // Scan for the AI Player.
                var playerToAdd = EntityManager.instance.GetLivingEntityByGameObject(col.gameObject);

                if (playerToAdd == null) {
                    // Scan for the Non AI Player.
                    playerToAdd = col.gameObject.GetComponent<Player>();
                    if (playerToAdd is null)
                        continue;
                }

                // Avoiding adding the own gameobject.
                if (c.player.gameObject != col.gameObject) {

                    //calculate the Angle between player and other player.
                    // 1. calculate the direction.
                    // direction = Destination - Source.
                    var dir = (col.gameObject.transform.position - c.player.gameObject.transform.position).normalized;

                    if (Vector3.Angle(c.player.gameObject.transform.forward, dir) < 46) {
                        c.PlayersInsideStrike.Add(playerToAdd);
                    } else {
                        c.PlayersInsideStrike.Remove(playerToAdd);
                    }

                    // 2. calculate the angle using the inverse tangent method.
                    // Return Angle in Radians.
                    //var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg - 90;
                    //if (angle < 45 && angle > -45)
                    //    c.PlayersInsideStrike.Add(playerToAdd);
                    //else
                    //    c.PlayersInsideStrike.Remove(playerToAdd);
                }
            }

            StrikeAreaPlayerCount = c.PlayersInsideStrike.Count;
        }
    }
}


