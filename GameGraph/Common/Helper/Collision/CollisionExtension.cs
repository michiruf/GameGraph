using System;
using UnityEngine;

namespace GameGraph.Common.Helper
{
    public static class CollisionExtension
    {
        private static GameGraphCollisionWrapper CreateCollisionWrapper(GameObject o, bool alsoCreate = false)
        {
            var wrapper = o.GetComponent<GameGraphCollisionWrapper>();
            if (alsoCreate && wrapper == null)
                wrapper = o.AddComponent<GameGraphCollisionWrapper>();
            return wrapper;
        }

        private static GameGraphCollisionStayWrapper CreateCollisionStayWrapper(GameObject o, bool alsoCreate = false)
        {
            var wrapper = o.GetComponent<GameGraphCollisionStayWrapper>();
            if (alsoCreate && wrapper == null)
                wrapper = o.AddComponent<GameGraphCollisionStayWrapper>();
            return wrapper;
        }

        public static void AddOnCollisionEnterListener(this Collider c, Action<Collision> a)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject, true);
            wrapper.collisionEnterListeners.Add(a);
        }

        public static void RemoveOnCollisionEnterListener(this Collider c, Action<Collision> a)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject);
            if (wrapper)
                wrapper.collisionEnterListeners.Remove(a);
        }

        public static void ClearOnCollisionEnterListeners(this Collider c)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject);
            if (wrapper)
                wrapper.collisionEnterListeners.Clear();
        }

        public static void AddOnCollisionExitListener(this Collider c, Action<Collision> a)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject, true);
            wrapper.collisionExitListeners.Add(a);
        }

        public static void RemoveOnCollisionExitListener(this Collider c, Action<Collision> a)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject);
            if (wrapper)
                wrapper.collisionExitListeners.Remove(a);
        }

        public static void ClearOnCollisionExitListeners(this Collider c)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject);
            if (wrapper)
                wrapper.collisionExitListeners.Clear();
        }

        public static void AddOnCollisionStayListener(this Collider c, Action<Collision> a)
        {
            var wrapper = CreateCollisionStayWrapper(c.gameObject, true);
            wrapper.collisionStayListeners.Add(a);
        }

        public static void RemoveOnCollisionStayListener(this Collider c, Action<Collision> a)
        {
            var wrapper = CreateCollisionStayWrapper(c.gameObject);
            if (wrapper)
                wrapper.collisionStayListeners.Remove(a);
        }

        public static void ClearOnCollisionStayListeners(this Collider c)
        {
            var wrapper = CreateCollisionStayWrapper(c.gameObject);
            if (wrapper)
                wrapper.collisionStayListeners.Clear();
        }

        public static void AddOnTriggerEnterListener(this Collider c, Action<Collider> a)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject, true);
            wrapper.triggerEnterListeners.Add(a);
        }

        public static void RemoveOnTriggerEnterListener(this Collider c, Action<Collider> a)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject);
            if (wrapper)
                wrapper.triggerEnterListeners.Remove(a);
        }

        public static void ClearOnTriggerEnterListeners(this Collider c)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject);
            if (wrapper)
                wrapper.triggerEnterListeners.Clear();
        }

        public static void AddOnTriggerExitListener(this Collider c, Action<Collider> a)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject, true);
            wrapper.triggerExitListeners.Add(a);
        }

        public static void RemoveOnTriggerExitListener(this Collider c, Action<Collider> a)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject);
            if (wrapper)
                wrapper.triggerExitListeners.Remove(a);
        }

        public static void ClearOnTriggerExitListeners(this Collider c)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject);
            if (wrapper)
                wrapper.triggerExitListeners.Clear();
        }

        public static void AddOnTriggerStayListener(this Collider c, Action<Collider> a)
        {
            var wrapper = CreateCollisionStayWrapper(c.gameObject, true);
            wrapper.triggerStayListeners.Add(a);
        }

        public static void RemoveOnTriggerStayListener(this Collider c, Action<Collider> a)
        {
            var wrapper = CreateCollisionStayWrapper(c.gameObject);
            if (wrapper)
                wrapper.triggerStayListeners.Remove(a);
        }

        public static void ClearOnTriggerStayListeners(this Collider c)
        {
            var wrapper = CreateCollisionStayWrapper(c.gameObject);
            if (wrapper)
                wrapper.triggerStayListeners.Clear();
        }
    }
}
