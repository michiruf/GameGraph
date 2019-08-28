using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace GameGraph
{
    [Serializable]
    public class GameGraph : ISerializationCallbackReceiver
    {
        public List<Node> nodes = new List<Node>();

        [NonSerialized]
        public Action graphChangedEvent;

        public void OnBeforeSerialize()
        {
            Debug.LogError("OnBeforeSerialize");
        }

        public void OnAfterDeserialize()
        {
            Debug.LogError("OnAfterDeserialize");
        }

        public void AddNodeByName(string name)
        {
            nodes.Add(new Node(name));
            graphChangedEvent?.Invoke();
        }
    }
}
