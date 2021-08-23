using Helpers;
using Projectiles;
using UnityEngine;

namespace Units.Guns
{
    public class WoFGun : Gun
    {
        private GameObject _projectile;
        private ProjectilePooler _pooler;
        private GameObject _target;
        private float _time;
        private IUpgrade _upgrade;
        private GameObject _parent;
        private MagickanUnit _parentMagickan;
        private EnemyListener _listener;
        private const float BASE_ATTACK_SPEED = 5;

        private new void Awake() {
            base.Awake();
            Time = BASE_ATTACK_SPEED;
        }

        private new void Update() {
            ComputeShooting<Projectile>(_parentMagickan.TargetPathTile());
        }
        
        protected override void Shoot<T>() {
            if (Target == null) return;
            ParentUnit.ComputeRotationFromChild();
            HandleProjectileSpawn<T>();
            Time = 0.0f;
        }

        protected override void HandleProjectileSpawn<T>() {
            GameObject p = Pooler.SpawnFromPool(Name, _target.transform.position, Quaternion.identity);
            Vector3 position = ConfigureProjectile<T>(p, out Projectile projectile);
            ShootProjectile(projectile, Vector2.zero, position);
        }

        private Vector3 ConfigureProjectileTransform() {
            Vector3 position = _target.transform.position;
            return position;
        }
        
        private Vector3 ConfigureProjectile<T>(GameObject p, out Projectile projectile) where T : Projectile {
            Vector3 position = ConfigureProjectileTransform();
            projectile = p.GetComponent<T>();
            return position;
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

        protected override float Time {
            get => _time;
            set => _time = value;
        }

        protected override IUpgrade Upgrade {
            get => _upgrade;
            set => _upgrade = value;
        }

        protected override GameObject Parent {
            get => _parent;
            set => _parent = value;
        }

        protected override AbstractUnit ParentUnit {
            get => _parentMagickan;
            set => _parentMagickan = (MagickanUnit)value;
        }

        protected override EnemyListener Listener {
            get => _listener;
            set => _listener = value;
        }

        protected override GameObject Projectile {
            get => _projectile;
            set => _projectile = value;
        }

        protected override bool UsesSecondary => true;

        protected override string Name => "WoF";

        #endregion
    }
}
