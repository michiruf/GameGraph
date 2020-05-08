using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Spawn
    {
        // Properties
        public GameObject prefab;

        public void Invoke()
        {
            Object.Instantiate(prefab);
        }
    }
}
