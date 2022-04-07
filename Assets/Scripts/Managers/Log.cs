using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Log
    {

        private readonly ILogger _logger;
        public static Log Instance;

        public Log() {
            if (Instance != null) return;
            _logger = new Logger(new LogHandler());
            Instance = this;

        }

        public ILogger Logger => _logger;

        public void DisableLogger() {
            _logger.logEnabled = false;
            
        }

    }
}