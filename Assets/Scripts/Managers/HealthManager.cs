using Enemies;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class HealthManager : MonoBehaviour, ILoggable
    {

        private int _health;
        private int _healthLastRound;

        private Log _log;

        public static HealthManager Instance;

        private UIManager ui;

        private void Awake() {
            if (Instance == null) Instance = this;
            _health = 100;
            _healthLastRound = _health;
            ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
            ui.DisplayHealth(_health);
        }

        public void AddLogger() {
            _log = FindObjectOfType<Log>();
        }
        
        private void FixedUpdate() {
            if (_health > 0) return;
            _log.Logger.Log(LogType.Log, "Game over");
            _log.DisableLogger();
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        public void OnEnemyPassedThrough(AbstractEnemy enemy) {
            _health -= enemy.Enemy.totalHealth;
            ui.DisplayHealth(_health);
        }

        public void Log() {
            int loss = _healthLastRound - _health;
            if (loss == 0) return;
            _log.Logger.Log(LogType.Log, $"0: Lost {loss} hit points, current hit points: {_health}");
            _healthLastRound = _health;
        }
    }
}
