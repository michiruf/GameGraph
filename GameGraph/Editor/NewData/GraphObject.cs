using System;
using UnityEngine;

namespace GameGraph.Editor.NewData
{
    public class GraphObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private int serializedVersion;
        [SerializeField] private SerializationHelper.JsonSerializedElement m_SerializedGraph;

        private GraphData graphInternal;
        public GraphData graph
        {
            get => graphInternal;
            set
            {
                if (graphInternal != null)
                    graphInternal.owner = null;
                graphInternal = value;
                if (graphInternal != null)
                    graphInternal.owner = this;
            }
        }

        public bool isDirty { get; set; }

        public void OnBeforeSerialize()
        {
            serializedVersion++;

            if (graph != null)
            {
                m_SerializedGraph = SerializationHelper.Serialize(graph);
                m_IsSubGraph = graph.isSubGraph;
            }
        }

        public void OnAfterDeserialize()
        {
            var deserializedGraph =
                SerializationHelper.Deserialize<GraphData>(m_SerializedGraph, GraphUtil.GetLegacyTypeRemapping());
            deserializedGraph.isSubGraph = m_IsSubGraph;
            if (graph == null)
                graph = deserializedGraph;
            else
                m_DeserializedGraph = deserializedGraph;
        }
    }
}
