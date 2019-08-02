using System;
using System.Linq;
using GameGraph.Editor.Util;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace GameGraph.Editor
{
    public class GameGraphWindow : EditorWindow
    {
        private GameGraph graph;

        [SerializeField] public string assetGuid;

        public void Initialize(string assetGuid)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            if (asset == null)
                return;

            if (!EditorUtility.IsPersistent(asset))
                return;

            if (this.assetGuid == assetGuid)
                return;

            this.assetGuid = assetGuid;

//            LoadGraph(asset);
            InitializeUi(asset.name);
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

            graph = asset.GetGameGraph();
        }

        private void InitializeUi(string title)
        {
            titleContent = new GUIContent(title);

            const string stylePath = GameGraphEditorConstants.ResourcesUxmlPath + "/GameGraphWindow.uss";
            rootVisualElement.AddStylesheet(stylePath);

            const string layoutPath = GameGraphEditorConstants.ResourcesUxmlPath + "/GameGraphWindow.uxml";
            rootVisualElement.AddLayout(layoutPath);

            rootVisualElement.RegisterCallback<GeometryChangedEvent>(e =>
            {
                Debug.LogError("GeometryChangedEvent");
                InitializeChildren(rootVisualElement);
            });

            Repaint();
        }

        private void InitializeChildren(VisualElement element)
        {
            if (element is IGameGraphVisualElement c)
                c.graph = graph;
            element.Children().ToList().ForEach(InitializeChildren);
        }
    }
}
