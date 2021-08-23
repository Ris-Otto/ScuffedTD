using System;
using Helpers;
using JetBrains.Annotations;

namespace Upgrades
{
    public class MagickanUpgrade : IUpgrade
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
        public readonly string newProjectile;


        public MagickanUpgrade() {
            
        }
        
        public MagickanUpgrade(string upgradeName, int damage, int pierce, float range, float secondsPerAttackModifier,
            int price, float projectileSpeed, [NotNull] DamageType damageType, int shotCount) {
            _upgradeName = upgradeName;
            _damage = damage;
            _pierce = pierce;
            _range = range;
            _secondsPerAttackModifier = secondsPerAttackModifier;
            _projectileSpeed = projectileSpeed;
            _price = price;
            _shotCount = shotCount;
            _damageType = damageType ?? throw new ArgumentNullException(nameof(damageType), "DamageType can't be null");
        }
        
        public MagickanUpgrade(string upgradeName, int damage, int pierce, float range, float secondsPerAttackModifier,
            int price, float projectileSpeed) {
            _upgradeName = upgradeName;
            _damage = damage;
            _pierce = pierce;
            _range = range;
            _secondsPerAttackModifier = secondsPerAttackModifier;
            _projectileSpeed = projectileSpeed;
            _price = price;
        }

        public MagickanUpgrade(string upgradeName, string projectile, int price, int atkSpeed = 1) {
            _upgradeName = upgradeName;
            newProjectile = projectile;
            _secondsPerAttackModifier = atkSpeed;
            _price = price;
        }

        public int GetBuyValue() {
            return price;
        }

        public int GetSellValue() {
            return 0;
        }

        public void CumulateUpgrades(IUpgrade next) {
            _upgradeName = next.upgradeName;
            _damage += next.damage;
            _pierce += next.pierce;
            _range += next.range; 
            _secondsPerAttackModifier = next.secondsPerAttackModifier * secondsPerAttackModifier;
            _damageType = next.damageType ?? _damageType;
            _projectileSpeed += next.projectileSpeed;
            MagickanUpgrade gu = (MagickanUpgrade) next;
            _shotCount += gu._shotCount;
        }

        public void TryAddProjectile() {
            
        }

        public override string ToString() { return _upgradeName; }

        public int damage => _damage;

        public int pierce => _pierce;

        public float range => _range;

        public float secondsPerAttackModifier => _secondsPerAttackModifier;

        public DamageType damageType => _damageType;

        public int Shot_count => _shotCount;

        public int price => _price;

        public string upgradeName => _upgradeName;

        public float projectileSpeed => _projectileSpeed;
    }
}
