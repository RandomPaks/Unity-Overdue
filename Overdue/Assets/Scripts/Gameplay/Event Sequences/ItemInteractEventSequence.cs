using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractEventSequence : AEventSequence, IInteractable
{
    public void Interact()
    {
        this.PlayEvent();
    }

    public override void PlayEvent()
    {
        this.OnFinishEvent();
    }

    public void StartHover()
    {
        
    }

    public void StopHover()
    {
        
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }

    public override void OnTriggerEnter(Collider other)
    {
        
    }
}
