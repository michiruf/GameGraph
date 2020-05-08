using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class InvertBool
    {
        // Output
        public bool @out => !@in;

        // Properties
        public bool @in { private get; set; }
    }
}
