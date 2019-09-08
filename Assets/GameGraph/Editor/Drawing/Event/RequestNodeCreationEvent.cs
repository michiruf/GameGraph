using System;
using UnityEngine;

namespace GameGraph.Editor
{
    public class RequestNodeCreationEvent
    {
        public readonly Type type;
        public readonly Vector2? position;
        public readonly string parameterId;

        public RequestNodeCreationEvent(Type type, Vector2? position, string parameterId)
        {
            this.type = type;
            this.position = position;
            this.parameterId = parameterId;
        }
    }
}
