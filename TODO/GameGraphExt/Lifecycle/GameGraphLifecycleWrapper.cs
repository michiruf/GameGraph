using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGraph.Common.Helper
{
    public class GameGraphLifecycleWrapper : MonoBehaviour
    {
        public readonly List<Action> destroyListener = new();

        // TODO Move lifecycle

        void OnDestroy()
        {
            foreach (var l in destroyListener)
                l?.Invoke();
        }
    }
}
