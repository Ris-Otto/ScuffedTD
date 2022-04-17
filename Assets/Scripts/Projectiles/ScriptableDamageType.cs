using System;
using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu(fileName = nameof(name), menuName = "ScriptableObject/DamageType")]
    public class ScriptableDamageType : ScriptableObject, IComparable<ScriptableDamageType>
    {

        public DamageType damageType;

        public new string name;

        public int CompareTo(ScriptableDamageType other) {
            return CompareTo(other.damageType);
        }

        private int CompareTo(DamageType type) {
            if (damageType > type) {
                return 1;
            }
            if(damageType < type) return -1;
            
            return 0;
        }
    }
}
