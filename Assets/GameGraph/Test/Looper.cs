using System;
using GameGraph;

[GameGraph]
public class Looper
{
    [Property] // 
    public int count;

    [Trigger] //
    public Action execute;

    [Method] //
    public void Spawn()
    {
        for (var i = 0; i < count; i++)
        {
            execute?.Invoke();
        }
    }
}
