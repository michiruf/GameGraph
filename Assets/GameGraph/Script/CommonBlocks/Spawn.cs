using GameGraph.Annotation;
using JetBrains.Annotations;
using UnityEngine;
using Property = GameGraph.Annotation.PropertyAttribute;

namespace GameGraph.CommonBlocks
{
    [GameGraph]
    [UsedImplicitly]
    public class Spawn
    {
        [Property] //
        public GameObject prefab;

        [Method] //
        public void Invoke()
        {
            Debug.LogError("M CALLED YEAH!");
        }
    }
}
