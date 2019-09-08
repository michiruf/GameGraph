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
        public override void OnInspectorGUI()
        {
            if (!GUILayout.Button(EditorConstants.OpenEditorText))
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

        public static bool ShowGraphEditWindow(string path)
        {
            var extension = Path.GetExtension(path);
            if (extension != "." + EditorConstants.FileExtension)
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
                CreateInstance<GameGraphWindow>().InitializeAndShow(guid);

            return true;
        }
    }
}
