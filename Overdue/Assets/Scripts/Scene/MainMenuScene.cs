using UnityEngine;
using TMPro;
using DG.Tweening;

public class MainMenuScene : MonoBehaviour
{
    [SerializeField] TMP_Text newGame, loadGame, exit; 

    void Start()
    {
        newGame.fontMaterial.SetFloat("_FaceDilate", -1.0f);
        loadGame.fontMaterial.SetFloat("_FaceDilate", -1.0f);
        exit.fontMaterial.SetFloat("_FaceDilate", -1.0f);

        newGame.fontMaterial.DOFloat(0.0f, "_FaceDilate", 3.0f);
        loadGame.fontMaterial.DOFloat(0.0f, "_FaceDilate", 3.0f);
        exit.fontMaterial.DOFloat(0.0f, "_FaceDilate", 3.0f);
    }
}
