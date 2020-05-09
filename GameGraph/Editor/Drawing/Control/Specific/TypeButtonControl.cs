using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameGraph.Editor
{
    public class TypeButtonControl : ButtonControl<Type>
    {
        private TypeSearchWindowProvider typeSearchWindowProvider => this.GetUserDataOrCreate(() =>
        {
            // Bottleneck is not reflection, but creation of the search window,
            // so no need to cache this for all instances
            var data = CodeAnalyzer.GetNodeTypes().Concat(CodeAnalyzer.GetNonNodeTypes());
            var provider = ScriptableObject.CreateInstance<TypeSearchWindowProvider>();
            provider.Initialize(EditorConstants.ParameterSearchWindowHeadline, data);
            return provider;
        });

        public TypeButtonControl(string fieldName, string label, EditorNode node, string buttonText) : base(fieldName, label, node, buttonText)
        {
            // TODO Magic constant should get eliminated

            if (node.propertyValues.ContainsKey(fieldName + "Internal"))
                if (node.propertyValues[fieldName + "Internal"] is string stringValue)
                    value = Type.GetType(stringValue);

            SetButtonText();
            button.clickable.clicked += () => CreateSearchWindow(type =>
            {
                value = type;
                SetButtonText();
                PersistState();
            }, button.GetScreenPosition() + button.clickable.lastMousePosition);
        }

        private void SetButtonText()
        {
            if (value != null)
                button.text = value.Name;
        }

        private void CreateSearchWindow(Action<Type> callback, Vector2 position)
        {
            typeSearchWindowProvider.callback = (type, vector2) => callback.Invoke(type);
            SearchWindow.Open(new SearchWindowContext(position), typeSearchWindowProvider);
        }

        public override void PersistState()
        {
            // TODO Magic constant should get eliminated
            if (value != null)
                node.propertyValues[fieldName + "Internal"] = value.AssemblyQualifiedName;
        }
    }
}
