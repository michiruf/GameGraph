using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Logger
    {
        public object message { private get; set; }
        public Object context { private get; set; }
        public LogType type { private get; set; }

        public void Log()
        {
            switch (type)
            {
                case LogType.Info:
                    Debug.Log(message, context);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message, context);
                    break;
                case LogType.Error:
                    Debug.LogError(message, context);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public enum LogType
        {
            Info,
            Warning,
            Error
        }
    }
}
