using System.Collections.Generic;
using UnityEngine;

namespace GameGraph
{
    public class GraphObject : ScriptableObject
    {
        public Dictionary<string, Node> nodes
        {
            get => nodesInternal.dictionary;
            set => nodesInternal.dictionary = value;
        }
        [SerializeField] private StringNodeDictionary nodesInternal = new StringNodeDictionary();

        public Dictionary<string, Parameter> parameters
        {
            get => parametersInternal.dictionary;
            set => parametersInternal.dictionary = value;
        }
        [SerializeField] private StringParameterDictionary parametersInternal = new StringParameterDictionary();
    }
}
