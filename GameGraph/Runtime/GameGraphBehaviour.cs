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

        public Dictionary<string, Object> parameterInstances => parameterInstancesInternal.dictionary;
        [SerializeField] [HideInInspector]
        private StringUnityObjectDictionary parameterInstancesInternal = new StringUnityObjectDictionary();

        public GraphExecutor executor { get; private set; }

        void OnEnable()
        {
            if (graph == null)
            {
                throw new ArgumentException($"Graph on GameGraphBehaviour on {gameObject.name} must be present!");
            }

            executor = new GraphExecutor(graph,
                // This cast could be unnecessary because the parameter could be of same type as parameterInstances
                // but to be able to maybe put other stuff than UnityEngine.Objects in here it must be present
                parameterInstances.ToDictionary(pair => pair.Key, pair => (object) pair.Value));
            executor.ConstructGraph();
            executor.OrderNodesByExecutionOrder();
            executor.Start(gameObject);
        }

        void OnDisable()
        {
            executor = null;
        }

        void Update()
        {
            executor?.Update();
        }

        void LateUpdate()
        {
            executor?.LateUpdate();
        }

        void FixedUpdate()
        {
            executor?.FixedUpdate();
        }

        private void OnDestroy()
        {
            executor?.OnDestroy();
        }
    }
}
