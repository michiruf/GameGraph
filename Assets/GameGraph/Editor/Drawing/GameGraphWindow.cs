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

        private ToolbarButton toolbarSaveButton => rootVisualElement.QCached<ToolbarButton>("save");
        private ToolbarButton toolbarReopenButton => rootVisualElement.QCached<ToolbarButton>("reopen");

        public void InitializeAndShow(string assetGuid)
        {
            Initialize(assetGuid);
            Show();
        }

        private void Initialize(string assetGuid)
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

            // Initialize window stuff
            this.MakeWindowReceivableByChildren();
            this.RegisterWindowHooksForEasyAccess();
            this.AddEventBus();

            // Initialize UI
            titleContent = new GUIContent(assetExists.name);
            rootVisualElement.AddStylesheet(EditorConstants.ResourcesUxmlPath + "/Style.uss");
            rootVisualElement.AddLayout(EditorConstants.ResourcesUxmlPath + "/GameGraphWindow.uxml");
            toolbarSaveButton.clickable.clicked += SaveGraph;
            toolbarReopenButton.clickable.clicked += () =>
            {
                Close();
                CreateInstance<GameGraphWindow>().InitializeAndShow(assetGuid);
            };

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
            this.RemoveEventBus();
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

        private void DistributeGraphAndInitializeChildren(VisualElement element = null)
        {
            // NOTE Using a query here as well could improve the performance alot
            if (element == null)
                element = rootVisualElement;
            if (element is IGraphVisualElement c)
                c.Initialize(graph);

            element.Children().ToList().ForEach(DistributeGraphAndInitializeChildren);
        }
    }
}
