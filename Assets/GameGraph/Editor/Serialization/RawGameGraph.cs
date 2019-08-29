using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [Serializable]
    public class RawGameGraph
    {
        private string idInternal;
        private int serializedVersion;
        private bool isDirtyInternal;
        public List<RawNode> nodes = new List<RawNode>();
        public List<RawEdge> edges = new List<RawEdge>();

        public string id
        {
            get
            {
                if (string.IsNullOrEmpty(idInternal))
                    idInternal = GUID.Generate().ToString();
                return idInternal;
            }
        }

        public bool isDirty
        {
            get
            {
                return isDirtyInternal ||
                       nodes.Aggregate(false, (b, node) => b || node.isDirty) ||
                       edges.Aggregate(false, (b, edge) => b || edge.isDirty);
            }
            set { isDirtyInternal = value; }
        }


        // TODO
        //public void RegisterCompleteObjectUndo(string actionName)
        //{
        //    Undo.RegisterCompleteObjectUndo(this, actionName);
        //    serializedVersion++;
        //    isDirtyInternal = true;
        //}
    }
}
