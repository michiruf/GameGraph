using UnityEngine;

namespace Presentation
{
public class MovementBehaviour : MonoBehaviour
{
    public PlayerState playerState;

    void Update()
    {
        if (!playerState.canMove)
            return;
        // Or if stunned
        // Or pause menu is open
        // Or the controller disconnects
        // When the game is loading
        // If the game is finished
        // While a jetpack is turned on
        // While swimming
        // ...

        var inputDirection = GetInputDirection();
        var movementDirection = CameraMovementProjection(inputDirection);
        Move(movementDirection);
    }

    private Vector2 GetInputDirection()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private Vector3 CameraMovementProjection(Vector2 direction)
    {
        // ...
        return Vector3.zero;
    }

    private void Move(Vector3 direction)
    {
        // ...
    }
}
}
