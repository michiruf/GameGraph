using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameGraph.Editor
{
    // TODO Maybe add the viewport dimensions
    
    [Serializable]
    public class EditorGameGraph : ISerializationCallbackReceiver
    {
        [SerializeField] private int serializedVersion;
        public List<EditorNode> nodes = new List<EditorNode>();
        public List<EditorEdge> edges = new List<EditorEdge>();
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
