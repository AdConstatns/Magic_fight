namespace AmazingTeam.MagicFight {

    using System;
    using Apex;
    using Apex.AI;
    using Apex.AI.Components;
    using Apex.Units;
    using UnityEngine;
    using System.Collections.Generic;
   

    /// <summary>
    /// Component representing the player. Instantiates the context and holds player specific variables
    /// </summary>
    [AddComponentMenu("MagicFight/Player/Player", 0)]
    public class Player : LivingEntity, IContextProvider {

        //The range inside which to scan for enemies and power-ups
        public float scanRange;
        // The range inside which to attack the enemies       
        public float StrikeRange;
        public float StrikeIncreasePercentage = 20;
        [HideInInspector]
        public float CachedStrikeRange;

        private SurvivalContext _context;
        private PlayerShooting _playerShooting;
        private PlayerHealth _playerHealth;
        // AI Player Movement.
        private PlayerAIMovement _playerAIMovement;     
        // Player Bomb Attack.
        private PlayerBombs _playerBombs;
        // Player Fire Attack
        private PlayerFire _playerFire;
        //Player Thunder Attack
        private PlayerThunder _playerThunder;
        // Player Ground Attack
        private PlayerLava _playerLava;
        private UtilityAIComponent _playerAI;
        private IUnitFacade _navUnit;
        private FieldOfViewAsset.FieldOfView _fieldOfView;
        private FieldOfView _fieldOfView1;
        [SerializeField]
        private readonly float FOVIncreasePercentage = 20;
        // Set by the Player
        private bool _IsPlayerShooting;
        // Player has multiple attacks
        private bool _IsComboAttack;

        PlayerAnimation _playerAnimation;

        // For Handline multiple targets.
        public HashSet<LivingEntity> AttackTarget;

        [HideInInspector]
        // Total Powerup Collected.
        public int PowerUpCount;
        public PlayerCollectable CollectedPowerup;
        // Cached initial Scan Value.      
        private  float _cachedScanRange;
        [HideInInspector]
        // Cached Fire Particle Scale initial value
        public float _cachedFireParticleScale = 1.2f;       

        private float _fovUpdateSpeed = 0.02f;

        public static Player focusedPlayer {
            get;
            set;
        }

        public IUnitFacade NavUnit {
            get { return _navUnit; }
        }


        public int currentAmmo {
            get { return _playerShooting.currentAmmo; }
        }

        public int currentBombs {
            get { return _playerBombs.currentBombs; }
        }

        public bool canThrowBomb {
            get { return _playerBombs.canThrowBomb; }
        }

        public int currentBandAids {
            get { return _playerHealth.currentBandAids; }
        }

        public int currentFires {
            get { return _playerFire.currentFires; }           
        }

        public int currentThunders {
            get { return _playerThunder.currentThunders; }

        }

        public int currentLavas {
            get { return _playerLava.currentLavas; }
        }

        public Vector3 spawnPoint {
            get;
            private set;
        }

        public Vector3 gunTipOffset {
            get { return _playerShooting.transform.position - this.transform.position; }
        }

        public IAIContext GetContext(Guid aiId) {
            return _context;
        }

        // Value set by the AI Player to control shooting.
        public bool IsShooting {
            get { return _playerShooting.shooting; }
        }

        // Variable Set by the Non-AI Player to control shooting.
        public bool IsPlayerShooting {
            get { return _IsPlayerShooting; }
            set { _IsPlayerShooting = value; }
        }

        public bool IsComboAttack {
            get { return _IsComboAttack; }
            set { _IsComboAttack = value; }
        }

        protected override void OnAwake() {
            _context = new SurvivalContext(this);

            _navUnit = this.GetUnitFacade();
            _playerShooting = this.GetComponentInChildren<PlayerShooting>();
            _playerAIMovement = this.GetComponent<PlayerAIMovement>();                  
            _playerAI = this.GetComponent<UtilityAIComponent>();
            _playerHealth = this.GetComponent<PlayerHealth>();
            _playerBombs = this.GetComponentInChildren<PlayerBombs>();
            _playerFire = GetComponentInChildren<PlayerFire>();
            _playerThunder = GetComponentInChildren<PlayerThunder>();
            _playerLava = GetComponentInChildren<PlayerLava>();
            _playerAnimation = GetComponent<PlayerAnimation>();

            _fieldOfView = this.GetComponentInChildren<FieldOfViewAsset.FieldOfView>();
            _fieldOfView1 = this.GetComponentInChildren<FieldOfView>();          

            AttackTarget = new HashSet<LivingEntity>();

            _cachedScanRange = scanRange;
            CachedStrikeRange = StrikeRange;           
        }

        private void OnEnable() {
            _playerShooting.enabled = true;
            _playerAIMovement.enabled = true;
            _playerAI.enabled = true;
            _playerAnimation.enabled = true;
            this.spawnPoint = this.transform.position;
        }

        public void StartFiring() {
            _playerShooting.shooting = true;
        }

        public void StopFiring() {
            _playerShooting.shooting = false;
        }

        public void MoveTo(Vector3 destination) {
            _playerAIMovement.Move(destination);
        }

        public void StopMoving() {
            _playerAIMovement.Stop();
        }

        public void Reload() {
            _playerShooting.Reload();
        }

        public void ThrowBomb() {
            _playerBombs.ThrowBomb();
        }

        public void AddBombs(int amount) {
            _playerBombs.AddBombs(amount);
        }

        public void UseBandAid() {
            _playerHealth.UseBandAid();
        }     

        void ComboAttack() {
            int attackType = 0;
            if (_playerFire.currentFires > 0)
                attackType++;
            if (_playerThunder.currentThunders > 0)
                attackType++;
            if (_playerLava.currentLavas > 0)
                attackType++;

            if (attackType > 1)
                IsComboAttack = true;
            else
                IsComboAttack = false;
        }

        void ResetPowerUpCount() {
            PowerUpCount = 0;
        }

        public void ShowFireEffect() {
            _playerFire.ShowFireEffect();
        }

        public void ShowFireAttackEffect() {
            _playerFire.ShowAttackFireEffect();
        }

        public void ShowThunderEffect() {
            _playerThunder.ShowThunderEffect();
        }

        public void ShowAttackThunderEffect() {
            _playerThunder.ShowAttackThunderEffect();
        }

        public void ShowLavaEffect() {
            _playerLava.ShowLavaEffect();
        }

        public void ShowLavaAttackEffect() {
            _playerLava.ShowAttackLavaEffect();
        }

        // Adding the Fire Powerup
        public void AddFire(int amount) {
            _playerFire.AddFire(amount);
            // Show Fire Effect
            _playerFire.ShowFireEffect();
            //Increase Scan Range of the player
            //IncreaseScanRange();
            IncreaseStrikeRange();
            // On Adding fire the Player Field of View Increase by 20 percent.            
            IncreaseFOVDistance();
            // Enable the Field Of View Particle System.
            EnableFireStrikeAreaParticle(_playerFire.gameObject);       
        }

        public void AddThunder(int amount) {
            _playerThunder.AddThunder(amount);
            // Show Thunder Effect
            _playerThunder.ShowThunderEffect();    
            //Increase Scan Range of the player
            //IncreaseScanRange();
            IncreaseStrikeRange();
            // On Adding Thunder the Player Field of View Increase by 20 percent.          
            IncreaseFOVDistance();
            // Enable the Strike Area Particle System.
            EnableThunderStrikeAreaParticle(_playerThunder.gameObject);        
        }

        public void AddLava(int amount) {
            _playerLava.AddLava(amount);
            // Show Lava Effect
            _playerLava.ShowLavaEffect();
            //Increase Scan Range of the player
            //IncreaseScanRange();
            IncreaseStrikeRange();
            // On Adding Lava the Player Field of View Increase by 20 percent.
            IncreaseFOVDistance();
            // Enable and Scale the Field Of View Particle System.
            EnableLavaStrikeAreaParticle(_playerLava.gameObject);
        }

        public void UseFire(AbilityMode mode) {
            _playerFire.UseFire(mode);

            if (_playerFire.currentFires <= 0) {
                DisableFireStrikeAreaParticle(_playerFire.gameObject);
            }

            DisableFieldOfView();
            ResetPowerUpCount();            
            ResetStrikeRange();
        }
        public void UseThunder(AbilityMode mode) {
            _playerThunder.UseThunder(mode);

            if (_playerThunder.currentThunders <= 0) {
                DisableThunderStrikeAreaParticle(_playerThunder.gameObject);
            }
            DisableFieldOfView();
            ResetPowerUpCount();            
            ResetStrikeRange();
        }
        public void UseLava(AbilityMode mode) {
            _playerLava.UseLava(mode);

            if (_playerLava.currentLavas <= 0) {
                DisableLavaStrikeAreaParticle(_playerLava.gameObject);
            }
            DisableFieldOfView();
            ResetPowerUpCount();            
            ResetStrikeRange();            
        }

        void IncreaseScanRange() {
            // Increrase the scan range by 20 percent
            // i.e 3 is 100 percent
            //     0.6 is 20 percent.
            //scanRange += 0.6f; 
            // Increase the scan range by FOVIncreasePercentage
            scanRange += _cachedScanRange * (FOVIncreasePercentage/100);
        }

        void ResetStrikeRange() {
            if(_playerFire.currentFires <= 0 && _playerThunder.currentThunders <= 0 && _playerLava.currentLavas <= 0)
                StrikeRange = CachedStrikeRange;  
        }

        protected void IncreaseStrikeRange() {  
            // Increrase the scan range by 20 percent
            // i.e 3 is 100 percent
            //     0.6 is 20 percent.
            //scanRange += 0.6f; 
            // Increase the scan range by FOVIncreasePercentage
            StrikeRange += CachedStrikeRange * (StrikeIncreasePercentage / 100);
        }

        void ResetScanRange() {
            scanRange = _cachedScanRange;
        }

        public void EnableFireStrikeAreaParticle(GameObject strikeArea) {
            strikeArea.transform.GetChild(0).gameObject.SetActive(true);
            ParticleSystem ps = strikeArea.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            var shape = ps.shape;
            // radius should be scaled gradually. 
            // i.e 1 hit it should be 0.3. 
            //     2 hit it should be 0.6.
            shape.radius = 0.3f * PowerUpCount;           
        }

        public void DisableFireStrikeAreaParticle(GameObject strikeArea) {
            strikeArea.transform.GetChild(0).gameObject.SetActive(false);
            ParticleSystem ps = strikeArea.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            var shape = ps.shape;
            shape.radius = 0.0f;
        }

        public void EnableLavaStrikeAreaParticle(GameObject strikeArea) {
            strikeArea.transform.GetChild(0).gameObject.SetActive(true);
            Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
            Vector3 cachedInitialscale = new Vector3(_cachedFireParticleScale, _cachedFireParticleScale, _cachedFireParticleScale);
            // Scale should be incremented gradually. 
            // i.e 1 hit it should be 1. 
            //     2 hit it should be 2.
            strikeArea.transform.GetChild(0).gameObject.transform.localScale = cachedInitialscale + scale * PowerUpCount;
        }

        public void DisableLavaStrikeAreaParticle(GameObject strikeArea) {
            strikeArea.transform.GetChild(0).gameObject.SetActive(false);
            Vector3 scale = new Vector3(1.2f, 1.2f, 1.2f);
            strikeArea.transform.GetChild(0).gameObject.transform.localScale = scale;
        }

        public void EnableThunderStrikeAreaParticle(GameObject strikeArea) {
            strikeArea.transform.GetChild(0).gameObject.SetActive(true);
            ParticleSystem ps = strikeArea.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            var shape = ps.shape;
            // radius should be scaled gradually. 
            // i.e 1 hit it should be 1. 
            //     2 hit it should be 2.
            shape.radius =  1.0f * PowerUpCount;
        }      

        public void DisableThunderStrikeAreaParticle(GameObject strikeArea) {
            strikeArea.transform.GetChild(0).gameObject.SetActive(false);
            ParticleSystem ps = strikeArea.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            var shape = ps.shape;
            shape.radius = 0.0f;
        }

        public void DisableFieldOfView() {
            // Disable the field of view if all the Powerup's is equal to zero.
            if (_playerFire.currentFires <= 0 && _playerThunder.currentThunders <= 0 && _playerLava.currentLavas <= 0) {
                //_fieldOfView.enabled = false;
                StartCoroutine(ChangeToZero1(0));
                StartCoroutine(ChangeToZero(0));                          
                return;
            }
        }

        private System.Collections.IEnumerator ChangeToZero(float pct) {
            float preChangePct = _fieldOfView.ViewRadius;
            float elapsed = 0f;

            while (elapsed < _fovUpdateSpeed) {
                elapsed += 0.02f * Time.deltaTime;
                _fieldOfView.ViewRadius = Mathf.Lerp(preChangePct, pct, elapsed / _fovUpdateSpeed);
                yield return null;
            }

            _fieldOfView.ViewRadius = 0;
            _fieldOfView.ManualReset();
        }

        private System.Collections.IEnumerator ChangeToZero1(float pct) {
            float preChangePct = _fieldOfView1.viewRadius;
            float elapsed = 0f;

            while (elapsed < _fovUpdateSpeed) {
                elapsed += 0.02f * Time.deltaTime;
                _fieldOfView1.viewRadius = Mathf.Lerp(preChangePct, pct, elapsed / _fovUpdateSpeed);
                yield return null;
            }

            _fieldOfView1.viewRadius = 0;           
        }

        public void AddBandAid(int amount) {
            _playerHealth.AddBandAid(amount);                   
        }

        public void IncreaseFOVDistance() {
            if (PowerUpCount > CollectedPowerup.Maximum )  // 5 is the max power up count
                return;          

            StartCoroutine(ChangeToPct1(StrikeRange));
            StartCoroutine(ChangeToPct(StrikeRange));

            //_fieldOfView.ViewRadius = scanRange;
            PowerUpCount++;
        }

        private System.Collections.IEnumerator ChangeToPct(float pct) {
            float preChangePct = _fieldOfView.ViewRadius;
            float elapsed = 0f;

            while (elapsed < _fovUpdateSpeed) {
                elapsed += 0.02f * Time.deltaTime;
                _fieldOfView.ViewRadius = Mathf.Lerp(preChangePct, pct, elapsed / _fovUpdateSpeed);
                yield return null;
            }

            _fieldOfView.ViewRadius = StrikeRange;
        }

        private System.Collections.IEnumerator ChangeToPct1(float pct) {
            float preChangePct = _fieldOfView1.viewRadius;
            float elapsed = 0f;

            while (elapsed < _fovUpdateSpeed) {
                elapsed += 0.02f * Time.deltaTime;
                _fieldOfView1.viewRadius = Mathf.Lerp(preChangePct, pct, elapsed / _fovUpdateSpeed);
                yield return null;
            }

            _fieldOfView1.viewRadius = StrikeRange;
        }

        public void OnDeath() {
            // Turn off the movement and shooting scripts.
            _playerAIMovement.enabled = false;
            _playerShooting.enabled = false;
            _playerAI.enabled = false;
            _playerAnimation.enabled = false;
            HUDState.DisplayMaxPowerupMessage(false);

        }

        protected override void OnAttackTargetChanged(LivingEntity newAttackTarget) {
            //When a new attack target is set, we want to turn towards the target.
            var t = newAttackTarget != null ? newAttackTarget.gameObject.transform : null;
            _playerAIMovement.LookAt(t);
        }

        protected override void OnAttackTargetDead() {
            //When our target dies, stop shooting
            this.attackTarget = null;
            _playerShooting.shooting = false;
        }

        private void OnDrawGizmos() {
            //Gizmos.DrawWireSphere(this.transform.position, scanRange);           
        }

        // For Player Field of View
        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
            if (!angleIsGlobal) {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }      
    }
}