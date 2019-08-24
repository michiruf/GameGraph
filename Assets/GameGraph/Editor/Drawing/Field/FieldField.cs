using System;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class FieldField : VisualElement
    {
        public FieldField(FieldData data, ValueEntry value)
        {
            Add(new Label(data.name));
            Add(new Label(data.type.Name));
            Add(new Label(value.value?.ToString() ?? "No value"));
        }
    }
}
