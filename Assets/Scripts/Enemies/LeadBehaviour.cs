using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class LeadBehaviour : AbstractEnemy
    {
        private float _distanceTravelled;
        private int _waypointIdx;
        private ActiveObjectsTracker _et;
        private Projectile lastProjectile;
        public Enemy enemy;
        private Vector3 _spawnOffset;
        private float _timeToSave;

        private new void Awake() {
            base.Awake();
        }

        protected override int ComputeOnHitBehaviourOverload(Projectile projectile, int remainingDamage) {
            if (remainingDamage <= 0) return 0;
            if(remainingDamage >= Enemy.totalHealth) {
                ResetThis();
                return Enemy.totalHealth;
            }
            AbstractEnemy[] es = InstantiateMultipleChildrenOnConditionsMet(Enemy.directChildren, projectile);
            ResetThis();
            if (!es[0].IsAppropriateDamageType(projectile)) return 1;
            return PassOnDamageToChild(projectile, remainingDamage-1, es[0]) + 1;
        }

        protected override Projectile LastProjectile {
            get => lastProjectile;
            set => lastProjectile = value;
        }
    
        #region getters/setters

        public override float distanceTravelled { 
            get => _distanceTravelled;
            protected set => _distanceTravelled = value; 
        }

        protected override int waypointIdx { get => _waypointIdx; set => _waypointIdx = value; }
        
        protected override ActiveObjectsTracker et {
            get => _et;
            set => _et = value;
        }
        
        protected override ScriptableDamageType DamageType => enemy.damageType;

        public override Enemy Enemy => enemy;

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
