using System;
using UnityEngine;

namespace Helpers
{
    public class EnemyListener : MonoBehaviour
    {
        private Economics _economics;

        private void Awake() {
            _economics = Economics.Instance;
        }

        public void Income(int killReward) {
            uint u = Convert.ToUInt32(killReward);
            _economics.ReceiveIncome(u);
        }
    }
}
