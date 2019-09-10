using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GameGraph.Editor
{
    // TODO Rename, is not weak anymore
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

    public static class WeakDataHolderExtension
    {
        private static readonly ConditionalWeakTable<object, WeakDataHolder> Instances =
            new ConditionalWeakTable<object, WeakDataHolder>();

        public static WeakDataHolder GetUserDataHolder(this object o)
        {
            return Instances.GetOrCreateValue(o);
        }

        public static void AddUserData<T>(this object o, T data, string name = null) where T : class
        {
            o.GetUserDataHolder().AddData(data, name);
        }

        public static T GetUserData<T>(this object o, string name = null) where T : class
        {
            return o.GetUserDataHolder().GetData<T>(name);
        }

        public static T GetUserDataOrCreate<T>(this object o, Func<T> creator, string name = null) where T : class
        {
            return o.GetUserDataHolder().GetDataOrCreate(creator, name);
        }

        public static bool HasUserData<T>(this object o, string name = null) where T : class
        {
            return o.GetUserDataHolder().HasData<T>(name);
        }

        public static void RemoveUserData<T>(this object o, string name = null) where T : class
        {
            o.GetUserDataHolder().RemoveData<T>(name);
        }

        public static List<T> GetAllUserData<T>(this object o) where T : class
        {
            return o.GetUserDataHolder().GetAllData<T>();
        }
    }
}
