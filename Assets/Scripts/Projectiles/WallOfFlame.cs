using System;
using Enemies;
using Managers;
using Units;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

namespace Projectiles
{
    public class WallOfFlame : Projectile
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
        private float _timeToLive;
        private float _time;
        #endregion

        protected override void ComputeMovement() {
            
        }
        
        public override void ResetProjectileFromEnemy() { }

        
        protected void Update() {
            _timeToLive += Time.deltaTime;
            _time += Time.deltaTime;
            if (_timeToLive >= 4f) {
                ResetThis();
            }
        }

        protected override void ResetThis() {
            _timeToLive = 0f;
            _time = 0f;
            base.ResetThis();
        }

        protected override void Awake() {
            base.Awake();
            _timeToLive = 0f;
            _time = 0f;
        }

        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            _ID = Random.Range(0, 100000);
            _listener = listener;
            _pierce = upgrade.pierce * 5;
            damage = 1;
        }
        
        private long GetID() {
            return Random.Range(0, 100000);
        }
        
        protected override void Hit(Collider2D col) {
            kills += _listener.Income(col.gameObject.GetComponent<AbstractEnemy>().Die(this, damage));
            if (pierce-- <= 0) {
                ResetThis();
            }
        }

        
        private void OnTriggerEnter2D(Collider2D other) {
            if (!(_time > 0.05f)) return;
            _ID = GetID();
            Hit(other);
            _time = 0.0f;
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
