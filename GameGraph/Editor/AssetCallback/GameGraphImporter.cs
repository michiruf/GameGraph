using System;
using System.IO;

using UnityEngine;

namespace GameGraph.Editor
{
    [UnityEditor.AssetImporters.ScriptedImporter(1, EditorConstants.FileExtension)]
    public class GameGraphImporter : UnityEditor.AssetImporters.ScriptedImporter
    {
        public const string MainAsset = "MainAsset";
        public const string ParameterAsset = "ParameterAsset";

        public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext ctx)
        {
            var rawTextGraph = File.ReadAllText(ctx.assetPath);
            var rawGraph = JsonUtility.FromJson<EditorGameGraph>(rawTextGraph);
            if (rawGraph == null)
                throw new ArgumentException();

            var executableGraph = rawGraph.Transformer().GetGraphObject(ctx.mainObject as GraphObject);
            ctx.AddObjectToAsset(MainAsset, executableGraph);
            ctx.SetMainObject(executableGraph);
        }
    }
}
