using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight {
    [AddComponentMenu("MagicFight/Player/PlayerThunder", 7)]
    public class PlayerThunder : MonoBehaviour {
        public int damage = 30;                  // The damage inflicted by each bullet.
        public float timeBetweenBombs = 1f;       // The minimum time between each bomb use.
        public float range = 5f;                  // The bombs explosion range.
        public int startingThunder = 3;
        public Light ThunderLight;
        [SerializeField]
        private int _currentThunder;
        public List<GameObject> Thunders;

        private List<ParticleSystem> _ThunderParticleSystems;

       
        private float _timer;                                    // A timer to determine when to fire.
        private AudioSource _ThunderAudio;                           // Reference to the audio source.

        private float _effectsDisplayTime = 0.2f;                // The explosion display time.
        private float _effectStartTime;

        public int currentThunders {
            get {
                return _currentThunder;
            }

            set {
                _currentThunder = value;
                HUDState.UpdateThunders(_currentThunder);
            }
        }

        public bool canThrowThunder {
            get {
                if (this.currentThunders <= 0) {
                    return false;
                }

                if (_timer < timeBetweenBombs) {
                    return false;
                }

                return true;
            }
        }

        public void ThrowThunder() {
            _timer = 0;

            EnableEffects();

            _ThunderAudio.Play();

            //Find all enemies within the blast range of the bomb
            var colliders = Physics.OverlapSphere(transform.position, range, Layers.players);

            for (int i = 0; i < colliders.Length; i++) {
                var col = colliders[i];

                //Get a reference to the hit enemy and apply the damage
                var player = EntityManager.instance.GetLivingEntityByGameObject(col.gameObject);
                if (player != null) {
                    player.TakeDamage(damage);
                }
            }

            this.currentThunders--;
        }


        public void AddThunder(int amount) {
            this.currentThunders += amount;
        }

        public void UseThunder(AbilityMode mode) {
            //Only Use Thunder if Thunder count is greater than zero.
            if (this.currentThunders <= 0) {
                return;
            }

            if (AbilityMode.Single == mode)
                this.currentThunders--;
            else if (AbilityMode.Multiple == mode)
                this.currentThunders = 0;

        }

        private void Awake() {
            // Set up the references.
            _ThunderAudio = GetComponent<AudioSource>();
        }

        private void Start() {
            this.currentThunders = startingThunder;

            var count = Thunders.Count;

            _ThunderParticleSystems = new List<ParticleSystem>();

            for (int i = 0; i < count; i++) {
                var prefab = Thunders[i];

                var go = GameObject.Instantiate(prefab, this.transform.position + Vector3.up, prefab.transform.rotation) as GameObject;

                go.transform.SetParent(this.transform);

                _ThunderParticleSystems.Add(go.GetComponent<ParticleSystem>());
            }
        }

        private void OnEnable() {
#if UNITY_EDITOR
            Debug.Log($"<color=yellow><b>Initializing Bomb: { startingThunder }</b></color>");
#endif
            //this.currentBombs = startingFires;
        }

        private void OnDisable() {
            DisableEffects();
        }

        private void Update() {
            // Add the time since Update was last called to the timer.
            _timer += Time.deltaTime;

            if (Time.time >= (_effectStartTime + _effectsDisplayTime)) {
                DisableEffects();
            }

            Flicker();
        }

        private void Flicker() {
            if (ThunderLight.enabled) {
                ThunderLight.intensity = UnityEngine.Random.Range(0f, 8f);
            }
        }

        private void DisableEffects() {
            if (ThunderLight != null)
                ThunderLight.enabled = false;
        }

        private void EnableEffects() {
            _effectStartTime = Time.time;
            ThunderLight.enabled = true;

            var count = _ThunderParticleSystems.Count;

            for (int i = 0; i < count; i++) {
                var ps = _ThunderParticleSystems[i];
                ps.Play();
            }
        }

        private void EnableEffects(int index) {
            _effectStartTime = Time.time;
            ThunderLight.enabled = true;

            if (_ThunderParticleSystems.Count >= index) {
                var ps = _ThunderParticleSystems[index];
                ps.Play();
            }
        }

        public void ShowThunderEffect() {
            EnableEffects(0);
        }

        public void ShowAttackThunderEffect() {
            EnableEffects(1);
        }
    }
}
