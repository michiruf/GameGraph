using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Operation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class TransformGetData
    {
        // Output
        public UnityEngine.Vector3 position => transform.position;
        public Quaternion rotation => transform.rotation;

        // Properties
        public Transform transform { private get; set; }
    }
}
