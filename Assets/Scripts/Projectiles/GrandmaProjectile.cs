using System.Collections.Generic;
using Enemies;
using Helpers;
using Units;
using Units.Guns;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

namespace Projectiles
{
    public class GrandmaProjectile : Projectile
    {
        
        #region fields
        public GameObject secondaryProjectile;
        private GrandmaUpgrade _currentUpgrade;
        private GameObject _target;
        private AbstractUnit Hangar;
        private float _range;
        private Vector3 _moveTarget;
        private ActiveObjectsTracker et;
        private EnemyListener _listener;
        private float _moveSpeed;
        public ScriptableDamageType _damageType;
        private int _damage;
        public float _rotationAmount;
        private float _time;
        private float _projectileSpeed;
        private Animation _anim;
        private List<Vector2> _availableTargets; //x = -9 to 7; y = -5 to 2;
        private long _ID;
        #endregion

        private void Awake() {
            Hangar = GetStation();
            _time = 0.0f;
            et = ActiveObjectsTracker.Instance;
            _moveSpeed = 1f;
            _damage = 1;
            _range = 20f;
            _anim = GetComponentInChildren<Animation>();
            InitialiseTargets();
            _moveTarget = ComputeDirection();
        }

        private void InitialiseTargets() {
            _availableTargets = new List<Vector2>();
            for (int i = -9; i <= 7; i++) {
                for (int j = -5; j <= 2; j++) {
                    Vector2 targetVector = new Vector2(i, j);
                    _availableTargets.Add(targetVector);
                }
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireSphere(transform.position, _range);
        }

        public override void ComputeMovementFromOther() {
            _anim.Play();
        }

        public GameObject TargetEnemy() {
            target = null;
            AbstractEnemy[] eb = et.enemies;
            return eb.Length == 0 ? null : eb[0].gameObject;
        }

        private void GenerateGun<T>(GameObject projectile) where T : Gun {
            GameObject gameObj = new GameObject {name = projectile.name + "Gun"};
            Gun thisGun = gameObj.AddComponent<T>();
            thisGun.SetProjectile(projectile);
            thisGun.GenerateGun(gameObject);
        }

        private bool HasReachedTarget() {
            return Vector3.Distance(transform.position, _moveTarget) < 0.1f;
        }

        private void FixedUpdate() {
            _time += Time.deltaTime;
            ComputeMovement();
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (!(_time > 0.5f)) return;
            _ID = GetID();
            Hit(other);
            _time = 0.0f;
        }
        
        protected override void Hit(Collider2D col) {
            _listener.Income(col.gameObject.GetComponent<AbstractEnemy>().Die(this));
        }

        #region Movement
        
        private void ComputeRotation(Vector2 direction) {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        protected override void ComputeMovement() {
            if (HasReachedTarget()) 
                _moveTarget = ComputeDirection();
            Move(_moveTarget - transform.position);
        }

        private void Move(Vector2 direction) {
            if (!Hangar.placed) return;
            transform.Translate(direction.normalized * (_moveSpeed * Time.deltaTime), Space.World);
            ComputeRotation(direction);
        }

        private Vector3 ComputeDirection() {
            Vector3 direction = _availableTargets[Random.Range(0, _availableTargets.Count)];
            return direction;
        }
        
        #endregion
        

        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            _listener = listener;
            SendParams(upgrade);
            if (_currentUpgrade._gun == 1) {
                GenerateGun<GrandmaGun>(secondaryProjectile);
            }
            _rotationAmount = 2*(Mathf.PI / _currentUpgrade.shotCount);
        }

        private void SendParams(IUpgrade upgrade) {
            currentUpgrade = (GrandmaUpgrade) upgrade;
        }

        private long GetID() {
            return Random.Range(0, 100000);
        }

        protected override float projectileSpeed {
            get => _projectileSpeed;
            set => _projectileSpeed = value;
        }

        protected override GameObject target {
            get => _target;
            set => _target = value;
        }

        protected override Vector2 dir { get; set; }
        public override int damage {
            get => _damage;
            set => _damage = value;
        }
        public override int pierce {
            get;
            set;
        }

        protected override float range {
            get => _range;
            set => _range = value;
        }
        protected override Vector2 spawnedAt { get; set; }
        public override ScriptableDamageType DamageType => _damageType;
        protected override bool hasCollided { get; set; }

        public override long ID => _ID;

        public IUpgrade currentUpgrade {
            get => _currentUpgrade;
            private set => _currentUpgrade = (GrandmaUpgrade)value;
        }

        private AbstractUnit GetStation() {
            return transform.parent.gameObject.GetComponent<AbstractUnit>();
        }
    }
}
