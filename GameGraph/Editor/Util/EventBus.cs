using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameGraph.Editor
{
    // Got from: https://stackoverflow.com/questions/368265/a-simple-event-bus-for-net
    [Obsolete("This is not a good practice in C#")]
    public class EventBus
    {
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
            listeners.ForEach(l => l.PostEvent(e));
        }

        private class EventListenerWrapper
        {
            public object listener { get; }
            private readonly Dictionary<Type, MethodInfo> methods;

            public EventListenerWrapper(object listener)
            {
                this.listener = listener;

                methods = listener.GetType().GetMethods(Constants.ReflectionFlags)
                    .Where(info => info.Name == "OnEvent")
                    .Where(info => info.GetParameters().Length == 1)
                    .ToDictionary(info => info.GetParameters()[0].ParameterType, info => info);

                if (methods.Count == 0)
                    throw new ArgumentException($"Class {listener.GetType().Name} does not contain at least one method OnEvent(EventType e)");
            }

            public void PostEvent(object e)
            {
                if (!methods.TryGetValue(e.GetType(), out var method))
                    //throw new ArgumentException($"No event receiver for {e.GetType()} found");
                    return;

                method.Invoke(listener, new[] {e});
            }
        }
    }
}
