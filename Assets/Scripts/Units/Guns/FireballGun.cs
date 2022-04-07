using System.Linq.Expressions;
using Managers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public class FireballGun : Gun
    {
        
        private MagickanUnit _parentMagickan;

        private const float BASE_ATTACK_SPEED = 2;

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

        protected override string Name => "Fireball";

        #endregion
    }
}
