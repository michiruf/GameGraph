using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace GameGraph.Editor
{
    public class GameGraphWindow : EditorWindow
    {
        [SerializeField] private string assetGuidInternal;
        public string assetGuid => assetGuidInternal;
        [SerializeField] private bool initialized;

        private RawGameGraph graph;

        public void Initialize(string assetGuid)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);

            // Check if asset exists
            var assetExists = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            if (assetExists == null)
                return;
            if (!EditorUtility.IsPersistent(assetExists))
                return;

            // Make window for one asset unique
            if (initialized && assetGuid.Equals(assetGuidInternal))
                return;
            assetGuidInternal = assetGuid;

            // Initialize UI
            titleContent = new GUIContent(assetExists.name);
            rootVisualElement.AddStylesheet(GameGraphEditorConstants.ResourcesUxmlPath + "/Style.uss");
            rootVisualElement.AddLayout(GameGraphEditorConstants.ResourcesUxmlPath + "/GameGraphWindow.uxml");
            RegisterSaveButton();
            RegisterReopenButton();

            // Initialize graph
            LoadGraph();
            DistributeGraphAndInitializeChildren();

            Repaint();
        }

        void OnEnable()
        {
            Initialize(assetGuidInternal);
        }

        void OnDisable()
        {
            rootVisualElement.Clear();
            initialized = false;
        }

        void OnDestroy()
        {
            if (graph.isDirty && EditorUtility.DisplayDialog(
                    "Game Graph Has Been Modified",
                    "Do you want to save the changes you made in the Graph?\n"
                    + "Your changes will be lost if you don't save them.",
                    "Save",
                    "Don't Save"))
                SaveGraph();
            //DestroyImmediate(graph);
        }

        private void LoadGraph()
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var textGraph = File.ReadAllText(assetPath);
            graph = JsonUtility.FromJson<RawGameGraph>(textGraph);
        }

        private void SaveGraph()
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var textGraph = JsonUtility.ToJson(graph, true);
            File.WriteAllText(assetPath, textGraph);
        }

        private void RegisterSaveButton()
        {
            rootVisualElement.FindElementByName<ToolbarButton>("save").clickable.clicked += SaveGraph;
        }

        private void RegisterReopenButton()
        {
            rootVisualElement.FindElementByName<ToolbarButton>("reopen").clickable.clicked += () =>
            {
                Close();
                var window = CreateInstance<GameGraphWindow>();
                window.Initialize(assetGuid);
                window.Show();
            };
        }

        private void DistributeGraphAndInitializeChildren(VisualElement element = null)
        {
            if (element == null)
                element = rootVisualElement;
            if (element is IGameGraphVisualElement c)
                c.Initialize(graph);

            element.Children().ToList().ForEach(DistributeGraphAndInitializeChildren);
        }
    }
}
