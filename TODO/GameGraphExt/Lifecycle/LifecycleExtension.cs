using System;
using UnityEngine;

namespace GameGraph.Common.Helper
{
    public static class LifecycleExtension
    {
        private static GameGraphLifecycleWrapper CreateCollisionWrapper(GameObject o, bool alsoCreate = false)
        {
            var wrapper = o.GetComponent<GameGraphLifecycleWrapper>();
            if (alsoCreate && wrapper == null)
                wrapper = o.AddComponent<GameGraphLifecycleWrapper>();
            return wrapper;
        }

        public static void AddOnDestroyListener(this GameObject o, Action a)
        {
            var wrapper = CreateCollisionWrapper(o, true);
            wrapper.destroyListener.Add(a);
        }

        public static void AddOnDestroyListener(this Component c, Action a)
        {
            var wrapper = CreateCollisionWrapper(c.gameObject, true);
            wrapper.destroyListener.Add(a);
        }


    }
}
