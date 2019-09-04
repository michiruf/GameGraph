using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph
{
    [GameGraph]
    [UsedImplicitly]
    public class Spawn
    {
        public GameObject prefab;

        public void Invoke()
        {
            Debug.LogError("M CALLED YEAH!");
        }
    }
}
