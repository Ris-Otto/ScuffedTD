using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Managers
{
    public class LogHandler : ILogHandler
    {
        private StreamWriter m_StreamWriter;
        private ILogHandler m_DefaultLogHandler = Debug.unityLogger.logHandler;
        string filePath = Application.persistentDataPath + DateTime.Now.ToString("MM-dd-yy-hh-mm-ss") + ".txt";
        
        public LogHandler() {
            
            FileStream mFileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            m_StreamWriter = new StreamWriter(mFileStream);

            // Replace the default debug log handler
            //Debug.unityLogger.logHandler = this;
        }
        

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            m_StreamWriter.WriteLine(format, args);
            m_StreamWriter.Flush();
            m_DefaultLogHandler.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            m_DefaultLogHandler.LogException(exception, context);
        }

        public void CloseFileStream() {
            m_StreamWriter.Close();
        }

        

    }
}