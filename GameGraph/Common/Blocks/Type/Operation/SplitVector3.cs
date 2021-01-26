using JetBrains.Annotations;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Operation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class SplitVector3
    {
        // Output
        public float x => @in.x;
        public float y => @in.y;
        public float z => @in.z;

        // Properties
        public UnityEngine.Vector3 @in { private get; set; }
    }
}
