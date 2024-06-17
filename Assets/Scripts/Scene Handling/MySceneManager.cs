using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviourSingleton<MySceneManager>
{
    [Header("Config")]
    [SerializeField] private GameObject loadingScreen = null;
    [SerializeField] private Slider loadingSlider = null;
    
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

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }

        loadingScreen.SetActive(false);
    }
}