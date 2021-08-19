using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class LeadBehaviour : AbstractEnemy
    {
        public GameObject[] _gameObjects;
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

        protected override bool IsAppropriateDamageType(ScriptableDamageType dmgType) {
            return Enemy.damageType.CompareTo(dmgType) <= 0;
        }

        protected override int ComputeOnHitBehaviour(Projectile projectile) {
            int pop = projectile.damage;
            if(pop >= Enemy.totalHealth) {
                ResetThis();
                return Enemy.totalHealth;
            }
            if(pop < 5) {
                GameObject[] children = 
                    {enemy.children[enemy.children.Length - 1], projectile.DamageType == DamageType
                        ? Enemy.children[Enemy.children.Length - 1]
                        : Enemy.children[Enemy.children.Length - pop]};
                InstantiateMultipleChildrenOnConditionsMet(children, projectile);
                ResetThis();
                return pop;
            }
            InstantiateChildOnConditionsMet(Enemy.children[Enemy.children.Length - (pop+1)], projectile);
            ResetThis();
            return pop;
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

        protected override bool dead {
            get => _dead;
            set => _dead = value;
        }
        protected override ActiveObjectsTracker et {
            get => _et;
            set => _et = value;
        }
        
        protected override ScriptableDamageType DamageType => enemy.damageType;

        public override Enemy Enemy => enemy;

        protected override Vector3 SpawnOffset {
            get => _spawnOffset;
            set => _spawnOffset = value;
        }

        #endregion
    }
}
