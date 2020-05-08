using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Animation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AnimatorTrigger
    {
        // Properties
        public Animator animator { private get; set; }
        public string propertyName;

        public void Invoke()
        {
            animator.SetTrigger(propertyName);
        }
    }
}
