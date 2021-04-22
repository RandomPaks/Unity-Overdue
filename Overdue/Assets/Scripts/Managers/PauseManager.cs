using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    bool isPaused = false;
    [SerializeField] GameObject pauseUI;
    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;

    public static PauseManager Instance { get; private set; }

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
                GameManager.Instance.SetState(GameState.PAUSED);
            }
            else
            {
                this.pauseUI.SetActive(false);
                //GameManager.Instance.toggleCursorLock(true);
                GameManager.Instance.SetState(GameState.GAME);
            }
        }
    }

    // resume button
    public void OnResumeClick()
    {
        this.pauseUI.SetActive(false);
        this.isPaused = false;
        //GameManager.Instance.toggleCursorLock(true);
        GameManager.Instance.SetState(GameState.GAME);
    }

    public void OnEnterResumeButton() => resumeButton.GetComponentInChildren<Text>().fontSize = 60;
    public void OnExitResumeButton() => resumeButton.GetComponentInChildren<Text>().fontSize = 48;
    public void OnEnterQuitButton() => quitButton.GetComponentInChildren<Text>().fontSize = 60;
    public void OnExitQuitButton() => quitButton.GetComponentInChildren<Text>().fontSize = 48;
}
