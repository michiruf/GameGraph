using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace GameGraph
{
    [Serializable]
    public class GameGraph : UnityEngine.Object
    {
        public readonly List<Node> nodes = new List<Node>();

        public Action graphChangedEvent;

        public void AddNodeByName(string name)
        {
            nodes.Add(new Node(name));
            graphChangedEvent?.Invoke();
        }
    }
}
