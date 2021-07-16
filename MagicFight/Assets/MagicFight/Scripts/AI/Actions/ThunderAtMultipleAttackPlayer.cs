namespace AmazingTeam.MagicFight {
    using Apex.AI;

    public class ThunderAtMultipleAttackPlayer : ActionBase {
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;
            var players = c.Players;
            var damage = c.player.GetComponentInChildren<PlayerThunder>().damage;

            foreach (var otherPlayer in players) {

                c.player.attackTarget = otherPlayer;

                if (c.player.attackTarget == null)
                    return;

                // Stop the player AI when getting hurt.
                if (otherPlayer.CompareTag(Tags.PlayerAI))
                    otherPlayer.GetComponent<PlayerAIMovement>().StopWander();
                else
                    // Stop the Player when getting hurt
                    otherPlayer.GetComponent<PlayerAIMovement>().Stop();                

                //Bring the hurt to the player.              
                otherPlayer.TakeDamage(damage * c.player.currentThunders);

                // Show the Thunder Effect
                otherPlayer.GetComponent<Player>().ShowAttackThunderEffect();                   
            }          
            // Thunder powerup is decrease after attack.
            // Ability Mode multiple will use all the power accumulated till now.
            player.UseThunder(AbilityMode.Multiple);          

        }
    }
}
