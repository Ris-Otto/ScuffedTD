using System;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace Helpers
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

