using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Networking.Types;

namespace GameGraph.Editor
{
    public static class PortFactory
    {
        public static Port Create(
            Orientation orientation,
            Direction direction,
            Port.Capacity capacity,
            Type type,
            string portId,
            bool showName = true)
        {
            var port = Port.Create<EdgeView>(orientation, direction, capacity, type);
            port.portName = showName ? portId.PrettifyName() : "";
            port.name = portId;
            return port;
        }
    }
}
