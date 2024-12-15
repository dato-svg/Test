using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI progressText;

    private AsyncOperation asyncOperation;

    public void NextSceneActive()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        continueButton.gameObject.SetActive(false);
        Debug.Log("ACTIVE NEXT SCENE LOADDD");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        asyncOperation = SceneManager.LoadSceneAsync(nextSceneIndex);
        asyncOperation.allowSceneActivation = false;

        float loadProgress = 0f;
        float smoothProgress = 0f;

        while (!asyncOperation.isDone)
        {
            loadProgress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            smoothProgress = Mathf.Lerp(smoothProgress, loadProgress, Time.deltaTime * 2f);

            progressBarImage.fillAmount = smoothProgress;
            progressText.text = (smoothProgress * 100f).ToString("F0", CultureInfo.InvariantCulture);

          
            if (smoothProgress >= 0.3f && smoothProgress < 0.31f)
            {
                yield return new WaitForSeconds(0.2f);
            }
            else if (smoothProgress >= 0.75f && smoothProgress < 0.76f)
            {
                yield return new WaitForSeconds(0.1f);
            }

         
            if (smoothProgress >= 0.99f)
            {
                continueButton.gameObject.SetActive(true);
            }

            yield return null;
        }

        Debug.Log("Finish");
    }

    public void ButtonClickActive()
    {
        asyncOperation.allowSceneActivation = true;
    }
}
