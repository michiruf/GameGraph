using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class StringLogger
    {
        public string message { private get; set; }
        public Object context { private get; set; }
        public LogType type { private get; set; }

        public void Log()
        {
            Logger.Log(message, context, type);
        }
    }
}
