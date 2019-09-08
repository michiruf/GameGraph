using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph
{
    [GameGraph("Common")]
    [UsedImplicitly]
    public class Spawn
    {
        [UsedImplicitly] public GameObject prefab;

        [UsedImplicitly]
        public void Invoke()
        {
            Object.Instantiate(prefab);
        }
    }
}
