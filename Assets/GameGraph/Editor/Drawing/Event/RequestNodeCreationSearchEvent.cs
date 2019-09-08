using UnityEngine;

namespace GameGraph.Editor
{
    public class RequestNodeCreationSearchEvent
    {
        public readonly Vector2 screenMousePosition;
        
        public RequestNodeCreationSearchEvent(Vector2 screenMousePosition)
        {
            this.screenMousePosition = screenMousePosition;
        }
    }
}
