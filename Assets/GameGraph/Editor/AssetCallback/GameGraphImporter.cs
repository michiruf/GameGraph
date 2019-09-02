using System;
using System.IO;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace GameGraph.Editor
{
    [ScriptedImporter(1, GameGraphEditorConstants.Extension)]
    public class GameGraphImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // TODO Remove this comment
            // This method is currently useless because we do not need
            // to do anything on import and the graph can still get
            // deserialized
            // But later when executing the graph, this could get
            // pretty handy, because a "faster" representation of the
            // data could get serialized as well
            // On the other hand this could be necessary as the
            // database for the assets may only exist in editor context

            Debug.Log(ctx.assetPath);

            var rawTextGraph = File.ReadAllText(ctx.assetPath);
            var rawGraph = JsonUtility.FromJson<RawGameGraph>(rawTextGraph);
            if (rawGraph == null)
                throw new ArgumentException();

            var executableGraph = rawGraph.ToExecutableGraph();
            ctx.AddObjectToAsset("MainAsset", executableGraph);
            ctx.SetMainObject(executableGraph);
        }
    }
}
