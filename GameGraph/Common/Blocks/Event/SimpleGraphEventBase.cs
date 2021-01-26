using System;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    // TODO Documentation!
    public abstract class SimpleGraphEventBase<TEvent> where TEvent : SimpleGraphEventBase<TEvent>
    {
        protected void FireEvent(GameObject target, Func<TEvent, Action> targetEventActionSelector, Action<TEvent> targetValueAssignment)
        {
            var behaviours = target.GetComponents<GameGraphBehaviour>();
            foreach (var behaviour in behaviours)
            {
                // Without the ? in the next line, a NPE occured because with multiple graphs the next one may not be initialized
                behaviour.executor?.GetInstances<TEvent>()?.ForEach(targetEventHandler =>
                {
                    // Skip self assignment and triggering
                    // May not be necessary
                    if (targetEventHandler == this)
                        return;

                    targetValueAssignment?.Invoke(targetEventHandler);
                    targetEventActionSelector(targetEventHandler)?.Invoke();
                });
            }
        }
    }
}
