using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Input")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GetButton
    {
        // Output
        public bool @out => Input.GetButton(button);

        // Properties
        public string button { private get; set; }
    }
}
