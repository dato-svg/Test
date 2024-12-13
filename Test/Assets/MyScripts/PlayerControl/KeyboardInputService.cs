using UnityEngine;

namespace PlayerControl
{
    public class KeyboardInputService : MonoBehaviour, IPlayerInput 
    {
        public float GetHorizontalInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }
    }
}
