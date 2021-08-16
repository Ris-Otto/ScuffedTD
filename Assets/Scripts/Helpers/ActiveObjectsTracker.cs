using System.Collections.Generic;
using Enemies;
using Spawners;
using Units;
using UnityEngine;
using UnityEngine.Events;

namespace Helpers
{
    public class ActiveObjectsTracker : MonoBehaviour {
    
        private List<AbstractEnemy> _enemies = new List<AbstractEnemy>();
        private List<AbstractUnit> _units = new List<AbstractUnit>();
        private UnityEvent m_event;
        public static ActiveObjectsTracker Instance;
        private bool _hasAdded;

        private void Awake() {
            m_event ??= new UnityEvent();
            m_event.AddListener(SendRoundEndMessage);
            _enemies = new List<AbstractEnemy>();
            _units = new List<AbstractUnit>();
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
            _enemies.Add(e);
            _hasAdded = true;
        }
    
        public void OnUnitSpawn(AbstractUnit u) {
            _units.Add(u);
        }

        public void RemoveAllEnemies() {
            _enemies.Clear();
        }

        public void RemoveEnemy(AbstractEnemy e) {
            _enemies.Remove(e);
        }

        public void RemoveUnit(AbstractUnit u) {
            _units.Remove(u);
        }

        private void SendRoundEndMessage() {
            RoundInformation.RoundEnd();
            GameObject.FindGameObjectWithTag("Start").GetComponent<RoundManager>().SetCanStartRound(true);
            _hasAdded = false;
        }

        public AbstractEnemy[] enemies => _enemies.ToArray();
        public AbstractUnit[] units => _units.ToArray();
    }
}
