using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Log : MonoBehaviour
    {

        private ILogger _logger;
        public static Log Instance;

        public void Awake() {
            if (Instance != null) return;
            _logger = new Logger(new LogHandler());
            Instance = this;
            _logger.Log(LogType.Log, "Round; Buy; Sell; Undo; Upgrade; Place; Pops; Health; Money;");
        }

        public ILogger Logger => _logger;

        public void DisableLogger() {
            _logger.logEnabled = false;
        }

    }
}