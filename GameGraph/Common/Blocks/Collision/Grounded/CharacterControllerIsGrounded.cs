using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Collision/Grounded")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class CharacterControllerIsGrounded
    {
        // Output
        public bool grounded => characterController.isGrounded;

        // Properties
        public CharacterController characterController { private get; set; }
    }
}
