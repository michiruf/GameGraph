using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameGraph.Common.Helper
{
    public class GameGraphCollisionStayWrapper : MonoBehaviour
    {
        public List<Action<Collision>> collisionStayListeners = new List<Action<Collision>>();
        public List<Action<Collider>> triggerStayListeners = new List<Action<Collider>>();

        void OnCollisionStay(Collision other)
        {
            foreach (var l in collisionStayListeners)
            {
                l?.Invoke(other);
            }
        }

        void OnTriggerStay(Collider other)
        {
            foreach (var l in triggerStayListeners)
            {
                l?.Invoke(other);
            }
        }
    }
}
