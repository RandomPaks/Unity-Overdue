using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEventSequence : AEventSequence
{
    [SerializeField] GameObject spirit;
    [SerializeField] GameObject alvaro; 

    public override void PlayEvent()
    {
        this.spirit.SetActive(false);
        this.alvaro.transform.position = Vector3.zero;

        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
