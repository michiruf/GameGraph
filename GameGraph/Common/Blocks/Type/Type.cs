using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Type
    {
        // Output
        public System.Type type => graphType.type;

        // Properties
        public GraphSerializableType graphType;
    }
}
