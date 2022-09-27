using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine
{
    public enum LogLevel : uint
    {
        Off = 0,
        Error = 8,
        Warning = 12,
        Info = 14,
        Debug = 15,
        All = 16777215
    }
    public class Log
    {
        private static LogLevel logLevel = LogLevel.All;
        private static ILogger log;
        public static void Init(ILogger logger,LogLevel level = LogLevel.All)
        {
            log = logger;
            logLevel = level;
        }

        public static void Debug(string message) {
            if (log != null && logLevel > LogLevel.Debug)
            {
                log.Debug(message);
            }
        }
        public static void Error(string message)
        {
            if (log != null && logLevel > LogLevel.Error)
            {
                log.Error(message);
            }
        }
        public static void Info(string message)
        {
            if (log != null && logLevel > LogLevel.Info)
            {
                log.Info(message);
            }
        }
        public static void Warning(string message)
        {
            if (log != null && logLevel > LogLevel.Warning)
            {
                log.Warning(message);
            }
        }

    }
}