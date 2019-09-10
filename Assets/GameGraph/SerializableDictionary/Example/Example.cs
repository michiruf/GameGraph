using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Example Asset")]
public class Example : ScriptableObject
{
    [SerializeField] private StringIntDictionary stringIntegerStore = new StringIntDictionary();
    private Dictionary<string, int> stringIntegers => stringIntegerStore.dictionary;

    [SerializeField] private GameObjectFloatDictionary gameObjectFloatStore = new GameObjectFloatDictionary();
    private Dictionary<GameObject, float> screenshots => gameObjectFloatStore.dictionary;
}
