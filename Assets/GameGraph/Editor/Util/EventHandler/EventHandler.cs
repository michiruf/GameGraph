//using System;
//using System.Collections.Generic;
//
//// TODO Remove
//namespace GameGraph.Editor
//{
//    public class EventHandler : IDisposable
//    {
//        private List<Subscription<object>> subscriptions;
//
//        public void Subscribe<T>(Action<T> callback, object subscriber) where T : class
//        {
//            subscriptions.Add(new Subscription<T>(callback, subscriber));
//        }
//
//        public void Subscribe<T>(Action<T> callback) where T : class
//        {
//        }
//
//        public void Unsubscribe<T>(Action<T> callback)
//        {
//        }
//
//        public void Unsubscribe(object subscriber)
//        {
//        }
//
//        public void Publish(object e)
//        {
//            subscriptions.ForEach(subscription =>
//            {
//                subscription.callback;
//            });
//        }
//
//        public void Dispose()
//        {
//            subscriptions = null;
//        }
//
//        private struct Subscription<T> where T : class
//        {
//            public readonly Action<T> callback;
//            public readonly object subscriber;
//
//            public Subscription(Action<T> callback, object subscriber)
//            {
//                this.callback = callback;
//                this.subscriber = subscriber;
//            }
//        }
//    }
//}

