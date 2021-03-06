using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Switch2Float
    {
        // Output
        public event Action out1;
        public event Action out2;

        // Properties
        public float value { private get; set; }
        public float in1 { private get; set; } = 0;
        public float in2 { private get; set; } = 1;

        public void Invoke()
        {
            if (Math.Abs(value - in1) < Constants.FloatingPrecision)
                out1?.Invoke();
            else if (Math.Abs(value - in2) < Constants.FloatingPrecision)
                out2?.Invoke();
        }
    }
}
