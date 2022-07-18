using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Operation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class QuaternionToDirection
    {
        // Output
        public UnityEngine.Vector3 direction => quaternion * input;

        // Properties
        public Quaternion quaternion { private get; set; }
        public UnityEngine.Vector3 input { private get; set; }
    }
}
