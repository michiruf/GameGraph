using System;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    // TODO Also add TField and cast value got from dictionary to value of field, maybe add a method for this
    //      Still serialization to handle. Fuck... (when creating adapters)
    public class ControlContainerView<TValue> : VisualElement
    {
        private readonly BaseField<TValue> field;
        private readonly string fieldName;
        private readonly EditorNode node;

        public ControlContainerView(BaseField<TValue> field, string fieldName, EditorNode node)
        {
            this.field = field;
            this.fieldName = fieldName;
            this.node = node;
            Initialize();
        }

        private void Initialize()
        {
            if (node.propertyValues.ContainsKey(fieldName))
            {
                var value = node.propertyValues[fieldName];
                if (!(value is TValue))
                    throw new ArgumentException("Value must be of type " + typeof(TValue) + " but is of type "
                                                + value.GetType());
                field.SetValueWithoutNotify((TValue) value);
            }

            field.RegisterValueChangedCallback(OnChange);
            Add(field);
        }

        private void OnChange(ChangeEvent<TValue> evt)
        {
            node.propertyValues[fieldName] = evt.newValue;
        }
    }
}
