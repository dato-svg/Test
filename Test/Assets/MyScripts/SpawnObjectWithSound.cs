using UnityEngine;

public class SpawnObjectWithSound : MonoBehaviour
{
    
    [SerializeField] private AudioClip soundClip; 
    [Header("Object to spawn")]
    [SerializeField] private GameObject objectToSpawn;
    
   
    public void SpawnObject()
    {
        if (objectToSpawn == null || soundClip == null)
        {
            return;
        }
        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        
      
        AudioSource audioSource = spawnedObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
        
     
        audioSource.Play();
    }
}
