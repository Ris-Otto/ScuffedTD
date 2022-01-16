using System;
using JetBrains.Annotations;
using Projectiles;

namespace Upgrades
{
    public class TackUpgrade : IUpgrade
    {
        private string _upgradeName;
        private int _damage;
        private int _pierce;
        private float _range;
        private float _secondsPerAttackModifier;
        private float _projectileSpeed;
        private int _shotCount;
        private readonly int _price;
        private DamageType _damageType;
        private int _attackType;
        private bool _hasAccessToCamo;
        
    
        public TackUpgrade(string upgradeName, int damage, int pierce, float range, float secondsPerAttackModifier,
            int price, float projectileSpeed, int shotCount) {
            _upgradeName = upgradeName;
            _damage = damage;
            _pierce = pierce;
            _range = range;
            _secondsPerAttackModifier = secondsPerAttackModifier;
            _projectileSpeed = projectileSpeed;
            _price = price;
            _shotCount = shotCount;
        }

        public TackUpgrade(string upgradeName, int damage, int pierce, float range, float secondsPerAttackModifier,
            int price, float projectileSpeed, int attackType, int shotCount, DamageType damageType) {
            _upgradeName = upgradeName;
            _damage = damage;
            _pierce = pierce;
            _range = range;
            _secondsPerAttackModifier = secondsPerAttackModifier;
            _projectileSpeed = projectileSpeed;
            _attackType = attackType;
            _price = price;
            _shotCount = shotCount;
            _damageType = damageType;
        }

        public TackUpgrade() {
        
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
            TackUpgrade forShotCountOnly = (TackUpgrade) next;
            _shotCount += forShotCountOnly.Shot_count;
            _projectileSpeed += next.projectileSpeed;
            _attackType += forShotCountOnly.Attack_type;
        }

        public override string ToString() { return _upgradeName; }

        private int Attack_type => _attackType;

        public int damage => _damage;

        public int pierce => _pierce;

        public float range => _range;

        public float secondsPerAttackModifier => _secondsPerAttackModifier;

        public DamageType damageType => _damageType;

        public int Shot_count => _shotCount;

        public int price => _price;

        public string upgradeName => _upgradeName;

        public float projectileSpeed => _projectileSpeed;
        
        public bool hasAccessToCamo => _hasAccessToCamo;
    }
}
