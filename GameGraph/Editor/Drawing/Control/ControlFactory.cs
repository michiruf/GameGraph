using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class ControlFactory
    {
        public static VisualElement Create(string name, string fieldName, Type type, EditorNode node)
        {
            if (type == typeof(int))
                return new ControlContainerView<int>(new IntegerField(name), fieldName, node);
            if (type == typeof(float))
                return new ControlContainerView<float>(new FloatField(name), fieldName, node);
            if (type == typeof(string))
                return new ControlContainerView<string>(new TextField(name), fieldName, node);
            if (type == typeof(Vector2))
                return new ControlContainerView<Vector2>(new Vector2Field(name), fieldName, node);
            if (type == typeof(Vector3))
                return new ControlContainerView<Vector3>(new Vector3Field(name), fieldName, node);
//            if (type == typeof(Type))
//                return new ControlView<Type>(new TypeField(name), node);

            // TODO More fields

            return null;
        }
    }
}
