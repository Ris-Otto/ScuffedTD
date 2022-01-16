using System;
using Managers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public class GrandmaGun : Gun
    {
        private GameObject _projectile;
        private ProjectilePooler _pooler;
        private GameObject _target;
        private float _time;
        private IUpgrade _upgrade;
        private GameObject _parent;
        private GrandmaProjectile _parentUnit;
        private EnemyListener _listener;
        private const float BASE_ATTACK_SPEED = 1;

        protected override void ConfigureGun(GameObject p) {
            ConfigureTransform(p);
            ParentUnit1 = Parent.GetComponent<GrandmaProjectile>();
            Upgrade = ParentUnit1.currentUpgrade;
        }

        private new void Update() {
            ComputeShooting<Projectile>(ParentUnit1.TargetEnemy());
        }

        protected override void ComputeShooting<T>(GameObject target) {
            Time += UnityEngine.Time.deltaTime;
            if (!(Time > AttackSpeed)) return;
            Target = target;
            Shoot<T>();
        }

        protected override void Shoot<T>() {
            if (Target == null) return;
            ParentUnit1.ComputeMovementFromOther();
            HandleProjectileSpawn<T>();
            Time = 0.0f;
        }

        protected override void HandleProjectileSpawn<T>() {
            if (!(_upgrade is GrandmaUpgrade up)) return;
            for (int i = 0; i < up.shotCount; i++) {
                GameObject p = Pooler.SpawnFromPool(Name, transform.position, Quaternion.identity);
                Vector3 position = ConfigureProjectile<T>(p, out Vector2 direction, out Projectile tp);
                if (i != 0) direction = RotateVector(direction, i);
                tp.Master = _parentUnit.Master;
                ShootProjectile(tp, direction, position);
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

        protected override float AttackSpeed => BASE_ATTACK_SPEED * Upgrade.secondsPerAttackModifier;

        protected override GameObject Target {
            get => _target;
            set => _target = value;
        }

        protected override float Time {
            get => _time;
            set => _time = value;
        }
        
        protected virtual bool UsesSecondary => false;

        protected override IUpgrade Upgrade {
            get => (GrandmaUpgrade)_upgrade;
            set => _upgrade = value;
        }

        protected override GameObject Parent {
            get => _parent;
            set => _parent = value;
        }

        protected override AbstractUnit ParentUnit {
            get;
            set;
        }

        private GrandmaProjectile ParentUnit1 {
            get => _parentUnit;
            set => _parentUnit = value;
        }

        protected override EnemyListener Listener {
            get => _listener;
            set => _listener = value;
        }

        protected override GameObject Projectile {
            get => _projectile;
            set => _projectile = value;
        }
        
        protected override string Name => "Albin";

        #endregion
    }
}
