using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Unity")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GetGameObject
    {
        // Output
        // ReSharper disable once Unity.NoNullPropagation
        public GameObject gameObject => component?.gameObject;

        // Properties
        public Component component { private get; set; }
    }
}
