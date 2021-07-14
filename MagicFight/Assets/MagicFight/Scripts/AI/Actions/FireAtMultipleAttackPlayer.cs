namespace AmazingTeam.MagicFight {   
    
    using Apex.AI;
    using UnityEngine;
    public class FireAtMultipleAttackPlayer : ActionBase { 
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;
            var players = c.AIPlayers;
            var damage = c.player.GetComponentInChildren<PlayerFire>().damage;

            foreach (var otherPlayer in players) {

                c.player.attackTarget = otherPlayer;

                if (c.player.attackTarget == null)
                    return;

                // Stop the player before getting hurt.
                otherPlayer.GetComponent<PlayerAIMovement>().StopWander();

                //Bring the hurt to the player.
                otherPlayer.TakeDamage(damage * c.player.currentFires);

                // Show the Fire Effect
                otherPlayer.GetComponent<Player>().ShowFireAttackEffect();               
            }           
            // Fire powerup is reset after attack.
            player.UseFire(AbilityMode.Multiple);           
        }
    }
}
