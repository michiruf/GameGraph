using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Input")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GetAxis
    {
        // Output
        public float @out => Input.GetAxis(axis);

        // Properties
        public string axis { private get; set; }
    }
}
