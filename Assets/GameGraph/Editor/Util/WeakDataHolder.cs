using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GameGraph.Editor
{
    public class WeakDataHolder
    {
        private readonly Dictionary<string, object> namedData = new Dictionary<string, object>();
        private readonly List<object> unnamedData = new List<object>();

        ~WeakDataHolder()
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

        public List<T> GetAllData<T>() where T : class
        {
            return unnamedData
                .FindAll(o => o is T)
                .Concat(namedData.Select(pair => pair.Value).ToList().FindAll(o => o is T))
                .Select(o => o as T).ToList();
        }

        public bool HasData<T>(string name = null) where T : class
        {
            if (string.IsNullOrEmpty(name))
                return unnamedData.FirstOrDefault(o => o is T) != default;
            return namedData.ContainsKey(name);
        }
    }

    public static class WeakDataHolderExtension
    {
        private static readonly ConditionalWeakTable<object, WeakDataHolder> Instances;

        public static WeakDataHolder GetUserDataHolder(this object o)
        {
            return Instances.GetOrCreateValue(o);
        }

        public static void AddUserData<T>(this object o, T data, string name = null) where T : class
        {
            var instance = GetUserDataHolder(o);
            instance.AddData(data, name);
        }

        public static T GetUserData<T>(this object o, string name = null) where T : class
        {
            var instance = GetUserDataHolder(o);
            return instance.GetData<T>(name);
        }

        public static List<T> GetAllUserData<T>(this object o) where T : class
        {
            var instance = GetUserDataHolder(o);
            return instance.GetAllData<T>();
        }

        public static bool HasUserData<T>(this object o, string name = null) where T : class
        {
            var instance = GetUserDataHolder(o);
            return instance.HasData<T>(name);
        }
    }
}
