using Enemies;
using Managers;
using Units;
using UnityEngine;
using Upgrades;
// ReSharper disable ConvertToAutoProperty

namespace Projectiles
{
    public class RocketGuyProjectile : Projectile
    {
        
        #region fields
        private float _projectileSpeed;
        
        private Vector2 _dir;
        private Vector2 _spawnedAt;
        private float _range;
        private int _popCount;
        private int _pierce;
        private EnemyListener _listener;
        private bool _hasCollided;
        public GameObject explosion;
        private float _explosionRadius;
        [SerializeField]
        public ScriptableDamageType _damageType;
        private int _maxPop;
        private long _ID;
        #endregion
    
        private void Awake() {
            spawnedAt = transform.position;
            pierce = 1;
        }
        private void FixedUpdate() {
            ComputeMovement();
        }

        protected override void ComputeMovement() {
            transform.Translate(dir.normalized * (projectileSpeed * Time.deltaTime), Space.World);
            CheckIfOutsideRange();
        }

        protected override void Hit(Collider2D col) {
            if (!IsTargetActive()) return;
            if (_hasCollided) return;
            AffectExplosionColliders();
            _hasCollided = true;
            Instantiate(explosion, target.transform.position, Quaternion.identity);
            ResetThis();
        }

        private void AffectExplosionColliders() {
            Collider2D[] cols =
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics2D.OverlapCircleAll(target.transform.position, _explosionRadius, 1 << LayerMask.NameToLayer("Enemy"));
            if(cols.Length > _maxPop)
                for (int i = 0; i < _maxPop; i++) {
                    _listener.Income(cols[i].gameObject.GetComponent<AbstractEnemy>().Die(this, damage));
                }
            else {
                foreach (Collider2D aCollider in cols) {
                    _listener.Income(aCollider.gameObject.GetComponent<AbstractEnemy>().Die(this, damage));
                }
            }
        }

        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            _ID = Random.Range(0, 100000);
            _listener = listener;
            RocketUpgrade rocketUpgrade = (RocketUpgrade) upgrade;
            damage = rocketUpgrade.damage;
            pierce = rocketUpgrade.pierce;
            range = rocketUpgrade.range;
            projectileSpeed = rocketUpgrade.projectileSpeed;
            //_damageType = rocketUpgrade.damageType;
            _maxPop = rocketUpgrade.maxPop;
            _explosionRadius = rocketUpgrade.explosionRadius;
        }

        private void OnTriggerEnter2D(Collider2D col) {
            Hit(col);
        }

        #region get/set

        
        protected override float projectileSpeed {
            get => _projectileSpeed;
            set => _projectileSpeed = value;
        }

        protected override Vector2 dir {
            get => _dir;
            set => _dir = value;
        }

        public override int damage {
            get => _popCount;
            set => _popCount = value;
        }
        public override int pierce {
            get => _pierce;
            set => _pierce = value;
        }

        protected override float range {
            get => _range;
            set => _range = value;
        }

        protected override Vector2 spawnedAt {
            get => _spawnedAt;
            set => _spawnedAt = value;
        }
        public override ScriptableDamageType DamageType => _damageType;

        protected override bool hasCollided {
            get => _hasCollided;
            set => _hasCollided = value;
        }
        
        public override long ID => _ID;

        #endregion
    }
}
