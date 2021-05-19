using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEventSequence : MonoBehaviour
{
    [SerializeField] protected AEventSequence nextEvent;

    public abstract void PlayEvent(); 

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (this.nextEvent != null)
            {
                this.nextEvent.gameObject.SetActive(true);
            }

            this.PlayEvent();

            this.gameObject.SetActive(false);
        }
    }*/
}
