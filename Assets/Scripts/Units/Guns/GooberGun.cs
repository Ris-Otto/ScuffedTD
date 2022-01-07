using Helpers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public class GooberGun : Gun
    {
        
        private GameObject _projectile;
        private ProjectilePooler _pooler;
        private GameObject _target;
        private float _time;
        private IUpgrade _upgrade;
        private GameObject _parent;
        private GooberUnit _parentGoober;
        private EnemyListener _listener;
        private const float BASE_ATTACK_SPEED = 1;
        private const float SHOT_ROTATION = 75 / 360f;

        
        protected override void HandleProjectileSpawn<T>() {
            GooberUpgrade up = (GooberUpgrade) _upgrade;
            for (int i = 0; i < up.shotCount; i++) {
                GameObject p = Pooler.SpawnFromPool(Name, transform.position, Quaternion.identity);
                Vector3 position = ConfigureProjectile<T>(p, out Vector2 direction, out Projectile tp);
                if(i != 0) direction = RotateVector(direction, i % 2 == 0 ,SHOT_ROTATION);
                tp.Master = _parentGoober;
                tp.SendParams(Upgrade, _listener);
                tp.SeekTarget(Target, direction , position);
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
            get => _parentGoober;
            set => _parentGoober = (GooberUnit)value;
        }
        
        protected virtual bool UsesSecondary => false;

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
