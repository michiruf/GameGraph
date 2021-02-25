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
        private ToolbarToggle toolbarAutoSaveButton => rootVisualElement.QCached<ToolbarToggle>("autoSave");
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
            this.AddEventBus();

            // Initialize UI
            titleContent = new GUIContent(assetExists.name);
            rootVisualElement.AddStylesheet(EditorConstants.ResourcesUxmlPath + "Style");
            rootVisualElement.AddLayout(EditorConstants.ResourcesUxmlPath + "GameGraphWindow");
            toolbarSaveButton.clickable.clicked += SaveGraph;
            toolbarReopenButton.clickable.clicked += () =>
            {
                Close();
                CreateInstance<GameGraphWindow>().InitializeAndShow(assetGuid);
            };
            toolbarAutoSaveButton.RegisterValueChangedCallback(evt =>
                graph.autoSave = toolbarAutoSaveButton.value);

            // Initialize graph
            LoadGraph();
            DistributeGraphAndInitializeChildren();

            // Prepare auto save button state from loaded graph and initialise auto save
            toolbarAutoSaveButton.value = graph.autoSave;

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
                    EditorConstants.CloseEditorSaveHeadline,
                    EditorConstants.CloseEditorSaveContent,
                    EditorConstants.CloseEditorSaveOk,
                    EditorConstants.CloseEditorSaveCancel))
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

        public void MayAutoSaveGraph()
        {
            if(graph.autoSave && graph.isDirty)
                SaveGraph();
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
