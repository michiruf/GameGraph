using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public class ElementLookup<T> where T : VisualElement
    {
        private readonly WeakReference<VisualElement> root;
        private readonly string name;
        private readonly string className;

        private T elementInternal;
        public T element
        {
            get
            {
                if (elementInternal == null)
                {
                    root.TryGetTarget(out var rootElement);
                    elementInternal = rootElement?.Q<T>(name, className);
                }
                return elementInternal;
            }
        }

        public static implicit operator T(ElementLookup<T> l) => l.element;

        public ElementLookup(VisualElement root, string name = null, string className = null)
        {
            this.root = new WeakReference<VisualElement>(root);
            this.name = name;
            this.className = className;
        }
    }

    public static class ElementLookupExtension
    {
        private static readonly Dictionary<WeakReference<VisualElement>, Dictionary<string, object>> Instances =
            new Dictionary<WeakReference<VisualElement>, Dictionary<string, object>>();

        public static ElementLookup<T> QCached<T>(this VisualElement root, string name = null, string className = null)
            where T : VisualElement
        {
            var rootReference = new WeakReference<VisualElement>(root);

            if (!Instances.ContainsKey(rootReference))
                Instances.Add(rootReference, new Dictionary<string, object>());

            var rootDictionary = Instances[rootReference];
            var id = name + className;
            if (!rootDictionary.ContainsKey(id))
                rootDictionary.Add(id, new ElementLookup<T>(root, name, className));

            return rootDictionary[id] as ElementLookup<T>;
        }
    }
}
