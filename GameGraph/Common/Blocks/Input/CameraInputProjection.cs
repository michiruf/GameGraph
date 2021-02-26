using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Input")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class CameraInputProjection
    {
        // Output
        public UnityEngine.Vector3 output => CalculateMovementByCamera(input);

        // Properties
        public UnityEngine.Vector2 input { private get; set; }
        public Camera camera { private get; set; }

        private UnityEngine.Vector3 CalculateMovementByCamera(UnityEngine.Vector2 value)
        {
            // Calculate movement by given camera if available
            if (camera != null)
                return CalculateMovementByCamera(value, camera);

            // Else, use world coordinates
            return value.x * UnityEngine.Vector3.right + value.y * UnityEngine.Vector3.forward;
        }

        private UnityEngine.Vector3 CalculateMovementByCamera(UnityEngine.Vector2 value, Camera camera)
        {
            // Standard-Assets approach:
            // NOTE This will -not- work when the camera's x angle is exactly 90 degrees!
            //var cameraForward = Camera.main.transform.forward.SetY(0).normalized;
            //var cameraRight = Camera.main.transform.right.SetZ(0).normalized;
            //movement = moveVertical * cameraForward + moveHorizontal * cameraRight;

            // My approach:
            var yAngle = camera.transform.eulerAngles.y;
            var sin = Mathf.Sin(yAngle * Mathf.Deg2Rad);
            var cos = Mathf.Cos(yAngle * Mathf.Deg2Rad);
            return new UnityEngine.Vector3(
                cos * value.x + sin * value.y,
                0f,
                -sin * value.x + cos * value.y
            );
        }
    }
}
