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
            Log(message, context, type);
        }

        public static void Log(object message, Object context, LogType type)
        {
            switch (type)
            {
                case LogType.Log:
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
    }
}
