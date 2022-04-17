using UnityEngine;

namespace Managers
{
    public class EnemyListener : MonoBehaviour
    {
        private Economics _economics;

        private void Awake() {
            _economics = Economics.Instance;
        }

        public int Income(int killReward) {
            _economics.ReceiveIncome(killReward);
            return killReward;
        }
    }
}
