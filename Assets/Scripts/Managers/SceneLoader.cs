using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [Header("UI")]
    public CanvasGroup loadingCanvas;
    public Slider progressBar;

    void Awake()
    {
        Debug.Log($"SceneLoader alive in scene: {SceneManager.GetActiveScene().name}");

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName, GameObject canvasToHide)
    {
        StartCoroutine(LoadSceneAsync(sceneName, canvasToHide)); 
    }

    private IEnumerator LoadSceneAsync(string sceneName, GameObject canvasToHide)
    {
        // Smooth fade in & out
        progressBar.value = 0;

        float fadeDuration = 0.5f;
        float fadeTime = 0f;

        float startAlpha = loadingCanvas.alpha;

        while (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            float normalizedTime = fadeTime / fadeDuration;
            loadingCanvas.alpha = Mathf.Lerp(startAlpha, 1f, normalizedTime);
            yield return null;
        }

        loadingCanvas.alpha = 1;
        loadingCanvas.blocksRaycasts = true;
        progressBar.value = 0;
        canvasToHide?.SetActive(false);

        yield return new WaitForSeconds(0.3f);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            progressBar.value = op.progress;
            yield return null;
        }

        
        while (progressBar.value < 1f)
        {
            progressBar.value += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        op.allowSceneActivation = true;
       
        fadeTime = 0f;
        startAlpha = loadingCanvas.alpha;

        while (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            float normalizedTime = fadeTime / fadeDuration;
            loadingCanvas.alpha = Mathf.Lerp(startAlpha, 0f, normalizedTime);
            yield return null;
        }

        loadingCanvas.alpha = 0f;
        loadingCanvas.blocksRaycasts = false;
    }
}
