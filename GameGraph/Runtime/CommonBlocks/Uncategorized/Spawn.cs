using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.CommonBlocks
{
    [GameGraph("Common/Uncategorized")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Spawn
    {
        public GameObject prefab;

        public void Invoke()
        {
            Object.Instantiate(prefab);
        }
    }
}
