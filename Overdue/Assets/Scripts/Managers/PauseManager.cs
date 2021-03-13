using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool isPaused = false;
    [SerializeField] GameObject pauseUI;

    public static PauseManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.isPaused = !this.isPaused;
            if (this.isPaused)
            {
                this.pauseUI.SetActive(true);
                GameManager.Instance.SetState(GameState.PAUSED);
            }
            else
            {
                this.pauseUI.SetActive(false);
                GameManager.Instance.SetState(GameState.GAME);
            }
        }
    }

    // resume button
    public void OnResumeClick()
    {
        this.pauseUI.SetActive(false);
        this.isPaused = false;
        GameManager.Instance.SetState(GameState.GAME);
    }
}
