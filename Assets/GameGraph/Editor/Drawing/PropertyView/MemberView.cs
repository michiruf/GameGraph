using GameGraph.CodeAnalysis;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class MemberView : VisualElement
    {
        public MemberView(MemberData data, bool ingoingPort, bool outgoingPort)
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlFieldTypePath + "/MemberView.uxml");

            // Set simple data
            this.FindElementByName<Label>("name").text = data.name.PrettifyName();
            this.FindElementByName<Label>("value").text = "VALUE NOT IMPLEMENTED";

            // Add ports
            if (ingoingPort)
            {
                var port = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, data.type);
                var container = this.FindElementByName<VisualElement>("ingoingPortContainer");
                container.Add(port);
                container.AddToClassList("exists");
            }
            if (outgoingPort)
            {
                var port = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, data.type);
                var container = this.FindElementByName<VisualElement>("outgoingPortContainer");
                container.Add(port);
                container.AddToClassList("exists");
            }
        }
    }
}
