using System;
using Enemies;
using Managers;
using Units;
using Unity.Mathematics;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

namespace Projectiles
{
    public class FireballProjectile : Projectile
    {
        
        #region fields
        private float _projectileSpeed;
        public Vector2 _dir;
        private Vector2 _spawnedAt;
        [SerializeField] private float _range;
        [SerializeField] private int _damage;
        [SerializeField] private int _pierce;
        private EnemyListener _listener;
        private bool _hasCollided;
        public ScriptableDamageType _damageType;
        private float _shotRotation;
        public GameObject explosion;
        private long _ID;
        #endregion

        private void Awake() {
            _shotRotation = Mathf.PI / 2f;
        }
        
        private void FixedUpdate() {
            CheckIfOutsideRange();
            ComputeMovement();
        }
        
        private Vector2 RotateVector(Vector2 vector) {
            float x = Mathf.Cos(_shotRotation) * vector.x - Mathf.Sin(_shotRotation) * vector.y;
            float y = Mathf.Sin(_shotRotation) * vector.x + Mathf.Cos(_shotRotation) * vector.y;
            return new Vector2(x, y);
        }
        
        protected override void ComputeMovement() {
            if (!target.activeSelf) {
                ResetThis();
                return;
            }
            transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3)RotateVector(target.transform.position-transform.position));
            transform.Translate
                (dir.normalized * (projectileSpeed * Time.deltaTime), Space.World);
        }
        

        protected override void CheckIfOutsideRange() {
            if (!IsTargetActive()) {
                Vector3 position = transform.position;
                AffectExplosionColliders(position);
                Instantiate(explosion, position, Quaternion.identity);
                ResetThis();
            }
            if (Vector3.Distance(transform.position, spawnedAt) > (range*1.5f))
                ResetThis();
        }

        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            spawnedAt = transform.position;
            _ID = Random.Range(0, 100000);
            _listener = listener;
            MagickanUpgrade magick = (MagickanUpgrade) upgrade;
            damage = 3;
            pierce = 1;
            range = magick.range;
            projectileSpeed = 35f;
        }

        private void OnTriggerEnter2D(Collider2D col) {
            Hit(col);
        }
        
        protected override void Hit(Collider2D col) {
            AffectExplosionColliders();
            Instantiate(explosion, target.transform.position, Quaternion.identity);
            ResetThis();
        }
        
        private void AffectExplosionColliders() {
            Collider2D[] cols =
                Physics2D.OverlapCircleAll(target.transform.position, 1, LayerMask.GetMask("Enemy"));
            foreach (Collider2D aCollider in cols) {
                _listener.Income(aCollider.gameObject.GetComponent<AbstractEnemy>().Die(this, damage));
            }
        }
        
        private void AffectExplosionColliders(Vector3 pos) {
            Collider2D[] cols =
                Physics2D.OverlapCircleAll(pos, 1, LayerMask.GetMask("Enemy"));
            foreach (Collider2D aCollider in cols) {
                _listener.Income(aCollider.gameObject.GetComponent<AbstractEnemy>().Die(this, damage));
            }
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
