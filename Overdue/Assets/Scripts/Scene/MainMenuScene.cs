using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class MainMenuScene : MonoBehaviour
{
    [SerializeField] TMP_Text newGame, loadGame, exit;
    [SerializeField] float secs = 2.5f;

    void Start()
    {
        newGame.fontMaterial.SetFloat("_FaceDilate", -1.0f);
        loadGame.fontMaterial.SetFloat("_FaceDilate", -1.0f);
        exit.fontMaterial.SetFloat("_FaceDilate", -1.0f);

        newGame.fontMaterial.DOFloat(0.0f, "_FaceDilate", secs);
        loadGame.fontMaterial.DOFloat(0.0f, "_FaceDilate", secs);
        exit.fontMaterial.DOFloat(0.0f, "_FaceDilate", secs);
    }

    public void OnNewGameClick() => SceneManager.LoadScene("Game Scene");

    public void OnExitClick() => Application.Quit();
}
