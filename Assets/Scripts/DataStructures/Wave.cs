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
        private readonly float _timeUntilNext; //time-out tills nästa våg
        private readonly Vector3 spawnPoint;
        private readonly int camoPos = -1;
        private float waveLength;
        
        public Wave(float timeUntilNext, List<BloonType> types) {
            _timeUntilNext = timeUntilNext;
            _bloons = types;
            waveLength = _bloons[0].Amount * _bloons[0].Interval;
            spawnPoint = GameObject.FindGameObjectWithTag("Pooler").transform.position;
        }

        public Wave(float timeUntilNext, List<BloonType> types, int camoPos) {
            this.camoPos = camoPos;
            _timeUntilNext = timeUntilNext;
            _bloons = types;
            spawnPoint = GameObject.FindGameObjectWithTag("Pooler").transform.position;
        }

        //Coroutine
        public IEnumerator SpawnEnemies(BloonType type) {
            for (int i = 0; i < type.Amount; i++) {
                if (i == camoPos) {
                    yield return SpawnSpecialistEnemy(type); //gäller tills vidare enbart kamoflageballonger
                }
                yield return SpawnEnemy(type);
            }
        }

        private YieldInstruction SpawnEnemy(BloonType type) {
            Object.Instantiate(type.Type, spawnPoint, Quaternion.identity);
            return new WaitForSeconds(type.Interval);
        }
        
        private YieldInstruction SpawnSpecialistEnemy(BloonType type) {
            //AbstractEnemy är vår template för fiender
            AbstractEnemy e = Object.Instantiate(type.Type, spawnPoint, Quaternion.identity).GetComponent<AbstractEnemy>();
            //ActiveObjectsTracker är en Singleton som håller koll på torn och fiender
            e.SetCamo(ActiveObjectsTracker.Instance, true); //snuskig work-around p.g.a. dålig hjärna
            return new WaitForSeconds(type.Interval);
        }
        
        public float TimeUntilNext => _timeUntilNext; 
        public List<BloonType> Get => _bloons; 

        public float WaveLength => waveLength;
    }
}
