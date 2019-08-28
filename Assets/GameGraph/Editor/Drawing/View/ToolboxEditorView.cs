using GameGraph.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    // TODO Should extend a ScrollView, but then the content goes down
    [UsedImplicitly]
    public class ToolboxEditorView : VisualElement, IGameGraphVisualElement
    {
        public ToolboxEditorView()
        {
            this.AddLayout(GameGraphEditorConstants.ResourcesUxmlViewPath + "/ToolboxEditorView.uxml");
        }

        public void Initialize(RawGameGraph graph)
        {
            var container = this.FindElementByName<VisualElement>("container");
            var graphEventHandler = GraphEventHandler.Get(graph.id);

            foreach (var gameGraphComponent in CodeAnalyzer.GetGameGraphComponents())
            {
                var button = new Button();
                button.text = gameGraphComponent.PrettifyName();
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
