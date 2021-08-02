using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight {
    [AddComponentMenu("MagicFight/Player/PlayerLava", 8)]
    public class PlayerLava : MonoBehaviour {
        public int damage = 10;                  // The damage inflicted by each bullet.
        public float timeBetweenLava = 1f;       // The minimum time between each bomb use.
        public float range = 5f;                  // The bombs explosion range.
        public int startingLava = 0;
        public Light LavaLight;
        [SerializeField]
        private int _currentLavas;
        public List<GameObject> Lavas;

        private List<ParticleSystem> _LavaParticleSystems;

     
        private float _timer;                                    // A timer to determine when to fire.
        private AudioSource _LavaAudio;                           // Reference to the audio source.

        private float _effectsDisplayTime = 0.2f;                // The explosion display time.
        private float _effectStartTime;

        public int currentLavas {
            get {
                return _currentLavas;
            }

            set {
                _currentLavas = value;
                if (gameObject.CompareTag("Player"))  // Update for player 
                    HUDState.UpdateLavas(_currentLavas);
            }
        }

        public bool canThrowFire {
            get {
                if (this.currentLavas <= 0) {
                    return false;
                }

                if (_timer < timeBetweenLava) {
                    return false;
                }

                return true;
            }
        }

        public void ThrowLava() {
            _timer = 0;

            EnableEffects();

            _LavaAudio.Play();

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

            this.currentLavas--;
        }


        public void AddLava(int amount) {
            this.currentLavas += amount;
        }

        public void UseLava(AbilityMode mode) {
            //Only Use Thunder if Thunder count is greater than zero.
            if (this.currentLavas <= 0) {
                return;
            }

            if (AbilityMode.Single == mode)
                this.currentLavas--;
            else if (AbilityMode.Multiple == mode)
                this.currentLavas = 0;
        }

        private void Awake() {
            // Set up the references.
            _LavaAudio = GetComponent<AudioSource>();
        }

        private void Start() {
            this.currentLavas = startingLava;

            var count = Lavas.Count;

            _LavaParticleSystems = new List<ParticleSystem>();

            for (int i = 0; i < count; i++) {
                var prefab = Lavas[i];

                var go = GameObject.Instantiate(prefab, this.transform.position + Vector3.up, prefab.transform.rotation) as GameObject;

                go.transform.SetParent(this.transform);

                _LavaParticleSystems.Add(go.GetComponent<ParticleSystem>());
            }
        }

        private void OnEnable() {
#if UNITY_EDITOR
            Debug.Log($"<color=yellow><b>Initializing Bomb: { startingLava }</b></color>");
#endif
            //this.currentBombs = startingLava;
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
            if (LavaLight.enabled) {
                LavaLight.intensity = UnityEngine.Random.Range(0f, 8f);
            }
        }

        private void DisableEffects() {
            if (LavaLight != null)
                LavaLight.enabled = false;
        }

        private void EnableEffects() {
            _effectStartTime = Time.time;
            LavaLight.enabled = true;

            var count = _LavaParticleSystems.Count;

            for (int i = 0; i < count; i++) {
                var ps = _LavaParticleSystems[i];
                ps.Play();
            }
        }      

        private void EnableEffects(int index) {
            _effectStartTime = Time.time;
            LavaLight.enabled = true;

            if (_LavaParticleSystems.Count >= index) {
                var ps = _LavaParticleSystems[index];
                ps.Play();
            }
        }

        public void ShowLavaEffect() {
            EnableEffects(0);
        }

        public void ShowAttackLavaEffect() {
            EnableEffects(1);
        }
    }
}

