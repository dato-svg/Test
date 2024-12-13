using UnityEngine;

namespace Trigger
{
    public enum MoneyChangeType
    {
        Add,
        Subtract
    }
    
    public class MoneyChanger : MonoBehaviour , IMoneyChanger
    {
        [SerializeField] private int amount = 100;
        [SerializeField] private MoneyChangeType changeType;
        [SerializeField] private bool randomCount;

        private SpawnObjectWithSound _spawnSount;

        private void Start()
        {
            _spawnSount = GetComponent<SpawnObjectWithSound>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                ChangeMoney(player,other);
                if (_spawnSount != null)
                {
                    _spawnSount.SpawnObject();
                }
                
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            
            }
        }
        
        public void ChangeMoney(Player player, Collider collider)
        {
            if (randomCount)
            {
                amount = Random.Range(1, 4);
            }
            if (changeType == MoneyChangeType.Add)
            {
                collider.GetComponent<MoneyPopup>().AddMoney(amount);
            }
            else if (changeType == MoneyChangeType.Subtract)
            {
                collider.GetComponent<MoneySubtract>().RemoveMoney(amount);
            }
        }
        
        
    }
    
    
}
