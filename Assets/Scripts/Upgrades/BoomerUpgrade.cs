using Projectiles;

namespace Upgrades
{
    public class BoomerUpgrade : IUpgrade
    {
        public int GetBuyValue() {
            return 0;
        }

        public int GetSellValue() {
            return 0;
        }

        public string name { get; }
        public void CumulateUpgrades(IUpgrade next, IUpgrade last) {
            
        }

        public float secondsPerAttackModifier { get; }
        public int damage { get; }
        public int pierce { get; }
        public float range { get; }
        public DamageType damageType { get; }
        public int price { get; }
        public string upgradeName { get; }
        public float projectileSpeed { get; }
        public bool hasAccessToCamo { get; }
    }
}