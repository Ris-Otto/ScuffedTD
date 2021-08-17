using Helpers;
using UnityEngine;

namespace Projectiles
{
    public class WallOfFlame : Projectile
    {
        protected override void ComputeMovement() {
            
        }

        public override void SendParams(IUpgrade upgrade, EnemyListener listener) {
            
        }

        protected override float projectileSpeed { get; set; }
        protected override GameObject target { get; set; }
        protected override Vector2 dir { get; set; }
        public override int damage { get; set; }
        public override int pierce { get; set; }
        protected override float range { get; set; }
        protected override Vector2 spawnedAt { get; set; }
        protected override bool hasCollided { get; set; }
        public override long ID { get; }
        public override ScriptableDamageType DamageType { get; }
    }
}
