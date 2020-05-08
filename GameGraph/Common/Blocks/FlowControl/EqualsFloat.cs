using System;
using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class EqualsFloat
    {
        // Output
        public bool @out => Math.Abs(in1 - in2) < Constants.FloatingPrecision;

        // Properties
        public float in1 { private get; set; }
        public float in2 { private get; set; }
    }
}
