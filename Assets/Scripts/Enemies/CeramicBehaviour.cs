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
        private int selfHealth;
        private Vector3 _spawnOffset;
        private SpriteRenderer _spriteRenderer;
        
        protected new void Awake() {
            base.Awake();
            selfHealth = Enemy.selfHealth;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override int ComputeOnHitBehaviourOverload(Projectile projectile, int remainingDamage) {
            if (remainingDamage <= 0) return 0;
            if(remainingDamage >= Enemy.totalHealth) {
                ResetThis();
                return Enemy.totalHealth;
            }
            selfHealth -= remainingDamage;
            if (selfHealth <= 0) {
                AbstractEnemy[] es = InstantiateMultipleChildrenOnConditionsMet(Enemy.directChildren, projectile);
                ResetThis();
                return PassOnDamageToChild(projectile, remainingDamage-1, es[0]) + 1;
            }
            projectile.ResetProjectileFromEnemy();
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
