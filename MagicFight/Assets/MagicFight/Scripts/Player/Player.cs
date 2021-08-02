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
        public float strikeRange;

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
        [SerializeField]
        private readonly float FOVIncreasePercentage = 20;
        // Set by the Player
        private bool _IsPlayerShooting;
        // Player has multiple attacks
        private bool _IsComboAttack;

        PlayerAnimation _playerAnimation;

        // For Handline multiple targets.
        public HashSet<LivingEntity> AttackTarget;

        // public FOVCollider _fOVCollider;

        public int PowerUpCount;
        public PlayerCollectable CollectedPowerup;
        public  float _cachedScanRange;

        public float _fovUpdateSpeed = 1;

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
            //_fOVCollider = this.GetComponentInChildren<FOVCollider>();
            
            AttackTarget = new HashSet<LivingEntity>();

            _cachedScanRange = scanRange;
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
            IncreaseScanRange();
            // On Adding fire the Player Field of View Increase by 20 percent.            
            IncreaseFOVDistance();
            // Enable the Field Of View Particle System.
            EnableStrikeAreaParticle(_playerFire.gameObject);       
            // Check for combo Attack. i.e player has more than one attack 
            ComboAttack();
        }

        public void AddThunder(int amount) {
            _playerThunder.AddThunder(amount);
            // Show Thunder Effect
            _playerThunder.ShowThunderEffect();    
            //Increase Scan Range of the player
            IncreaseScanRange();
            // On Adding Thunder the Player Field of View Increase by 20 percent.          
            IncreaseFOVDistance();
            // Enable the Strike Area Particle System.
            EnableStrikeAreaParticle(_playerThunder.gameObject);         
            // Check for combo Attack. i.e player has more than one attack 
            ComboAttack();
        }

        public void AddLava(int amount) {
            _playerLava.AddLava(amount);
            // Show Lava Effect
            _playerLava.ShowLavaEffect();          
            //Increase Scan Range of the player
            IncreaseScanRange();
            // On Adding Lava the Player Field of View Increase by 20 percent.
            IncreaseFOVDistance();
            // Enable the Field Of View Particle System.
            EnableStrikeAreaParticle(_playerLava.gameObject);        
            // Check for combo Attack. i.e player has more than one attack 
            ComboAttack();

        }

        public void UseFire(AbilityMode mode) {
            _playerFire.UseFire(mode);

            if (_playerFire.currentFires <= 0) {
                DisableStrikeAreaParticle(_playerFire.gameObject);
            }

            DisableFieldOfView();
            ResetPowerUpCount();
            ResetScanRange();
        }
        public void UseThunder(AbilityMode mode) {
            _playerThunder.UseThunder(mode);

            if (_playerThunder.currentThunders <= 0) {
                DisableStrikeAreaParticle(_playerThunder.gameObject);
            }
            DisableFieldOfView();
            ResetPowerUpCount();
            ResetScanRange();
        }
        public void UseLava(AbilityMode mode) {
            _playerLava.UseLava(mode);

            if (_playerLava.currentLavas <= 0) {
                DisableStrikeAreaParticle(_playerLava.gameObject);
            }
            DisableFieldOfView();
            ResetPowerUpCount();
            ResetScanRange();
        }

        void IncreaseScanRange() {
            // Increrase the scan range by 20 percent
            // i.e 3 is 100 percent
            //     0.6 is 20 percent.
            //scanRange += 0.6f; 
            // Increase the scan range by FOVIncreasePercentage
            scanRange += _cachedScanRange * (FOVIncreasePercentage/100);
        }

        void ResetScanRange() {
            scanRange = _cachedScanRange;
        }

        public void EnableStrikeAreaParticle(GameObject strikeArea) {
            strikeArea.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void DisableStrikeAreaParticle(GameObject strikeArea) {
            strikeArea.transform.GetChild(0).gameObject.SetActive(false);
        }

        public void DisableFieldOfView() {
            // Disable the field of view if all the Powerup's is equal to zero.
            if (_playerFire.currentFires <= 0 && _playerThunder.currentThunders <= 0 && _playerLava.currentLavas <= 0) {
                //_fieldOfView.enabled = false;
                _fieldOfView.Reset();                
                return;
            }
        }      

        public void AddBandAid(int amount) {
            _playerHealth.AddBandAid(amount);                   
        }

        public void IncreaseFOVDistance() {
            if (PowerUpCount > CollectedPowerup.Maximum )  // 5 is the max power up count
                return;

           // if (!_fieldOfView.enabled)
           //    _fieldOfView.enabled = true;
            // Field of view will become larger based on scanRange which is depend on Powerup Collected.
            //_fieldOfView.ViewRadius = scanRange;
            //StartCoroutine(ChangeToPct(scanRange));

            _fieldOfView.ViewRadius = scanRange;
            PowerUpCount++;
        }

        //private System.Collections.IEnumerator ChangeToPct(float pct) {
        //    float preChangePct = _fieldOfView.viewRadius;
        //    float elapsed = 0f;

        //    while (elapsed < _fovUpdateSpeed) {
        //        elapsed += 0.02f * Time.deltaTime;
        //        _fieldOfView.viewRadius = Mathf.Lerp(preChangePct, pct, elapsed / _fovUpdateSpeed);
        //        yield return null;
        //    }
           
        //    _fieldOfView.viewRadius = scanRange;
        //}

       

        //// starting value for the Lerp
        //static float t = 0.0f;

        //private System.Collections.IEnumerator ChangeToPct1(float pct) {
        //     // animate the game object from -1 to +1 and back
        //    float minimum = _fieldOfView.viewRadius;
        //    float maximum = scanRange;

        //// animate the position of the game object...
        //_fieldOfView.viewRadius = Mathf.Lerp(minimum, scanRange, t);

        //    // .. and increase the t interpolater
        //    t += 0.03f * Time.deltaTime;

        //    // now check if the interpolator has reached 1.0
        //    // and swap maximum and minimum so game object moves
        //    // in the opposite direction.
        //    while (t > 1.0f) {
        //        //float temp = maximum;
        //        //maximum = minimum;
        //        //minimum = temp;
        //        t = 0.0f;
        //    }

        //    yield return null;
        //}



        public void OnDeath() {
            // Turn off the movement and shooting scripts.
            _playerAIMovement.enabled = false;
            _playerShooting.enabled = false;
            _playerAI.enabled = false;
            _playerAnimation.enabled = false;

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

        //public int GreatestOfThreePowerUp(int num1, int num2, int num3) {
        //    // set the value of the three numbers
        //    //num1 = 10;
        //    //num2 = 20;
        //    //num3 = 50;
        //    int greatestNum;

        //    if (num1 > num2) {
        //        if (num1 > num3) {
        //            //Console.Write("Number one is the largest!\n");
        //            greatestNum = num1;
        //        } else {
        //            //Console.Write("Number three is the largest!\n");
        //            greatestNum = num3;
        //        }
        //    } else if (num2 > num3)
        //        //Console.Write("Number two is the largest!\n");
        //        greatestNum = num2;
        //    else
        //        //Console.Write("Number three is the largest!\n");
        //        greatestNum = num3;

        //    return greatestNum;
        //}
    }
}