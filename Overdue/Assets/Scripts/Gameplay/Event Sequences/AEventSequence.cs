using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEventSequence : MonoBehaviour
{
    [SerializeField] protected AEventSequence nextEvent;

    public abstract void PlayEvent();

    public virtual void OnFinishEvent()
    {

        if (this.nextEvent != null)
        {
            this.nextEvent.gameObject.SetActive(true);
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.PlayEvent();
        }
    }
}
