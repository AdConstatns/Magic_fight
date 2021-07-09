namespace AmazingTeam.MagicFight {
    using Apex.AI;

    public class ThunderAtMultipleAttackPlayer : ActionBase {
        public override void Execute(IAIContext context) {
            var c = (SurvivalContext)context;

            var player = c.player;
            var players = c.AIPlayers;

            foreach (var otherPlayer in players) {

                c.player.attackTarget = otherPlayer;

                if (c.player.attackTarget == null)
                    return;

                    //Bring the hurt to the player.
                    //otherPlayer.TakeDamage((int)AbilityType.Thunder);
                    otherPlayer.TakeDamage((int)AbilityType.Thunder * c.player.currentThunders);

                    // Show the Thunder Effect
                    otherPlayer.GetComponent<Player>().ShowAttackThunderEffect();                   
            }          
            // Thunder powerup is decrease after attack.
            // Ability Mode multiple will use all the power accumulated till now.
            player.UseThunder(AbilityMode.Multiple);          

        }
    }
}
