using JetBrains.Annotations;

namespace GameGraph
{
    [GameGraph("Common/FlowControl")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class InvertBool
    {
        // Output
        public bool @out { get; private set; }

        // Properties
        public bool @in { private get; set; }
    }
}
