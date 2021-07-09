namespace AmazingTeam.MagicFight {
  
    using Apex.Steering.Behaviours;  
    using Apex.AI;
    public class FireAtMultipleAttackPlayer : ActionBase { 
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;
            var players = c.AIPlayers;            

            foreach (var otherPlayer in players) {

                c.player.attackTarget = otherPlayer;

                if (c.player.attackTarget == null)
                    return;

                //Bring the hurt to the player.      
                //otherPlayer.TakeDamage((int)AbilityType.Fire);
                otherPlayer.TakeDamage((int)AbilityType.Fire * c.player.currentFires);    
                // Show the Fire Effect
                otherPlayer.GetComponent<Player>().ShowFireAttackEffect();              
            }           
           
            // Fire powerup is reset after attack.
            player.UseFire(AbilityMode.Multiple);           
        }
    }
}
