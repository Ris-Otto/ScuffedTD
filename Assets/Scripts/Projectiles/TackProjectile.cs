using System;
using Enemies;
using Managers;
using Units;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

namespace Projectiles
{
    public class TackProjectile : Projectile
    {
        [SerializeField]
        private float _projectileSpeed;
        private Vector2 _dir;
        [SerializeField]
        private Vector2 _spawnedAt;
        [SerializeField]
        private float _range;
        private int _popCount;
        private int _pierce;
        [SerializeField]
        private bool _hasCollided;
        [SerializeField]
        private ScriptableDamageType _damageType;
        private long _ID;

        protected override void Awake() {
            base.Awake();
            spawnedAt = transform.position;
        }

        private void FixedUpdate() {
            ComputeMovement();
        }
    
        protected override void ComputeMovement() {
            transform.Translate
                (dir.normalized * (projectileSpeed * Time.deltaTime), Space.World);
            CheckIfOutsideRange();
        }
    
        protected override void CheckIfOutsideRange() {
            if (Vector3.Distance(transform.position, spawnedAt) > (range + range*0.25)) 
                ResetThis();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Hit(other);
        }

        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            _ID = Random.Range(0, 100000);
            _listener = listener;
            TackUpgrade tackUpgrade = (TackUpgrade) upgrade;
            damage = tackUpgrade.damage;
            pierce = tackUpgrade.pierce;
            range = tackUpgrade.range;
            projectileSpeed = tackUpgrade.projectileSpeed;
        }

        protected override void Hit(Collider2D col) {
            //if (!IsTargetActive()) return;
            if (pierce <= 0 && _hasCollided) {
                ResetThis();
            }
            else {
                kills +=(_listener.Income(col.gameObject.GetComponent<AbstractEnemy>().Die(this, damage)));
                _hasCollided = true;
            }
            if (--pierce <= 0 && hasCollided) {
                ResetThis();
            }
        }

        #region getset

        protected override float projectileSpeed {
            get => _projectileSpeed;
            set => _projectileSpeed = value;
        }

        protected override Vector2 dir {
            get => _dir;
            set => _dir = value;
        }

        protected override int damage {
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
        
        public override long ID {
            get => _ID;
            set => _ID = value;
        }
    
        #endregion
    }
}
