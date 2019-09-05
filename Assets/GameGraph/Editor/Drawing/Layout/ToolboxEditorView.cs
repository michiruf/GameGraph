using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    // NOTE Should extend a ScrollView, but then the content goes down
    [UsedImplicitly]
    public class ToolboxEditorView : VisualElement, IGraphVisualElement
    {
        public ToolboxEditorView()
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlLayoutPath + "/ToolboxEditorView.uxml");
        }

        public void Initialize(EditorGameGraph graph)
        {
            var container = this.FindElementByName<VisualElement>("container");
            var graphEventHandler = GraphEventHandler.Get(graph);

            foreach (var gameGraphComponent in CodeAnalyzer.GetBlockTypes())
            {
                var button = new Button();
                button.text = gameGraphComponent.name.PrettifyName();
                button.clickable.clicked += () => graphEventHandler.Publish(new NodeAddEvent(gameGraphComponent));
                container.Add(button);
            }
        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<ToolboxEditorView, UxmlTraits>
        {
        }
    }
}
