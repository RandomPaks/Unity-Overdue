using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class SavePromptUI : MonoBehaviour
{
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    Action<bool> onPlayerChosen;

    void Awake()
    {
        yesButton.onClick.RemoveListener(OnClickedYes);
        yesButton.onClick.AddListener(OnClickedYes);
        noButton.onClick.RemoveListener(OnClickedNo);
        noButton.onClick.AddListener(OnClickedNo);
    }
    
    public void Show(Action<bool> onPlayerChosenCallback)
    {
        gameObject.SetActive(true);
        
        onPlayerChosen = onPlayerChosenCallback;

        GameManager.Instance.SetState(GameState.PAUSED);
        GameManager.Instance.toggleCursorLock(false);
    }

    public void Hide()
    {
        GameManager.Instance.toggleCursorLock(true);
        GameManager.Instance.SetState(GameState.GAME);
        
        gameObject.SetActive(false);
    }

    void OnClickedYes()
    {
        onPlayerChosen?.Invoke(true);
        Hide();
    }

    void OnClickedNo()
    {
        onPlayerChosen?.Invoke(false);
        Hide();
    }
}
