using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace GameGraph.Editor
{
    // TODO Maybe enhance with nested exclude and includes
    // TODO ... @see https://stackoverflow.com/questions/540749/can-a-c-sharp-class-inherit-attributes-from-its-interface
    public static class CodeAnalyzer
    {
        public static IEnumerable<TypeData> GetBlockTypes()
        {
            // NOTE Query via AssetDatabase.FindAssets() to have more performance? 
            // E.g. AssetDatabase.FindAssets("t:MonoScript", new []{"Assets"});
            return AssetDatabase.GetAllAssetPaths()
                .Where(s => s.StartsWith("Assets/") && s.EndsWith(".cs"))
                .Select(s => AssetDatabase.LoadMainAssetAtPath(s) as MonoScript)
                .Where(script => script != null)
                .Select(script => script.GetClass())
                .Where(type => type?.GetCustomAttribute<GameGraphAttribute>() != null)
                .Select(type => new TypeData(type));
        }

        public static BlockData GetBlockData(string assemblyQualifiedName)
        {
            return GetBlockData(Type.GetType(assemblyQualifiedName));
        }

        public static BlockData GetBlockData(Type type)
        {
            // Get field and property data
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new MemberData<FieldInfo>(info))
                .ToList();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new MemberData<PropertyInfo>(info))
                .ToList();

            // Get event data
            var events = type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new MemberData<EventInfo>(info))
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
                .Select(info => new MemberData<MethodInfo>(info))
                .ToList();

            return new BlockData(new TypeData(type), fields, properties, events, methods);
        }

        private static Type GetTypeFromAllAssemblies(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Select(assembly => assembly.GetType(name, false))
                .FirstOrDefault(type => type != null);
        }
    }
}
