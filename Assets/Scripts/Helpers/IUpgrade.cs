namespace Helpers
{
    public interface IUpgrade : IMoneyObject
    {
        //Simply a container for Upgrade types. idk.
        public string ToString();
        public void CumulateUpgrades(IUpgrade next);

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
