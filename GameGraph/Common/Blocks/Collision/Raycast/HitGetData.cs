using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Collision/Grounded")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class HitGetData
    {
        // Output
        public Collider collider => hit.collider;
        public Rigidbody rigidbody => hit.rigidbody;
        public Transform transform => hit.transform;
        public UnityEngine.Vector3 point => hit.point;
        public UnityEngine.Vector3 normal => hit.normal;
        public float distance => hit.distance;

        // Properties
        public RaycastHit hit { private get; set; }
    }
}
