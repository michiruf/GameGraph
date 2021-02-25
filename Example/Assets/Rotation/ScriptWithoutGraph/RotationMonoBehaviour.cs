using UnityEngine;

namespace Presentation
{
    public class RotationMonoBehaviour : MonoBehaviour
    {
        public new GameObject gameObject;
        public float rotationSpeed;

        void Update()
        {
            gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
