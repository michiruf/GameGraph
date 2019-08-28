using GameGraph.Annotation;
using Property = GameGraph.Annotation.PropertyAttribute;

[GameGraph]
public class Rotation
{
    [GameGraph.Annotation.Property] //
    public float rotationSpeed = 1.338f;
    
    [Method] //
    public void Rotate()
    {
        // Do it
    }
}
