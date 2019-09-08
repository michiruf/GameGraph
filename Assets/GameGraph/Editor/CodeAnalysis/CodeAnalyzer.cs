using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.MemoryProfiler;

namespace GameGraph.Editor
{
    public static class CodeAnalyzer
    {
        public static IEnumerable<Type> GetNodeTypes()
        {
            var gameGraphTypes = Assembly.GetAssembly(typeof(GameGraphBehaviour)).GetTypes();
            return AssetDatabase.GetAllAssetPaths()
                .Where(s => s.StartsWith("Assets/") && s.EndsWith(".cs"))
                .Select(s => AssetDatabase.LoadMainAssetAtPath(s) as MonoScript)
                .Where(script => script != null)
                .Select(script => script.GetClass())
                .Where(type => !gameGraphTypes.Contains(type))
                .Concat(gameGraphTypes)
                .Where(type => type?.GetCustomAttribute<GameGraphAttribute>() != null);
        }

        public static IEnumerable<Type> GetNonNodeTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly =>
                    EditorConstants.AssemblyModulesToIncludeForParameters.Contains(assembly.GetName().Name))
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
                .Select(info => new MemberData<FieldInfo>(info))
                .ToList();
            var properties = type.GetProperties(GameGraphConstants.ReflectionFlags)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new MemberData<PropertyInfo>(info))
                .ToList();
            var propertyMethods = properties.SelectMany(data => new[]
            {
                data.info.GetMethod,
                data.info.SetMethod
            });

            // Get event data
            var events = type.GetEvents(GameGraphConstants.ReflectionFlags)
                .Where(info => info.GetCustomAttribute<ExcludeFromGraphAttribute>() == null)
                .Select(info => new MemberData<EventInfo>(info))
                .ToList();
            var eventMethods = events.SelectMany(data => new[]
            {
                data.info.AddMethod,
                data.info.RemoveMethod
            });

            // Get method data
            var typeMethods = type.GetMethods(GameGraphConstants.ReflectionFlags);
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
