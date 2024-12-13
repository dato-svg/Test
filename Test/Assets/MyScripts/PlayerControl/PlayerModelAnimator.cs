using UnityEngine;

namespace PlayerControl
{
    public class PlayerModelAnimator : MonoBehaviour
    {
        [SerializeField] private Transform model;
        [SerializeField] private float tiltSpeed = 2f;
        [SerializeField] private float maxTiltAngle = 15f;
        
        private float _currentTiltAngle = 0f;
        
        
        public void UpdateTilt(float direction)
        {
            float targetTilt = direction * maxTiltAngle;
            _currentTiltAngle = Mathf.Lerp(_currentTiltAngle, targetTilt, tiltSpeed * Time.deltaTime);

            if (model != null)
            {
                model.localRotation = Quaternion.Euler(0, 0, -_currentTiltAngle);
            }
        }
        
        public void ResetTilt()
        {
            UpdateTilt(0);
        }
        
    }
}
