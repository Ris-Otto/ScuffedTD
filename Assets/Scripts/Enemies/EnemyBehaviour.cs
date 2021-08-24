using System.Linq;
using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class EnemyBehaviour : AbstractEnemy
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

