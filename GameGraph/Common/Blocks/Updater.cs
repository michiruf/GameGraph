using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Updater : IUpdateHook, ILateUpdateHook, IFixedUpdateHook
    {
        // Output
        public event Action update;

        // Properties
        public float deltaTime { get; private set; }
        public UpdaterType updateType = UpdaterType.Update;

        [ExcludeFromGraph]
        public void Update()
        {
            if (updateType != UpdaterType.Update)
                return;
            deltaTime = Time.deltaTime;
            update?.Invoke();
        }

        [ExcludeFromGraph]
        public void LateUpdate()
        {
            if (updateType != UpdaterType.LateUpdate)
                return;
            deltaTime = Time.deltaTime;
            update?.Invoke();
        }

        [ExcludeFromGraph]
        public void FixedUpdate()
        {
            if (updateType != UpdaterType.FixedUpdate)
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
