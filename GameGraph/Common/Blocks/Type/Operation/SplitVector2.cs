using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Operation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class SplitVector2
    {
        // Output
        public float x => @in.x;
        public float y => @in.y;

        // Properties
        public UnityEngine.Vector2 @in { private get; set; }
    }
}
