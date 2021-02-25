using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Destroy
    {
        public GameObject @object;

        public void Invoke()
        {
            Object.Destroy(@object);
        }
    }
}
