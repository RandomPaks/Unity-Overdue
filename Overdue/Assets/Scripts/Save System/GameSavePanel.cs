using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameSavePanel : MonoBehaviour
{
    [SerializeField] Button button = null;
    [SerializeField] CanvasGroup canvasGroup = null;

    GameSave gameSave = null;

    public void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    public void Initialize(GameSave newGameSave)
    {
        gameSave = newGameSave;
        canvasGroup.alpha = 1.0f;
    }

    public void InitializeNoGameSave()
    {
        button.enabled = false;
        canvasGroup.alpha = 0.5f;
    }
    
    void OnButtonClicked()
    {
        SaveManager.Instance.LoadGameSave(gameSave);
    }
}
