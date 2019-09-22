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
        private StringUnityObjectDictionary parameterInstancesInternal = new StringUnityObjectDictionary();

        public GraphExecutor executor { get; private set; }

        void Start()
        {
            if (graph == null)
            {
                graphNull = true;
                throw new ArgumentException($"Graph on GameGraphBehaviour on {gameObject.name} must be present!");
            }

            executor = new GraphExecutor(graph,
                // This cast could be unnecessary because the parameter could be of same type as parameterInstances
                // but to be able to maybe put other stuff than UnityEngine.Objects in here it must be present
                parameterInstances.ToDictionary(pair => pair.Key, pair => (object) pair.Value));
            executor.ConstructGraph();
            executor.OrderNodesByExecutionOrder();
            executor.Start();
        }

        void Update()
        {
            if (graphNull) return;
            executor.Update();
        }

        void LateUpdate()
        {
            if (graphNull) return;
            executor.LateUpdate();
        }

        void FixedUpdate()
        {
            if (graphNull) return;
            executor.FixedUpdate();
        }
    }
}
