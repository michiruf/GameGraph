using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Collision/Grounded")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class CharacterControllerGroundedCheck : ILateUpdateHook
    {
        // Configuration
        public float distance;
        public string layer;

        // Output
        private bool internalGrounded;
        private bool internalGroundedIsCached;
        public bool grounded
        {
            get
            {
                if (!internalGroundedIsCached)
                {
                    internalGrounded = CheckGrounded();
                    internalGroundedIsCached = true;
                }
                return internalGrounded;
            }
        }

        // Properties
        public CharacterController characterController { private get; set; }

        [ExcludeFromGraph]
        public void LateUpdate()
        {
            internalGroundedIsCached = false;
        }

        private bool CheckGrounded()
        {
            var localScale = characterController.transform.localScale;
            var characterRadius = characterController.radius * Mathf.Max(localScale.x, localScale.z);
            return Physics.SphereCast(
                new Ray(
                    characterController.transform.position + UnityEngine.Vector3.up * characterRadius,
                    UnityEngine.Vector3.down
                ),
                characterRadius,
                distance,
                LayerMask.GetMask(layer));
        }
    }
}
