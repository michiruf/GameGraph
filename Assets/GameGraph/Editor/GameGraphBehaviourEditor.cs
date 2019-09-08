using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    [CustomEditor(typeof(GameGraphBehaviour))]
    public class GameGraphBehaviourEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var behaviour = (GameGraphBehaviour) target;

            if (behaviour.graph == null)
                return;

            EditorGUILayout.Space();
            AddGraphDependingFields(behaviour.graph);
            EditorGUILayout.Space();
            AddOpenButton(behaviour.graph);
        }

        private void AddGraphDependingFields(GraphObject graph)
        {
            foreach (var pair in graph.parameters)
            {
                var parameter = pair.Value;
                parameter.instance =
                    EditorGUILayout.ObjectField(parameter.name, parameter.instance, parameter.type.type, true);
            }
        }

        private void AddOpenButton(GraphObject graph)
        {
            if (!GUILayout.Button(EditorConstants.OpenEditorText))
                return;

            var path = AssetDatabase.GetAssetPath(graph);
            OpenGameGraphEditor.ShowGraphEditWindow(path);
        }
    }
}
