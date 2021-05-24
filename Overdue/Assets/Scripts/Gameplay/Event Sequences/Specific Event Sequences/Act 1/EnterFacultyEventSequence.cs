using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFacultyEventSequence : AEventSequence
{
    [SerializeField] GameObject noteObject;
    [SerializeField] GameObject keyObject;
    [SerializeField] GameObject sandraCharacter;

    public override void PlayEvent()
    {
        this.noteObject.SetActive(true);
        this.keyObject.SetActive(true);
        this.sandraCharacter.SetActive(false);
        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
