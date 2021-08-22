using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class ZebraBehaviour : AbstractEnemy
    {
        private float _distanceTravelled;
        private int _waypointIdx;
        private bool _dead;
        private ActiveObjectsTracker _et;
        private Projectile lastProjectile;
        public Enemy enemy;
        private Vector3 _spawnOffset;

        protected new void Awake() {
            base.Awake();
        }

        protected override bool IsAppropriateDamageType(ScriptableDamageType dmgType) {
            return dmgType.damageType.damageType != 3 && dmgType.damageType.damageType != 3; //TODO XD
        }

        /*protected override int ComputeOnHitBehaviour(Projectile projectile) {
            int pop = projectile.damage;
            if (pop >= enemy.totalHealth) {
                ResetThis();
                return enemy.totalHealth;
            }
            if (pop >= 2) {
                GameObject[] children = 
                    {enemy.children[enemy.children.Length - 1], enemy.children[enemy.children.Length - (2)]};
                InstantiateMultipleChildrenOnConditionsMet(children, projectile);
                ResetThis();
                return pop;
            }
            GameObject[] childrens =
                {enemy.children[enemy.children.Length - 1], enemy.children[enemy.children.Length - (pop+1)]};
            InstantiateMultipleChildrenOnConditionsMet(childrens, projectile);
            ResetThis();
            return pop;
        }*/



        #region getset
        public override float distanceTravelled {
            get => _distanceTravelled;
            protected set => _distanceTravelled = value;
        }

        protected override int waypointIdx {
            get => _waypointIdx;
            set => _waypointIdx = value;
        }

        protected override bool dead {
            get => _dead;
            set => _dead = value;
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
