using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private readonly int _amount;
        private readonly float _interval;
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

        public static IEnumerator SpawnWave(Wave wave) {
            return wave.Get().Select(wave.SpawnEnemies).GetEnumerator();
        }
        
        public IEnumerable<Wave> Get => _wavesList;


    }
}
