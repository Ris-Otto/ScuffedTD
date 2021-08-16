using System;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace Helpers
{
    [Serializable]
    public class DamageType : IComparable<DamageType>
    {
        private const int DEFAULT = -1;

        [FormerlySerializedAs("_damageType")] public int damageType;

        private DamageType Normal() {
            damageType = DEFAULT;
            return this;
        }
        private static readonly DamageType NORMAL = new DamageType().Normal();
    
        private DamageType Sharp() {
            damageType = 0;
            return this;
        }
        public static readonly DamageType SHARP = new DamageType().Sharp();

        private DamageType Ice() {
            damageType = 1;
            return this;
        }
        public static readonly DamageType ICE = new DamageType().Ice();
        
        private DamageType Magic() {
            damageType = 2;
            return this;
        }
        public static readonly DamageType MAGIC = new DamageType().Magic();
        
        private DamageType Explosive() {
            damageType = 3;
            return this;
        }
        public static readonly DamageType EXPLOSIVE = new DamageType().Explosive();

        private DamageType Fire() {
            damageType = 4;
            return this;
        }
        public static readonly DamageType FIRE = new DamageType().Fire();
        
        private DamageType All() {
            damageType = 100;
            return this;
        }
        public static readonly DamageType ALL = new DamageType().All();
        
        public int CompareTo(DamageType other) {
            if (damageType < other.damageType)
                return -1;
            return damageType > other.damageType ? 1 : 0;
        }

        public override string ToString() {
            return damageType switch {
                -1 => "Normal",
                0 => "Sharp",
                1 => "Ice",
                2 => "Magic",
                3 => "Explosive",
                4 => "Fire",
                100 => "All",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

