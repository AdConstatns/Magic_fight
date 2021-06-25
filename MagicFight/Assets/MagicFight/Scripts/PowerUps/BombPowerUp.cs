namespace AmazingTeam.MagicFight
{
    /// <summary>
    /// A Bomb power-up
    /// </summary>
    /// <seealso cref="AmazingTeam.MagicFight.PowerUp" />
    public sealed class BombPowerUp : PowerUp
    {
        protected override void OnPickup(Player p)
        {
            p.AddBombs(1);
        }
   }
}
