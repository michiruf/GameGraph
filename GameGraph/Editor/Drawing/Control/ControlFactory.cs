using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class ControlFactory
    {
        public static Control Create(string name, string fieldName, Type type, EditorNode node)
        {
            if (type == typeof(float))
                return new FieldControl<float, FloatField>(fieldName, name, node);
            if (type == typeof(int))
                return new FieldControl<int, IntegerField>(fieldName, name, node);
            if (type == typeof(string))
                return new FieldControl<string, TextField>(fieldName, name, node);
            if (type == typeof(bool))
                return new FieldControl<bool, Toggle>(fieldName, name, node);
            if (type == typeof(Vector2))
                return new FieldControl<Vector2, Vector2Field>(fieldName, name, node);
            if (type == typeof(Vector3))
                return new FieldControl<Vector3, Vector3Field>(fieldName, name, node);
            if (type == typeof(Color))
                return new FieldControl<Color, ColorField>(fieldName, name, node);
            if (type == typeof(GraphSerializableType))
                return new TypeButtonControl(fieldName, name, node, "Select Type");
            if (type.IsEnum)
                return new EnumFieldControl(fieldName, name, node, type);

            return null;
        }
    }
}
