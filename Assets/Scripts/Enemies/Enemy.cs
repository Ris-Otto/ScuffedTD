using Helpers;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy")]
    public class Enemy : ScriptableObject
    {
        public float speed;
        public int totalHealth;
        public ScriptableDamageType damageType;
        public GameObject popObject;
        public GameObject[] children;
        public GameObject[] directChild;

    }
}
