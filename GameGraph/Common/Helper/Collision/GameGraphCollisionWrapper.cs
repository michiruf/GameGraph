using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGraph.Common.Helper
{
    public class GameGraphCollisionWrapper : MonoBehaviour
    {
        public List<Action<Collision>> collisionEnterListeners = new List<Action<Collision>>();
        public List<Action<Collision>> collisionExitListeners = new List<Action<Collision>>();
        public List<Action<Collider>> triggerEnterListeners = new List<Action<Collider>>();
        public List<Action<Collider>> triggerExitListeners = new List<Action<Collider>>();

        void OnCollisionEnter(Collision other)
        {
            foreach (var l in collisionEnterListeners)
            {
                l?.Invoke(other);
            }
        }

        void OnCollisionExit(Collision other)
        {
            foreach (var l in collisionExitListeners)
            {
                l?.Invoke(other);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            foreach (var l in triggerEnterListeners)
            {
                l?.Invoke(other);
            }
        }

        void OnTriggerExit(Collider other)
        {
            foreach (var l in triggerExitListeners)
            {
                l?.Invoke(other);
            }
        }
    }
}
