using Trigger;
using UnityEngine;

namespace PlayerControl
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] private float lateralSpeed = 2f;
        [SerializeField] private float tiltAngle = 15f;
        [SerializeField] private float rotationSmoothness = 10f;

        [SerializeField] private Transform _childTransform;

        private IPlayerInput _playerInput;
        private Rigidbody _rb;
        private float _currentRotation = 0f;

        private Vector3 _currentDirection = Vector3.forward; 
        private Quaternion _targetRotation; 

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

#if UNITY_EDITOR
            _playerInput = gameObject.AddComponent<KeyboardInputService>();
#else
            _playerInput = gameObject.AddComponent<TouchInputService>();
#endif
            _targetRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 forwardMovement = _currentDirection * forwardSpeed * Time.fixedDeltaTime;

            float horizontalInput = _playerInput.GetHorizontalInput();
            Vector3 lateralMovement = Vector3.right * horizontalInput * lateralSpeed * Time.fixedDeltaTime;

            Vector3 newPosition = _rb.position + forwardMovement + lateralMovement;
            _rb.MovePosition(newPosition);

            float targetRotation = horizontalInput * tiltAngle;
           _currentRotation = Mathf.LerpAngle(_currentRotation, targetRotation, rotationSmoothness * Time.fixedDeltaTime);
           
           Quaternion tiltQuaternion = Quaternion.Euler(0, _currentRotation, 0);
            _childTransform.rotation = Quaternion.Slerp(_childTransform.rotation, tiltQuaternion, rotationSmoothness * Time.fixedDeltaTime);
        }


        

        private void ChangeDirection(Vector3 newDirection)
        {
            _currentDirection = newDirection.normalized;

           
            if (_currentDirection != Vector3.zero)
            {
                _targetRotation = Quaternion.LookRotation(_currentDirection);
            }
        }
    }
}
