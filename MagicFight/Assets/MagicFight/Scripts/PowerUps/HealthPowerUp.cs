namespace AmazingTeam.MagicFight
{
    /// <summary>
    /// A health power-up, i.e. a band aid.
    /// </summary>
    /// <seealso cref="AmazingTeam.MagicFight.PowerUp" />
    public sealed class HealthPowerUp : PowerUp
    {
        protected override void OnPickup(Player p)
        {
            p.AddBandAid(1);
        }
    }
}
