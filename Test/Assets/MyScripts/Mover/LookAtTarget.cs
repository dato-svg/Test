using CameraControl;
using UnityEngine;

namespace Mover
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;


        private void Start()
        {
            _target = GameObject.Find("Main Camera").transform;
        }
        
        
        private void Update()
        {
            if (_target != null)
            {
              
                transform.LookAt(_target);
            }
        }
    }
}
