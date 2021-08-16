using System.Collections;
using System.Collections.Generic;
using DataStructures;
using Helpers;
using UnityEngine;

namespace Spawners
{
    public class EnemyPooler : MonoBehaviour
    {
        
        public GameObject prefab;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;
        private Queue<GameObject> _pool;
        public static EnemyPooler Instance;

        private void Awake() {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();
            _pool = new Queue<GameObject>();
            if (Instance == null) {
                Instance = this;
            }
        }

        public void CreatePool(int numObjects) {
            for(int i = 0; i < numObjects; i++) {
                GameObject gameObj = Instantiate(prefab);
                gameObj.name += i;
                gameObj.SetActive(false);
                _pool.Enqueue(gameObj);
            }
        }

        public GameObject SpawnFromPool(string objectName, Vector2 pos, Quaternion rot) {
            if (mega.TryGetValue(objectName, out Queue<GameObject> megaman)) 
                if (megaman == null || megaman.Count == 0) return null;
            GameObject toSpawn = megaman.Dequeue();
            ConfigureObjectToSpawn(pos, rot, toSpawn);
            return toSpawn;
        }
        

        private static void ConfigureObjectToSpawn(Vector2 pos, Quaternion rot, GameObject toSpawn) {
            toSpawn.SetActive(true);
            toSpawn.transform.position = pos;
            toSpawn.transform.rotation = rot;
        }

        private Dictionary<string, Queue<GameObject>> mega {
            get => _poolDictionary;
            set => _poolDictionary = value;
        }
    }
}
