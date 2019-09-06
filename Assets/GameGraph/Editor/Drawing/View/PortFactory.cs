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
            string portName = null,
            string tooltip = null)
        {
            // TODO Add a tooltip when hovering over the port
            
            var port = Port.Create<EdgeView>(orientation, direction, capacity, type);
            port.name = portId;
            port.portName = portName ?? "";
            return port;
        }
    }
}
