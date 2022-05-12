using Managers;
using Units;
using UnityEngine;
using Upgrades;

namespace Projectiles
{
    public class RoFProjectile : Projectile
    {
        [SerializeField]
        private ScriptableDamageType _damageType;
        private int _damage;
        private float _range;
        private long _ID;

        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            _listener = listener;
            TackUpgrade tackUpgrade = (TackUpgrade) upgrade;
            damage = tackUpgrade.damage;
            range = tackUpgrade.range;
            
        }

        protected override void ComputeMovement() {
            
        }


        protected override void Hit(Collider2D col) {
            
        }
        

        protected override float projectileSpeed { get; set; }

        protected override Vector2 dir { get; set; }

        protected override int damage {
            get => _damage;
            set => _damage = value;
        }
        public override int pierce { get; set; }

        protected override float range {
            get => _range;
            set => _range = value;
        }

        protected override Vector2 spawnedAt { get; set; }
        protected override bool hasCollided { get; set; }
        
        public override long ID {
            get => _ID;
        }

        public override ScriptableDamageType DamageType => _damageType;
    }
}
