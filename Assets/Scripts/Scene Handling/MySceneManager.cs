using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject loadingScreen = null;
    [SerializeField] private Slider loadingSlider = null;
    [SerializeField] private float minLoadingTime = 2f;

    private string _sceneName = null;

    private void LoadScene()
    {
        LoadSceneByName(_sceneName);
    }

    public void SetSceneName(string newName)
    {
        _sceneName = newName;
    }

    public void LoadSceneByNameAsync(string name)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(name));
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif

        Application.Quit();
    }

    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }

    private IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        operation.allowSceneActivation = false;

        float elapsedTime = 0f;
        float fakeProgress = 0f;

        while (!operation.isDone)
        {
            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);

            elapsedTime += Time.deltaTime;

            fakeProgress = Mathf.Clamp01(elapsedTime / minLoadingTime);

            loadingSlider.value = Mathf.Min(realProgress, fakeProgress);

            if (realProgress >= 0.9f && elapsedTime >= minLoadingTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        loadingScreen.SetActive(false);
    }
}