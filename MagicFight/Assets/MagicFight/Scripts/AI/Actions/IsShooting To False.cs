namespace AmazingTeam.MagicFight {
    using Apex.AI;
    public class IsShootingToFalse : ActionBase {

        public override void Execute(IAIContext context) {

            var c = (SurvivalContext)context;

            var player = c.player;
            
           if( player.currentFires <= 0 && player.currentThunders <=0 && player.currentLavas <= 0) {
                player.IsPlayerShooting = false;
           }    
        }
    }
}
