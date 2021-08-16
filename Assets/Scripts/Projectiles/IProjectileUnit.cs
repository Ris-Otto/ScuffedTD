using Helpers;
using Units;
using UnityEngine;

namespace Projectiles
{
    public interface IProjectileUnit
    {
        public ActiveObjectsTracker AOT { get; }
        public GameObject SecondaryProjectile { get; }
        
        public IUpgrade CurrentUpgrade { get; set; }

        public void HandleProjectileSpawn();

        public void TargetEnemy(int targetingStyle = 0);

        public void ComputeMovement();

        public void InheritUpgrades();

        public void SendParams(IUpgrade upgrade, EnemyListener listener);

        public void Shoot();

        public AbstractUnit GetStation();
    }
}
