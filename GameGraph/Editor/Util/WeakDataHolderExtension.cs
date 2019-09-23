using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    // TODO https://docs.unity3d.com/2018.3/Documentation/ScriptReference/Experimental.UIElements.VisualElement-userData.html
    //      Could make this completely obsolete
    [Obsolete("Use VisualElement.userData")]
    public static class WeakDataHolderExtension
    {
        private static readonly ConditionalWeakTable<object, DataHolder> Instances =
            new ConditionalWeakTable<object, DataHolder>();

        public static DataHolder GetUserDataHolder(this object o)
        {
            // TODO Instead of handling this myself, every VisualElement has a field "userData" 
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
