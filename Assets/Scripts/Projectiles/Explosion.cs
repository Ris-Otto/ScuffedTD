using UnityEngine;

namespace Projectiles
{
    public class Explosion : MonoBehaviour
    {
        private ParticleSystem exp;

        private void Start() {
            exp = gameObject.GetComponent<ParticleSystem>();
        }
        
        private void FixedUpdate()
        {
            if(!exp.isPlaying)
                Destroy(gameObject);
        }
    }
}
