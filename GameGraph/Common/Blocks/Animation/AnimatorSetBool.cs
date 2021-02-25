using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Animation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AnimatorSetBool
    {
        // Properties
        public Animator animator { private get; set; }
        public string propertyName;
        public bool value;

        public void Invoke()
        {
            animator.SetBool(propertyName, value);
        }
    }
}
