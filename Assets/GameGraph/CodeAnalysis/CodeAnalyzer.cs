using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameGraph.Annotation;
using UnityEditor;
using UnityEngine;
using PropertyAttribute = UnityEngine.PropertyAttribute;

namespace GameGraph.CodeAnalysis
{
    public static class CodeAnalyzer
    {
        // TODO Add cache

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
            var type = GetTypeFromAllAssemblies(name);

            // Get field depending data
            var typeFields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var properties = typeFields
                .Where(info => info.GetCustomAttribute<PropertyAttribute>() != null)
                .Select(info => new FieldData(info.Name, info.FieldType))
                .ToList();
            var triggers = typeFields
                .Where(info => info.GetCustomAttribute<TriggerAttribute>() != null)
                .Select(info => new FieldData(info.Name, info.FieldType))
                .ToList();

            // Get method depending data
            var typeMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var methods = typeMethods
                .Where(info => info.GetCustomAttribute<MethodAttribute>() != null)
                .Select(info => new MethodData(info.Name, info.ReturnType, info.GetParameters()))
                .ToList();

            return new ComponentData(properties, triggers, methods);
        }

        private static Type GetTypeFromAllAssemblies(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Select(assembly => assembly.GetType(name, false))
                .FirstOrDefault(type => type != null);
        }
    }
}
