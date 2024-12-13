using UnityEngine;

namespace PlayerControl
{
    public class TouchInputService : MonoBehaviour, IPlayerInput
    {
        private float _screenWidth;
        
        
        private void Awake()
        {
            _screenWidth = Screen.width;
        }
        
        
        public float GetHorizontalInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    return touch.position.x < _screenWidth / 2 ? -1f : 1f;
                }
            }

            return 0f; 
        }
        
    }
}
