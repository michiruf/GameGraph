using GameGraph;
using UnityEngine;

[GameGraph]
public class Rotator : IStartHook
{
    public float deltaTime { private get; set; } = Time.fixedDeltaTime;
    public float rotationSpeed { private get; set; }

    private GameObject cube;

    [ExcludeFromGraph]
    public void Start()
    {
        cube = GameObject.FindWithTag("Player");
    }

    public void Rotate()
    {
        cube.transform.Rotate(Vector3.up, rotationSpeed * deltaTime);
    }
}
