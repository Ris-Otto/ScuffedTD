using Managers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public class MagickanGun : Gun
    {
        private MagickanUnit _parentMagickan;

        private const float BASE_ATTACK_SPEED = 1;

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

        protected override string Name => "MagickanProjectile";

        #endregion
    }
}
