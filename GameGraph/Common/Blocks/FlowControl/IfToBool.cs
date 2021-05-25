using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class IfToBool
    {
        // Output
        public event Action invoked;
        public bool result { get; private set; }

        public void True()
        {
            result = true;
            invoked?.Invoke();
        }

        public void False()
        {
            result = false;
            invoked?.Invoke();
        }
    }
}
