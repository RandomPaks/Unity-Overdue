using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MainMenuScene : MonoBehaviour
{
    //Only needs one text to get the font shader
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
