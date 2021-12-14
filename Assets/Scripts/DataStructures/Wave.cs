using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStructures
{
    public class Wave
    {
        private readonly List<BloonType> _bloons;
        private readonly float _timeUntilNext;
        private readonly Vector3 spawnPoint;
        
        public Wave(float timeUntilNext, List<BloonType> types) {
            _timeUntilNext = timeUntilNext;
            _bloons = types;
            spawnPoint = GameObject.FindGameObjectWithTag("Pooler").transform.position;
        }

        public IEnumerator SpawnEnemies(BloonType type) {
            for (int i = 0; i < type.Amount; i++) {
                yield return SpawnEnemy(type);
            }
        }

        private YieldInstruction SpawnEnemy(BloonType type) {
            Object.Instantiate(type.Type, spawnPoint, Quaternion.identity);
            return new WaitForSeconds(type.Interval);
        }
        
        public float TimeUntilNext() { return _timeUntilNext; }
        public IEnumerable<BloonType> Get() { return _bloons; }
    }
}
