namespace AmazingTeam.MagicFight
{
    using Apex.LoadBalancing;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Represents the player's health
    /// </summary>

    [AddComponentMenu("MagicFight/Player/PlayerHealth", 3)]
    public class PlayerHealth : MonoBehaviour, IHealth 
    {
        public int startingHealth = 100;
        public int startingBandAids = 0;
        public AudioClip deathClip;                              
        public GameObject zPrefab;

        private ParticleSystem Zs;
        private Animator _anim;                                 
        private AudioSource _playerAudio;                        
        private Player _player;      
        private int _currentBandAids;

        [SerializeField]
        private int _currentHealth;

        public HealthBar _healthBar;

        public PlayerAIMovement _playerAIMovement;

        public int currentHealth
        {
            get
            {
                return _currentHealth;
            }

            private set
            {
                _currentHealth = Mathf.Max(0, value);
                HUDState.UpdateHealth(_currentHealth);

                if(_healthBar != null)
                    _healthBar.UpdateHealthBar();
            }
        }

        public int currentBandAids
        {
            get {
                return _currentBandAids;
            }

            set {
                _currentBandAids = value;
                HUDState.UpdateBandAids(_currentBandAids);
            }
        }
        
        protected void Awake()
        {
            // Setting up the references.
            _anim = GetComponent<Animator>();
            _playerAudio = GetComponent<AudioSource>();
            _player = GetComponent<Player>();
            _playerAIMovement = GetComponent<PlayerAIMovement>();
            //_healthBar = GetComponentInChildren<HealthBar>();

            // Commented by Tholkappiyan
            // To Disable Damage Indicator.
            ////Damage indicator
            //var go = GameObject.Instantiate(zPrefab, this.transform.position + (Vector3.up * 1), zPrefab.transform.rotation) as GameObject;
            //go.transform.SetParent(this.transform);
            //Zs = go.GetComponent<ParticleSystem>();
            //Zs.transform.position += Vector3.up;
        }

        private void OnEnable()
        {
            
#if UNITY_EDITOR
            Debug.Log("<color=yellow><b>Initializing: BandAids, Health </b></color>");
#endif
            // Set the initial health of the player.
            this.currentHealth = startingHealth;

            zPrefab.SetActive(false);
           
        }      

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            TakeDamage(amount);
        }

        public void TakeDamage(int amount)
        {
            if (currentHealth <= 0)
            {
                return;
            }

            zPrefab.SetActive(true);
            //Zs.Play();

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            // Play the hurt sound effect.
            _playerAudio.Play();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0)
            {
                // Tell the animator that the player is dead.
                _anim.SetTrigger("IS_DIE");

                // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
                _playerAudio.clip = deathClip;
                _playerAudio.Play();

                // On death Stop the wander of the player.
                _playerAIMovement.StopWander();               
                _player.OnDeath();


                //Recycle the entity
                // 4f is the time afterwhich the player is recycled.
                LoadBalancer.defaultBalancer.ExecuteOnce(() => EntityManager.instance.Recycle(_player), 1f);
            }  else {
               StartCoroutine(WaitForParticleSystem());          
            }
        }

        IEnumerator WaitForParticleSystem() {
            yield return new WaitForSeconds(2.0f);
            // Wander functionallity should be enabled for Player AI.
            if (gameObject.CompareTag(Tags.PlayerAI)) {
                if(currentHealth > 0)
                     GetComponent<PlayerAIMovement>().StartWander();
            }
              
        }

        public void UseBandAid()
        {
            //Only heal if alive and damaged
            if (this.currentHealth == this.startingHealth || this.currentHealth <= 0)
            {
                return;
            }

            this.currentBandAids--;

            // Heal to full.
            this.currentHealth = startingHealth;
        }

        public void AddBandAid(int amount)
        {
            this.currentBandAids += amount;
        }
    }
}