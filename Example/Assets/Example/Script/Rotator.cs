using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

[GameGraph("RealTest")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class Rotator
{
    public float deltaTime { private get; set; } = Time.fixedDeltaTime;
    public float rotationSpeed { private get; set; }

    public GameObject gameObject { private get; set; }

    [UsedImplicitly]
    public void Rotate()
    {
        gameObject.transform.Rotate(Vector3.up, rotationSpeed * deltaTime);
    }
}
