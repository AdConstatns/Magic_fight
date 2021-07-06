namespace AmazingTeam.MagicFight {
    using Apex.AI;
    public class FireAtMultipleAttackPlayer : ActionBase {
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;
            var players = c.players;            

            foreach (var otherPlayer in players) {

                c.player.attackTarget = otherPlayer;

                if(c.player.attackTarget != null)
                //Bring the hurt to the player.                       
                otherPlayer.TakeDamage(10);

                // Show the Fire Effect
                otherPlayer.GetComponent<Player>().ShowFireAttackEffect();
            }
           
            // After shooting all the players make it false.
            player.IsPlayerShooting = false;
            // Fire powerup is reset after attack.
            player.UseFire();
           
            
        }
    }
}
