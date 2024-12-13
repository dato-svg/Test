using UnityEngine;

namespace CameraControl
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Quaternion rotation;
        [SerializeField] private float followSpeed = 10f;

        private void LateUpdate()
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedTime);
            
            transform.rotation = rotation;
        }
    }
}