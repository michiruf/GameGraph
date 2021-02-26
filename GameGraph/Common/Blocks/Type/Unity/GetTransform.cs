using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Unity")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GetTransform
    {
        // Output
        // ReSharper disable once Unity.NoNullPropagation
        // ReSharper disable once Unity.NoNullCoalescing
        public Transform transform => component?.transform ?? gameObject.transform;

        // Properties
        public GameObject gameObject { private get; set; }
        public Component component { private get; set; }
    }
}
