using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    // TODO Should extend a ScrollView, but then the content goes down
    [UsedImplicitly]
    public class ToolboxEditorView : VisualElement, IGameGraphVisualElement
    {
        public GameGraph graph { get; set; }

        public ToolboxEditorView()
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlViewPath + "/ToolboxEditorView.uxml");
        }

        public void Initialize()
        {
            var container = this.FindElementByName<VisualElement>("container");

            foreach (var gameGraphComponent in CodeAnalysis.GetGameGraphComponents())
            {
                var button = new Button();
                button.text = gameGraphComponent;
                button.clickable.clicked += () => { graph.AddNodeByName(gameGraphComponent); };
                container.Add(button);
            }
        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<ToolboxEditorView, UxmlTraits>
        {
        }
    }
}
