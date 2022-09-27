using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine
{
    public interface ILogger
    {
        void Debug(string message);
        void Error(string message);
        void Info(string message);
        void Warning(string message);
    }
}