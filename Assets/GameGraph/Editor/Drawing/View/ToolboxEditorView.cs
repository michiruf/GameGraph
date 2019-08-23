using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class ToolboxEditorView : VisualElement, IGameGraphVisualElement
    {
        public GameGraph graph { get; set; }

        public ToolboxEditorView()
        {
            InitializeUi();
            InitializeControls();
        }

        private void InitializeUi()
        {
            const string stylePath = GameGraphEditorConstants.ResourcesUxmlViewPath + "/ToolboxEditorView.uss";
            this.AddStylesheet(stylePath);
            const string layoutPath = GameGraphEditorConstants.ResourcesUxmlViewPath + "/ToolboxEditorView.uxml";
            this.AddLayout(layoutPath);
        }

//        private void ApplyTraits(UxmlTraits traits)
//        {
//            if (!string.IsNullOrEmpty(traits.title))
//                this.FindElementByName<Label>("title").text = traits.title;
//        }

        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<ToolboxEditorView, UxmlTraits>
        {
        }

//        public new class UxmlTraits : VisualElement.UxmlTraits
//        {
//            public string title;
//
//            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
//            {
//                base.Init(ve, bag, cc);
//                if (!(ve is ToolboxEditorView v))
//                    return;
//
//                title = "title".ToAttribute().GetValueFromBag(bag, cc);
//
//                v.ApplyTraits(this);
//            }
//        }

        private void InitializeControls()
        {
            var container = this.FindElementByName<Box>("container");

            foreach (var gameGraphComponent in CodeAnalysis.GetGameGraphComponents())
            {
                container.Add(new Button(() => { Debug.LogError("TODO"); })
                {
                    text = gameGraphComponent
                });
            }
        }
    }
}
