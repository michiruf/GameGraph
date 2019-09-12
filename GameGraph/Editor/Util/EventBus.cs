using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameGraph.Editor
{
    // Got from: https://stackoverflow.com/questions/368265/a-simple-event-bus-for-net
    public class EventBus
    {
        private static EventBus instanceInternal;
        private readonly List<EventListenerWrapper> listeners = new List<EventListenerWrapper>();

        public void Register(object listener)
        {
            if (listeners.All(l => l.listener != listener))
                listeners.Add(new EventListenerWrapper(listener));
        }

        public void Unregister(object listener)
        {
            listeners.RemoveAll(l => l.listener == listener);
        }

        public void Dispatch(object e)
        {
            listeners
                .Where(l => l.eventType == e.GetType())
                .ToList()
                .ForEach(l => l.PostEvent(e));
        }

        private class EventListenerWrapper
        {
            public object listener { get; }
            public Type eventType { get; }

            private readonly MethodBase method;

            public EventListenerWrapper(object listener)
            {
                this.listener = listener;

                var type = listener.GetType();

                // TODO Handles only one element?!
                method = type.GetMethod("OnEvent");
                if (method == null)
                    throw new ArgumentException("Class " + type.Name + " does not contain method OnEvent");

                var parameters = method.GetParameters();
                if (parameters.Length != 1)
                    throw new ArgumentException("Method OnEvent of class " + type.Name +
                                                " has invalid number of parameters (should be one)");

                eventType = parameters[0].ParameterType;
            }

            public void PostEvent(object e)
            {
                method.Invoke(listener, new[] {e});
            }
        }
    }
}
