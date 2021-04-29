using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class SavePromptUI : MonoBehaviour
{
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    
    public bool WillSave { get; private set; }
    public bool HasPlayerChosen { get; private set; }

    void Awake()
    {
        yesButton.onClick.RemoveListener(OnClickedYes);
        yesButton.onClick.AddListener(OnClickedYes);
        noButton.onClick.RemoveListener(OnClickedNo);
        noButton.onClick.AddListener(OnClickedNo);
    }

    void Start()
    {
        HasPlayerChosen = false;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        HasPlayerChosen = false;
        
        GameManager.Instance.SetState(GameState.PAUSED);
        GameManager.Instance.toggleCursorLock(false);
    }

    public void Hide()
    {
        GameManager.Instance.toggleCursorLock(true);
        GameManager.Instance.SetState(GameState.GAME);
        
        HasPlayerChosen = false;
        gameObject.SetActive(false);
    }

    void OnClickedYes()
    {
        WillSave = true;
        HasPlayerChosen = true;
    }

    void OnClickedNo()
    {
        WillSave = false;
        HasPlayerChosen = true;
    }
}
