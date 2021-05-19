using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    float ticks;
    [SerializeField] GameObject loadingPanel;
    TMP_Text loadingText;

    public static LoadingManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        loadingText = GetComponentInChildren<TMP_Text>();

        SceneManager.LoadSceneAsync("Main Menu Scene", LoadSceneMode.Additive);
    }

    void Update()
    {
        ticks += Time.deltaTime;
        if (ticks > 1.33f)
        {
            loadingText.text = "Loading...";
            ticks = 0.0f;
        }
        else if (ticks > 1.0f)
            loadingText.text = "Loading..";
        else if (ticks > 0.66f)
            loadingText.text = "Loading.";
        else if (ticks > 0.33f)
            loadingText.text = "Loading";
    }

    public void LoadGame()
    {
        loadingPanel.SetActive(true);
        SceneManager.UnloadSceneAsync("Main Menu Scene");
        SceneManager.LoadSceneAsync("Game Scene", LoadSceneMode.Additive);
    }
}
