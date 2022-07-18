using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    public static class VisualElementUserDataHolderExtension
    {
        public static DataHolder GetUserDataHolder(this VisualElement el)
        {
            el.userData ??= new DataHolder();
            return (DataHolder)el.userData;
        }

        public static void AddUserData<T>(this VisualElement el, T data, string name = null) where T : class
        {
            el.GetUserDataHolder().AddData(data, name);
        }

        public static T GetUserData<T>(this VisualElement el, string name = null) where T : class
        {
            return el.GetUserDataHolder().GetData<T>(name);
        }

        public static T GetUserDataOrCreate<T>(this VisualElement el, Func<T> creator, string name = null) where T : class
        {
            return el.GetUserDataHolder().GetDataOrCreate(creator, name);
        }

        public static bool HasUserData<T>(this VisualElement el, string name = null) where T : class
        {
            return el.GetUserDataHolder().HasData<T>(name);
        }

        public static void RemoveUserData<T>(this VisualElement el, string name = null) where T : class
        {
            el.GetUserDataHolder().RemoveData<T>(name);
        }

        public static List<T> GetAllUserData<T>(this VisualElement el) where T : class
        {
            return el.GetUserDataHolder().GetAllData<T>();
        }
    }
}
