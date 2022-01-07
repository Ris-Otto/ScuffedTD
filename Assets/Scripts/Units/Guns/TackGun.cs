using Helpers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public class TackGun : Gun
    {
        
        private GameObject _projectile;
        private ProjectilePooler _pooler;
        private GameObject _target;
        private float _time;
        private IUpgrade _upgrade;
        private GameObject _parent;
        private TackUnit _parentUnit;
        private EnemyListener _listener;
        private const float BASE_ATTACK_SPEED = 1;
        

        protected override void HandleProjectileSpawn<T>() {
            TackUpgrade up = (TackUpgrade) _upgrade;
            for (int i = 0; i < up.Shot_count; i++) {
                GameObject p = Pooler.SpawnFromPool(Name, transform.position, Quaternion.identity);
                Vector3 position = ConfigureProjectile<T>(p, out Vector2 direction, out Projectile projectile);
                if(i != 0) direction = RotateVector(direction, i);
                ShootProjectile(projectile, direction, position);
            }
        }

        protected override Vector3 ConfigureProjectileTransform(out Vector2 direction) {
            Vector3 position = Parent.transform.position;
            direction = Vector2.up;
            return position;
        }
        
        private Vector2 RotateVector(Vector2 vector, int amount) {
            float x = Mathf.Cos(_parentUnit._rotationAmount*amount) * vector.x - Mathf.Sin(_parentUnit._rotationAmount*amount) * vector.y;
            float y = Mathf.Sin(_parentUnit._rotationAmount*amount) * vector.x + Mathf.Cos(_parentUnit._rotationAmount*amount) * vector.y;
            return new Vector2(x, y);
        }

        #region getset
        protected override ProjectilePooler Pooler {
            get => _pooler;
            set => _pooler = value;
        }

        protected override float AttackSpeed => BASE_ATTACK_SPEED * _upgrade.secondsPerAttackModifier;

        protected override GameObject Target {
            get => _target;
            set => _target = value;
        }
        
        protected virtual bool UsesSecondary => false;

        protected override float Time {
            get => _time;
            set => _time = value;
        }

        protected override IUpgrade Upgrade {
            get => (TackUpgrade)_upgrade;
            set => _upgrade = value;
        }

        protected override GameObject Parent {
            get => _parent;
            set => _parent = value;
        }

        protected override AbstractUnit ParentUnit {
            get => _parentUnit;
            set => _parentUnit = (TackUnit)value;
        }

        protected override EnemyListener Listener {
            get => _listener;
            set => _listener = value;
        }

        protected override GameObject Projectile {
            get => _projectile;
            set => _projectile = value;
        }
        
        protected override string Name => "TackProjectile";

        #endregion
    }
}
