using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Collision")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ColliderSetEnabled
    {
        // Properties
        public Collider collider { private get; set; }
        public bool enabled;

        public void Invoke()
        {
            collider.enabled = enabled;
        }
    }
}
