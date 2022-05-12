using System.Collections;
using System.Collections.Generic;
using Enemies;
using Managers;
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
        private AbstractUnit Hangar;
        private float _range;
        private Vector3 _moveTarget;
        private ActiveObjectsTracker et;
        private float _moveSpeed;
        public ScriptableDamageType _damageType;
        private int _damage;
        public float _rotationAmount;
        private float _projectileSpeed;
        private Animation _anim;
        private List<Vector2> _availableTargets; //x = -9 to 7; y = -5 to 2;
        private long _ID;
        private CircleCollider2D myCollider;
        #endregion

        protected override void Awake() {
            base.Awake();
            Hangar = GetStation();
            et = ActiveObjectsTracker.Instance;
            _moveSpeed = 1f;
            _damage = 1;
            _range = 20f;
            _anim = GetComponent<Animation>();
            InitialiseTargets();
            _moveTarget = ComputeDirection();
            myCollider = GetComponent<CircleCollider2D>();
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

        public override void ComputeMovementFromOther() {
            _anim.Play();
        }

        public GameObject TargetEnemy() {
            //target = null;
            AbstractEnemy[] eb = et.NonCamo;
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
            ComputeMovement();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            _ID = GetID();
            StartCoroutine(DealDamage(other));
        }
        
        protected override void Hit(Collider2D col) {
            AffectExplosionColliders();
        }

        private IEnumerator DealDamage(Collider2D other) {
            _anim.Play();
            yield return new WaitUntil(() => AnimationStopped(_anim));
            Hit(other);
        }
        
        private void AffectExplosionColliders() {
            Collider2D[] cols =
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics2D.OverlapCircleAll(transform.position, myCollider.radius, 1 << LayerMask.NameToLayer("Enemy"));
            foreach (Collider2D aCollider in cols) 
                _listener.Income(aCollider.gameObject.GetComponent<AbstractEnemy>().Die(this, damage));
            
            
        }

        private static bool AnimationStopped(Animation anim) => !anim.isPlaying;

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
            SendParams(upgrade);//wtf
            if (_currentUpgrade._gun == 1) {
                GenerateGun<GrandmaGun>(secondaryProjectile);
            }
            _rotationAmount = 2*(Mathf.PI / _currentUpgrade.shotCount);
        }

        private void SendParams(IUpgrade upgrade) {
            currentUpgrade = (GrandmaUpgrade) upgrade;//?XD
        }

        private long GetID() {
            return Random.Range(0, 100000);
        }

        protected override float projectileSpeed {
            get => _projectileSpeed;
            set => _projectileSpeed = value;
        }

        protected override Vector2 dir { get; set; }

        protected override int damage {
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
