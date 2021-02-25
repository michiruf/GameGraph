using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace Presentation
{
    [GameGraph("Presentation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PlayerState : MonoBehaviour
    {
        public bool canMove;
    }
}
