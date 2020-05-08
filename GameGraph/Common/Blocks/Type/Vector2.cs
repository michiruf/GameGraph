using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Vector2
    {
        // Output
        public UnityEngine.Vector2 @out => new UnityEngine.Vector2(x, y);

        // Properties
        public float x { private get; set; }
        public float y { private get; set; }
    }
}
