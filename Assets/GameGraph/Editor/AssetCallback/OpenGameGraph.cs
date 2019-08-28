using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameGraph.Editor
{
    [ScriptedImporter(13, GameGraphEditorConstants.Extension)]
    public class GameGraphImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // This method is currently useless because we do not need
            // to do anything on import and the graph can still get
            // deserialized
            // But later when executing the graph, this could get
            // pretty handy, because a "faster" representation of the
            // data could get serialized as well
            // On the other hand this could be necessary as the
            // database for the assets may only exist in editor context

            // Variant like in the shader graph, but highly stripped:
            //Debug.Log(ctx.assetPath);
            //var graph = JsonUtility.FromJson<GameGraph>(ctx.assetPath);
            //if (graph == null)
            //    throw new ArgumentException();
            //ctx.AddObjectToAsset("MainAsset", graph);
            //ctx.SetMainObject(graph);

            // Variant from the samples:
            // https://docs.unity3d.com/2019.2/Documentation/ScriptReference/Experimental.AssetImporters.AssetImporterEditor.html
            //var root = ObjectFactory.CreateInstance<GameGraphWrapper>();
            //root.graph = JsonUtility.FromJson<GameGraph>(ctx.assetPath);
            //ctx.AddObjectToAsset("MainAsset", root);
            //ctx.SetMainObject(root);
        }
    }

    [CustomEditor(typeof(GameGraphImporter))]
    public class OpenGameGraphEditor : ScriptedImporterEditor
    {
        [OnOpenAsset(0)]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            var path = AssetDatabase.GetAssetPath(instanceId);
            return ShowGraphEditWindow(path);
        }

        public override void OnInspectorGUI()
        {
            if (!GUILayout.Button("Open Editor"))
                return;

            var importer = target as AssetImporter;
            Assert.IsNotNull(importer, "Importer != null");
            ShowGraphEditWindow(importer.assetPath);
        }

        private static bool ShowGraphEditWindow(string path)
        {
            var extension = Path.GetExtension(path);
            if (extension != "." + GameGraphEditorConstants.Extension)
                return false;

            var guid = AssetDatabase.AssetPathToGUID(path);
            var foundWindow = false;
            foreach (var w in Resources.FindObjectsOfTypeAll<GameGraphWindow>())
            {
                if (w.assetGuid != guid)
                    continue;
                foundWindow = true;
                w.Focus();
            }

            // TODO While dev, always kill the window and recreate it
            if (true)
                foreach (var w in Resources.FindObjectsOfTypeAll<GameGraphWindow>())
                {
                    w.Close();
                }

            if (!foundWindow)
            {
                var window = CreateInstance<GameGraphWindow>();
                window.Initialize(guid);
                window.Show();
            }

            return true;
        }
    }
}
