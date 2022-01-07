using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class ProjectilePooler : MonoBehaviour
    {
        
        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        private void Awake() {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        }

        public Dictionary<string, Queue<GameObject>> Get() {
            return _poolDictionary;
        }

        public GameObject SpawnFromPool(string objectName, Vector2 pos, Quaternion rot) {
            if(!_poolDictionary.TryGetValue(objectName, out Queue<GameObject> megaman))
                return null;
            GameObject toSpawn = megaman.Dequeue();
            ConfigureToSpawn(pos, rot, toSpawn);
            megaman.Enqueue(toSpawn);
            return toSpawn;

        }

        private static void ConfigureToSpawn(Vector2 pos, Quaternion rot, GameObject toSpawn) {
            toSpawn.SetActive(true);
            toSpawn.transform.position = pos;
            toSpawn.transform.rotation = rot;
        }

        public void CreatePool(GameObject obj, int _size) {
            Queue<GameObject> newPool = new Queue<GameObject>();
            for(int i = 0; i < _size; i++) {
                GameObject gameObj = Instantiate(obj);
                gameObj.SetActive(false);
                newPool.Enqueue(gameObj);
            }
            _poolDictionary ??= new Dictionary<string, Queue<GameObject>> {{obj.name, newPool}};
            if (!_poolDictionary.ContainsKey(obj.name))
                _poolDictionary.Add(obj.name, newPool);
            else
                _poolDictionary[obj.name] = newPool;
        }
    }
}
