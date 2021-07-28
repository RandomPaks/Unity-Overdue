using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add more if necessary
public enum GameState { GAME, PAUSED, PHONE, CUTSCENE, CG, OTHER}

public class GameManager : MonoBehaviour
{
    GameState state;
    [SerializeField] UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerController;
    [SerializeField] GameObject player;
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] SpiritController spiritController;
    
    public static GameManager Instance { get; private set; }

    public GameObject Player => player;

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
            this.state = GameState.CG;
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (this.state == GameState.CG)
            {
                this.state = GameState.GAME;
            }
        };

        CGManager.Instance.OnShowCG += () =>
        {
            this.state = GameState.CG;
        };
        CGManager.Instance.OnCloseCG += () =>
        {
            if (this.state == GameState.CG)
            {
                this.state = GameState.GAME;
            }
        };
    }
    
    void Update()
    {
        if (this.state == GameState.GAME)
        {
            this.playerController.HandleUpdate();
            this.playerInteraction.HandleUpdate();
            if (this.spiritController != null && this.spiritController.gameObject.activeSelf)
            {
                this.spiritController.HandleUpdate();
            }
            //Debug.Log("GAME");

        }
        else if (this.state == GameState.CG)
        {
            CGManager.Instance.HandleUpdate();
            DialogManager.Instance.HandleUpdate();
            //Debug.Log("CG");
        }
        else if(this.state == GameState.PHONE)
        {
            if (this.spiritController != null && this.spiritController.gameObject.activeSelf)
            {
                this.spiritController.HandleUpdate();
            }
            //Debug.Log("PHONE");
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
        if (state == GameState.GAME)
        {
            this.toggleCursorLock(true);
        }
    }

    public GameState GetState() => this.state;

    public void toggleCursorLock(bool toggle)
    {
        playerController.m_MouseLook.SetCursorLock(toggle);
    }
}
