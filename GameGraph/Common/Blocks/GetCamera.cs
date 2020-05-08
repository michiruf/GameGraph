using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GetCamera
    {
        public Camera mainCamera => Camera.main;
        public Camera currentCamera => Camera.current;
    }
}
