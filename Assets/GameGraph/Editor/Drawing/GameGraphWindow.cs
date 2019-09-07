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
        [SerializeField] private string assetGuidInternal;
        public string assetGuid => assetGuidInternal;
        [SerializeField] private bool initialized;

        private EditorGameGraph graph;

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
            rootVisualElement.AddStylesheet(EditorConstants.ResourcesUxmlPath + "/Style.uss");
            rootVisualElement.AddLayout(EditorConstants.ResourcesUxmlPath + "/GameGraphWindow.uxml");
            RegisterSaveButton();
            RegisterReopenButton();

            // Initialize graph
            LoadGraph();
            DistributeGraphAndInitializeChildren();

            // TODo
            //RegisterChildCallbacks();
            rootVisualElement.AddUserData(this);
            this.AddEventBus();

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
            var graphText = File.ReadAllText(assetPath);
            graph = JsonUtility.FromJson<EditorGameGraph>(graphText);
        }

        private void SaveGraph()
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var graphText = JsonUtility.ToJson(graph, true);
            File.WriteAllText(assetPath, graphText);

            // Force import of the asset to trigger the importer
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        private void RegisterSaveButton()
        {
            rootVisualElement.Q<ToolbarButton>("save").clickable.clicked += SaveGraph;
        }

        private void RegisterReopenButton()
        {
            rootVisualElement.Q<ToolbarButton>("reopen").clickable.clicked += () =>
            {
                Close();
                var window = CreateInstance<GameGraphWindow>();
                window.Initialize(assetGuid);
                window.Show();
            };
        }

        private void DistributeGraphAndInitializeChildren(VisualElement element = null)
        {
            // NOTE Using a query here as well could improve the performance alot
            if (element == null)
                element = rootVisualElement;
            if (element is IGraphVisualElement c)
                c.Initialize(titleContent.text, graph, this);

            element.Children().ToList().ForEach(DistributeGraphAndInitializeChildren);
        }
    }
}
