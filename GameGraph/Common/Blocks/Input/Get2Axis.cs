using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Input")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Get2Axis
    {
        // Output
        public UnityEngine.Vector2 @out => new UnityEngine.Vector2(Input.GetAxis(axis1), Input.GetAxis(axis2));

        // Properties
        public string axis1 { private get; set; }
        public string axis2 { private get; set; }
    }
}
