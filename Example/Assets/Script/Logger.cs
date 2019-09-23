using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

[GameGraph]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class Logger
{
    public string value;

    public void Log()
    {
        Debug.Log(value);
    }
}
