using System;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    // TODO Documentation!
    public abstract class GraphEventSenderBase<TEvent> where TEvent : class, IGraphEventReceiver
    {
        // Annotated just in case this analysis will be enabled in the future
        [ExcludeFromGraph]
        protected void FireEvent(GameObject target, Action<TEvent> targetValueAssignment)
        {
            var behaviours = target.GetComponents<GameGraphBehaviour>();
            foreach (var behaviour in behaviours)
            {
                // Without the ? in the next line, a NPE occured because with multiple graphs the next one may not be initialized
                behaviour.executor?.GetInstances<TEvent>()?.ForEach(targetEventHandler =>
                {
                    targetValueAssignment(targetEventHandler);
                    targetEventHandler.OnEventInvoked();
                });
            }
        }
    }
}
