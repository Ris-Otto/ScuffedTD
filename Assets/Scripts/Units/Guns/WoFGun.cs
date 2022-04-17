using Projectiles;
using UnityEngine;

namespace Units.Guns
{
    public class WoFGun : Gun
    {
        private MagickanUnit _parentMagickan;

        private const float BASE_ATTACK_SPEED = 5;

        private new void Awake() {
            base.Awake();
            _time = BASE_ATTACK_SPEED;
        }

        private new void FixedUpdate() {
            ComputeShooting<Projectile>(_parentMagickan.TargetPathTile());
        }
        
        protected override void Shoot<T>(GameObject target) {
            if (target == null) return;
            ParentUnit.ComputeRotationFromChild();
            HandleProjectileSpawn<T>(target);
            _time = 0.0f;
        }

        protected override void HandleProjectileSpawn<T>(GameObject target) {
            GameObject p = _pooler.SpawnFromPool(Name, target.transform.position, Quaternion.identity);
            Vector3 position = ConfigureProjectile<T>(p, target, out Projectile projectile);
            ShootProjectile(projectile, Vector2.zero, position, target);
        }

        private Vector3 ConfigureProjectileTransform(GameObject target) {
            return target.transform.position;
        }
        
        private Vector3 ConfigureProjectile<T>(GameObject p, GameObject target, out Projectile projectile) where T : Projectile {
            Vector3 position = ConfigureProjectileTransform(target);
            projectile = p.GetComponent<T>();
            projectile.Master = ParentUnit;
            Debug.Log(projectile.Master);
            return position;
        }

        #region getset
        
        protected override float AttackSpeed => BASE_ATTACK_SPEED * _upgrade.secondsPerAttackModifier;
        

        protected override AbstractUnit ParentUnit {
            get => _parentMagickan;
            set => _parentMagickan = (MagickanUnit)value;
        }
        
        protected override GameObject Projectile {
            get => _projectile;
            set => _projectile = value;
        }

        protected override string Name => "WoF";

        #endregion
    }
}
