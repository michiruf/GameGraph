using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace Presentation
{
    [GameGraph("Presentation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Rotate
    {
        public float deltaTime { private get; set; } = Time.fixedDeltaTime;
        public float rotationSpeed { private get; set; }

        public GameObject gameObject { private get; set; }

        public void Invoke()
        {
            gameObject.transform.Rotate(Vector3.up, rotationSpeed * deltaTime);
        }
    }
}
