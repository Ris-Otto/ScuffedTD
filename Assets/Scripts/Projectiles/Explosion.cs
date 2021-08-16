using UnityEngine;

namespace Projectiles
{
    public class Explosion : MonoBehaviour
    {
        ParticleSystem exp;
        ParticleSystem.MainModule mainModule;

        private void Start() {
            exp = gameObject.GetComponent<ParticleSystem>();
            mainModule = exp.main;
        }
        
        private void Update()
        {
            if(!exp.isPlaying)
                Destroy(gameObject);
        }

        public void setDuration(float duration) {
            mainModule.duration = 0.1f*duration;
        }
    }
}
