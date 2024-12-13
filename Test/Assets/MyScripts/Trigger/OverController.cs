using PlayerControl;
using UnityEngine;

public class OverController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Animator>().SetTrigger("Win");
            other.GetComponent<PlayerMovement>().enabled = false;
            _gameManager._player.GetComponent<PlayerMovement>().enabled = false;
            _gameManager.EnablePanel(2);
            
        }
    }
}
