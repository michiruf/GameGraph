using System;
using System.Collections.Generic;
using System.Linq;

namespace GameGraph.Editor
{
    // TODO Rename, is not weak anymore
    public class DataHolder
    {
        private readonly Dictionary<string, object> namedData = new Dictionary<string, object>();
        private readonly List<object> unnamedData = new List<object>();

        ~DataHolder()
        {
            namedData.Clear();
            unnamedData.Clear();
        }

        public void AddData<T>(T data, string name = null) where T : class
        {
            if (string.IsNullOrEmpty(name))
            {
                unnamedData.Add(data);
                return;
            }
            namedData.Add(name, data);
        }

        public T GetData<T>(string name = null) where T : class
        {
            if (string.IsNullOrEmpty(name))
                return unnamedData.FirstOrDefault(o => o is T) as T;
            return namedData[name] as T;
        }

        public T GetDataOrCreate<T>(Func<T> creator, string name) where T : class
        {
            if (!HasData<T>(name))
                AddData(creator.Invoke(), name);
            return GetData<T>(name);
        }

        public bool HasData<T>(string name = null) where T : class
        {
            if (string.IsNullOrEmpty(name))
                return unnamedData.FirstOrDefault(o => o is T) != default;
            return namedData.ContainsKey(name);
        }

        public void RemoveData<T>(string name = null) where T : class
        {
            if (string.IsNullOrEmpty(name))
            {
                unnamedData.RemoveAll(o => GetAllData<T>().Contains(o));
                return;
            }
            namedData.Remove(name);
        }

        public List<T> GetAllData<T>() where T : class
        {
            return unnamedData
                .FindAll(o => o is T)
                .Concat(namedData.Select(pair => pair.Value).ToList().FindAll(o => o is T))
                .Select(o => o as T).ToList();
        }
    }
}
