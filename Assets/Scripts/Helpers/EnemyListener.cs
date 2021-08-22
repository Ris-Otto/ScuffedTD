using System;
using UnityEngine;

namespace Helpers
{
    public class EnemyListener : MonoBehaviour
    {
        private Economics _economics;

        private void Awake() {
            _economics = GameObject.FindGameObjectWithTag("EconomicsHandler").GetComponent<Economics>();
        }

        public void Income(int killReward) {
            uint u = Convert.ToUInt32(killReward);
            _economics.ReceiveIncome(u);
        }
    }
}
