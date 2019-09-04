using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GameGraph.Editor
{
    // TODO Add cache
    // TODO Maybe enhance with nested exclude and includes
    // ... @see https://stackoverflow.com/questions/540749/can-a-c-sharp-class-inherit-attributes-from-its-interface
    public static class CodeAnalyzer
    {
        public static Type GetTypeFromAllAssemblies(string name)
        {
            // TODO Not every class is found here. E.g. "If"-Block
            // TODO Maybe us the assembly name to lookup the type via Type.GetType()?
            return AppDomain.CurrentDomain.GetAssemblies()
                .Select(assembly => assembly.GetType(name, false))
                .FirstOrDefault(type => type != null);
        }

        public static string[] GetGameGraphComponents()
        {
            // NOTE Eliminate the string tuple value
            var classes = AssetDatabase.GetAllAssetPaths()
                .Where(s => s.StartsWith("Assets/") && s.EndsWith(".cs"))
                .Select(s => Tuple.Create(s, AssetDatabase.LoadMainAssetAtPath(s)))
                .Select(tuple => Tuple.Create(tuple.Item1, tuple.Item2.name))
                .Select(tuple => Tuple.Create(tuple.Item1, GetTypeFromAllAssemblies(tuple.Item2)))
                .Where(tuple => tuple.Item2 != null)
                .Where(o => o.Item2.GetCustomAttribute<GameGraphAttribute>() != null)
                .ToList();

            Debug.Log("Found " + classes.Count + " GameGraphComponents");
            return classes.Select(tuple => tuple.Item2.Name).ToArray();
        }

        public static ComponentData GetComponentData(string name)
        {
            return GetComponentData(GetTypeFromAllAssemblies(name));
        }

        public static ComponentData GetComponentData(Type type)
        {
            // Get field and property data
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new PropertyData(info))
                .ToList();
            //var realProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            //    .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
            //    .Select(info => new MemberData(info.Name, info.PropertyType, info))
            //    .ToList();
            // TODO Enhance with properties: .Concat(type.GetProperties(BindingFlags.Public | BindingFlags.Instance));

            // Get event data
            var events = type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new EventData(info))
                .ToList();
            var eventMethods = events.SelectMany(data => new[]
            {
                data.info.AddMethod,
                data.info.RemoveMethod
            });

            // Get method data
            var typeMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var methods = typeMethods
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Where(info => !eventMethods.Contains(info))
                .Select(info => new MethodData(info))
                .ToList();

            return new ComponentData(fields, events, methods);
        }
    }
}
