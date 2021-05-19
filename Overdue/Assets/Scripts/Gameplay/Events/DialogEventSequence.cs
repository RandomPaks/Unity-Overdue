using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEventSequence : AEventSequence
{
    [SerializeField] Dialog dialog; 

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.PlayEvent();

            if (this.nextEvent != null)
            {
                this.nextEvent.gameObject.SetActive(true);
            }

            this.gameObject.SetActive(false);
        }
    }*/

    public override void PlayEvent()
    {
        //CGDialogs.Instance.BeginDialog();
        //StartCoroutine(DialogManager.Instance.ShowDialog(this.dialog));
        this.ShowDialog();
        Debug.Log(this.dialog.Lines[0].Line);
    }

    void ShowDialog()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(this.dialog, this.OnFinishDialog));
    }

    void OnFinishDialog()
    {
        Debug.Log("Finished");
    }
}