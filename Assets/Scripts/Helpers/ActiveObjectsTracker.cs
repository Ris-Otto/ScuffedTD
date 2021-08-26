using System.Collections.Generic;
using Enemies;
using Spawners;
using Units;
using UnityEngine;
using UnityEngine.Events;

namespace Helpers
{
    public class ActiveObjectsTracker : MonoBehaviour {
    
        private List<AbstractEnemy> _enemies;
        private List<AbstractUnit> _units;
        private List<AbstractEnemy> _camo;
        private List<AbstractEnemy> _allEnemies;
        private UnityEvent m_event;
        public static ActiveObjectsTracker Instance;
        private bool _hasAdded;

        private void Awake() {
            m_event ??= new UnityEvent();
            m_event.AddListener(SendRoundEndMessage);
            _enemies = new List<AbstractEnemy>();
            _units = new List<AbstractUnit>();
            _camo = new List<AbstractEnemy>();
            _allEnemies = new List<AbstractEnemy>();
            if (Instance != null && Instance != this) {
                Debug.Log("Invalid Instance");
            }
            if (Instance == null) {
                Instance = this;
            }
        }

        private void FixedUpdate() {
            if (_enemies.Count == 0 && _hasAdded) {
                m_event.Invoke();
            }
        }

        public void OnEnemySpawn(AbstractEnemy e) {
            _hasAdded = true;
            if (!e.IsCamo) {
                _enemies.Add(e);
            }
            else {
                _camo.Add(e);
            }
            _allEnemies.Add(e);
        }
    
        public void OnUnitSpawn(AbstractUnit u) {
            _units.Add(u);
        }

        public void RemoveAllEnemies() {
            _enemies.Clear();
        }

        public void RemoveEnemy(AbstractEnemy e) {
            if (!e.IsCamo) {
                _enemies.Remove(e);
            }
            else {
                _camo.Remove(e);
            }
            _allEnemies.Remove(e);
        }

        public void RemoveUnit(AbstractUnit u) {
            _units.Remove(u);
        }

        private void SendRoundEndMessage() {
            RoundInformation.RoundEnd();
            GameObject.FindGameObjectWithTag("Start").GetComponent<RoundManager>().SetCanStartRound(true);
            _hasAdded = false;
        }

        public AbstractEnemy[] NonCamo => _enemies.ToArray();
        public AbstractUnit[] Units => _units.ToArray();
        public AbstractEnemy[] AllEnemies => _allEnemies.ToArray();
    }
}
