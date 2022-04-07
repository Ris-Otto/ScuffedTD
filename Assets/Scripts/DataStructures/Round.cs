using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace DataStructures
{
    public class BloonType
    {
        public BloonType(string type, int amount, float interval) {
            //i princip väldigt illa eftersom detta är en referens till ett objekt och inte bara värdet så ifall 
            //vi ändrar på något här så ändras template-objektet men de får man väl fixa i något skede
            _type = Resources.Load<GameObject>("Prefabs/Enemies/" + type); 
            _amount = amount;
            _interval = interval;
        }

        private readonly GameObject _type; //red, blue, green...
        private readonly int _amount; //mängden som ska instatieras
        private readonly float _interval; //intervall mellan bloons
        public GameObject Type => _type;
        public int Amount=> _amount;
        public float Interval => _interval;
    }
    
    public class Round
    {
        
        private readonly List<Wave> _wavesList;
        private readonly int camoWave;

        public Round(IEnumerable<Wave> listOfWaves) {
            _wavesList = new List<Wave>();
            _wavesList.AddRange(listOfWaves);
        }
        
        public Round(IEnumerable<Wave> listOfWaves, int camoWave) {
            this.camoWave = camoWave;
            _wavesList = new List<Wave>();
            _wavesList.AddRange(listOfWaves);
        }

        public static IEnumerator SpawnWave(Wave wave) {
            return wave.Get().Select(wave.SpawnEnemies).GetEnumerator();
        }
        
        public IEnumerable<Wave> Get => _wavesList;


    }
}
