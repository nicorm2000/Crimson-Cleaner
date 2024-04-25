using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private string _sceneName = null;

    private void LoadScene()
    {
        LoadSceneByName(_sceneName);
    }

    public void SetSceneName(string newName)
    {
        _sceneName = newName;
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
}