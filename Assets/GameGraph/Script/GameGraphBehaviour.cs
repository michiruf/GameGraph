using System;
using UnityEngine;

namespace GameGraph
{
    public class GameGraphBehaviour : MonoBehaviour
    {
        public GraphObject graph;
        private bool graphNotNull = true;

        void Start()
        {
            if (graph == null)
            {
                graphNotNull = false;
                throw new ArgumentException($"Graph on GameGraphBehaviour on {gameObject.name} must be present!");
            }

            graph.ConstructGraph();
            graph.Start();
        }

        void Update()
        {
            if (graphNotNull)
                graph.Update();
        }

        void LateUpdate()
        {
            if (graphNotNull)
                graph.LateUpdate();
        }

        void FixedUpdate()
        {
            if (graphNotNull)
                graph.FixedUpdate();
        }
    }
}
