using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingTeam.MagicFight
{
    [AddComponentMenu("MagicFight/Player/PlayerFire", 6)]
    public class PlayerFire : MonoBehaviour
    {
        public int damage = 20;                  // The damage inflicted by each bullet.
        public float timeBetweenBombs = 1f;       // The minimum time between each bomb use.
        public float range = 5f;                  // The bombs explosion range.
        public int startingFires = 3;
        public Light FireLight;
        [SerializeField]
        private int _currentLavas;
        public List<GameObject> Fires;
        private List<ParticleSystem> _FireParticleSystems;
      
        private int _currentFires;
        private float _timer;                                    // A timer to determine when to fire.
        private AudioSource _fireAudio;                           // Reference to the audio source.

        private float _effectsDisplayTime = 0.2f;                // The explosion display time.
        private float _effectStartTime;

        public int currentFires {
            get {
                return _currentFires;
            }

            set {
                _currentFires = value;
                HUDState.UpdateFires(_currentFires);
            }
        }

        public bool canThrowFire {
            get {
                if (this.currentFires <= 0) {
                    return false;
                }

                if (_timer < timeBetweenBombs) {
                    return false;
                }

                return true;
            }
        }

        public void ThrowFire() {
            _timer = 0;

            EnableEffects();

            _fireAudio.Play();

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

            this.currentFires--;
        }


        public void AddFire(int amount) {
            this.currentFires += amount;
        }

        public void UseFire(AbilityMode mode) {
            //Only Use Fire if Fire count is greater than zero.
            if (this.currentFires <= 0) {
                return;
            }

            if (AbilityMode.Single == mode)
                this.currentFires--;
            else if (AbilityMode.Multiple == mode)
                this.currentFires = 0;
        }

        private void Awake() {
            // Set up the references.
            _fireAudio = GetComponent<AudioSource>();
        }

        private void Start() {
            this.currentFires = startingFires;

            var count = Fires.Count;

            _FireParticleSystems = new List<ParticleSystem>();

            for (int i = 0; i < count; i++) {
                var prefab = Fires[i];

                var go = GameObject.Instantiate(prefab, this.transform.position + Vector3.up, prefab.transform.rotation) as GameObject;

                go.transform.SetParent(this.transform);

                _FireParticleSystems.Add(go.GetComponent<ParticleSystem>());
            }
        }

        private void OnEnable() {
#if UNITY_EDITOR
            Debug.Log($"<color=yellow><b>Initializing Bomb: { startingFires }</b></color>");
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
            if (FireLight.enabled) {
                FireLight.intensity = UnityEngine.Random.Range(0f, 8f);
            }
        }

        private void DisableEffects() {
            if (FireLight != null)
                FireLight.enabled = false;
        }

        private void EnableEffects() {
            _effectStartTime = Time.time;
            FireLight.enabled = true;

            var count = _FireParticleSystems.Count;

            for (int i = 0; i < count; i++) {
                var ps = _FireParticleSystems[i];
                ps.Play();
            }
        }

        private void EnableEffects(int index) {
            _effectStartTime = Time.time;
            FireLight.enabled = true;

            if (_FireParticleSystems.Count >= index) {
                var ps = _FireParticleSystems[index];
                ps.Play();
            }
        }

        public void ShowFireEffect() {  
            EnableEffects(0);
        }

        public void ShowAttackFireEffect() {
            EnableEffects(1);
        }

      

    }
}
