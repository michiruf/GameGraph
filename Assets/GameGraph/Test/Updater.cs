using System;
using GameGraph;
using UnityEngine;

[GameGraph]
public class Updater : IUpdateHook
{
    public float deltaTime;
    public UpdaterType type = UpdaterType.Update;

    public event Action update;

    [ExcludeFromGraph]
    public void Update()
    {
        if (type != UpdaterType.Update)
            return;
        deltaTime = Time.deltaTime;
        update?.Invoke();
    }

    [ExcludeFromGraph]
    public void LateUpdate()
    {
        if (type != UpdaterType.LateUpdate)
            return;
        deltaTime = Time.deltaTime;
        update?.Invoke();
    }

    [ExcludeFromGraph]
    public void FixedUpdate()
    {
        if (type != UpdaterType.FixedUpdate)
            return;
        deltaTime = Time.fixedDeltaTime;
        update?.Invoke();
    }

    public enum UpdaterType
    {
        Update,
        LateUpdate,
        FixedUpdate
    }
}
