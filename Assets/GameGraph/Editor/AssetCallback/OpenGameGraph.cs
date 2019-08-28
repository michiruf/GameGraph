using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameGraph.Editor
{
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
                // TODO While dev, always kill the window and recreate it
                if (true)
                {
                    try
                    {
                        w.Close();
                    }
                    catch
                    {
                        // ignored
                    }
                    continue;
                }

                if (w.assetGuid != guid)
                    continue;
                foundWindow = true;
                w.Focus();
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
