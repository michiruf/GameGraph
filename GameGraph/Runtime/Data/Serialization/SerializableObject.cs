using System;
using System.Reflection;
using DeJson;
using JetBrains.Annotations;
using UnityEngine;
using Object = System.Object;

namespace GameGraph
{
    [Serializable]
    public class SerializableObject : ISerializationCallbackReceiver
    {
        private static readonly Deserializer Deserializer = new Deserializer();

        [SerializeField] private SerializableType type;
        [SerializeField] private string objectInternal;

        public object @object { get; private set; }

        public SerializableObject(object @object)
        {
            this.@object = @object;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return @object == ((SerializableObject) obj).@object;
        }

        public override int GetHashCode()
        {
            if (@object == null)
                return -1;

            return @object.GetHashCode();
        }

        public void OnBeforeSerialize()
        {
            if (@object == null)
                return;

            type = @object.GetType();
            objectInternal = Serializer.Serialize(@object);
        }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrEmpty(objectInternal))
                return;

            // Access via reflection to build the generic type
            var method = GetType()
                .GetMethod("DeserializeWrapper", BindingFlags.Instance | BindingFlags.NonPublic)?
                .MakeGenericMethod(type.type);
            @object = method?.Invoke(this, new object[] {objectInternal});
        }

        [UsedImplicitly]
        internal T DeserializeWrapper<T>(string json)
        {
            return Deserializer.Deserialize<T>(json);
        }
    }
}
