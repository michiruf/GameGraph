using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Timer : IUpdateHook
    {
        // Output
        public event Action onEnd;
        public bool timerRunning => timerRunningInternal;

        // Properties
        public float duration { private get; set; }
        public bool avoidDuplicateStart { private get; set; } = true;

        private bool timerRunningInternal;
        private float endTime;

        public void Start()
        {
            if (avoidDuplicateStart && timerRunningInternal)
                return;

            timerRunningInternal = true;
            endTime = Time.realtimeSinceStartup + duration;
        }

        [ExcludeFromGraph]
        public void Update()
        {
            if (timerRunningInternal && Time.realtimeSinceStartup > endTime)
            {
                timerRunningInternal = false;
                onEnd?.Invoke();
            }
        }
    }
}
