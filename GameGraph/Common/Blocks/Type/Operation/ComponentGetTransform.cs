using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Operation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ComponentGetTransform
    {
        // Output
        public Transform transform => component.transform;

        // Properties
        public Component component { private get; set; }
    }
}
