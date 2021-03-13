using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add more if necessary
public enum GameState { GAME, PAUSED, DIALOG}

public class GameManager : MonoBehaviour
{
    GameState state;
    [SerializeField] UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerController; 
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.state = GameState.GAME;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.state == GameState.GAME)
        {
            this.playerController.HandleUpdate();
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
