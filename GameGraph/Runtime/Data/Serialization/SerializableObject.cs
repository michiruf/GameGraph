using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace GameGraph
{
    [Serializable]
    public class SerializableObject : ISerializationCallbackReceiver
    {
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Converters = new List<JsonConverter> {
                new StringEnumConverter()
            }
        };
        
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
            return @object == ((SerializableObject)obj).@object;
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
            objectInternal = JsonConvert.SerializeObject(@object, settings);
        }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrEmpty(objectInternal))
                return;

            @object = JsonConvert.DeserializeObject(objectInternal, type, settings);
        }
    }
}
