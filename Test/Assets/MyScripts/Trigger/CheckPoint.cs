using UnityEngine;

namespace Trigger
{
    public class CheckPoint : MonoBehaviour
    {
        private Animator _animator;
        private SpawnObjectWithSound _spawnSound;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spawnSound = GetComponent<SpawnObjectWithSound>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() != null)
            {
                _animator.SetTrigger("Active");
                if (_spawnSound != null)
                {
                    _spawnSound.SpawnObject();
                }
               
            }
        }
    }
}
