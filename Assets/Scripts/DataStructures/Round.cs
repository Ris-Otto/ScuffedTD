using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DataStructures
{
    public class BloonType
    {
        public BloonType(string type, int amount, float interval) {
            _type = Resources.Load<GameObject>("Prefabs/Enemies/" + type);
            _amount = amount;
            _interval = interval;
        }
        private readonly GameObject _type;
        private int _amount;
        private float _interval;
        public GameObject Type => _type;
        public int Amount=> _amount;
        public float Interval => _interval;
    }
    
    public class Round
    {
        
        private readonly List<Wave> _wavesList;

        public Round(IEnumerable<Wave> listOfWaves) {
            _wavesList = new List<Wave>();
            _wavesList.AddRange(listOfWaves);
        }

        public IEnumerator SpawnWave(Wave wave) {
            foreach (var type in wave.Get()) 
                yield return wave.SpawnEnemies(type);
        }
        
        public List<Wave> Get => _wavesList;


    }
}
