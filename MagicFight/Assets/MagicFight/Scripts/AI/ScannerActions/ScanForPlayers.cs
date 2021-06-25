namespace AmazingTeam.MagicFight {

    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;

    [FriendlyName("Scan for Players", "Scans the game world for Players and stores them in the context")]
    public sealed class ScanForPlayers : ActionBase {

        [ApexSerialization(defaultValue = 0.0f), FriendlyName("Player Count", "How large a radius points are sampled in, in a square with the entity in the center")]
        public int Count = 0;   

        public override void Execute(IAIContext context) {
            // Player Count Reset
            //Count = 0;

            var c = (SurvivalContext)context;

            var player = c.player;
            c.players.Clear();

            // Use OverlapSphere for getting all relevant colliders within scan range, filtered by the scanning layer
            var colliders = Physics.OverlapSphere(player.position, player.scanRange, Layers.players);
            
            for (int i = 0; i < colliders.Length; i++) {
                var col = colliders[i];

                if (col.isTrigger) {
                    continue;
                }

                var playerToAdd = EntityManager.instance.GetLivingEntityByGameObject(col.gameObject);

                if (playerToAdd == null) {
                    continue;
                }

                // Avoiding adding the own gameobject.
                if(c.player.gameObject != col.gameObject) {
                    c.players.Add(playerToAdd);                  
                }               
            }
            // Visually display the Player Count
            Count = c.players.Count;
        }
    }
}


