using UnityEngine;

public class ObjectRotator : MonoBehaviour
{

    [SerializeField] private Animator _player;
    
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private int totalRotations = 1;

    private bool isRotating = false;
    private float remainingDegrees;
    private void Start()
    {
        remainingDegrees = totalRotations * 360f;
    }
    
    private void Update()
    {
      
        if (isRotating && remainingDegrees > 0)
        {
            RotateObject();
        }
    }
    
    [ContextMenu("StartRotation")]
    public void StartRotation()
    {
        _player.GetComponent<Animator>().enabled = false;
        remainingDegrees = totalRotations * 360f;
        isRotating = true;
    }
    
    private void RotateObject()
    {
        float degreesToRotate = rotationSpeed * Time.deltaTime;

        if (degreesToRotate > remainingDegrees)
        {
            degreesToRotate = remainingDegrees;
        }
        
        transform.Rotate(0f, degreesToRotate, 0f);
        
        remainingDegrees -= degreesToRotate;

        if (remainingDegrees <= 0)
        {
            isRotating = false;
            _player.GetComponent<Animator>().enabled = true;
        }
    }
}