using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEditor.SceneManagement;

namespace GameGraph.Editor
{
    public class CreateGameGraph : EndNameEditAction
    {
        [MenuItem("Assets/Create/Game Graph", false, 2000)]
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
            var graph = new EditorGameGraph();
            File.WriteAllText(pathName, EditorJsonUtility.ToJson(graph));
            AssetDatabase.Refresh();
        }
    }
}
