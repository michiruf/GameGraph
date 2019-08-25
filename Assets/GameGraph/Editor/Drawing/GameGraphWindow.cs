using System.IO;
using System.Linq;
using System.Text;
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

        public GameGraphWindow()
        {
        }

        public void Initialize(string newAssetGuid)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(newAssetGuid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            if (asset == null)
                return;

            if (!EditorUtility.IsPersistent(asset))
                return;

            if (assetGuid == newAssetGuid)
                return;
            assetGuid = newAssetGuid;

            // Load loayut and style
            rootVisualElement.AddStylesheet(GameGraphEditorConstants.ResourcesUxmlPath + "/Style.uss");
            rootVisualElement.AddLayout(GameGraphEditorConstants.ResourcesUxmlPath + "/GameGraphWindow.uxml");

            titleContent = new GUIContent(asset.name);
            graph = LoadGraph(asset);
            RegisterReopenButton();
            DistributeGraphAndInitializeChildren();
            Repaint();
        }

        private GameGraph LoadGraph(Object asset)
        {
            var path = AssetDatabase.GetAssetPath(asset);
            var textGraph = File.ReadAllText(path, Encoding.UTF8);
            return JsonUtility.FromJson(textGraph, typeof(GameGraph)) as GameGraph ?? new GameGraph();
        }

        private void RegisterReopenButton()
        {
            var button = rootVisualElement.FindElementByName<ToolbarButton>("reopen");
            button.clickable.clicked += () =>
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
