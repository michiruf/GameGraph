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
            var port = Port.Create<EdgeView>(orientation, direction, capacity, type);
            port.name = portId;
            port.portName = portName ?? "";
            port.tooltip = tooltip;
            return port;
        }
    }
}
