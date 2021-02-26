using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Type/Operation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class TransformSetData
    {
        // Output
        public event Action invoked;

        // Properties
        public Transform transform { private get; set; }
        public UnityEngine.Vector3 position { private get; set; }
        public Quaternion rotation { private get; set; }

        public void Invoke()
        {
            transform.position = position;
            transform.rotation = rotation;
            invoked?.Invoke();
        }
    }
}
