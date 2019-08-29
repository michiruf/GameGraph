using GameGraph.CodeAnalysis;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class PropertyView : VisualElement
    {
        public PropertyView(MemberData data)
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlViewPath + "/PropertyView.uxml");

            // Set simple data
            this.FindElementByName<Label>("name").text = data.name.PrettifyName();
            this.FindElementByName<Label>("value").text = "VALUE NOT IMPLEMENTED";

            // Add ports
            {
                var port = Port.Create<EdgeView>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, data.type);
                port.portName = "";
                var container = this.FindElementByName<VisualElement>("ingoingPortContainer");
                container.Add(port);
                container.AddToClassList("exists");
            }
            {
                var port = Port.Create<EdgeView>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, data.type);
                port.portName = "";
                var container = this.FindElementByName<VisualElement>("outgoingPortContainer");
                container.Add(port);
                container.AddToClassList("exists");
            }
        }
    }
}
