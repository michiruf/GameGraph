using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Animation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AnimatorGetBool
    {
        // Output
        public bool value => animator.GetBool(propertyName);

        // Properties
        public Animator animator { private get; set; }
        public string propertyName;
    }
}
