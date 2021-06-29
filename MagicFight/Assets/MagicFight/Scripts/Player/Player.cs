namespace AmazingTeam.MagicFight {

    using System;
    using Apex;
    using Apex.AI;
    using Apex.AI.Components;
    using Apex.Units;
    using UnityEngine;

    /// <summary>
    /// Component representing the player. Instantiates the context and holds player specific variables
    /// </summary>
    [AddComponentMenu("MagicFight/Player/Player", 0)]
    public class Player : LivingEntity, IContextProvider {
        public float scanRange;                     //The range inside which to scan for enemies and power-ups

        private SurvivalContext _context;
        private PlayerShooting _playerShooting;
        private PlayerHealth _playerHealth;
        private PlayerAIMovement _playerAIMovement;
        private PlayerBombs _playerBombs;
        private UtilityAIComponent _playerAI;
        private IUnitFacade _navUnit;
        private FieldOfView _fieldOfView;
        [SerializeField]
        private readonly float FOVIncreasePercentage = 1;

        public FOVCollider _fOVCollider;

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

        protected override void OnAwake() {
            _context = new SurvivalContext(this);

            _navUnit = this.GetUnitFacade();
            _playerShooting = this.GetComponentInChildren<PlayerShooting>();
            _playerAIMovement = this.GetComponent<PlayerAIMovement>();
            _playerAI = this.GetComponent<UtilityAIComponent>();
            _playerHealth = this.GetComponent<PlayerHealth>();
            _playerBombs = this.GetComponentInChildren<PlayerBombs>();
            _fieldOfView = this.GetComponentInChildren<FieldOfView>();
            //_fOVCollider = this.GetComponentInChildren<FOVCollider>();
        }

        private void OnEnable() {
            _playerShooting.enabled = true;
            _playerAIMovement.enabled = true;
            _playerAI.enabled = true;
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

        // Adding the Fire Powerup
        public void AddFire(int amount) {

        }

        public void AddBandAid(int amount) {
            _playerHealth.AddBandAid(amount);
            // On Adding the BandAid Field of View Increase by 1 percent.
            IncreaseFOVDistance();
            // On Adding the BandAid FOVSensor Increase by 1 percent.
            Invoke("IncreaseFOVSensor", 0.1f);
            
        }

        public void IncreaseFOVDistance() {

             if(!_fieldOfView.enabled)
                _fieldOfView.enabled = true;
            // Field of view will become larger based on powerup Collected.
            _fieldOfView.viewRadius = _fieldOfView.viewRadius + ((_fieldOfView.viewRadius / 100) * FOVIncreasePercentage * _playerHealth.currentBandAids);
           
        }

        public void IncreaseFOVSensor() {
           if(_fOVCollider != null && _fOVCollider.enabled) {
                _fOVCollider.Length = _fOVCollider.Length + ((_fOVCollider.Length / 100) * FOVIncreasePercentage * _playerHealth.currentBandAids);
                _fOVCollider.CreateCollider();
            }
            

        }

        public void OnDeath() {
            // Turn off the movement and shooting scripts.
            _playerAIMovement.enabled = false;
            _playerShooting.enabled = false;
            _playerAI.enabled = false;
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