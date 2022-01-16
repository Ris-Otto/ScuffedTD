using System;
using Helpers;
using JetBrains.Annotations;
using Units.Guns;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Upgrades
{
    public class GrandmaUpgrade : IUpgrade
    {
        
        private string _upgradeName;
        private int _damage;
        private int _pierce;
        private float _range;
        private float _secondsPerAttackModifier;
        private DamageType _damageType;
        private int _shotCount;
        private readonly int _price;
        private float _projectileSpeed;
        public int _gun;
        private bool _hasAccessToCamo;

        public GrandmaUpgrade(string upgradeName, int damage, int pierce, float range, float secondsPerAttackModifier,
            int price, float projectileSpeed, int shotCount, bool hasAccessToCamo = false, DamageType damageType = DamageType.FIRE) {
            _pierce = pierce;
            _upgradeName = upgradeName;
            _damage = damage;
            _range = range;
            _damageType = damageType;
            _secondsPerAttackModifier = secondsPerAttackModifier;
            _projectileSpeed = projectileSpeed;
            _hasAccessToCamo = hasAccessToCamo;
            _price = price;
            _shotCount = shotCount;
        }

        public GrandmaUpgrade() {
        
        }

        public GrandmaUpgrade(GrandmaUpgrade upgradeRef) {
            
        }

        public GrandmaUpgrade(string upgradeName, int damage, int price, int gun, float secondsPerAttackModifier, 
            bool hasAccessToCamo = false,
            DamageType damageType = DamageType.FIRE) {
            _upgradeName = upgradeName;
            _damage = damage;
            _damageType = damageType;
            _price = price;
            _hasAccessToCamo = hasAccessToCamo;
            _gun = gun;
        }

        public void CumulateUpgrades(IUpgrade next, IUpgrade last) {
            _upgradeName = next.upgradeName;
            _damage += next.damage != 0 ? next.damage : 0;
            _range += next.range != 0 ? next.range : 0;
            _hasAccessToCamo = next.hasAccessToCamo ? next.hasAccessToCamo : last.hasAccessToCamo;
            _secondsPerAttackModifier 
                = next.secondsPerAttackModifier != 0 ? next.secondsPerAttackModifier : 1 * (secondsPerAttackModifier);
            _damageType = next.damageType;
            _projectileSpeed += next.projectileSpeed != 0 ? next.projectileSpeed : 0;
            GrandmaUpgrade gu = (GrandmaUpgrade) next;
            _gun = gu._gun != 0 ? gu._gun : _gun;
            _shotCount += gu.shotCount;
        }


        public int GetBuyValue() {
            return price;
        }

        public int GetSellValue() {
            return 0;
        }

        public override string ToString() {
            return _upgradeName;
        }
    
        public int damage => _damage;

        public int pierce => _pierce;

        public float range {
            get => _range;
            set => _range = value;
        }

        public float secondsPerAttackModifier => _secondsPerAttackModifier;

        public DamageType damageType => _damageType;

        public int shotCount => _shotCount;

        public int price => _price;

        public string upgradeName => _upgradeName;

        public float projectileSpeed => _projectileSpeed;
        
        public bool hasAccessToCamo => _hasAccessToCamo;
    }
}
