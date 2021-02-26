using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Unity")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GetComponent
    {
        // Output
        public event Action fetched;
        public object component { get; private set; }

        // Properties
        public GameObject sourceGameObject { private get; set; }
        public Component sourceComponent { private get; set; }
        public GraphSerializableType type;

        public void Fetch()
        {
            // Bypassing unity's lifetime check is okay here, because we improve performance and also do not need this check
            // since this node is intended to use only the selected line of GameObject or Component
            // ReSharper disable once Unity.NoNullPropagation
            component = sourceComponent?.GetComponent(type.type) ?? sourceGameObject.GetComponent(type.type);

            fetched?.Invoke();
        }
    }
}
