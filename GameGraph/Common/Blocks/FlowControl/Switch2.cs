using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Switch2
    {
        // Output
        public event Action out1;
        public event Action out2;

        // Properties
        public object value { private get; set; }
        public object in1 { private get; set; } = 0;
        public object in2 { private get; set; } = 1;

        public void Invoke()
        {
            if (value == in1)
                out1?.Invoke();
            else if (value == in2)
                out2?.Invoke();
        }
    }
}
