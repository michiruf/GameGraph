using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Animation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AnimatorGetInt
    {
        // Output
        public int value => animator.GetInteger(propertyName);

        // Properties
        public Animator animator { private get; set; }
        public string propertyName;
    }
}
