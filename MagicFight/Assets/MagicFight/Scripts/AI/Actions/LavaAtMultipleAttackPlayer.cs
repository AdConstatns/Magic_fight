namespace AmazingTeam.MagicFight {
    using Apex.AI;
    public class LavaAtMultipleAttackPlayer : ActionBase {
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;
            var players = c.AIPlayers;
            var damage = c.player.GetComponentInChildren<PlayerLava>().damage;

            foreach (var otherPlayer in players) {

                c.player.attackTarget = otherPlayer;

                if (c.player.attackTarget == null)
                    return;

                // Stop the player before getting hurt.
                otherPlayer.GetComponent<PlayerAIMovement>().StopWander();

                //Bring the hurt to the player.                    
                otherPlayer.TakeDamage( damage * c.player.currentLavas);

                // Show the Lava Effect
                otherPlayer.GetComponent<Player>().ShowLavaAttackEffect();               
            }
           
            // Thunder powerup is decrease after attack.
            player.UseLava(AbilityMode.Multiple);
        }
    }
}
