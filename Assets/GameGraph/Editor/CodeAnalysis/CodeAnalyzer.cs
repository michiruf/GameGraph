using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace GameGraph.Editor
{
    public static class CodeAnalyzer
    {
        public static IEnumerable<Type> GetNodeTypes()
        {
            // NOTE Query via AssetDatabase.FindAssets() to have more performance? 
            //      E.g. AssetDatabase.FindAssets("t:MonoScript", new []{"Assets"});
            return AssetDatabase.GetAllAssetPaths()
                .Where(s => s.StartsWith("Assets/") && s.EndsWith(".cs"))
                .Select(s => AssetDatabase.LoadMainAssetAtPath(s) as MonoScript)
                .Where(script => script != null)
                .Select(script => script.GetClass())
                .Where(type => type?.GetCustomAttribute<GameGraphAttribute>() != null);
        }

        public static IEnumerable<Type> GetNonNodeTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type != null && type.GetCustomAttribute<GameGraphAttribute>() == null);
        }

        public static ClassData GetNodeData(Type type)
        {
            // NOTE Maybe enhance with nested exclude and includes
            //      @see https://stackoverflow.com/questions/540749/can-a-c-sharp-class-inherit-attributes-from-its-interface

            // Get field and property data
            var fields = type.GetFields(GameGraphConstants.ReflectionFlags)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .ToList();
            var properties = type.GetProperties(GameGraphConstants.ReflectionFlags)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .ToList();
            var propertyMethods = properties.SelectMany(info => new[]
            {
                info.GetMethod,
                info.SetMethod
            });

            // Get event data
            var events = type.GetEvents(GameGraphConstants.ReflectionFlags)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .ToList();
            var eventMethods = events.SelectMany(info => new[]
            {
                info.AddMethod,
                info.RemoveMethod
            });

            // Get method data
            var typeMethods = type.GetMethods(GameGraphConstants.ReflectionFlags);
            var methods = typeMethods
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                // Respect only void returning methods with nor arguments
                .Where(info => info.ReturnType == typeof(void) && info.GetParameters().Length == 0)
                // Remove property and event methods
                .Where(info => !propertyMethods.Contains(info) && !eventMethods.Contains(info))
                .ToList();

            return new ClassData(type, fields, properties, events, methods);
        }
    }
}
