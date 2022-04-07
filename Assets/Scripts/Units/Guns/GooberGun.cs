using Managers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public class GooberGun : Gun
    {
        
        private GooberUnit _parentGoober;
        
        private const float BASE_ATTACK_SPEED = 1;
        private const float SHOT_ROTATION = 75 / 360f;

        
        protected override void HandleProjectileSpawn<T>(GameObject target) {
            if (!(_upgrade is GooberUpgrade up)) return;
            for (int i = 0; i < up.shotCount; i++) {
                GameObject p = _pooler.SpawnFromPool(Name, transform.position, Quaternion.identity);
                Vector3 position = ConfigureProjectile<T>(p, target, out Vector2 direction, out Projectile tp);
                if (i != 0) direction = RotateVector(direction, i % 2 == 0, SHOT_ROTATION);
                tp.Master = _parentGoober;
                tp.SendParams(_upgrade, _listener);
                tp.SeekTarget(target, direction, position);
            }
        }

        private Vector2 RotateVector(Vector2 vector, bool mod, float shotRotation) {
            if (mod)
                shotRotation = -(shotRotation);
            float x = Mathf.Cos(shotRotation) * vector.x - Mathf.Sin(shotRotation) * vector.y;
            float y = Mathf.Sin(shotRotation) * vector.x + Mathf.Cos(shotRotation) * vector.y;
            return new Vector2(x, y);
        }

        #region getset

        protected override float AttackSpeed => BASE_ATTACK_SPEED * _upgrade.secondsPerAttackModifier;

        protected override AbstractUnit ParentUnit {
            get => _parentGoober;
            set => _parentGoober = (GooberUnit)value;
        }

        protected override GameObject Projectile {
            get => _projectile;
            set => _projectile = value;
        }
        
        protected override string Name => "Albin";

        #endregion
    }
}
