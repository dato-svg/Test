using UnityEngine;

namespace Trigger
{
    public class BadDoorTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<PlayerVisual>().BadVisualChanger();
        }
    }
}
