using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Animation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AnimatorSetFloat
    {
        // Properties
        public Animator animator { private get; set; }
        public string propertyName;
        public float value;

        public void Invoke()
        {
            animator.SetFloat(propertyName, value);
        }
    }
}
