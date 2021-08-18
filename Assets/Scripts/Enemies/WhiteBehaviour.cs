using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class WhiteBehaviour : AbstractEnemy
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
            waypointIdx = 0;
            distanceTravelled = 0;
            et = ActiveObjectsTracker.Instance;
            SendThisHasSpawnedToActiveObjectsTracker(et);
        }

        protected override bool IsAppropriateDamageType(ScriptableDamageType dmgType) {
            Debug.Log(enemy.damageType.CompareTo(dmgType));
            Debug.Log(enemy.damageType.name + " " + enemy.damageType.damageType + " - " + dmgType.name + " " + dmgType.damageType);
            return enemy.damageType.CompareTo(dmgType) != 0;
        }

        protected override int ComputeOnHitBehaviour(Projectile projectile) {
            int pop = projectile.damage;
            if(pop >= enemy.totalHealth) {
                ResetThis();
                return enemy.totalHealth;
            }
            if(pop < 5) {
                //InstantiateChildOnConditionsMet(enemy.children[enemy.children.Length - 1], projectile);
                //InstantiateChildOnConditionsMet(enemy.children[enemy.children.Length-(pop)], projectile, true, TODO);
                GameObject[] children = 
                    {enemy.children[enemy.children.Length - 1], enemy.children[enemy.children.Length - (pop)]};
                InstantiateMultipleChildrenOnConditionsMet(children, projectile);
                ResetThis();
                return pop;
            }
            InstantiateChildOnConditionsMet(enemy.children[enemy.children.Length-(pop+1)], projectile);
            ResetThis();
            return pop;
        }

        protected override Projectile LastProjectile {
            get => lastProjectile;
            set => lastProjectile = value;
        }

        public override Enemy Enemy => enemy;
    
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

        protected override Vector3 SpawnOffset {
            get => _spawnOffset;
            set => _spawnOffset = value;
        }

        #endregion
    }
}
