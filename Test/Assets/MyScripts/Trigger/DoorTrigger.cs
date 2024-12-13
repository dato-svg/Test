using UnityEngine;

namespace Trigger
{
    public class DoorTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<PlayerVisual>().GoodVisualChanger();
        }
    }
}
