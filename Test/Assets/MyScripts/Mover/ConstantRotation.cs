using UnityEngine;

namespace Mover
{
    public class ConstantRotation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 50f;
        [SerializeField] private bool rotateRight = true;
        
        private void Update()
        {
            float rotationDirection = rotateRight ? 1f : -1f;
            
            transform.Rotate(0f, rotationSpeed * rotationDirection * Time.deltaTime, 0f);
        }
    }
}
