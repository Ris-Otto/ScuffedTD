using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Spawners;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DataStructures
{
    public class Wave
    {
        private List<BloonType> _bloons;
        private readonly float _timeUntilNext;
        private readonly Vector3 spawnPoint;
        
        public Wave(float timeUntilNext, List<BloonType> types) {
            _timeUntilNext = timeUntilNext;
            _bloons = types;
            spawnPoint = GameObject.FindGameObjectWithTag("Pooler").transform.position;
        }

        public IEnumerator SpawnEnemies(BloonType type) {
            for (int i = 0; i < type.Amount; i++) {
                Object.Instantiate(type.Type, spawnPoint, Quaternion.identity);
                yield return new WaitForSeconds(type.Interval);
            }
        }
        
        public float TimeUntilNext() { return _timeUntilNext; }
        public List<BloonType> Get() { return _bloons; }
    }
}
