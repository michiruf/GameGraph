using GameGraph;
using UnityEngine;

namespace Presentation
{
    [GameGraph("Presentation")]
    [CreateAssetMenu(fileName = "MovementSettings", menuName = "ScriptableObjects/MovementSettings")]
    public class CharacterControllerMovementSettings : ScriptableObject
    {
        public float movementSpeed = 30f;
        public float gravityMultiplier = 1f;
        public float rotationSmoothness = 1f;
        public float rotationThreshold = 0.05f;
        public bool airborneMovementEnabled;
        public float airborneMovementSmoothness;
    }
}
