using System;
using GameGraph.CodeAnalysis;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class MethodView : VisualElement
    {
        public MethodView(MethodData data, ValueEntry value)
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlViewPath + "/MethodView.uxml");

            // Set simple data
            this.FindElementByName<Label>("name").text = data.name.PrettifyName();

            // Add port
            var container = this.FindElementByName<VisualElement>("ingoingPortContainer");
            var port = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Action));
            container.Add(port);
            container.AddToClassList("exists");
        }
    }
}
