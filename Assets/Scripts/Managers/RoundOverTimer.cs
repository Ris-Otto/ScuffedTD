using UnityEngine;

namespace Managers
{
    public class RoundOverTimer : MonoBehaviour
    {
        private double _target;
        private double current;
        

        public void StartTimer(double target) {
            _target = target;
            current = 0;
        }

        public bool Timer() {
            if (!(current >= _target)) return false;
            current = 0;
            return true;
        }

        public void FixedUpdate() {
            current += Time.deltaTime;
        }
    }
}