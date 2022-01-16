using System;
using Managers;
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
        private float _timeToSave;
        
        protected new void Awake() {
            base.Awake();
            selfHealth = Enemy.selfHealth;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override int ComputeOnHitBehaviour(Projectile projectile, int remainingDamage) {
            if (IsCamo && !projectile.Master.CanAccessCamo) return 0;
            if (remainingDamage <= 0) return 0;
            if(remainingDamage >= Enemy.totalHealth) {
                ResetThis();
                return Enemy.totalHealth;
            }
            selfHealth -= remainingDamage;
            if (selfHealth <= 0) {
                AbstractEnemy[] es = InstantiateChildren(Enemy.directChildren, projectile);
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
        protected override ScriptableDamageType damageType => enemy.damageType;

        protected override float timeToSave {
            get => _timeToSave;
            set => _timeToSave = value;
        }
        
        protected override Vector3 savedPos {
            get => _spawnOffset;
            set => _spawnOffset = value;
        }
        
        #endregion
    }
}
