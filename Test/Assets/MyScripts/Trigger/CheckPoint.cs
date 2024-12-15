using UnityEngine;

namespace Trigger
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private bool _door;
        
        private Animator _animator;
        private SpawnObjectWithSound _spawnSound;
        
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spawnSound = GetComponent<SpawnObjectWithSound>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerUI>() != null)
            {
                if (_door)
                {
                    other.GetComponent<PlayerUI>().Multiplier++;
                }
                _animator.SetTrigger("Active");
                if (_spawnSound != null)
                {
                    _spawnSound.SpawnObject();
                }
               
            }
        }
    }
}
