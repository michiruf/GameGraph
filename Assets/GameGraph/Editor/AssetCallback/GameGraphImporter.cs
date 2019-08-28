using UnityEditor.Experimental.AssetImporters;

namespace GameGraph.Editor
{
    [ScriptedImporter(1, GameGraphEditorConstants.Extension)]
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
}
