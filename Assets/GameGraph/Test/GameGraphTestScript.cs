using System;
using GameGraph.Annotation;
using UnityEngine;
using Property = GameGraph.Annotation.PropertyAttribute;

[GameGraph]
public class GameGraphTestScript
{
    [Trigger] //
    public Action a = () => Debug.LogError("A CALLED YEAH!");

    [Property] //
    public float p = 1.337f;

    [Method] //
    public void M()
    {
        Debug.LogError("M CALLED YEAH!");
    }
}
