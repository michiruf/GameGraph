using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph
{
    [GameGraph("Common")]
    [UsedImplicitly]
    public class Updater : IUpdateHook
    {
        public float deltaTime { get; private set; }
        public UpdaterType type = UpdaterType.Update;

        [UsedImplicitly]
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
}
