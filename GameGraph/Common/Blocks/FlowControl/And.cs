using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class And
    {
        // Output
        public bool @out => in1 && in2;

        // Properties
        public bool in1 { private get; set; }
        public bool in2 { private get; set; }
    }
}
