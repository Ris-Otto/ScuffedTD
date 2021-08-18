using System;
using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public class CeramicBehaviour : AbstractEnemy
    {
        
        private float _distanceTravelled;
        private int _waypointIdx;
        private bool _dead;
        private ActiveObjectsTracker _et;
        private Projectile lastProjectile;
        public Enemy enemy;
        private Vector3 _spawnOffset;
        private SpriteRenderer _spriteRenderer;
        
        protected new void Awake() {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override int ComputeOnHitBehaviour(Projectile projectile) {


            return 0;
        }

        protected override bool IsAppropriateDamageType(ScriptableDamageType dmgType) {
            return DamageType.CompareTo(dmgType) >= 0;
        }

        protected override void SetOffset(Vector2 dir, float size) {
            
        }

        public override int Die(Projectile projectile) {

            
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
