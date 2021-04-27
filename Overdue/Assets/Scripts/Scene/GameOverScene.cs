using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] TMP_Text gameOver, continueGame, mainMenu;
    [SerializeField] float secs = 2.5f;
    bool fade = false;

    void Start()
    {
        gameOver.fontMaterial.SetFloat("_FaceDilate", -1.0f);
        continueGame.fontMaterial.SetFloat("_FaceDilate", -1.0f);
        mainMenu.fontMaterial.SetFloat("_FaceDilate", -1.0f);

        continueGame.fontMaterial.DOFloat(0.0f, "_FaceDilate", 3.0f);
        mainMenu.fontMaterial.DOFloat(0.0f, "_FaceDilate", 3.0f);
        FadeInGameOver();
    }
    void FadeInGameOver() => gameOver.fontMaterial.DOFloat(0.0f, "_FaceDilate", 2.5f).OnComplete(() => FadeOutGameOver());

    void FadeOutGameOver() => gameOver.fontMaterial.DOFloat(-0.25f, "_FaceDilate", 2.5f).OnComplete(() => FadeInGameOver());
}
