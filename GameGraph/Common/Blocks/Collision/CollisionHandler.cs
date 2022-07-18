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
        public Collision collision { get; private set; }
        public Collider hit { get; private set; }
        public GameObject hitGameObject { get; private set; }
        public event Action onEnter;
        public event Action onStay;
        public event Action onExit;

        // Properties
        public Collider collider { private get; set; }
        public bool registerOnStart { private get; set; } = true;

        [ExcludeFromGraph]
        public void Start()
        {
            if (registerOnStart)
                Register();
        }

        public void Register()
        {
            if (onEnter != null)
                collider.AddOnCollisionEnterListener(c =>
                {
                    collision = c;
                    hit = c.collider;
                    hitGameObject = c.gameObject;
                    onEnter?.Invoke();
                });
            if (onStay != null)
                collider.AddOnCollisionStayListener(c =>
                {
                    collision = c;
                    hit = c.collider;
                    hitGameObject = c.gameObject;
                    onStay?.Invoke();
                });
            if (onExit != null)
                collider.AddOnCollisionExitListener(c =>
                {
                    collision = c;
                    hit = c.collider;
                    hitGameObject = c.gameObject;
                    onExit?.Invoke();
                });
        }
    }
}
