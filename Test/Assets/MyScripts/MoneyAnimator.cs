using System.Collections;
using UnityEngine;

public class MoneyAnimator : MonoBehaviour
{
    [SerializeField] private Transform[] objectsToMove; 
    [SerializeField] private Transform targetPoint; 
    [SerializeField] private float moveDuration = 0.2f; 

    [SerializeField] private GameObject _mainObject;

    [ContextMenu("StartMovingObjects")]
    public void StartMovingObjects()
    {
        _mainObject.SetActive(true);
        foreach (Transform obj in objectsToMove)
        {
            StartCoroutine(MoveObjectWithAcceleration(obj, targetPoint.position, moveDuration));
        }
    }

    private IEnumerator MoveObjectWithAcceleration(Transform obj, Vector3 target, float duration)
    {
        Vector3 startPosition = obj.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
         
            float t = elapsedTime / duration;

        
            float acceleration = Mathf.Pow(t, 2f);

        
            obj.position = Vector3.Lerp(startPosition, target, acceleration);

       
            elapsedTime += Time.deltaTime * 2f;
            yield return null;
        }

        
        obj.position = target;

        
        _mainObject.SetActive(false);
    }
}