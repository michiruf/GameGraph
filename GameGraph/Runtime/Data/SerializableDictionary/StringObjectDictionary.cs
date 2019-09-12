using System;
using Object = UnityEngine.Object;

namespace GameGraph
{
    [Serializable]
    public class StringObjectDictionary : SerializableDictionary<string, Object>
    {
    }
}
