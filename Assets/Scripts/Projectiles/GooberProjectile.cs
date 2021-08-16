using System;
using Enemies;
using Helpers;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

namespace Projectiles
{
    public class GooberProjectile : Projectile
    {
        
        #region fields
        public float _projectileSpeed;
        private GameObject _target;
        public Vector2 _dir;
        private Vector2 _spawnedAt;
        [SerializeField]
        private float _range;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private int _pierce;
        private EnemyListener _listener;
        private bool _hasCollided;
        public ScriptableDamageType _damageType;
        private Animation anim;
        private long _ID;
        #endregion
    
        private void Awake() {
            anim = GetComponentInChildren<Animation>();
            spawnedAt = transform.position;
        }

        private void FixedUpdate() {
            if (!anim.isPlaying) {
                anim.Play("AlbinFlyingAnim");
            }
            ComputeMovement();
        }
        private void Update() { if (pierce == 0) ResetThis(); }

        protected override void ComputeMovement() {
            transform.Translate
                (dir.normalized * (projectileSpeed * Time.deltaTime), Space.World);
            CheckIfOutsideRange();
        }
    
        protected override void CheckIfOutsideRange() {
            if (Vector3.Distance(transform.position, spawnedAt) > (range + range*0.5f)) 
                gameObject.SetActive(false);
        }

        protected override void Hit(Collider2D col) {
            //if (!IsTargetActive()) return;
            if (pierce <= 0 ) {
                ResetThis();
            }
            else {
                _listener.Income(col.gameObject.GetComponent<AbstractEnemy>().Die(this));
                _hasCollided = true;
                pierce--;
            }
        }
        
        protected override void ResetThis() {
            hasCollided = false;
            gameObject.SetActive(false);
        }

        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            _ID = Random.Range(0, 100000);
            _listener = listener;
            damage = upgrade.damage;
            pierce = upgrade.pierce;
            range = upgrade.range;
            projectileSpeed = upgrade.projectileSpeed;
            //_damageType = upgrade.damageType;
        }
        
        private void OnTriggerEnter2D(Collider2D col) {
            Hit(col);
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
