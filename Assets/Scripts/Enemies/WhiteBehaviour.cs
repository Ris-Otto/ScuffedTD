using Managers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class WhiteBehaviour : AbstractEnemy
    {
        private float _distanceTravelled;
        private int _waypointIdx;
        private ActiveObjectsTracker _et;
        private Projectile lastProjectile;
        public Enemy enemy;
        private Vector3 _spawnOffset;
        private float _timeToSave;
        private bool _isCamo;
    
    
        private new void Awake() {
            base.Awake();
        }

        public override bool IsAppropriateDamageType(Projectile projectile) {
            return enemy.damageType.CompareTo(projectile.DamageType) != 0;
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
        
        protected override ActiveObjectsTracker et {
            get => _et;
            set => _et = value;
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
