using System;
using System.Collections;
using Helpers;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        private RoundInformation _roundInformation;
        private EnemyPooler _pooler;
        
        private void Awake() {
            _roundInformation = RoundInformation.Instance;
            _pooler = EnemyPooler.Instance;
        }
    }
}
