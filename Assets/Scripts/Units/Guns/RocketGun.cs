using System;
using Managers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public class RocketGun : Gun
    {

        private RocketGuyUnit _parentUnit;

        private const float BASE_ATTACK_SPEED = 1.25f;
        
        
        #region getset

        protected override float AttackSpeed => BASE_ATTACK_SPEED * _upgrade.secondsPerAttackModifier;

        protected override AbstractUnit ParentUnit {
            get => _parentUnit;
            set => _parentUnit = (RocketGuyUnit)value;
        }
        
        protected override GameObject Projectile {
            get => _projectile;
            set => _projectile = value;
        }
        protected override string Name => "Bomba";

        #endregion
        
    }
}
