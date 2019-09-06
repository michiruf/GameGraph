using UnityEditor.Experimental.GraphView;

namespace GameGraph.Editor
{
    public class ParameterView : BlackboardField, IGraphElement
    {
        public EditorGameGraph graph { private get; set; }
        private EditorParameter parameter;

        public void Initialize(EditorParameter parameter)
        {
            this.parameter = parameter;
            Initialize();
        }

        public void Initialize(TypeData typeData)
        {
            Initialize(new EditorParameter(typeData));
        }

        private void Initialize()
        {
            text = parameter.typeName;
            // TODO Fill this with more stuff
        }

        public void PersistState()
        {
            parameter.name = text;

            if (!graph.parameters.Contains(parameter))
                graph.parameters.Add(parameter);
        }

        public void RemoveState()
        {
            graph.parameters.Remove(parameter);
        }
    }
}
