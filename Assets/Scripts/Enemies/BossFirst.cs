using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class BossFirst : AbstractEnemy
    {
        
        private float _distanceTravelled;
        private int _waypointIdx;
        private bool _dead;
        private ActiveObjectsTracker _et;
        private Projectile lastProjectile;
        public Enemy enemy;
        private Vector3 _spawnOffset;

        private new void Awake() {
            base.Awake();
        }

        protected override void SetOffset(Vector2 dir, float size) {
            SpawnOffset = Vector3.zero;
        }
        
        public override int Die(Projectile projectile) {
            if (!CantBePoppedByProjectile(projectile)) return ComputeOnHitBehaviour(projectile);
            projectile.pierce++;
            return 0;
        }
        
        protected override int ComputeOnHitBehaviour(Projectile projectile) {
            int pop = projectile.damage;
            if(pop >= Enemy.totalHealth) {
                ResetThis();
                return Enemy.totalHealth;
            }
            InstantiateChildOnConditionsMet(Enemy.children[Enemy.children.Length-(pop)], projectile);
            ResetThis();
            return pop;
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

        protected override bool dead {
            get => _dead;
            set => _dead = value;
        }

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
