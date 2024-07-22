using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class SceneLoader : MonoBehaviour
{
    public bool Unload;

    private void Awake()
    {
        LoadScene("Game_cene");
        LoadScene("MainMenu_scene");

    }

    private void Update()
    {
        if (Unload == true)
        {
            UnloadScene("MainMenu_scene");
        }
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string SceneName)
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }
}