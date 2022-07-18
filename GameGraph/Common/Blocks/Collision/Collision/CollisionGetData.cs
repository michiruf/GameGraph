using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Collision/Grounded")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class CollisionGetData
    {
        // Output
        public Collider collider => collision.collider;
        public Rigidbody rigidbody => collision.rigidbody;
        public Transform transform => collision.transform;
        public GameObject gameObject => collision.gameObject;
        public UnityEngine.Vector3 impulse => collision.impulse;
        public UnityEngine.Vector3 relativeVelocity => collision.relativeVelocity;
        public int contactCount => collision.contactCount;

        // Properties
        public Collision collision { private get; set; }
    }
}
