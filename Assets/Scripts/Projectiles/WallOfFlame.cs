using System;
using Enemies;
using Helpers;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

namespace Projectiles
{
    public class WallOfFlame : Projectile
    {
        #region fields
        public float _projectileSpeed;
        private GameObject _target;
        public Vector2 _dir;
        private Vector2 _spawnedAt;
        private float _range;
        private int _damage;
        private int _pierce;
        private EnemyListener _listener;
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

        protected void Awake() {
            _timeToLive = 0f;
            _time = 0f;
        }

        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            _ID = Random.Range(0, 100000);
            _listener = listener;
            damage = 1;
        }
        
        private long GetID() {
            return Random.Range(0, 100000);
        }
        
        protected override void Hit(Collider2D col) {
            _listener.Income(col.gameObject.GetComponent<AbstractEnemy>().DieOverload(this, damage));
        }

        
        private void OnTriggerStay2D(Collider2D other) {
            if (!(_time > 0.1f)) return;
            _ID = GetID();
            Hit(other);
            _time = 0.0f;
        }

        #region getters/setters

        protected override float projectileSpeed {
            get => _projectileSpeed;
            set => _projectileSpeed = value;
        }

        protected override GameObject target {
            get => _target;
            set => _target = value;
        }

        protected override Vector2 dir {
            get => _dir;
            set => _dir = value;
        }

        public override int damage {
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
