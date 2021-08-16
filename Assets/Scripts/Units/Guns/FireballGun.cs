using System.Linq.Expressions;
using Helpers;
using Projectiles;
using UnityEngine;

namespace Units.Guns
{
    public class FireballGun : Gun
    {
        
        private GameObject _projectile;
        private ProjectilePooler _pooler;
        private GameObject _target;
        private float _time;
        private IUpgrade _upgrade;
        private GameObject _parent;
        private MagickanUnit _parentMagickan;
        private EnemyListener _listener;
        private const float BASE_ATTACK_SPEED = 2;

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

        protected override string Name => "Fireball";

        #endregion
    }
}
