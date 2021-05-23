using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room108EventSequence : AEventSequence
{
    [SerializeField] GameObject spiritController;
    [SerializeField] GameObject eventBarriers; 

    public override void PlayEvent()
    {
        this.spiritController.SetActive(true);
        this.eventBarriers.SetActive(true);
        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
