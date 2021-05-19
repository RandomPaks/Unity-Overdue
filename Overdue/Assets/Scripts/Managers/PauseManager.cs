using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] GameObject pauseUI;

    public static PauseManager Instance { get; private set; }

    private GameState prevState;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.isPaused = !this.isPaused;
            if (this.isPaused)
            {
                this.pauseUI.SetActive(true);
                GameManager.Instance.toggleCursorLock(false);
                prevState = GameManager.Instance.GetState();
                GameManager.Instance.SetState(GameState.PAUSED);
            }
            else
            {
                this.pauseUI.SetActive(false);
                //GameManager.Instance.toggleCursorLock(true);
                GameManager.Instance.SetState(prevState);
            }
        }
    }

    // resume button
    public void OnResumeClick()
    {
        this.pauseUI.SetActive(false);
        this.isPaused = false;
        //GameManager.Instance.toggleCursorLock(true);
        GameManager.Instance.SetState(prevState);
    }

    public void OnQuitClick() => LoadingManager.Instance.LoadMainMenu();
}
