namespace AmazingTeam.MagicFight {
    using Apex.AI;
    public class LavaAtMultipleAttackPlayer : ActionBase {
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;
            var players = c.players;

            foreach (var otherPlayer in players) {

                c.player.attackTarget = otherPlayer;

                if (c.player.attackTarget != null) {
                    //Bring the hurt to the player.                       
                    otherPlayer.TakeDamage(30);

                    // Show the Thunder Effect
                    otherPlayer.GetComponent<Player>().ShowLavaAttackEffect();
                }

            }

            // After shooting all the players make it false.
            player.IsPlayerShooting = false;
            // Thunder powerup is decrease after attack.
            player.UseLava();
        }
    }
}
