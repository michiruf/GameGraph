//using JetBrains.Annotations;
//using UnityEditor.Experimental.GraphView;
//using UnityEngine;
//
//namespace GameGraph.Editor
//{
//    public class GraphEditorPresenter
//    {
//        private readonly EditorGameGraph graph;
//        private GraphEditorView view;
//        private EventBus eventBus;
//
//        private NodeSearchWindowProvider nodeSearchWindowProvider => this.GetUserDataOrCreate(() =>
//        {
//            var provider = ScriptableObject.CreateInstance<NodeSearchWindowProvider>();
//            provider.Initialize(view);
//            return provider;
//        });
//
//        public GraphEditorPresenter(EditorGameGraph graph)
//        {
//            this.graph = graph;
//        }
//
//        public void Attach(GraphEditorView view)
//        {
//            this.view = view;
//            eventBus = view.GetEventBus();
//            eventBus.Register(this);
//        }
//
//        public void Detach(GraphEditorView view)
//        {
//            eventBus.Unregister(this);
//        }
//
//        [UsedImplicitly]
//        public void OnEvent(RequestNodeCreationSearchEvent e)
//        {
//            nodeSearchWindowProvider.callback = (type, position) =>
//                eventBus.Dispatch(new RequestNodeCreationEvent(type, position, null));
//            SearchWindow.Open(new SearchWindowContext(e.screenMousePosition), nodeSearchWindowProvider);
//        }
//
//        [UsedImplicitly]
//        public void OnEvent(RequestNodeCreationEvent e)
//        {
//            var nodeView = new NodeView();
//            nodeView.graph = graph;
//            view.AddElement(nodeView);
//            nodeView.Initialize(e.type, e.position ?? Vector2.zero, e.parameterId);
//            nodeView.PersistState();
//        }
//    }
//}

// TODO Maybe think about a presenter strategy, but Unity gives very little space forcallbacks
