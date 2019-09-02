using System;
using GameGraph.Annotation;
using UnityEngine;
using Property = GameGraph.Annotation.PropertyAttribute;

[GameGraph]
public class GameGraphTestScript
{
    [Trigger] //
    public Action action = () => Debug.LogError("A CALLED YEAH!");

    [Property] //
    public float property = 1.337f;

    [Method] //
    public void Method()
    {
        Debug.LogError("M CALLED YEAH!");
    }
}
