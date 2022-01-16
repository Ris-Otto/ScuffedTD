using System;
using JetBrains.Annotations;
using Projectiles;

namespace Upgrades
{
    public class RocketUpgrade : IUpgrade
    {
        private string _upgradeName;
        private int _damage;
        private int _pierce;
        private float _range;
        private float _secondsPerAttackModifier;
        private float _projectileSpeed;
        private int _price;
        private DamageType _damageType;
        private float _explosionRadius;
        private int _maxPop;
        private bool _hasAccessToCamo;


        public RocketUpgrade(string upgradeName, int damage, int pierce, float range, float secondsPerAttackModifier,
            int price, float projectileSpeed, float explosionRadius, int maxPop, [NotNull] DamageType damageType) {
            _upgradeName = upgradeName;
            _damage = damage;
            _pierce = pierce;
            _range = range;
            _secondsPerAttackModifier = secondsPerAttackModifier;
            _projectileSpeed = projectileSpeed;
            _explosionRadius = explosionRadius;
            _maxPop = maxPop;
            _price = price;
            _damageType = damageType;
        }

        public RocketUpgrade(string upgradeName, int damage, int pierce, float range, float secondsPerAttackModifier,
            int price, float projectileSpeed, float explosionRadius, int maxPop) {
            _upgradeName = upgradeName;
            _damage = damage;
            _pierce = pierce;
            _range = range;
            _secondsPerAttackModifier = secondsPerAttackModifier;
            _projectileSpeed = projectileSpeed;
            _explosionRadius = explosionRadius;
            _maxPop = maxPop;
            _price = price;
        }

        public RocketUpgrade() {
        
        }
        public int GetBuyValue() {
            return price;
        }

        public int GetSellValue() {
            return 0;
        }

        public void CumulateUpgrades(IUpgrade next, IUpgrade last) {
            _upgradeName = next.upgradeName;
            _damage += next.damage;
            _pierce += next.pierce;
            _range += next.range;
            _hasAccessToCamo = next.hasAccessToCamo ? next.hasAccessToCamo : last.hasAccessToCamo;
            _secondsPerAttackModifier 
                = next.secondsPerAttackModifier * secondsPerAttackModifier;
            _damageType = next.damageType;
            _projectileSpeed += next.projectileSpeed;
        }

        public override string ToString() {
            return _upgradeName;
        }
    
        public int damage => _damage;

        public int pierce => _pierce;

        public float range => _range;

        public float secondsPerAttackModifier => _secondsPerAttackModifier;

        public DamageType damageType => _damageType;

        public int price => _price;

        public string upgradeName => _upgradeName;

        public float projectileSpeed => _projectileSpeed;
        public float explosionRadius => _explosionRadius;
        public int maxPop => _maxPop;
        
        public bool hasAccessToCamo => _hasAccessToCamo;
    }
}
