using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class IsNull
    {
        // Output
        public bool isNull => @object == null;

        // Properties
        public object @object { private get; set; }
    }
}
