using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    // TODO HERE I AM
    // The complete approach is bullshit.
    // Instead of saving a already saved asset, attach the instances to the behaviour.
    // This way is totally easy to persist references and it is totally easy to make them changeable!

    [CustomEditor(typeof(GameGraphBehaviour), true)] // TODO For children might not be necessary after rework
    public class GameGraphBehaviourEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var behaviour = (GameGraphBehaviour) target;

            if (behaviour.graph == null)
                return;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(EditorConstants.ParameterInspectorLabel, EditorStyles.boldLabel);
            AddGraphDependingFields(behaviour.graph, behaviour.parameterInstances);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            AddOpenButton(behaviour.graph);
        }

        private void AddGraphDependingFields(GraphObject graph, Dictionary<string, Object> parameterInstances)
        {
            // Fill instance dictionary with parameters
            foreach (var pair in graph.parameters)
            {
                if (!parameterInstances.ContainsKey(pair.Key))
                    parameterInstances.Add(pair.Key, null);
            }

            // Remove instance entries that do not exist in parameters
            foreach (var pair in parameterInstances.ToDictionary(pair => pair.Key, pair => pair.Value))
            {
                if (!graph.parameters.ContainsKey(pair.Key))
                    parameterInstances.Remove(pair.Key);
            }

            // Draw parameters
            var parameterInstancesProperty = serializedObject.FindProperty("parameterInstancesInternal");
            var values = parameterInstancesProperty.FindPropertyRelative("values");
            var i = 0;
            foreach (var pair in graph.parameters)
            {
                var parameter = pair.Value;
                var element = values.GetArrayElementAtIndex(i);
                EditorGUILayout.ObjectField(element, parameter.type, new GUIContent(parameter.name.PrettifyName()));
                i++;
            }

            serializedObject.ApplyModifiedProperties();
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
