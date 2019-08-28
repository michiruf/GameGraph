using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class GameGraphWindow : EditorWindow
    {
        [SerializeField] public string assetGuid;
        private GameGraph graph;

        public void Initialize(string assetGuidP)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuidP);

            // Check if asset exists
            var assetExists = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            if (assetExists == null)
                return;
            if (!EditorUtility.IsPersistent(assetExists))
                return;

            // Make window for one asset unique
            if (assetGuid == assetGuidP)
                return;
            assetGuid = assetGuidP;

            // Load layout and style
            rootVisualElement.AddStylesheet(GameGraphEditorConstants.ResourcesUxmlPath + "/Style.uss");
            rootVisualElement.AddLayout(GameGraphEditorConstants.ResourcesUxmlPath + "/GameGraphWindow.uxml");

            titleContent = new GUIContent(assetExists.name);
            LoadGraph();
            RegisterSaveButton();
            RegisterReopenButton();
            DistributeGraphAndInitializeChildren();
            Repaint();
        }

        private void LoadGraph()
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var textGraph = File.ReadAllText(assetPath);
            graph = JsonUtility.FromJson<GameGraph>(textGraph);
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
                window.Show();
                window.Initialize(assetGuid);
            };
        }

        private void DistributeGraphAndInitializeChildren(VisualElement element = null)
        {
            if (element == null)
                element = rootVisualElement;
            if (element is IGameGraphVisualElement c)
            {
                c.graph = graph;
                c.Initialize();
            }
            element.hierarchy.Children().ToList().ForEach(DistributeGraphAndInitializeChildren);
        }
    }
}
