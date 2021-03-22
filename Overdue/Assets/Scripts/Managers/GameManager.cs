using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add more if necessary
public enum GameState { GAME, PAUSED, DIALOG}

public class GameManager : MonoBehaviour
{
    GameState state;
    [SerializeField] UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerController;
    [SerializeField] PlayerInteraction playerInteraction;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.state = GameState.GAME;

        DialogManager.Instance.OnShowDialog += () =>
        {
            this.state = GameState.DIALOG;
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (this.state == GameState.DIALOG)
            {
                this.state = GameState.GAME;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (this.state == GameState.GAME)
        {
            this.playerController.HandleUpdate();
            this.playerInteraction.HandleUpdate();
        }   
        else if (this.state == GameState.DIALOG)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }

    void FixedUpdate()
    {
        if (this.state == GameState.GAME)
        {
            this.playerController.HandleFixedUpdate();
        }
    }

    public void SetState(GameState state)
    {
        this.state = state; 
    }
}
