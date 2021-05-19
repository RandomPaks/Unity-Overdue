using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    float ticks;
    [SerializeField] RawImage black;
    [SerializeField] TMP_Text loadingText;

    public static LoadingManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        SceneManager.LoadSceneAsync("Main Menu Scene");
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (loadingText.gameObject.active)
        {
            ticks += Time.deltaTime;
            if (ticks > 0.8f)
            {
                loadingText.text = "Loading...";
                ticks = 0.0f;
            }
            else if (ticks > 0.6f)
                loadingText.text = "Loading..";
            else if (ticks > 0.4f)
                loadingText.text = "Loading.";
            else if (ticks > 0.2f)
                loadingText.text = "Loading";
        }
    }
    
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame()
    {
        black.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("Main Menu Scene");
        scenesLoading.Add(SceneManager.LoadSceneAsync("Game Scene"));
        StartCoroutine(LoadingProgress());
    }

    public void LoadMainMenu()
    {
        black.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("Game Scene");
        scenesLoading.Add(SceneManager.LoadSceneAsync("Main Menu Scene"));
        StartCoroutine(LoadingProgress());
    }

    public IEnumerator LoadingProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        black.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
        scenesLoading.Clear();
    }
}
