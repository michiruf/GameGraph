using System;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    // TODO Also add TField and cast value got from dictionary to value of field, maybe add a method for this
    //      Still serialization to handle. Fuck... (when creating adapters)
    public abstract class ControlBase<TValue, TSerializedValue, TField> : VisualElement
        where TField : BaseField<TValue>, new()
    {
        private readonly string fieldName;
        private readonly string label;
        private readonly EditorNode node;

        public ControlBase(string fieldName, string label, EditorNode node)
        {
            this.fieldName = fieldName;
            this.label = label;
            this.node = node;
            Initialize();
        }

        protected abstract TValue GetValue(TSerializedValue value);

        private void Initialize()
        {
            var field = new TField {label = label};

            if (node.propertyValues.ContainsKey(fieldName))
            {
                var valueRaw = node.propertyValues[fieldName];
                if (!(valueRaw is TSerializedValue))
                    throw new ArgumentException("Value must be of type " + typeof(TValue) + " but is of type " +
                                                valueRaw.GetType());
                var value = GetValue((TSerializedValue) valueRaw);
                field.SetValueWithoutNotify(value);
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
