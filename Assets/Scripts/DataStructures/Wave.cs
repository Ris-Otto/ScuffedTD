using System.Collections;
using System.Collections.Generic;
using Enemies;
using Managers;
using UnityEngine;

namespace DataStructures
{
    public class Wave
    {
        private readonly List<BloonType> _bloons;
        private readonly float _timeUntilNext;
        private readonly Vector3 spawnPoint;
        private readonly int camoPos = -1;
        
        public Wave(float timeUntilNext, List<BloonType> types) {
            _timeUntilNext = timeUntilNext;
            _bloons = types;
            spawnPoint = GameObject.FindGameObjectWithTag("Pooler").transform.position;
        }

        public Wave(float timeUntilNext, List<BloonType> types, int camoPos) {
            this.camoPos = camoPos;
            _timeUntilNext = timeUntilNext;
            _bloons = types;
            spawnPoint = GameObject.FindGameObjectWithTag("Pooler").transform.position;
        }

        public IEnumerator SpawnEnemies(BloonType type) {
            for (int i = 0; i < type.Amount; i++) {
                if (i == camoPos) {
                    yield return SpawnSpecialistEnemy(type);
                }
                yield return SpawnEnemy(type);
            }
        }

        private YieldInstruction SpawnEnemy(BloonType type) {
            Object.Instantiate(type.Type, spawnPoint, Quaternion.identity);
            return new WaitForSeconds(type.Interval);
        }
        
        private YieldInstruction SpawnSpecialistEnemy(BloonType type) {
            AbstractEnemy e = Object.Instantiate(type.Type, spawnPoint, Quaternion.identity).GetComponent<AbstractEnemy>();
            e.SetCamo(ActiveObjectsTracker.Instance, true);
            return new WaitForSeconds(type.Interval);
        }
        
        public float TimeUntilNext() { return _timeUntilNext; }
        public IEnumerable<BloonType> Get() { return _bloons; }
    }
}
