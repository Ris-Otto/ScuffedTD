using System;

namespace Projectiles
{
    [Serializable]
    public enum DamageType
    {
        SHARP = 0,
        ICE = 1,
        MAGIC = 2,
        EXPLOSIVE = 4,
        FIRE = 8,
        ALL = 1024
    }
}

