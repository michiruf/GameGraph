using System;
using GameGraph.Annotation;
using Property = GameGraph.Annotation.PropertyAttribute;

[GameGraph]
public class Update
{
    [Trigger] //
    public Action update;
}
