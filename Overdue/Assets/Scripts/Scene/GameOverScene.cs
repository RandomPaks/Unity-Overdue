using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] TMP_Text gameOver, mainMenu;
    [SerializeField] float secs = 2.5f;

    void Start()
    {
        gameOver.fontMaterial.SetFloat("_FaceDilate", -1.0f);
        mainMenu.fontMaterial.SetFloat("_FaceDilate", -1.0f);

        mainMenu.fontMaterial.DOFloat(0.0f, "_FaceDilate", secs);
        FadeInGameOver();
    }
    void FadeInGameOver() => gameOver.fontMaterial.DOFloat(0.0f, "_FaceDilate", secs).OnComplete(() => FadeOutGameOver());

    void FadeOutGameOver() => gameOver.fontMaterial.DOFloat(-0.25f, "_FaceDilate", secs).OnComplete(() => FadeInGameOver());

    public void OnMainMenuClick()
    {
        LoadingManager.Instance.MainMenuFromGameOver();
    }

    public void OnEndGameClick()
    {
        LoadingManager.Instance.MainMenuFromEndGame();
    }
}
