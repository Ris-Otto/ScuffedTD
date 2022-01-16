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
            return damageType.CompareTo(other.damageType);
        }
    }
}
