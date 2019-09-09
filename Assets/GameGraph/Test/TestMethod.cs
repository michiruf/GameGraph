using GameGraph;
using UnityEngine;

[GameGraph("Test")]
public class TestMethod
{
    public void Foo()
    {
        Debug.LogError("Foo called!");
    }
}
