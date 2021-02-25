using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    // TODO Think about naming. "MergeExecution"?!
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Merge
    {
        // Output
        public event Action @out;

        // Properties
        public bool distinctPerFrame;

        private int lastCallFrameCount = -1;

        public void In1()
        {
            if (distinctPerFrame && lastCallFrameCount == Time.frameCount)
                return;
            lastCallFrameCount = Time.frameCount;

            @out?.Invoke();
        }

        public void In2()
        {
            if (distinctPerFrame && lastCallFrameCount == Time.frameCount)
                return;
            lastCallFrameCount = Time.frameCount;

            @out?.Invoke();
        }
    }
}
