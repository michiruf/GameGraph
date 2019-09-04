using UnityEngine;

namespace GameGraph
{
    public class GameGraphInstanceProviderBehaviour : MonoBehaviour
    {
        public GameGraphBehaviour gameGraphBehaviour;
        public Component instance;
        public string instanceName;

        // TODO Provide a given instance to the graph
        // TODO Use the name to determine its node (implement a binding name field in the graph node views)
        
        // TODO Instead of having a instance provider, the elements that are respected for references are under the
        // ... game graph behaviour
    }
}
