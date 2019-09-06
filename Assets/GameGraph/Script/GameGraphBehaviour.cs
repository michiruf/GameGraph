using System;
using UnityEngine;

namespace GameGraph
{
    public class GameGraphBehaviour : MonoBehaviour
    {
        public GraphObject graph;
        
        private GraphExecutor graphExecutor;
        private bool graphNotNull = true;

        void Start()
        {
            if (graph == null)
            {
                graphNotNull = false;
                throw new ArgumentException($"Graph on GameGraphBehaviour on {gameObject.name} must be present!");
            }

            graphExecutor = new GraphExecutor(graph);
            graphExecutor.ConstructGraph();
            graphExecutor.Start();
        }

        void Update()
        {
            if (graphNotNull)
                graphExecutor.Update();
        }

        void LateUpdate()
        {
            if (graphNotNull)
                graphExecutor.LateUpdate();
        }

        void FixedUpdate()
        {
            if (graphNotNull)
                graphExecutor.FixedUpdate();
        }
    }
}
