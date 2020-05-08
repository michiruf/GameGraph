using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Animation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AnimatorGetFloat
    {
        // Output
        public float value => animator.GetFloat(propertyName);

        // Properties
        public Animator animator { private get; set; }
        public string propertyName;
    }
}
