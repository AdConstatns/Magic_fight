namespace AmazingTeam.MagicFight
{
    using UnityEngine;

    /// <summary>
    /// A facade for updating the HUD
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class HUDState : MonoBehaviour
    {
        private static AmmoUI _ammo;
        private static BandAidUI _bandaids;
        private static BombUI _bombs;
        private static HealthUI _health;
        private static ScoreUI _score;

        public static void UpdateAmmo(int currentAmmo) {
            _ammo.SetAmmo(currentAmmo);
        }

        public static void UpdateBandAids(int currentBandAids) {
            if(_bandaids != null)
            _bandaids.SetBandAids(currentBandAids);
        }

        public static void UpdateBombs(int currentBombs) {
            _bombs.SetBombs(currentBombs);
        }

        public static void UpdateHealth(int currentHealth) {
            if (_health != null)
            _health.SetHealth(currentHealth);
        }

        public static void UpdateScore(int score)
        {
            _score.AddScore(score);
        }

        private void Awake()
        {
#if UNITY_EDITOR
            Debug.Log("<color=yellow><b>HUD Initialization (Health, Bandaids, Score)</b></color>");
#endif
             _ammo = GetComponentInChildren<AmmoUI>();
            _bombs = GetComponentInChildren<BombUI>();
            _bandaids = GetComponentInChildren<BandAidUI>();
            _health = GetComponentInChildren<HealthUI>();
            _score = GetComponentInChildren<ScoreUI>();
        }
    }
}
