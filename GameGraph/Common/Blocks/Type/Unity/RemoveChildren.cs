using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Unity")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RemoveChildren
    {
        // Output
        public event Action removed;

        // Properties
        public GameObject sourceGameObject { private get; set; }
        public Component sourceComponent { private get; set; }

        public void Remove()
        {
            // Bypassing unity's lifetime check is okay here, because we improve performance and also do not need this check
            // since this node is intended to use only the selected line of GameObject or Component
            // ReSharper disable once Unity.NoNullPropagation
            // ReSharper disable once Unity.NoNullCoalescing
            var transform = sourceComponent?.transform ?? sourceGameObject.transform;

            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }

            removed?.Invoke();
        }
    }
}
