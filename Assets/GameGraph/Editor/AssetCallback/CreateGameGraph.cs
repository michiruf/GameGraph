using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;

namespace GameGraph.Editor
{
    public class CreateGameGraph : EndNameEditAction
    {
        [MenuItem("Assets/Create/Game Graph", false, 1000)]
        public static void CreateMaterialGraph()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                CreateInstance<CreateGameGraph>(),
                "New Game Graph." + GameGraphEditorConstants.Extension,
                null,
                null);
        }

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            var graph = new GameGraph();
            File.WriteAllText(pathName, EditorJsonUtility.ToJson(graph));
            AssetDatabase.Refresh();
        }
    }
}
