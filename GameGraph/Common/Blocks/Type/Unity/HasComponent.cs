using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Unity")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class HasComponent
    {
        // Output
        public bool hasComponent { get; private set; }
        public event Action @true;
        public event Action @false;
        public event Action always;

        // Properties
        public GameObject sourceGameObject { private get; set; }
        public Component sourceComponent { private get; set; }
        public GraphSerializableType type;

        public void Fetch()
        {
            // Bypassing unity's lifetime check is okay here, because we improve performance and also do not need this check
            // since this node is intended to use only the selected line of GameObject or Component
            // ReSharper disable once Unity.NoNullPropagation
            hasComponent = sourceComponent?.GetComponent(type.type) ?? sourceGameObject.GetComponent(type.type);

            if (hasComponent)
                @true?.Invoke();
            else
                @false?.Invoke();

            always?.Invoke();
        }
    }
}
