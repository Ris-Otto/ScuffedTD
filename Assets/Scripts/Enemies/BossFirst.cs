using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class BossFirst : AbstractEnemy
    {
        
        private float _distanceTravelled;
        private int _waypointIdx;
        private ActiveObjectsTracker _et;
        private Projectile lastProjectile;
        public Enemy enemy;
        private int selfHealth;
        private Vector3 _spawnOffset;

        private new void Awake() {
            base.Awake();
            selfHealth = Enemy.selfHealth;
        }

        protected override int ComputeOnHitBehaviourOverload(Projectile projectile, int remainingDamage) {
            if (selfHealth > 0) {
                selfHealth -= remainingDamage;
                return 0;
            }
            //Executive decision to not carry over damage to BossChildren
            InstantiateMultipleChildrenOnConditionsMet(Enemy.directChildren, projectile);
            return 1;
        }
        
        #region getters/setters

        protected override Projectile LastProjectile {
            get => lastProjectile;
            set => lastProjectile = value;
        }

        public override float distanceTravelled { 
            get => _distanceTravelled;
            protected set => _distanceTravelled = value; 
        }

        protected override int waypointIdx { get => _waypointIdx; set => _waypointIdx = value; }

        protected override ActiveObjectsTracker et {
            get => _et;
            set => _et = value;
        }


        public override Enemy Enemy => enemy;
        protected override ScriptableDamageType DamageType => enemy.damageType;

        protected override Vector3 SpawnOffset {
            get => _spawnOffset;
            set => _spawnOffset = value;
        }

        #endregion
    }
}
