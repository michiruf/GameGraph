using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Spawn
    {
        // Output
        public event Action spawned;
        public Object instance { get; private set; }

        // Properties
        public GameObject prefab;
        public Transform parent;
        public UnityEngine.Vector3 position;
        public Quaternion rotation;

        public void Invoke()
        {
            instance = Object.Instantiate(prefab, position, rotation, parent);
            spawned?.Invoke();
        }
    }
}
