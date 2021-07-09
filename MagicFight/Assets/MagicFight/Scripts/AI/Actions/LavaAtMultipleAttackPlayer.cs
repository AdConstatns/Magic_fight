namespace AmazingTeam.MagicFight {
    using Apex.AI;
    public class LavaAtMultipleAttackPlayer : ActionBase {
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;
            var players = c.AIPlayers;

            foreach (var otherPlayer in players) {

                c.player.attackTarget = otherPlayer;

                if (c.player.attackTarget == null)
                    return;

                    //Bring the hurt to the player.     
                    //otherPlayer.TakeDamage((int)AbilityType.Lava);
                    otherPlayer.TakeDamage((int)AbilityType.Lava * c.player.currentLavas);

                    // Show the Thunder Effect
                    otherPlayer.GetComponent<Player>().ShowLavaAttackEffect();
               
            }
           
            // Thunder powerup is decrease after attack.
            player.UseLava(AbilityMode.Multiple);
        }
    }
}
