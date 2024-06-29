using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreenManager : MonoBehaviour
{
    public GameObject loadScreen;
    public TextMeshProUGUI loadingText;
    public GameObject loadingIcon;

    private string sceneToLoad;

    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        StartCoroutine(StartLoading());
    }

    public void ReloadScene()
    {
        sceneToLoad = SceneManager.GetActiveScene().name;
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        loadScreen.SetActive(true);
        loadingIcon.transform.DORotate(new Vector3(0f, 0f, -360f), 1.5f, RotateMode.FastBeyond360).SetLoops(-1);

        yield return new WaitForSeconds(3f); // Искусственная задержка загрузки

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingText.text = "Loading... " + (progress * 100f).ToString("F0") + "%";
            yield return null;
        }
    }
}
