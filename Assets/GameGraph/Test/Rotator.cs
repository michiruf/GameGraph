using GameGraph;
using UnityEngine;

[GameGraph]
public class Rotator : IStartHook
{
    public float deltaTime = Time.fixedDeltaTime;
    public float rotationSpeed = 1f;

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
