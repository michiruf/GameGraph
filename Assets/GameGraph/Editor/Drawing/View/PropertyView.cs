using System;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class PropertyView : VisualElement
    {
        private Label nameLabel => this.QCached<Label>("name");
        private Label valueLabel => this.QCached<Label>("value");
        private VisualElement ingoingPortContainer => this.QCached<VisualElement>("ingoingPortContainer");
        private VisualElement outgoingPortContainer => this.QCached<VisualElement>("outgoingPortContainer");

        private PropertyView()
        {
            this.AddLayout(EditorConstants.ResourcesUxmlViewPath + "/PropertyView.uxml");
        }

        public PropertyView(FieldInfo info) : this()
        {
            Initialize(info.Name, info.FieldType, true, true);
        }

        public PropertyView(PropertyInfo info) : this()
        {
            Initialize(info.Name, info.PropertyType,
                info.SetMethod?.IsPublic ?? false,
                info.GetMethod?.IsPublic ?? false);
        }

        private void Initialize(string name, Type type, bool hasInput, bool hasOutput)
        {
            nameLabel.text = name.PrettifyName();
            valueLabel.text = "TODO";

            if (hasInput)
            {
                var port = PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Input,
                    Port.Capacity.Single,
                    type,
                    name,
                    null,
                    type.Name);
                ingoingPortContainer.Add(port);
                ingoingPortContainer.AddToClassList("exists");
            }

            if (hasOutput)
            {
                var port = PortFactory.Create(
                    Orientation.Horizontal,
                    Direction.Output,
                    Port.Capacity.Single,
                    type,
                    name,
                    null,
                    type.Name);
                outgoingPortContainer.Add(port);
                outgoingPortContainer.AddToClassList("exists");
            }
        }
    }
}
