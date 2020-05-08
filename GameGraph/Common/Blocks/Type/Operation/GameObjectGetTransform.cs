using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Operation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GameObjectGetTransform
    {
        // Output
        public Transform transform => gameObject.transform;

        // Properties
        public GameObject gameObject { private get; set; }
    }
}
