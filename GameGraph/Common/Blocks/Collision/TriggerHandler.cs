using System;
using GameGraph.Common.Helper;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Collision")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class TriggerHandler : IStartHook
    {
        // Output
        public Collider hit { get; private set; }
        public event Action onEnter;
        public event Action onStay;
        public event Action onExit;

        // Properties
        public Collider collider { private get; set; }

        public void Start()
        {
            if (onEnter != null)
                collider.AddOnTriggerEnterListener(c =>
                {
                    hit = c;
                    onEnter?.Invoke();
                });
            if (onStay != null)
                collider.AddOnTriggerStayListener(c =>
                {
                    hit = c;
                    onStay?.Invoke();
                });
            if (onExit != null)
                collider.AddOnTriggerExitListener(c =>
                {
                    hit = c;
                    onExit?.Invoke();
                });
        }
    }
}
