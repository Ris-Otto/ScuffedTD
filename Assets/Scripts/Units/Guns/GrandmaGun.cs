using System;
using Managers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public class GrandmaGun : Gun
    {
        
        private GrandmaProjectile _parentUnit;

        private const float BASE_ATTACK_SPEED = 1;

        protected override void ConfigureGun(GameObject p) {
            ConfigureTransform(p);
            ParentUnit1 = _parent.GetComponent<GrandmaProjectile>();
            _upgrade = ParentUnit1.currentUpgrade;
        }

        private new void FixedUpdate() {
            ComputeShooting<Projectile>(ParentUnit1.TargetEnemy());
        }

        protected override void ComputeShooting<T>(GameObject target) {
            _time += Time.deltaTime;
            if (!(_time > AttackSpeed)) return;
            Shoot<T>(target);
        }

        protected override void Shoot<T>(GameObject target) {
            if (target == null) return;
            ParentUnit1.ComputeMovementFromOther();
            HandleProjectileSpawn<T>(target);
            _time = 0.0f;
        }

        protected override void HandleProjectileSpawn<T>(GameObject target) {
            if (!(_upgrade is GrandmaUpgrade up)) return;
            for (int i = 0; i < up.shotCount; i++) {
                GameObject p = _pooler.SpawnFromPool(Name, transform.position, Quaternion.identity);
                Vector3 position = ConfigureProjectile<T>(p, target, out Vector2 direction, out Projectile tp);
                if (i != 0) direction = RotateVector(direction, i);
                tp.Master = _parentUnit.Master;
                ShootProjectile(tp, direction, position, target);
            }
        }

        protected override Vector3 ConfigureProjectileTransform(GameObject target, out Vector2 direction) {
            Vector3 position = _parent.transform.position;
            direction = Vector2.up;
            return position;
        }
        
        private Vector2 RotateVector(Vector2 vector, int amount) {
            float x = Mathf.Cos(_parentUnit._rotationAmount*amount) * vector.x - Mathf.Sin(_parentUnit._rotationAmount*amount) * vector.y;
            float y = Mathf.Sin(_parentUnit._rotationAmount*amount) * vector.x + Mathf.Cos(_parentUnit._rotationAmount*amount) * vector.y;
            return new Vector2(x, y);
        }

        #region getset

        protected override float AttackSpeed => BASE_ATTACK_SPEED * _upgrade.secondsPerAttackModifier;

        protected override AbstractUnit ParentUnit {
            get;
            set;
        }

        private GrandmaProjectile ParentUnit1 {
            get => _parentUnit;
            set => _parentUnit = value;
        }

        protected override GameObject Projectile {
            get => _projectile;
            set => _projectile = value;
        }
        
        protected override string Name => "Albin";

        #endregion
    }
}
