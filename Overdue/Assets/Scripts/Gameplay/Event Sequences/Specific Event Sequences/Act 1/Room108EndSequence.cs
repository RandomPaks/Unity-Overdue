using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room108EndSequence : AEventSequence
{
    [SerializeField] GameObject spiritController;
    [SerializeField] Light mainGateLights;

    public override void PlayEvent()
    {
        this.mainGateLights.intensity += 2; 
        this.spiritController.SetActive(false);
        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
