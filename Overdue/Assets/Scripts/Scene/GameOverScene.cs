using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] TMP_Text gameOver, continueGame, mainMenu;
    [SerializeField] float secs = 2.5f;

    void Start()
    {
        gameOver.fontMaterial.SetFloat("_FaceDilate", -1.0f);
        continueGame.fontMaterial.SetFloat("_FaceDilate", -1.0f);
        mainMenu.fontMaterial.SetFloat("_FaceDilate", -1.0f);

        continueGame.fontMaterial.DOFloat(0.0f, "_FaceDilate", secs);
        mainMenu.fontMaterial.DOFloat(0.0f, "_FaceDilate", secs);
        FadeInGameOver();
    }
    void FadeInGameOver() => gameOver.fontMaterial.DOFloat(0.0f, "_FaceDilate", secs).OnComplete(() => FadeOutGameOver());

    void FadeOutGameOver() => gameOver.fontMaterial.DOFloat(-0.25f, "_FaceDilate", secs).OnComplete(() => FadeInGameOver());
}
