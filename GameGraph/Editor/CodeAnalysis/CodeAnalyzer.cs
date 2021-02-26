using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameGraph.Editor
{
    public static class CodeAnalyzer
    {
        private static IEnumerable<Type> GetAllExternalAssemblyTypes()
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();
            if (EditorConstants.AssemblyModulesFilterEnabled)
                assemblies = assemblies.Where(assembly =>
                {
                    var name = assembly.GetName().Name;
                    return EditorConstants.AssemblyModules.Contains(name) ||
                           EditorConstants.AssemblyModulesStartWith.Aggregate(false, (b, s) =>
                               b || name.StartsWith(s));
                });
            return assemblies
                .SelectMany(assembly => assembly.GetTypes());
        }

        public static IEnumerable<TypeData> GetNodeTypes()
        {
            // For any reason GetAllExternalAssemblyTypes() does not return assets without assembly definition
            return AssetDatabase.GetAllAssetPaths()
                .Where(s => s.StartsWith("Assets/") && s.EndsWith(".cs"))
                .Select(s => AssetDatabase.LoadMainAssetAtPath(s) as MonoScript)
                .Where(script => script != null)
                .Select(script => script.GetClass())
                .Union(GetAllExternalAssemblyTypes())
                .Where(type => type?.GetCustomAttribute<GameGraphAttribute>() != null)
                .Select(type => new TypeData(type));
        }

        public static IEnumerable<TypeData> GetNonNodeTypes()
        {
            return GetAllExternalAssemblyTypes()
                // Since these will be persisted in Inspector, we only need UnityEngine.Object
                .Where(type => typeof(Object).IsAssignableFrom(type))
                // GameGraph elements are handled in method above
                .Where(type => type.GetCustomAttribute<GameGraphAttribute>() == null)
                .Select(type => new TypeData(type));
        }

        public static ClassData GetNodeData(Type type)
        {
            // NOTE Maybe enhance with nested excludes and includes
            //      https://stackoverflow.com/questions/540749/can-a-c-sharp-class-inherit-attributes-from-its-interface

            // Get field and property data
            var fields = type.GetFields(Constants.ReflectionFlags)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new MemberData<FieldInfo>(info))
                .ToList();
            var properties = type.GetProperties(Constants.ReflectionFlags)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new MemberData<PropertyInfo>(info))
                .ToList();
            var propertyMethods = properties.SelectMany(data => new[]
            {
                data.info.GetMethod,
                data.info.SetMethod
            });

            // Get event data
            var events = type.GetEvents(Constants.ReflectionFlags)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new MemberData<EventInfo>(info))
                .ToList();
            var eventMethods = events.SelectMany(data => new[]
            {
                data.info.AddMethod,
                data.info.RemoveMethod
            });

            // Get method data
            var typeMethods = type.GetMethods(Constants.ReflectionFlags);
            var methods = typeMethods
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                // Respect only void returning methods with nor arguments
                .Where(info => info.ReturnType == typeof(void) && info.GetParameters().Length == 0)
                // Remove property and event methods
                .Where(info => !propertyMethods.Contains(info) && !eventMethods.Contains(info))
                .Select(info => new MethodData(info))
                .ToList();

            return new ClassData(new TypeData(type), fields, properties, events, methods);
        }
    }
}
