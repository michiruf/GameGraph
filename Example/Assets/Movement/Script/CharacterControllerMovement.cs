﻿using GameGraph;
 using GameGraph.Common.Helper;
 using JetBrains.Annotations;
 using UnityEngine;

 namespace Presentation
{
    [GameGraph("Presentation")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class CharacterControllerMovement
    {
        // Configuration
        public CharacterControllerMovementSettings settings;

        // Properties
        public CharacterController characterController { private get; set; }
        public Vector3 direction { private get; set; }
        public float deltaTime { private get; set; }
        public bool grounded { private get; set; }

        public void Move()
        {
            // Normalize the movement if its too big
            if (direction.magnitude > 1f)
                direction.Normalize();

            // Apply the speed factor
            direction *= settings.movementSpeed;

            // While the player is airborne, have less steering
            if (settings.airborneMovementEnabled && !grounded)
                direction *= LerpUtils.SmoothnessToLerp(settings.airborneMovementSmoothness);

            // Calculate gravity
            var gravityVelocity = grounded
                ? Vector3.zero
                : new Vector3(0f, characterController.velocity.y + Physics.gravity.y * settings.gravityMultiplier * deltaTime, 0f);
            characterController.Move((direction + gravityVelocity) * deltaTime);

            // Rotate towards the movement direction
            var rotationVelocity = direction;
            rotationVelocity.y = 0f;
            if (rotationVelocity.magnitude > settings.rotationThreshold)
            {
                var rotation = Quaternion.Lerp(
                    characterController.transform.rotation,
                    Quaternion.LookRotation(rotationVelocity),
                    LerpUtils.SmoothnessToLerp(settings.rotationSmoothness));
                characterController.transform.localRotation = rotation;
            }
        }
    }
}
