using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Unity")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GetComponent
    {
        // Output
        // ReSharper disable once Unity.NoNullPropagation
        // Bypassing unity's lifetime check is okay here, because we improve performance and also do not need this check
        // since this node is intended to use only one of GameObject or Component
        public object component => sourceComponent?.GetComponent(type) ?? sourceGameObject.GetComponent(type);

        // Properties
        public GameObject sourceGameObject { private get; set; }
        public Component sourceComponent { private get; set; }
        public System.Type type;
    }
}
