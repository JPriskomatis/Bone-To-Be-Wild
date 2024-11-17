using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneTransition : Singleton<SceneTransition>
{
    private AsyncOperation scenSync;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBarFill;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void GoToScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        //loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            //loadingBarFill.value = progressValue;
            yield return null;
        }
    }
}
