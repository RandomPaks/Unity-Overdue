using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEventSequence : AEventSequence
{
    [SerializeField] Dialog dialog; 

    public override void PlayEvent()
    {
        //CGDialogs.Instance.BeginDialog();
        StartCoroutine(DialogManager.Instance.ShowDialog(this.dialog));
        Debug.Log(this.dialog.Lines[0].Line);
    }

    void OnTriggerEnter(Collider other)
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
    }
}