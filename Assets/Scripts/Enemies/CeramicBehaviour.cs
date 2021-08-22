using System;
using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class CeramicBehaviour : AbstractEnemy
    {
        
        private float _distanceTravelled;
        private int _waypointIdx;
        private ActiveObjectsTracker _et;
        private Projectile lastProjectile;
        public Enemy enemy;
        private Vector3 _spawnOffset;
        private SpriteRenderer _spriteRenderer;
        
        protected new void Awake() {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override int ComputeOnHitBehaviour(Projectile projectile) {
            int pop = projectile.damage;
            if (Enemy.totalHealth <= 0) return 0;
            Enemy.totalHealth -= pop;
            return 0;

        }

        public override int Die(Projectile projectile) {
            if (!CantBePoppedByProjectile(projectile)) return ComputeOnHitBehaviour(projectile);
            projectile.pierce++;
            return 0;
        }

        
        
        #region getset
        public override float distanceTravelled {
            get => _distanceTravelled;
            protected set => _distanceTravelled = value;
        }

        protected override int waypointIdx {
            get => _waypointIdx;
            set => _waypointIdx = value;
        }

        protected override ActiveObjectsTracker et {
            get => _et;
            set => _et = value;
        }

        public override Enemy Enemy => enemy;

        protected override Projectile LastProjectile {
            get => lastProjectile;
            set => lastProjectile = value;
        }
        protected override ScriptableDamageType DamageType => enemy.damageType;

        protected override Vector3 SpawnOffset {
            get => _spawnOffset;
            set => _spawnOffset = value;
        }
        #endregion
    }
}
