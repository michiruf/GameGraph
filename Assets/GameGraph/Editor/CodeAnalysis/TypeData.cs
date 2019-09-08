using System;
using System.Reflection;

namespace GameGraph.Editor
{
    public struct TypeData
    {
        public readonly Type type;
        public GameGraphAttribute gameGraphAttribute;

        public bool isGameGraphType => gameGraphAttribute != null;

        public TypeData(Type type)
        {
            this.type = type;
            gameGraphAttribute = type.GetCustomAttribute<GameGraphAttribute>();
        }
    }
}
