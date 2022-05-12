using UnityEngine;

namespace Managers
{
    public class RoundOverTimer : MonoBehaviour
    {
        private double _target;
        private double current;
        private bool tickingDown;
        

        public void StartTimer(double target) {
            tickingDown = true;
            _target = target;
            current = 0;
        }

        public bool Timer() {
            if (!(current >= _target)) return false;
            current = 0;
            tickingDown = false;
            return !tickingDown;
        }

        /*public void FixedUpdate() {
            if (!tickingDown) return;
            current += Time.deltaTime;
            Debug.Log(current + " : " + _target);
        }*/
    }
}