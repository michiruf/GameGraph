using System;
using GameGraph.Annotation;
using UnityEngine;
using Property = GameGraph.Annotation.PropertyAttribute;

[GameGraph]
public class GameGraphAnotherTestScript
{
    [Trigger] //
    public Action a2 = () => Debug.LogError("A2 CALLED YEAH!");

    [Property] //
    public float p2 = 1.338f;

    [Method] //
    public void M2()
    {
        Debug.LogError("M2 CALLED YEAH!");
    }
}
