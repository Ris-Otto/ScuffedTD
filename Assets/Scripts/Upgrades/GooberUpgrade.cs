using System;
using JetBrains.Annotations;
using Projectiles;
using UnityEngine;

namespace Upgrades
{
    public class GooberUpgrade : IUpgrade{

        private string _upgradeName;
        private int _damage;
        private int _pierce;
        private float _range;
        private float _secondsPerAttackModifier;
        private DamageType _damageType;
        private int _shotCount;
        private readonly int _price;
        private float _projectileSpeed;
        private bool _hasAccessToCamo;
        
    
        public GooberUpgrade(string upgradeName, int damage, int pierce, float range, float secondsPerAttackModifier,
            int price, float projectileSpeed, int shotCount, DamageType damageType = DamageType.SHARP) {
            _upgradeName = upgradeName;
            _damage = damage;
            _pierce = pierce;
            _range = range;
            _secondsPerAttackModifier = secondsPerAttackModifier;
            _projectileSpeed = projectileSpeed;
            _price = price;
            _shotCount = shotCount;
            _damageType = damageType;
        }
        
        public GooberUpgrade(string upgradeName, int damage, int pierce, float range, float secondsPerAttackModifier,
            int price, float projectileSpeed, int shotCount, bool camoAccess, DamageType damageType = DamageType.SHARP) {
            _hasAccessToCamo = camoAccess;
            _upgradeName = upgradeName;
            _damage = damage;
            _pierce = pierce;
            _range = range;
            _secondsPerAttackModifier = secondsPerAttackModifier;
            _projectileSpeed = projectileSpeed;
            _price = price;
            _shotCount = shotCount;
            _damageType = damageType;
        }

        public GooberUpgrade() {
        
        }

        public void CumulateUpgrades(IUpgrade next, IUpgrade last) {
            _upgradeName = next.upgradeName;
            _hasAccessToCamo = next.hasAccessToCamo ? next.hasAccessToCamo : last.hasAccessToCamo;
            _damage += next.damage;
            _pierce += next.pierce;
            _range += next.range;
            _secondsPerAttackModifier 
                = next.secondsPerAttackModifier * secondsPerAttackModifier;
            _damageType = next.damageType;
            _projectileSpeed += next.projectileSpeed;
            GooberUpgrade gu = (GooberUpgrade) next;
            _shotCount += gu._shotCount;
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

        public float range => _range;

        public float secondsPerAttackModifier => _secondsPerAttackModifier;

        public DamageType damageType => _damageType;

        public int shotCount => _shotCount;

        public int price => _price;

        public string upgradeName => _upgradeName;

        public float projectileSpeed => _projectileSpeed;

        public bool hasAccessToCamo => _hasAccessToCamo;
    }
}
