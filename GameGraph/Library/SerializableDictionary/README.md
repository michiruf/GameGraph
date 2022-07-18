# SerializeableDictionary Usage

## Usage

All the below examples of a type must be used. So in the end there must be one of each:
* Typed dictionary (StringIntDictionary for example)
* Typed drawer for that specific typed dictionary

### Example implementation of a dictionary type

```csharp
// ---------------
//  String => Int
// ---------------
[Serializable]
public class StringIntDictionary : SerializableDictionary<string, int>
{
}
```

### Example drawer for a given dictionary type

```csharp
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

internal class SerializableStringIntTemplate : SerializableKeyValueTemplate<string, int>
{
}
```

### Example usage in a scriptable object

```csharp
[CreateAssetMenu(menuName = "Example Asset")]
public class Example : ScriptableObject
{
    [SerializeField] private StringIntDictionary stringIntegerStore = new StringIntDictionary();
    private Dictionary<string, int> stringIntegers => stringIntegerStore.dictionary;

    [SerializeField] private GameObjectFloatDictionary gameObjectFloatStore = new GameObjectFloatDictionary();
    private Dictionary<GameObject, float> screenshots => gameObjectFloatStore.dictionary;
}
```

## Example

See example in `../SerializableDictionary.Example` assembly

## Source

Got from: [https://wiki.unity3d.com/index.php/SerializableDictionary]()

Modifications:
* Remove "New" method and add constructor
  -> static public T New<T>() where T : SerializableDictionary<K, V>, new() 
