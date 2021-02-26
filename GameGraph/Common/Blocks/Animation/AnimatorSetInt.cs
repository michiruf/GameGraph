using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Animation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AnimatorSetInt
    {
        // Properties
        public Animator animator { private get; set; }
        public string propertyName;
        public int value;

        public void Invoke()
        {
            animator.SetInteger(propertyName, value);
        }
    }
}
