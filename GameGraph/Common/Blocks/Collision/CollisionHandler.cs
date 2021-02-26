using System;
using GameGraph.Common.Helper;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Collision")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class CollisionHandler : IStartHook
    {
        // Output
        public Collision hit { get; private set; }
        public event Action onEnter;
        public event Action onStay;
        public event Action onExit;

        // Properties
        public Collider collider { private get; set; }

        [ExcludeFromGraph]
        public void Start()
        {
            if (onEnter != null)
                collider.AddOnCollisionEnterListener(c =>
                {
                    hit = c;
                    onEnter?.Invoke();
                });
            if (onStay != null)
                collider.AddOnCollisionStayListener(c =>
                {
                    hit = c;
                    onStay?.Invoke();
                });
            if (onExit != null)
                collider.AddOnCollisionExitListener(c =>
                {
                    hit = c;
                    onExit?.Invoke();
                });
        }
    }
}
