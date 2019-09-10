using UnityEditor;
using UnityEngine;

// ---------------
//  String => Int
// ---------------
[CustomPropertyDrawer(typeof(StringIntDictionary))]
public class StringIntDictionaryDrawer : SerializableDictionaryDrawer<string, int>
{
    protected override SerializableKeyValueTemplate<string, int> GetTemplate()
    {
        return GetGenericTemplate<SerializableStringIntTemplate>();
    }
}

// Maybe inline this class?
internal class SerializableStringIntTemplate : SerializableKeyValueTemplate<string, int>
{
}

// ---------------
//  GameObject => Float
// ---------------
[CustomPropertyDrawer(typeof(GameObjectFloatDictionary))]
public class GameObjectFloatDictionaryDrawer : SerializableDictionaryDrawer<GameObject, float>
{
    protected override SerializableKeyValueTemplate<GameObject, float> GetTemplate()
    {
        return GetGenericTemplate<SerializableGameObjectFloatTemplate>();
    }
}

// Maybe inline this class?
internal class SerializableGameObjectFloatTemplate : SerializableKeyValueTemplate<GameObject, float>
{
}
