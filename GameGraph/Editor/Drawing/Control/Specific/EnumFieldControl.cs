using System;
using UnityEditor.UIElements;

namespace GameGraph.Editor
{
    public class EnumFieldControl : FieldControl<Enum, EnumField>
    {
        public EnumFieldControl(string fieldName, string label, EditorNode node, Type type) : base(fieldName, label, node)
        {
            if (!(type.GetEnumValues().GetValue(0) is Enum firstEnum))
                throw new ArgumentException($"Field {fieldName} for type {type.Name} is not an enum!");
            field.Init(firstEnum);

            InitializeValue();
        }
    }
}
