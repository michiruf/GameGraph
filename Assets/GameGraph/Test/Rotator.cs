using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

[GameGraph]
[UsedImplicitly]
public class Rotator
{
    public float deltaTime { private get; set; } = Time.fixedDeltaTime;
    public float rotationSpeed { private get; set; }

    public GameObject go { private get; set; }

    [UsedImplicitly]
    public void Rotate()
    {
        go.transform.Rotate(Vector3.up, rotationSpeed * deltaTime);
    }
}
