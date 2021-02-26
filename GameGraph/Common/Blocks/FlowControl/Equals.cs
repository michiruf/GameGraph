using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Equals
    {
        // Output
        public bool @out => in1 == in2;

        // Properties
        public object in1 { private get; set; }
        public object in2 { private get; set; }
    }
}
