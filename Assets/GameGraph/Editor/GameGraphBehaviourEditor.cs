using UnityEditor;

namespace GameGraph.Editor
{
    [CustomEditor(typeof(GameGraphBehaviour))]
    public class GameGraphBehaviourEditor : UnityEditor.Editor
    {
        // TODO Message to Eric
        // Ich füge im Graph (neben Knoten und Kanten) auch noch Parameter hinzu.
        // Die werden dann auch abgespeichert undso.
        // Und bei dem Behaviour mit dem ich den Graph lade, da füge ich zu den bereits benannten Parametern noch die Werte hinzu.
        // Dann lass ichs laufen und braucht kein separates Script um Sachen zu providen.
        
        // @see https://unity3d.com/de/learn/tutorials/topics/interface-essentials/adding-buttons-custom-inspector
    }
}
