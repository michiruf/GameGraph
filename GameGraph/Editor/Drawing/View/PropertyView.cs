using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class PropertyView : VisualElement
    {
        private readonly EditorNode node;

        private Label nameOnlyLabel => this.QCached<Label>("nameOnly");
        private VisualElement nameAndValueContainer => this.QCached<VisualElement>("nameAndValue");
        private VisualElement ingoingPortContainer => this.QCached<VisualElement>("ingoingPortContainer");
        private VisualElement outgoingPortContainer => this.QCached<VisualElement>("outgoingPortContainer");

        private PropertyView(EditorNode node)
        {
            this.node = node;
            this.AddLayout(EditorConstants.ResourcesUxmlViewPath + "PropertyView");
        }

        public PropertyView(MemberData<FieldInfo> data, EditorNode node) : this(node)
        {
            Initialize(data.info.Name, data.info.FieldType, true, true);
        }

        public PropertyView(MemberData<PropertyInfo> data, EditorNode node) : this(node)
        {
            Initialize(data.info.Name, data.info.PropertyType,
                data.info.SetMethod?.IsPublic ?? false,
                data.info.GetMethod?.IsPublic ?? false);
        }

        private void Initialize(string name, Type type, bool hasInput, bool hasOutput)
        {
            // TODO Add these controls to the ports like ShaderGraph
            var prettyName = ObjectNames.NicifyVariableName(name);
            var valueField = ControlFactory.Create(prettyName, name, type, node);
            if (valueField != null)
            {
                nameOnlyLabel.RemoveFromHierarchy();
                nameAndValueContainer.Add(valueField);
            }
            else
            {
                nameOnlyLabel.text = prettyName;
                nameAndValueContainer.RemoveFromHierarchy();
            }

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
                    Port.Capacity.Multi,
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
