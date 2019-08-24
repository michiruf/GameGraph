using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class GameGraphWindow : EditorWindow
    {
        // TODO Get this game graph from asset (for now, do not handle serialization)
        private GameGraph graph = new GameGraph();

        [SerializeField] public string assetGuid;

        // TODO Test this
//        public GameGraphWindow()
//        {
//            
//        }

        public void Initialize(string assetGuid)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath); // TODO
            if (asset == null)
                return;

            if (!EditorUtility.IsPersistent(asset))
                return;

            if (this.assetGuid == assetGuid)
                return;

            this.assetGuid = assetGuid;

            //LoadGraph(asset);
            InitializeWindow(asset.name);
            RegisterReopenButton();
            DistributeGraphAndInitializeChildren();

            Repaint();
        }

        private void LoadGraph(GameGraph asset)
        {
            var path = AssetDatabase.GetAssetPath(asset);
            //var textGraph = File.ReadAllText(path, Encoding.UTF8);
            //graphObject = CreateInstance<GraphObject>();
            //graphObject.hideFlags = HideFlags.HideAndDontSave;
            //graphObject.graph = JsonUtility.FromJson(textGraph, graphType) as IGraph;
            //graphObject.graph.OnEnable();
            //graphObject.graph.ValidateGraph();

//            graph = asset.GetGameGraph();
        }

        private void InitializeWindow(string title)
        {
            titleContent = new GUIContent(title);
            rootVisualElement.AddStylesheet(GameGraphEditorConstants.ResourcesUxmlPath + "/Style.uss");
            rootVisualElement.AddLayout(GameGraphEditorConstants.ResourcesUxmlPath + "/GameGraphWindow.uxml");
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
