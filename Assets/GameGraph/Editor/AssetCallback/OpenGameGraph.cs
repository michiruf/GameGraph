using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using UnityEngine.Assertions;
using GameGraphC = GameGraph.GameGraph;

namespace GameGraph.Editor
{
    [ScriptedImporter(13, GameGraphEditorConstants.Extension)]
    public class GameGraphImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            Debug.Log(ctx.assetPath);
            var view = AssetDatabase.LoadAssetAtPath<GameGraphC>(ctx.assetPath);
            if (view == null)
                throw new ArgumentException();

            // TODO Is this necessary?
            //var gameGraph = view.GetGameGraph();
            //ctx.AddObjectToAsset("MainAsset", gameGraph);
            //ctx.SetMainObject(gameGraph);
        }
    }

    [CustomEditor(typeof(GameGraphImporter))]
    public class OpenGameGraphEditor : ScriptedImporterEditor
    {
        public override void OnInspectorGUI()
        {
            if (!GUILayout.Button("Open Editor"))
                return;

            var importer = target as AssetImporter;
            Assert.IsNotNull(importer, "Importer != null");
            ShowGraphEditWindow(importer.assetPath);
        }

        [OnOpenAsset(0)]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            var path = AssetDatabase.GetAssetPath(instanceId);
            return ShowGraphEditWindow(path);
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

            if (!foundWindow)
            {
                var window = CreateInstance<GameGraphWindow>();
                window.Show();
                window.Initialize(guid);
            }

            return true;
        }
    }
}
