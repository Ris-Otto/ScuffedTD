using Managers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class ZebraBehaviour : AbstractEnemy
    {
        private float _distanceTravelled;
        private int _waypointIdx;
        private ActiveObjectsTracker _et;
        private Projectile lastProjectile;
        public Enemy enemy;
        private Vector3 _spawnOffset;
        private float _timeToSave;
        private bool _isCamo;

        protected new void Awake() {
            base.Awake();
        }

        public override bool IsAppropriateDamageType(Projectile projectile) {
            return projectile.DamageType.damageType != DamageType.ICE && projectile.DamageType.damageType != DamageType.EXPLOSIVE; //TODO XD
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
