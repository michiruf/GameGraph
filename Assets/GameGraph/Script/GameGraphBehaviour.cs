using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameGraph
{
    public class GameGraphBehaviour : MonoBehaviour
    {
        public GraphObject graph;
        private bool graphNull;

        public Dictionary<string, Object> parameterInstances => parameterInstancesInternal.dictionary;
        [SerializeField] [HideInInspector]
        private StringObjectDictionary parameterInstancesInternal = new StringObjectDictionary();

        void Start()
        {
            if (graph == null)
            {
                graphNull = true;
                throw new ArgumentException($"Graph on GameGraphBehaviour on {gameObject.name} must be present!");
            }

            // This cast could be unnecessary because the parameter could be of same type as parameterInstances
            // but to be able to maybe put other stuff than UnityEngine.Objects in here it must be present
            graph.ConstructGraph(parameterInstances.ToDictionary(pair => pair.Key, pair => (object) pair.Value));
            graph.OrderNodesByExecutionOrder();
            graph.Start();
        }

        public void Update()
        {
            if (graphNull) return;
            graph.Update();
        }

        public void LateUpdate()
        {
            if (graphNull) return;
            graph.LateUpdate();
        }

        public void FixedUpdate()
        {
            if (graphNull) return;
            graph.FixedUpdate();
        }
    }
}
