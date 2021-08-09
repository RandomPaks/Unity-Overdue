using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
class Tutorial
{
    [SerializeField] string text;
    [SerializeField] float timer;

    public string Text
    {
        get
        {
            return text;
        }
    }

    public float Timer
    {
        get
        {
            return timer;
        }
    }
}

public class TutorialEvent : AEventSequence
{
    [SerializeField] TMP_Text tutorialText;
    [SerializeField] Tutorial[] tutorials;

    public override void PlayEvent()
    {
        StartCoroutine(SetTutorialText());
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }

    IEnumerator SetTutorialText()
    {
        foreach (Tutorial line in tutorials)
        {
            tutorialText.text = line.Text;
            yield return new WaitForSeconds(line.Timer);
        }
        OnFinishEvent();
    }
}
