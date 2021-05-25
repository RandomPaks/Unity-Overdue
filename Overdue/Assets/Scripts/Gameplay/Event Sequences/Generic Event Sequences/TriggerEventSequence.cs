using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventSequence : AEventSequence
{
    public override void PlayEvent()
    {
        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
