using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace Presentation
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GetRandomPosition
    {
        public Vector3 from;
        public Vector3 to;

        public Vector3 randomPosition => new Vector3(
            Random.Range(@from.x, to.x),
            Random.Range(@from.y, to.y),
            Random.Range(@from.z, to.z)
        );
    }
}
