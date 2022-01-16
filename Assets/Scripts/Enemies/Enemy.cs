using Projectiles;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy")]
    public class Enemy : ScriptableObject
    {
        public float speed;
        public int totalHealth;
        public int selfHealth;
        public ScriptableDamageType damageType;
        public GameObject popObject;
        public GameObject[] directChildren;

    }
}
