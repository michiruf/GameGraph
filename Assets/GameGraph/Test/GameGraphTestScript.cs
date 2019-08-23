using System;
using GameGraph;
using UnityEngine;

[GameGraphComponent]
public class GameGraphTestScript
{
    [Trigger] //
    public Action a = () => Debug.LogError("TEST CALLED YEAH!");

    [GameGraph.Property] //
    public float p = 1.337f;

    [Method] //
    public void M()
    {
        Debug.LogError("M CALLED YEAH!");
    }
}
