using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Vector3
    {
        // Output
        public UnityEngine.Vector3 @out => new UnityEngine.Vector3(x, y, z);

        // Properties
        public float x { private get; set; }
        public float y { private get; set; }
        public float z { private get; set; }
    }
}
