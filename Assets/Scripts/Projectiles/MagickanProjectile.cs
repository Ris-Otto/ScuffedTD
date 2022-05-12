using System;
using Enemies;
using Managers;
using Units;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

namespace Projectiles
{
    public class MagickanProjectile : Projectile
    {
        #region fields
        public float _projectileSpeed;
        public Vector2 _dir;
        private Vector2 _spawnedAt;
        private float _range;
        private int _damage;
        private int _pierce;
        private bool _hasCollided;
        public ScriptableDamageType _damageType;
        private long _ID;
        #endregion

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

        /*protected override void Hit(Collider2D col) {
            kills += _listener.Income(col.gameObject.GetComponent<AbstractEnemy>().Die(this, damage));
            _hasCollided = true;
            if (--pierce <= 0 && hasCollided) {
                ResetThis();
            }
        }*/
        
        
        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            _ID = Random.Range(0, 100000);
            _listener = listener;
            MagickanUpgrade magick = (MagickanUpgrade) upgrade;
            damage = magick.damage;
            pierce = magick.pierce;
            range = magick.range;
            projectileSpeed = magick.projectileSpeed;
            //_damageType = magick.damageType;
        }

        private void OnTriggerEnter2D(Collider2D col) {
            Hit(col);
        }

        #region getters/setters

        protected override float projectileSpeed {
            get => _projectileSpeed;
            set => _projectileSpeed = value;
        }

        protected override Vector2 dir {
            get => _dir;
            set => _dir = value;
        }

        protected override int damage {
            get => _damage;
            set => _damage = value;
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
        }

        #endregion
    }
}
