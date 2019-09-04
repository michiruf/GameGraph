using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class RawGameGraph : ISerializationCallbackReceiver
    {
        [SerializeField] private int serializedVersion;
        public List<RawNode> nodes = new List<RawNode>();
        public List<RawEdge> edges = new List<RawEdge>();
        private bool isDirtyInternal;

        public bool isDirty
        {
            get
            {
                return isDirtyInternal ||
                       nodes.Aggregate(false, (b, node) => b || node.isDirty) ||
                       edges.Aggregate(false, (b, edge) => b || edge.isDirty);
            }
            set
            {
                isDirtyInternal = value;
                nodes.ForEach(node => node.isDirty = value);
                edges.ForEach(edge => edge.isDirty = value);
            }
        }
        
        public void OnBeforeSerialize()
        {
            serializedVersion++;
            isDirty = false;
        }

        public void OnAfterDeserialize()
        {
            isDirty = false;
        }

        // TODO Undo:
        //public void RegisterCompleteObjectUndo(string actionName)
        //{
        //    Undo.RegisterCompleteObjectUndo(this, actionName);
        //    serializedVersion++;
        //    isDirtyInternal = true;
        //}
    }
}
